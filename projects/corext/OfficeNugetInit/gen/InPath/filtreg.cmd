@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\WindowsSDKTools.10.0.17735\bin\x86\filtreg.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
