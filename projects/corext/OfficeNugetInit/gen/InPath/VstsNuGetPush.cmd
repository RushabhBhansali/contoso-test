@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\.A\Microsoft.VisualStudio.Services.Packaging.NuGet.PushTool.Eue08Xyo0Q9JN6Veg6zgTQ\tools\VstsNuGetPush.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
