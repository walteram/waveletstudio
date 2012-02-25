%windir%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe /property:Configuration=Release;OutDir=..\setup\files\Designer\ ..\WaveletStudio.sln
%windir%\Microsoft.Net\Framework\v4.0.30319\msbuild.exe /property:Configuration=Release;OutDir=..\setup\files\Library\  ..\WaveletStudio\WaveletStudio.csproj

del .\files\Designer\example.csv /Q
del .\files\Designer\*.pdb /Q
del .\files\Designer\ZedGraph.xml /Q
if exist .\files\Library\pt-BR rmdir .\files\Library\pt-BR /S /Q 