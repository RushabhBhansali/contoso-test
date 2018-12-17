@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\NMakeHarness.1.0.331\nmakeharness.cmd" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
