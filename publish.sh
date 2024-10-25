#!/bin/bash
echo on
rm -rf MauiApp3/bin/
sudo dotnet workload restore
echo "Compiling Application"
cd MauiApp3
dotnet publish -c Release -f net8.0-maccatalyst --self-contained -p:CreatePackage=false -p:GeneratePackageOnBuild=false
cd bin/Release/net8.0-maccatalyst/osx-arm64
open .