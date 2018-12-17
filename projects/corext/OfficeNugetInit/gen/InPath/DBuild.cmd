@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Office.StaticGraph.1.0.1513\lib\net45\DBuild.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
