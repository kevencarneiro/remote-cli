import {Component, OnInit} from '@angular/core';
import {SignalrService} from '../../services/signalr.service';
import {MachineInfo} from '../../models/machine-info';
import {BytesPipe} from 'angular-pipes';
import {MatDialog} from '@angular/material';
import {MulticastCommandComponent} from '../multicast-command/multicast-command.component';

@Component({
  selector: 'app-machine-list',
  templateUrl: './machine-list.component.html',
  styleUrls: ['./machine-list.component.scss']
})
export class MachineListComponent implements OnInit {

  connectedMachines: MachineInfo[] = [];
  commandOutput: string[] = [];

  constructor(private _signalrService: SignalrService, public dialog: MatDialog) {
  }

  connected(): void {

  }

  disconnected(): void {
    this.connectedMachines = [];
  }

  private getDescription(propertyName: string, data: any[]): string {
    const activeItems = data.filter(x => x.enabled);
    if (activeItems.length === 0) {
      return `Nenhum ${propertyName} ativo`;
    }
    return activeItems.length === 1 ? `${activeItems[0].name} está ativo` : `${activeItems.length} ${propertyName} ativos`;
  }

  getAntivirusDescription(machine: MachineInfo): string {
    return this.getDescription('antivírus', machine.antivirus);
  }

  getFirewallDescription(machine: MachineInfo): string {
    return this.getDescription('firewall', machine.firewalls);
  }

  getStorageDescription(machine: MachineInfo): string {
    const availableSize = machine.storageDevices.map(x => x.availableSize).reduce((a, b) => a + b, 0);
    const totalSize = machine.storageDevices.map(x => x.totalSize).reduce((a, b) => a + b, 0);

    const pipe = new BytesPipe;
    return `${pipe.transform(availableSize)} livres de ${pipe.transform(totalSize)}`;
  }

  ngOnInit() {
    this._signalrService.connected
      .subscribe((connected: boolean) => connected ? this.connected() : this.disconnected());
    this._signalrService.commandOutput.subscribe((output: string) => this.commandOutput.push(output));
    this._signalrService.machineRegistered.subscribe((machine: MachineInfo) => {
      this.connectedMachines.push(machine);
    });
    this._signalrService.machineUnregistered.subscribe((machineId: string) => {
      this.connectedMachines = this.connectedMachines.filter(x => x.id !== machineId);
    });
  }

  openMulticastCommand() {
    const dialogRef = this.dialog.open(MulticastCommandComponent, {
      width: '400px',
      data: this.connectedMachines
    });
  }
}
