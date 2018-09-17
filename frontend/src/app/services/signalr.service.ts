import {EventEmitter, Injectable} from '@angular/core';
import {CommandOutput, MachineInfo} from '../models/machine-info';
import {HubConnection, HubConnectionBuilder} from '@aspnet/signalr';
import {environment} from '../../environments/environment';
import {MessageType} from '../enums/message-type.enum';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  machineRegistered = new EventEmitter<MachineInfo>();
  machineUnregistered = new EventEmitter<string>();
  connected = new EventEmitter<Boolean>();
  commandOutput = new EventEmitter<CommandOutput>();

  private _hubConnection = new HubConnectionBuilder()
    .withUrl(environment.apiUrl + '/RemoteCLIManagementHub')
    .build();

  constructor() {
    this.registerEvents();
    this.connect();
  }

  public sendCommand(machineId: string, command: string): void {
    this._hubConnection.send('SendCommand', machineId, command);
  }

  private connect(): void {
    this._hubConnection.start()
      .then(() => {
        this.connected.emit(true);
      })
      .catch(() => this.onDisconnected());
  }

  private registerEvents(): void {
    this._hubConnection.on('MachineRegistered',
      (machine: MachineInfo) => this.machineRegistered.emit(machine));

    this._hubConnection.on('MachineUnregistered',
      (machineId: string) => this.machineUnregistered.emit(machineId));

    this._hubConnection.on('CommandOutput',
      (output: string, messageType: MessageType, machineId: string) =>
        this.commandOutput.emit(new CommandOutput({output, messageType, machineId})));

    this._hubConnection.onclose(() => this.onDisconnected());
  }

  private onDisconnected(): void {
    this.connected.emit(false);
    setTimeout(this.connect(), 1000);
  }
}
