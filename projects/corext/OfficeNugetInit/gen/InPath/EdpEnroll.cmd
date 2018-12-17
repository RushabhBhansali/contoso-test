@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Microsoft.Office.EnterpriseDataProtection.Tools.16.0.9304.1000\bin\EdpEnroll.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
