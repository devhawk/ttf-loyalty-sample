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
            // Need to verify caller is in the Minters role
        }

        public static void MintTo(byte[] account, BigInteger quantity) 
        {
            // Need to verify caller is in the Minters role
        }

        // Burnable behavior
        public static void Burn(BigInteger quantity) 
        {
        }

        public static void BurnFrom(byte[] account, BigInteger quantity) 
        {
        }

        // Minters Roles behavior

        /*
        Notes:
            base Roles behavior defines a role property + GetRoleMembers method.
            Loyalty definition renames GetRoleMembers as GetMinters.
            Seems like the expected api model is contract.Minters.GetRoleMembers, but that doesn't work in Neo or GRPC
            I would think that "RoleMember" should be replaced with "Minters" across the entire Roles API, not just GetRoleMembers
        */

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

        /*
        Notes:
            This API seems under specified. The description of Allowance references
            "A Request by a party or account to the owner of a token(s)" but the owner
            is not specified as a parameter. ApproveAllowance is even odder, suggesting
            the AllowanceRequest could be forwarded to other parties and that "When all
            Approvals are obtained, an AllowanceResponse could be sent." But I don't
            understand how that would work on a block chain. Are we invoking other 
            contracts or getting approval from human beings? 
        */
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
