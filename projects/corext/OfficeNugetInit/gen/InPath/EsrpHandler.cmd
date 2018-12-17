@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Office.Signing.1.0.14\lib\net45\MS.Office.EsrpHandler.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
