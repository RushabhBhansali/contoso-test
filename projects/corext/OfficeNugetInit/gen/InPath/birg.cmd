@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Motif.BuildTools.x86.1.0.100\birg.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
