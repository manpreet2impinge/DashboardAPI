#!/bin/bash
set -e
if [ -n "$BucketPath" ]; then
    rm -rf Configs
    aws s3 sync s3://$BucketPath .
fi
dotnet $1