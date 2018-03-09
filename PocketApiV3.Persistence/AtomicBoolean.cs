using System;
using System.Threading;

namespace PocketApiV3.Persistence
{
    struct AtomicBoolean
        : IComparable, IComparable<bool>, IComparable<AtomicBoolean>,
        IEquatable<bool>, IEquatable<AtomicBoolean>
    {
        public AtomicBoolean(bool initialValue)
        {
            _value = BoolToInt(initialValue);
        }

        /// <summary>
        /// Sets a new value, returning the previous value.
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns>The previous value.</returns>
        public bool Set(bool newValue)
        {
            int newValueInt = BoolToInt(newValue);
            int oldValueInt = Interlocked.Exchange(ref _value, newValueInt);
            bool oldValue = IntToBool(oldValueInt);
            return oldValue;
        }

        /// <summary>
        /// Tries setting a new value, returning a boolean indicating
        /// whether or not the value changed from its previous value.
        /// </summary>
        /// <param name="newValue"></param>
        /// <returns>TRUE if the value was changed, otherwise FALSE.</returns>
        public bool TrySet(bool newValue)
        {
            int newValueInt = BoolToInt(newValue);
            int oldValueInt = Interlocked.Exchange(ref _value, newValueInt);
            return newValueInt != oldValueInt;
        }

        public int CompareTo(object obj)
        {
            switch (obj)
            {
                case bool b:
                    return CompareTo(b);
                case AtomicBoolean atomicBool:
                    return CompareTo(atomicBool);
                default:
                    return 1;
            }
        }

        public int CompareTo(bool other) =>
            IntToBool(VolatileValue).CompareTo(other);

        public int CompareTo(AtomicBoolean other) =>
            IntToBool(VolatileValue).CompareTo(IntToBool(other.VolatileValue));

        public override bool Equals(object obj)
        {
            switch (obj)
            {
                case bool b:
                    return Equals(b);
                case AtomicBoolean atomicBool:
                    return Equals(atomicBool);
                default:
                    return false;
            }
        }

        public bool Equals(bool other) =>
            IntToBool(VolatileValue).Equals(other);

        public bool Equals(AtomicBoolean other) =>
            VolatileValue.Equals(other.VolatileValue);

        public override int GetHashCode() =>
            IntToBool(VolatileValue).GetHashCode();

        public override string ToString() =>
            IntToBool(VolatileValue).ToString();

        public static bool operator ==(AtomicBoolean x, AtomicBoolean y) =>
            x.VolatileValue == y.VolatileValue;

        public static bool operator ==(AtomicBoolean x, bool y) =>
            IntToBool(x.VolatileValue).Equals(y);

        public static bool operator ==(bool x, AtomicBoolean y) =>
            x.Equals(IntToBool(y.VolatileValue));

        public static bool operator !=(AtomicBoolean x, AtomicBoolean y) =>
            !(x == y);

        public static bool operator !=(AtomicBoolean x, bool y) => !(x == y);

        public static bool operator !=(bool x, AtomicBoolean y) => !(x == y);

        public static implicit operator bool(AtomicBoolean atomicBool) =>
            IntToBool(atomicBool.VolatileValue);

        // This kinda breaks the point...
        //public static implicit operator AtomicBoolean(bool b) =>
        //    new AtomicBoolean(b);

        int _value;

        int VolatileValue => Thread.VolatileRead(ref _value);

        static int BoolToInt(bool value) => value ? 1 : 0;
        static bool IntToBool(int value) => value == 0 ? false : true;
    }
}
