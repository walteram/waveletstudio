@echo off
 
@echo Instrumenting Binary
set VS100TEAMTOOLS="%VS100COMNTOOLS%..\..\Team Tools\Performance Tools\"
 
%VS100TEAMTOOLS%\VSInstr.exe ..\src\Tests\WaveletStudio.Tests\bin\Debug\WaveletStudio.dll
%VS100TEAMTOOLS%\VSInstr.exe ..\src\Tests\WaveletStudio.Tests\bin\Debug\WaveletStudio.Tests.dll
@echo on