@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\Microsoft.Office.FastModelCompiler.16.0.8415.3000\tools\fmcompiler.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
