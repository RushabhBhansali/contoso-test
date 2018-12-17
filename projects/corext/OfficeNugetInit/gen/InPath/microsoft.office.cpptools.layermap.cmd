@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\microsoft.office.cpptools.layermap.release.2018.7.16.5\layermap.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
