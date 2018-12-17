@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\VsMsBuild.Corext.3.0.0\v4.0\vsmsbuild.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
