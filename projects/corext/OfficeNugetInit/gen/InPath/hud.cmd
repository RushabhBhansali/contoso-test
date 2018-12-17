@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Microsoft.HUD.0.20.0\lib\x64\hud.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
