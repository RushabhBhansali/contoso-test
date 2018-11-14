get-childitem Env:

$vstsDrop = (Get-ChildItem $env:BUILD_ARTIFACTSTAGINGDIRECTORY -Recurse -Filter VSTSDrop.json)[0].FullName

$obj = get-content $vstsDrop | convertfrom-json

$url = $obj['VstsDropBuildArtifact']['VstsDropUrl']

Write-Host $url