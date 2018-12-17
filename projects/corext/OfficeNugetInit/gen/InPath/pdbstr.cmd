@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\microsoft.windows.dbg.srcsrv.10.0.17074.10021\lib\amd64\pdbstr.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
