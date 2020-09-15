using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;

namespace LoyaltySample
{
    public static class MinterRoleStorage
    {
        private static NeoArray<byte[]> GetMap()
        {
            var result = Storage.Get(nameof(MinterRoleStorage));
            return result == null ? new NeoArray<byte[]>() : result.Deserialize() as NeoArray<byte[]>;
        }

        private static void PutMap(NeoArray<byte[]> map)
        {
            Storage.Put(nameof(MinterRoleStorage), map.Serialize());
        }

        public static void Add(byte[] account)
        {
            var map = GetMap();
            if (Find(map, account) == -1)
            {
                map.Add(account);
                PutMap(map);
            }
        }

        public static void Remove(byte[] account)
        {
            var map = GetMap();
            var i = Find(map, account);
            if (i != -1)
            {
                map.RemoveAt(i);
                PutMap(map);
            }
        }

        private static int Find(NeoArray<byte[]> map, byte[] account)
        {
            for (int i = 0; i < map.Count; i++)
            {
                if (map[i] == account)
                {
                    return i;
                }
            }
            
            return -1;
        }

        public static bool Check(byte[] account)
        {
            var map = GetMap();
            return Find(map, account) >= 0;
        }

        public static byte[][] Get()
        {
            var map = GetMap();
            return map.Values;
        }
    }
}