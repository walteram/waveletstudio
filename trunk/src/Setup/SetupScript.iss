[Setup]
WizardImageFile=compiler:WizModernImage-IS.bmp
AppName=Wavelet Studio
AppVersion=1.3.0
ExtraDiskSpaceRequired=500
SetupIconFile=..\WaveletStudio.Designer\icon.ico
DefaultDirName={pf}\WaveletStudio
AppPublisher=WaveletStudio
AppPublisherURL=http://www.waveletstudio.net/
MinVersion=0,5.01sp2
UninstallDisplayName=Wavelet Studio
SolidCompression=True
OutputDir=Output
Compression=lzma2/ultra
InternalCompressLevel=ultra
ChangesAssociations=True
DefaultGroupName=Wavelet Studio
UninstallDisplayIcon=\icon.ico
OutputBaseFilename=WaveletStudioSetup

[Registry]
Root: HKCR; Subkey: ".wsd"; ValueType: string; ValueName: ""; ValueData: "Wavelet Studio Document"; Flags: uninsdeletevalue
Root: HKCR; Subkey: "Wavelet Studio Document"; ValueType: string; ValueName: ""; ValueData: "Wavelet Studio Document"; Flags: uninsdeletekey
Root: HKCR; Subkey: "Wavelet Studio Document\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\Designer\WaveletStudio.Designer.exe,0"
Root: HKCR; Subkey: "Wavelet Studio Document\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\Designer\WaveletStudio.Designer.exe"" ""%1"""

[Languages]
Name: "brazilianportuguese"; MessagesFile: "compiler:Languages\BrazilianPortuguese.isl"; LicenseFile: "..\..\res\docs\license-pt.txt"
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "..\..\res\docs\license-en.txt"

[_ISTool]
EnableISX=true

[Files]
Source: "C:\Program Files\ISTool\isxdl.dll"; Flags: dontcopy
Source: "..\WaveletStudio.Designer\icon.ico"; DestDir: "{app}\Designer"; Flags: ignoreversion replacesameversion
Source: "website.url"; DestDir: "{app}\Designer"; Flags: ignoreversion replacesameversion
Source: "Files\*.*"; Excludes: "documentation"; DestDir: "{app}"; Flags: ignoreversion createallsubdirs recursesubdirs replacesameversion
Source: "..\..\res\docs\license-*.txt"; Excludes: "documentation"; DestDir: "{app}"; Flags: ignoreversion createallsubdirs recursesubdirs replacesameversion

[Run]
Filename: "{app}\Designer\WaveletStudio.Designer.exe"; WorkingDir: "{app}\Designer\"; Flags: nowait postinstall skipifsilent; Description: "{cm:OpenDesigner}"

[Messages]
brazilianportuguese.WelcomeLabel1=Bem-vindo ao Assistente de Instalação do [name]
brazilianportuguese.WelcomeLabel2=Este assistente irá instalar o [name/ver] no seu computador.
brazilianportuguese.FinishedHeadingLabel=Finalizando o Assistente de Instalação do [name]

[CustomMessages]
english.DotNetNeeded=This Application needs the Microsoft .NET Framework 4 to be installed by an Administrator.
brazilianportuguese.DotNetNeeded=Este programa necessita que o Microsoft .NET Framework 4 seja instalado por um Administrador.
english.DownloadingDotNet=Downloading Microsoft .NET Framework 4.0
brazilianportuguese.DownloadingDotNet=Baixando o Microsoft .NET Framework 4.0
english.DownloadingDotNetDescription=This Application needs to install the Microsoft .NET Framework 4.0. Please wait while setup is downloading extra files to your computer.
brazilianportuguese.DownloadingDotNetDescription=Este programa requer que o Microsoft .NET Framework 4.0 (Full) esteja instalado. Por favor, aguarde enquanto a instalação faz o download dos arquivos necessários.
english.OpenDesigner=Run the Wavelet Studio Designer
brazilianportuguese.OpenDesigner=Executar o Wavelet Studio Designer

[ThirdPartySettings]
CompileLogMethod=append

