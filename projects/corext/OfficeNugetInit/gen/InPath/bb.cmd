@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\BBBat.16.0.10804.30000\otools\bin\bbnuget.bat" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
