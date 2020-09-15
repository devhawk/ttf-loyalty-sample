using Neo.SmartContract.Framework.Services.Neo;

namespace LoyaltySample
{
    public static class MinterRoleStorage
    {
        public static void Add(byte[] account)
        {
            var map = Storage.CurrentContext.CreateMap(nameof(MinterRoleStorage));
            map.Put(account, 1);
        }

        public static void Remove(byte[] account)
        {
            var map = Storage.CurrentContext.CreateMap(nameof(MinterRoleStorage));
            map.Delete(account);
        }

        public static bool Check(byte[] account)
        {
            var map = Storage.CurrentContext.CreateMap(nameof(MinterRoleStorage));
            return map.Get(account) != null;
        }
    }
}