@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Office.Rewind.1.0.1\content\rewind.cmd" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
