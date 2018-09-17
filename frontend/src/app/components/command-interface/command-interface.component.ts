import {Component, ElementRef, Input, OnInit, ViewChild} from '@angular/core';
import {SignalrService} from '../../services/signalr.service';
import {CommandOutput} from '../../models/machine-info';
import {MessageType} from '../../enums/message-type.enum';

@Component({
  selector: 'app-command-interface',
  templateUrl: './command-interface.component.html',
  styleUrls: ['./command-interface.component.scss']
})
export class CommandInterfaceComponent implements OnInit {
  @Input() machineId: string;
  currentCommand: string;
  consoleLines: CommandOutput[] = [];
  @ViewChild('commandInput') commandInput: ElementRef;

  public MessageType = MessageType;

  constructor(private _signalrService: SignalrService) { }

  outputCommand(output: CommandOutput) {
    if (output.machineId === this.machineId) {
      this.consoleLines.push(output);
    }
  }

  commandWindowClicked() {
    this.commandInput.nativeElement.focus();
  }

  ngOnInit() {
    this._signalrService.commandOutput.subscribe(output => this.outputCommand(output));
  }

  sendCommand() {
    this._signalrService.sendCommand(this.machineId, this.currentCommand);
    this.currentCommand = '';
  }
}
