@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Office.Engineering.DevTools.0.4.10\tools\RepositoryHelper\rh.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
