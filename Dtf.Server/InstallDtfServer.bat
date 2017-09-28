net stop DtfServer
"%~dp0Dtf.Server.exe" uninstall
"%~dp0Dtf.Server.exe" install
net start DtfServer