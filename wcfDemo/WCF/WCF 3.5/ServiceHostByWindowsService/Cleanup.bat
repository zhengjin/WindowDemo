@echo off
net stop WCF.ServiceHostByWindowsService
installutil /u bin\Debug\WCF.ServiceHostByWindowsService.exe
