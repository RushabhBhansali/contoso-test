@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\VCCompiler.Tools.x86.14.13.26128.4\xdcmake.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
