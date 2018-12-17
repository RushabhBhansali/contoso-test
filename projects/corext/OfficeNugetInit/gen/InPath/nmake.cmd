@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\NMakeHarness.1.0.331\x64\nmake.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
