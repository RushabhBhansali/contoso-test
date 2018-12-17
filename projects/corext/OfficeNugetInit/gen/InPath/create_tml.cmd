@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Microsoft.Office.Telemetry.TmlTools.16.0.8421.3425\tools\create_tml.bat" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
