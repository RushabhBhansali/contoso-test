#!/bin/bash

artifactDir=${1}
pattern="${artifactDir}/**/VSTSDrop.json"
files=( $pattern )

echo "${files[0]}"