@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Motif.BuildTools.x64.1.0.100\lsf.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
