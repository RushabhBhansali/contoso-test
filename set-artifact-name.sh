#!/bin/bash

artifactDir=${1}
pattern="${artifactDir}/**/VSTSDrop.json"
files=( $pattern )

result=$(cat ${files[0]} | python -c "import json,sys;obj=json.load(sys.stdin);print obj['VstsDropBuildArtifact']['VstsDropUrl'];")

name=${result#"https://mseng.artifacts.visualstudio.com/DefaultCollection/_apis/drop/drops/"}

echo ${name}