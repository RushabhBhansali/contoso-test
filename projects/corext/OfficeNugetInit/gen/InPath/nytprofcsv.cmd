@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\.A\Perl.ActiveState.89bUEJHAf8XBwsfQaLTsWQ\bin\nytprofcsv.bat" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
