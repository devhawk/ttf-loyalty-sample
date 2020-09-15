dotnet build ./contract
dotnet nxp3 reset -f
dotnet nxp3 transfer gas 10000 genesis owen
dotnet nxp3 transfer gas 10000 genesis alice
dotnet nxp3 contract deploy ./contract/bin/Debug/netstandard2.1/Loyalty.nef owen
dotnet nxp3 checkpoint create checkpoints/deployed -f
dotnet nxp3 contract invoke ./invoke-files/add-minter.neo-invoke.json owen
dotnet nxp3 checkpoint create checkpoints/minter-added -f
dotnet nxp3 contract invoke ./invoke-files/mint.neo-invoke.json alice
dotnet nxp3 checkpoint create checkpoints/minted -f
dotnet nxp3 contract invoke ./invoke-files/transfer.neo-invoke.json alice
dotnet nxp3 checkpoint create checkpoints/transfered -f
dotnet nxp3 contract invoke ./invoke-files/burn.neo-invoke.json alice
dotnet nxp3 checkpoint create checkpoints/burned -f
