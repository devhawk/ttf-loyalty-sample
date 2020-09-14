using System;
using Neo.SmartContract.Framework;

namespace LoyaltySample
{
    [ManifestExtra("Author", "Harry Pierson")]
    [ManifestExtra("Email", "hpierson@ngd.neo.org")]
    [ManifestExtra("Description", "This is a TTF Loyalty example")]
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public partial class LoyaltyToken : SmartContract
    {
    }
}
