using Neo.SmartContract.Framework;

namespace LoyaltySample
{
    public class NeoArray<T> 
    {
        [OpCode(OpCode.NEWARRAY0)]
        public extern NeoArray();

        public extern int Count
        {
            [OpCode(OpCode.SIZE)]
            get;
        }

        public extern T this[int key]
        {
            [OpCode(OpCode.PICKITEM)]
            get;
            [OpCode(OpCode.SETITEM)]
            set;
        }

        [OpCode(OpCode.APPEND)]
        public extern void Add(T item);

        [OpCode(OpCode.REMOVE)]
        public extern void RemoveAt(int index);

        [OpCode(OpCode.CLEARITEMS)]
        public extern void Clear();

        public extern T[] Values
        {
            [OpCode(OpCode.VALUES)]
            get;
        }
    }
}
