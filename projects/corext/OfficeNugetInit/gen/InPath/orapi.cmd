@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Microsoft.Office.Orapi.Tools.1.0.20181023.1\orapitools\orapieditor.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
