@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\.A\Microsoft.OfficeMarketplace.GateGen.lbDa9UxTotiFImUFBM2f0Q\tools\gategen.cmd" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