[PreCompile]
Name: "cleanup.bat"; Flags: abortonerror cmdprompt redirectoutput
Name: "build.bat"; Flags: abortonerror cmdprompt redirectoutput

[PostCompile]
Name: "cleanup.bat"; Flags: abortonerror cmdprompt redirectoutput
 
[Icons]
Name: "{group}\Wavelet Studio Designer"; Filename: "{app}\Designer\WaveletStudio.Designer.exe"; WorkingDir: "{app}\Designer"; Flags: runmaximized; IconFilename: "{app}\Designer\icon.ico"
Name: "{group}\Website"; Filename: "{app}\Designer\website.url"

[ThirdParty]
CompileLogMethod=append

[Code]
var
 dotnetRedistPath: string;
 downloadNeeded: boolean;
 dotNetNeeded: boolean;
 
procedure isxdl_AddFile(URL, Filename: PChar);
external 'isxdl_AddFile@files:isxdl.dll stdcall';
function isxdl_DownloadFiles(hWnd: Integer): Integer;
external 'isxdl_DownloadFiles@files:isxdl.dll stdcall';
function isxdl_SetOption(Option, Value: PChar): Integer;
external 'isxdl_SetOption@files:isxdl.dll stdcall';
 
const 
  dotnetRedistURL = 'http://download.microsoft.com/download/9/5/A/95A9616B-7A37-4AF6-BC36-D6EA96C8DAAE/dotNetFx40_Full_x86_x64.exe';
 
function InitializeSetup(): Boolean;
var
  IsInstalled: Cardinal;
begin
  Result := true;
  dotNetNeeded := true;
 
  // Check for required netfx installation
  if(Is64BitInstallMode()) then begin
   if (RegValueExists(HKLM, 'SOFTWARE\Wow6432Node\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install')) then begin
    RegQueryDWordValue(HKLM, 'SOFTWARE\Wow6432Node\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install', IsInstalled);
    if(IsInstalled = 1) then begin
     dotNetNeeded := false;
     downloadNeeded := false;
    end;
   end;
  end
  else begin
   if (RegValueExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install')) then begin
    RegQueryDWordValue(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full', 'Install', IsInstalled);
    if(IsInstalled = 1) then begin
     dotNetNeeded := false;
     downloadNeeded := false;
    end;
   end;
  end;

  if(dotNetNeeded) then begin
   if (not IsAdminLoggedOn()) then begin
    MsgBox('This Application needs the Microsoft .NET Framework 4 to be installed by an Administrator.', mbError, MB_OK);
    Result := false;
   end
   else begin
    dotnetRedistPath := ExpandConstant('{src}\dotnetfx.exe');
    if not FileExists(dotnetRedistPath) then begin
     dotnetRedistPath := ExpandConstant('{tmp}\dotnetfx.exe');
     if not FileExists(dotnetRedistPath) then begin
      isxdl_AddFile(dotnetRedistURL, dotnetRedistPath);
      downloadNeeded := true;
     end;
    end;
   end;
  end;
end;
 
function NextButtonClick(CurPage: Integer): Boolean;
var
  hWnd: Integer;
  ResultCode: Integer;
begin
  Result := true;
 
  if CurPage = wpReady then begin
  hWnd := StrToInt(ExpandConstant('{wizardhwnd}'));
 
  // don't try to init isxdl if it's not needed because it will error on < ie 3
  if (downloadNeeded) then begin
   isxdl_SetOption('label', 'Downloading Microsoft .NET Framework 4.0');
   isxdl_SetOption('description', 'This Application needs to install the Microsoft .NET Framework 4.0. Please wait while setup is downloading extra files to your computer.');
   if isxdl_DownloadFiles(hWnd) = 0 then Result := false;
   end;
   if (Result = true) and (dotNetNeeded = true) then begin
    if Exec(ExpandConstant(dotnetRedistPath), '/q /norestart', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then begin
     // handle success if necessary; ResultCode contains the exit code
     if not (ResultCode = 0) then begin
      Result := false;
     end;
    end 
    else begin
     // handle failure if necessary; ResultCode contains the error code
     Result := false;
    end;
   end;
  end;
end;





