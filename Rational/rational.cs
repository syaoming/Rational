using System;
using Rational.Rational;

namespace Math.Rational
{
    /*
     * NOTE:
     * We use a irreducible representation,
     * where each rational is encoded uniquely as a pair of ints,
     * with signed numerator and non-negative denominator.
     */

    /// <summary>
    /// A rational number data type, implemented with unique int pair.
    /// </summary>
    public struct rational : IComparable<rational>, IEquatable<rational>
            , IComparable<int>, IEquatable<int>
            //, IComparable<long>, IEquatable<long>
            , IComparable<float>, IEquatable<float>
        //, IComparable<double>, IEquatable<double>
    {
        private readonly int num;
        private readonly int denom;

        public int Numerator => num;
        public int Denominator => denom;

        public static readonly rational Zero = new rational(0, 1);
        public static readonly rational One = new rational(1, 1);
        public static readonly rational Half = new rational(1, 2);
        public static readonly rational PositiveInfinity = new rational(1, 0);
        public static readonly rational NegativeInfinity = new rational(-1, 0);
        public static readonly rational NaN = new rational(0, 0);

        public static readonly rational MaxValue = new rational(int.MaxValue, 1);
        public static readonly rational Epsilon = new rational(1, int.MaxValue);
        public static readonly rational MinValue = new rational(int.MinValue, 1);

        public rational(int numerator, int demoninator)
        {
            reduce(ref numerator, ref demoninator);
            num = numerator;
            denom = demoninator;
        }

        public rational(int n = 0)
        {
            num = n;
            denom = 1;
        }

        #region Arithmetic operations

        public static rational operator +(rational q) { return q; }
        public static rational operator -(rational q) { return new rational(checked(-q.num), q.denom); }

        /*
        public static rational operator +(rational lhs, rational rhs) { return Zero; }
        public static rational operator -(rational lhs, rational rhs) { return Zero; }
        public static rational operator *(rational lhs, rational rhs) { return Zero; }
        public static rational operator /(rational lsh, rational rhs) { return Zero; }
        */

        public static bool operator >(rational lhs, rational rhs)
        {
            if (IsNaN(lhs) || IsNaN(rhs)) return false;
            if (IsNegativeInfinity(lhs)) return false;
            if (IsNegativeInfinity(rhs)) return true;
            return checked((long) lhs.num * rhs.denom > (long) lhs.denom * rhs.num);
        }

        public static bool operator <(rational lhs, rational rhs)
        {
            if (IsNaN(lhs) || IsNaN(rhs)) return false;
            if (IsNegativeInfinity(rhs)) return false;
            if (IsNegativeInfinity(lhs)) return true;
            return checked((long) lhs.num * rhs.denom < (long) lhs.denom * rhs.num);
        }

        public static bool operator ==(rational lhs, rational rhs)
        {
            return lhs.num == rhs.num && lhs.denom == rhs.denom;
        }

        public static bool operator !=(rational lhs, rational rhs) { return !(lhs == rhs); }

        #endregion

        #region C# overrides/interfaces

        public override string ToString() { return $"{num}/{denom}"; }

        /// <summary>
        /// Behaves similarily to float: Nan, -inf, n, inf
        /// </summary>
        public int CompareTo(rational other)
        {
            if (this.denom == 0)
            {
                if (other.denom == 0)
                {
                    // both non-finite
                    if (this.num == other.num) return 0;
                    if (this.num == 0 || other.num == 1) return -1;
                    return 1;
                }
                else
                {
                    // this non-finite, other finite
                    if (this.num == 0) return -1;
                    return this.num;
                }
            }
            else
            {
                if (other.denom == 0)
                {
                    // this finite, other non-finite
                    if (other.num == 0) return -1;
                    return other.num;
                }
                else
                {
                    // both finite
                    if (this == other) return 0;
                    if (this < other) return -1;
                    return 1;
                }
            }
        }

        public bool Equals(rational other) { return this.num == other.num && this.denom == other.denom; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is rational && Equals((rational) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (num * 397) ^ denom;
            }
        }

        #endregion

        #region int compatibility

        public static implicit operator rational(int n) { return new rational(n); }

        public int CompareTo(int other) { throw new NotImplementedException(); }

        public bool Equals(int other) { throw new NotImplementedException(); }

        #endregion

        #region float compatibility

        public int CompareTo(float other) { throw new NotImplementedException(); }

        public bool Equals(float other) { throw new NotImplementedException(); }

        #endregion

        #region Static utilities 

        public static bool IsNaN(rational r) { return r.denom == 0 && r.num == 0; }
        public static bool IsNegativeInfinity(rational r) { return r.denom == 0 && r.num < 0; }
        public static bool IsPositiveInfinity(rational r) { return r.denom == 0 && r.num > 0; }
        public static bool IsInfinity(rational r) { return r.denom == 0 && r.num != 0; }

        // Convert to a unique, irreducible representation
        private static void reduce(ref int num, ref int denom)
        {
            // non-finites
            if (denom == 0)
            {
                num = System.Math.Sign(num);
                return;
            }

            // finites
            int sign = System.Math.Sign(num * denom);
            uint unum = (uint) System.Math.Abs(num);
            uint udenum = (uint) System.Math.Abs(denom);
            uint d = utility.gcd(unum, udenum);
            num = checked(sign * (int) (unum / d));
            denom = checked((int) (udenum / d));
        }

        #endregion
    }
}
