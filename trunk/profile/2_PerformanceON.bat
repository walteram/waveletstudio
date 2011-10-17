@echo on

@echo Turning ON performance coverage session recorder
set VS100TEAMTOOLS=%VS100COMNTOOLS%..\..\Team Tools\Performance Tools\
call "%VS100TEAMTOOLS%\VsPerfCmd" /start:trace /output:WaveletStudio.vsp
@echo on