@echo off
 
@echo Instrumenting Binary
set VS100TEAMTOOLS="%VS100COMNTOOLS%..\..\Team Tools\Performance Tools\"

if "%1" == "coverage" %VS100TEAMTOOLS%\VSInstr.exe /coverage ..\src\Tests\WaveletStudio.Tests\bin\Debug\WaveletStudio.dll
if NOT "%1" == "coverage" %VS100TEAMTOOLS%\VSInstr.exe ..\src\Tests\WaveletStudio.Tests\bin\Debug\WaveletStudio.dll
if NOT "%1" == "coverage" %VS100TEAMTOOLS%\VSInstr.exe ..\src\Tests\WaveletStudio.Tests\bin\Debug\WaveletStudio.Tests.dll

@echo on