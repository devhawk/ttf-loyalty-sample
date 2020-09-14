dotnet build ./contract
dotnet nxp3 reset -f
dotnet nxp3 transfer gas 1000 genesis alice
dotnet nxp3 contract deploy ./contract/bin/Debug/netstandard2.1/Loyalty.nef genesis
dotnet nxp3 checkpoint create checkpoints/mint -f
dotnet nxp3 contract invoke ./invoke-files/mint.neo-invoke.json alice
dotnet nxp3 checkpoint create checkpoints/transfer -f
dotnet nxp3 contract invoke ./invoke-files/transfer.neo-invoke.json alice
dotnet nxp3 checkpoint create checkpoints/burn -f
dotnet nxp3 contract invoke ./invoke-files/burn.neo-invoke.json alice
dotnet nxp3 checkpoint create checkpoints/after-burn -f