@echo off
@echo Turning ON performance coverage session recorder
set VS100TEAMTOOLS=%VS100COMNTOOLS%..\..\Team Tools\Performance Tools\

REM if "%1" == "coverage" start 2a_Coverage.bat

start /B call """%VS100TEAMTOOLS%\VsPerfMon""" /coverage /output:WaveletStudio.coverage /output:"WaveletStudio.coverage"

if NOT "%1" == "coverage" call "%VS100TEAMTOOLS%\VsPerfCmd" /start:trace /output:WaveletStudio.vsp

@echo on