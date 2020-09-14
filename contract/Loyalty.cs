using System;
using System.Numerics;
using Neo.SmartContract.Framework;

namespace LoyaltySample
{
    [ManifestExtra("Author", "Harry Pierson")]
    [ManifestExtra("Email", "hpierson@ngd.neo.org")]
    [ManifestExtra("Description", "This is a TTF Loyalty example")]
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public partial class LoyaltyToken : SmartContract
    {
        // default TTF Token operations
        public static string Name() => "Loyalty Sample Token";

        public static string Symbol() => "LST";

        public static BigInteger TotalSupply() => 0;

        public static BigInteger GetBalance(byte[] account) => 0;

        // Indivisible behavior
        public static ulong Decimals() => 0;

        // Mintable Behavior
        public static void Mint(BigInteger quantity) 
        {
        }

        public static void MintTo(byte[] account, BigInteger quantity) 
        {
        }

        // Burnable behavior
        public static void Burn(BigInteger quantity) 
        {
        }

        public static void BurnFrom(byte[] account, BigInteger quantity) 
        {
        }

        // Minters Roles behavior
        public static object GetMinters() 
        {
            // TODO: what's the right way to return a list of accounts?
            return null;
        }

        public static void AddRoleMember(string role, byte[] account)
        {
        }

        public static void RemoveRoleMember(string role, byte[] account)
        {
        }

        public static bool IsInRole(string role, byte[] account)
        {
            return false;
        }

        // RoleCheck is defined to be "Internal invocation", so I've marked it private
        private static bool RoleCheck(byte[] account)
        {
            return false;
        }


        // Delegable
        public static void Allowance(BigInteger quantity)
        {
        }

        public static void ApproveAllowance(BigInteger quantity)
        {
        }


        // Transfer behavior
        public static void Transfer(byte[] toAccount, BigInteger quantity) 
        {
        }

        public static void TransferFrom(byte[] fromAccount, byte[] toAccount, BigInteger quantity) 
        {
        }   
    }
}
