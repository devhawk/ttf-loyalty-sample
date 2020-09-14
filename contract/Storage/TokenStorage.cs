using System.Numerics;
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace LoyaltySample
{
    public static class TokenStorage
    {
        public static void Increase(byte[] key, BigInteger value) => Put(key, Get(key) + value);

        public static void Reduce(byte[] key, BigInteger value)
        {
            var oldValue = Get(key);
            if (oldValue == value)
                Remove(key);
            else
                Put(key, oldValue - value);
        }

        public static void Put(byte[] key, BigInteger value) => Storage.CurrentContext.CreateMap(nameof(TokenStorage)).Put(key, value);

        public static BigInteger Get(byte[] key) => Storage.CurrentContext.CreateMap(nameof(TokenStorage)).Get(key).ToBigInteger();

        public static void Remove(byte[] key) => Storage.CurrentContext.CreateMap(nameof(TokenStorage)).Delete(key);
    }
}