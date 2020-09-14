using System;
using System.ComponentModel;
using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;

namespace LoyaltySample
{
    [ManifestExtra("Author", "Harry Pierson")]
    [ManifestExtra("Email", "hpierson@ngd.neo.org")]
    [ManifestExtra("Description", "This is a TTF Loyalty example")]
    [Features(ContractFeatures.HasStorage | ContractFeatures.Payable)]
    public partial class LoyaltyToken : SmartContract
    {
        [DisplayName("Transfer")]
        public static event Action<byte[], byte[], BigInteger> OnTransfer;

        // default TTF Token operations
        public static string Name() => "Loyalty Sample Token";

        public static string Symbol() => "LST";

        public static BigInteger TotalSupply() => TotalSupplyStorage.Get();

        public static BigInteger GetBalance(byte[] account)
        {
            if (!ValidateAddress(account)) throw new Exception("Invalid account address");
            return TokenStorage.Get(account);
        }

        // Indivisible behavior
        public static ulong Decimals() => 0;

        // Mintable Behavior
        public static void Mint(BigInteger quantity) 
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            MintTo(tx.Sender, quantity);
        }

        public static void MintTo(byte[] account, BigInteger quantity) 
        {
            if (!ValidateAddress(account)) throw new Exception("Invalid to account");
            if (quantity <= 0) throw new Exception("Quantity must be greater than 0.");
            if (!IsPayable(account)) throw new Exception("to account cannot receive.");

            // Need to verify caller is in the Minters role

            TokenStorage.Increase(account, quantity);
            TotalSupplyStorage.Increase(quantity);
            OnTransfer(null, account, quantity);
        }

        // Burnable behavior
        public static void Burn(BigInteger quantity) 
        {
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            BurnFrom(tx.Sender, quantity);
        }

        public static void BurnFrom(byte[] account, BigInteger quantity) 
        {
            if (!ValidateAddress(account)) throw new Exception("Invalid to account");
            if (quantity <= 0) throw new Exception("Quantity must be greater than 0.");
            if (TokenStorage.Get(account) < quantity) throw new Exception("Insufficient balance.");

            TokenStorage.Reduce(account, quantity);
            TotalSupplyStorage.Reduce(quantity);
            OnTransfer(account, null, quantity);
        }

        // Minters Roles behavior

        /*
        Notes:
            base Roles behavior defines a role property + GetRoleMembers method.
            Loyalty definition renames GetRoleMembers as GetMinters.
            I've swapped "RoleMember" for "Minter" across the entire Roles behavior method
            If a given contract had multiple roles, then I could using the more generic
            "[Get/Add/Remove/Is]RoleMember" shape, but not for a contract with a single
            role. Also, it's not clear how [Add/Remove]RoleMember is supposed to be
            secured. Should there also be an "administrators" role? Is only the 
            contract owner allowed to add/remove Role Members?
        */

        public static object GetMinters() 
        {
            // TODO: what's the right way to return a list of accounts?
            return null;
        }

        public static void AddMinter(byte[] account)
        {
        }

        public static void RemoveMinter(byte[] account)
        {
        }

        public static bool IsMinter(byte[] account)
        {
            return false;
        }

        // RoleCheck is defined to be "Internal invocation", so I've marked it private
        private static bool MinterRoleCheck(byte[] account)
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
            Transaction tx = (Transaction)ExecutionEngine.ScriptContainer;
            TransferFrom(tx.Sender, toAccount, quantity);
        }

        public static void TransferFrom(byte[] fromAccount, byte[] toAccount, BigInteger quantity) 
        {
            if (!ValidateAddress(fromAccount)) throw new Exception("Invalid from account");
            if (!ValidateAddress(toAccount)) throw new Exception("Invalid to account");
            if (quantity <= 0) throw new Exception("Quantity must be greater than 0.");
            if (!IsPayable(toAccount)) throw new Exception("to account cannot receive.");
            if (!Runtime.CheckWitness(fromAccount) && !fromAccount.Equals(ExecutionEngine.CallingScriptHash)) throw new Exception("No authorization.");
            if (TokenStorage.Get(fromAccount) < quantity) throw new Exception("Insufficient balance.");
            if (fromAccount != toAccount)
            {
                TokenStorage.Reduce(fromAccount, quantity);
                TokenStorage.Increase(toAccount, quantity);
                OnTransfer(fromAccount, toAccount, quantity);
            }
        }   

        // other helper functions

        private static bool ValidateAddress(byte[] address) => address.Length == 20 && address.ToBigInteger() != 0;
        private static bool IsPayable(byte[] address) => Blockchain.GetContract(address)?.IsPayable ?? true;
    }
}
