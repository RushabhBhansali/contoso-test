@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\microsoft.office.liblet.props.release.2018.10.16.2\cleanprops.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
