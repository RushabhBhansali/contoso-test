@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Office.CloudBuildTools.1.0.1236\scripts\wixconverter.bat" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
