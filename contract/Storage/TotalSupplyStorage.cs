using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace LoyaltySample
{

    public static class TotalSupplyStorage
    {
        public static void Increase(BigInteger value) => Put(Get() + value);

        public static void Reduce(BigInteger value) => Put(Get() - value);

        public static void Put(BigInteger value) 
            => Storage.Put(Storage.CurrentContext, nameof(TotalSupplyStorage), value);

        public static BigInteger Get()
            => Storage.Get(Storage.CurrentContext, nameof(TotalSupplyStorage)).ToBigInteger();
    }
}