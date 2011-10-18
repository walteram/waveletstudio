@echo off
 
@echo Turning off performance coverage session recorder
set VS100TEAMTOOLS="%VS100COMNTOOLS%..\..\Team Tools\Performance Tools\"
%VS100TEAMTOOLS%\VSPerfCmd /shutdown

@echo on