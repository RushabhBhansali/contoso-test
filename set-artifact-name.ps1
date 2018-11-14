$vstsDrop = (Get-ChildItem $env:BUILD_ARTIFACTSTAGINGDIRECTORY -Recurse -Filter VSTSDrop.json)[0].FullName

$obj = get-content $vstsDrop | convertfrom-json

$url = $obj.VstsDropBuildArtifact.VstsDropUrl

$dropName = $url.Replace("https://mseng.artifacts.visualstudio.com/DefaultCollection/_apis/drop/drops/", "")

Write-Host "##vso[task.setvariable variable=myArtifactName]$($dropName)"