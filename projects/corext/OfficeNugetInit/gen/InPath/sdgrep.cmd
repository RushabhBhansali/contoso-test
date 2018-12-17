@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\microsoft.office.sdgrep.release.2018.7.25.1\sdgrep.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
