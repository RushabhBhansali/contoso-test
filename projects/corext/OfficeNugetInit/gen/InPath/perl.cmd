@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\.A\Perl.ActiveState.89bUEJHAf8XBwsfQaLTsWQ\bin\perl.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
