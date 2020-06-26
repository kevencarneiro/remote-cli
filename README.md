# Remote CLI

Control machines remotely using a web interface to views informations about the machine and send Powershell commands.

## Getting started

### Front-end:
- Install Angular CLI
- Run `ng serve`

### Back-end:
- Open the solution with Microsoft Visual Studio

### Generating agent installer:
- Install chocolatey
- Install InnoSetup running `choco install innosetup` in a Powershell windows with administrator rights
- Go to folder backend\build and run `.\build.ps1 --Target=Pack`

### Installing agent on the machines that will be controlled:
- Graphical install: Run the installer and follow the steps
- Quiet install: `RemoteCLIAgentSetup.exe /SP- /SUPPRESSMSGBOXES /VERYSILENT` 
