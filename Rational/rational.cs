/*
 * IRREDUCIBLE_REPRESENTATAION is a preferrable cnfiguration when binary con-
 * sistency is requred and performance is less a problem.
 */
#define IRREDUCIBLE_REPRESENTATION

using System;

namespace Math.Rational
{
    /// <summary>
    /// A rational struct implemented with int.
    /// Internal representation is always irreducible.
    /// </summary>
    public struct rational : IComparable<rational>, IEquatable<rational>
            , IComparable<int>, IEquatable<int>
            //, IComparable<long>, IEquatable<long>
            , IComparable<float>, IEquatable<float>
            //, IComparable<double>, IEquatable<double>
    {
        private int num;
        private int denom;

        public static readonly rational Zero = new rational(0, 1);
        public static readonly rational One = new rational(1, 1);
        public static readonly rational Half = new rational(1, 2);
        public static readonly rational PositiveInfinity = new rational(1, 0);
        public static readonly rational NegativeInfinity = new rational(-1, 0);
        public static readonly rational NaN = new rational(0, 0);

        public int Numerator => num;
        public int Denominator => denom;

        public rational(int numerator, int demoninator)
        {
            num = numerator;
            denom = demoninator;
        }

        public rational(int n)
        {
            num = n;
            denom = 1;
        }

        public static rational operator +(rational q) { return q;}
        public static rational operator -(rational q) { return new rational(-q.num, q.denom); }

        /*
        public static rational operator +(rational lhs, rational rhs) { return Zero; }
        public static rational operator -(rational lhs, rational rhs) { return Zero; }
        public static rational operator *(rational lhs, rational rhs) { return Zero; }
        public static rational operator /(rational lsh, rational rhs) { return Zero; }
        */

        public int CompareTo(rational other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(rational other)
        {
#if IRREDUCIBLE_REPRESENTATION
            return this.num == other.num && this.denom == other.denom;
#else
            // both non-finite
            if (this.denom == 0 && other.denom == 0)
            {
                return this.num * other.num > 0;
            }
            // one finite, one non-finite
            if (this.denom == 0 ^ other.denom == 0)
            {
                return false;
            }
            // both finite
            return this.num * other.denom == this.denom * other.num;
#endif
        }

        // Convert to a unique, irreducible representation
        private void Reduce()
        { }
        
#region int compatibility

        public static implicit operator rational(int n) 
        {
            return new rational(n);
        }

        public int CompareTo(int other) { throw new NotImplementedException(); }

        public bool Equals(int other) { throw new NotImplementedException(); }

#endregion

#region float compatibility

        public int CompareTo(float other) { throw new NotImplementedException(); }

        public bool Equals(float other) { throw new NotImplementedException(); }

#endregion
    }
}
