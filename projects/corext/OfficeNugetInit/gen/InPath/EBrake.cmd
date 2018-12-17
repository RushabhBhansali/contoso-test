@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\experiment.tools.1.0.3\ChangeGate\EBrake.cmd" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
