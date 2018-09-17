import {MessageType} from '../enums/message-type.enum';

export class MachineInfo {
  public id: string;
  public machineName: string;
  public networks: NetworkInfo[];
  public antivirus: SecuritySoftwareInfo[];
  public firewalls: SecuritySoftwareInfo[];
  public windowsVersion: string;
  public netFrameworkVersion: string;
  public storageDevices: StorageDeviceInfo[];
}

export class SecuritySoftwareInfo {
  public name: string;
  public enabled: boolean;
}

export class StorageDeviceInfo {
  public name: string;
  public totalSize: number;
  public availableSize: number;
  public label: string;
}

export class NetworkInfo {
  public adapterName: string;
  public iPv4: string;
}

export class CommandOutput {
  constructor(data: CommandOutput) {
    this.output = data.output;
    this.messageType = data.messageType;
    this.machineId = data.machineId;
  }
  readonly output: string;
  readonly messageType: MessageType;
  readonly machineId: string;
}
