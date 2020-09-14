dotnet build ./contract
dotnet nxp3 reset -f
dotnet nxp3 contract deploy ./contract/bin/Debug/netstandard2.1/safe-purchase.nef genesis
