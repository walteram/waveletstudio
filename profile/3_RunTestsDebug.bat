@echo on

@echo Running Unit Tests
set VS100TEAMTOOLS=%VS100COMNTOOLS%..\..\Team Tools\Performance Tools\
call "%VS100TEAMTOOLS%\VsPerfCLREnv" /traceon
call "%VS100COMNTOOLS%\..\IDE\mstest.exe" /testcontainer:..\src\Tests\WaveletStudio.Tests\bin\Debug\WaveletStudio.Tests.dll 

@echo on