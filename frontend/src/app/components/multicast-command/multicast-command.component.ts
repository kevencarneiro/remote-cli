import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {MachineInfo} from '../../models/machine-info';
import {FormBuilder, FormControl, FormGroup} from '@angular/forms';
import {SignalrService} from '../../services/signalr.service';

@Component({
  selector: 'app-multicast-command',
  templateUrl: './multicast-command.component.html',
  styleUrls: ['./multicast-command.component.scss']
})
export class MulticastCommandComponent implements OnInit {
  form: FormGroup;

  constructor(
    public dialogRef: MatDialogRef<MulticastCommandComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MachineInfo[],
    private formBuilder: FormBuilder,
    private signalrService: SignalrService
  ) {
    this.form = formBuilder.group({
      machines: formBuilder.control(null),
      command: formBuilder.control(null)
    });
  }

  ngOnInit(): void {
  }

  sendCommand(): void {
    const machines = this.form.get('machines').value || [];
    const command = this.form.get('command').value;

    machines.forEach(x => this.signalrService.sendCommand(x, command));
    this.dialogRef.close();
  }
}
