Front-end:
- Instale o Angular CLI
- Execute `ng serve`

Gerando instalador do Agente:
- Instale o chocolatey
- Instale o InnoSetup executando `choco install innosetup` em uma janela do PowerShell com privilégios administrativos
- Navegue até a pasta backend\build e execute `.\build.ps1 --Target=Pack`

Instalando o agente:
- Instalação gráfica: Execute o instalador e siga os passos
- Instalação silenciosa: `RemoteCLIAgentSetup.exe /SP- /SUPPRESSMSGBOXES /VERYSILENT` 