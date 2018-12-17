@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Office.DFSManager.1.0.9\lib\net45\dfsutil.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
