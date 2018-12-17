@GOTO :Start
----------------------------------------
-- tool redirection from corext.exe init
----------------------------------------
:Start
@"f:\nugetcache\.A\MsBuild.Corext.eG2TKbe2qhOmt0qAKLXqMw\v15.0\bin\roslyn\csi.exe" %*
@GOTO :EXIT
:EXIT
@exit /b %ERRORLEVEL%
