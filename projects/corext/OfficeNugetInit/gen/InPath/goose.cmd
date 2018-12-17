@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\goose.tools.1.0.0\goose.bat" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
