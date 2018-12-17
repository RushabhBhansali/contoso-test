@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\CloudBuild.BuildRequester.2.1.0\tools\CB.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
