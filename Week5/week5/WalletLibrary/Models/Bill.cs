using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WalletLibrary.Models
{
    public class Bill : IComparable<Bill> 
    {
        private Denomination _denomination;

        // Property to expose the private field
        /*
        internal int Amount
        {
            //get { return (int)this._denomination; }
            //get => (int)this._denomination;
        }
        */
        public int Amount => (int)this._denomination; // shorthand of the above constructor
        // Add read only Denomination property, type of Denomination Enum
        public Denomination Denomination => this._denomination;
        public string Portrait
       {
            get
            {
                switch (this._denomination)
                {
                    case Denomination.One:
                        return "George Washington";
                    case Denomination.Two:
                        return "Thomas Jefferson";
                    case Denomination.Five:
                        return "Abraham Lincoln";
                    case Denomination.Ten:
                        return "Alexander Hamilton";
                    case Denomination.Twenty:
                        return "Andrew Jackson";
                    case Denomination.Fifty:
                        return "Ulysses S. Grant";
                    case Denomination.Hundred:
                        return "Benjamin Franklin";
                    default:
                        return "Unknown";
                }
            }
       }
        
        // Constructor method does not have a return type
        public Bill(Denomination _denom)
        {
            this._denomination = _denom;
        }

        // method to implement IComparable<Bill>
        public int CompareTo(Bill? other)
        {
            return this.Amount.CompareTo(other?.Amount);
        }

        // Method overloading operator booleans
        public static bool operator < (Bill lhs, Bill rhs) => lhs.Amount < rhs.Amount;
        public static bool operator > (Bill lhs, Bill rhs) => lhs.Amount > rhs.Amount;
        public static bool operator <= (Bill lhs, Bill rhs) => lhs.Amount <= rhs.Amount;
        public static bool operator >= (Bill lhs, Bill rhs) => lhs.Amount >= rhs.Amount;
        public static bool operator == (Bill lhs, Bill rhs) => lhs.Amount == rhs.Amount;
        public static bool operator != (Bill lhs, Bill rhs) => lhs.Amount != rhs.Amount;
        
        public override bool Equals(object? obj) // This should be defined whenever == is overloaded
        {
            if (obj is null || obj is not Bill) return false;
            return this.Amount == ((Bill)obj).Amount;
        }
        public override int GetHashCode()
        {
            return this.Amount.GetHashCode();
        }
        public override string ToString()
        {
            return $"{this.Amount:C0} bill with {this.Portrait} portrait";
        }

        // Method overloading for binary operator * + - /
        public static List<Bill> operator * (Bill lhs, int rhs)
        {
            if (rhs > 1000) throw new ArgumentException("Stack too fat");
            if (rhs <= 0) throw new ArgumentException("Does not compute");
            List<Bill> result = new();
            for (int i = 0; i < rhs; i++)
            {
                result.Add(new Bill((Denomination)lhs.Amount));
            }
            return result;
        }


        // With merge
        public static List<Bill> operator + (Bill lhs, Bill rhs)
        {
            List<Bill> result = new List<Bill>();

            switch (lhs.Amount + rhs.Amount)
            {
                case 2 : result.Add(new Bill(Denomination.Two));
                    break;
                case 5:
                    result.Add(new Bill(Denomination.Five));
                    break;
                case 10:
                    result.Add(new Bill(Denomination.Ten));
                    break;
                case 20:
                    result.Add(new Bill(Denomination.Twenty));
                    break;
                case 50:
                    result.Add(new Bill(Denomination.Fifty));
                    break;
                case 100:
                    result.Add(new Bill(Denomination.Hundred));
                    break;
                default:
                    result.AddRange([lhs, rhs]);
                    break;
            }
            return result;
        }

        // Without merge
        public static List<Bill> operator + (List<Bill> lhs, Bill rhs)
        {
            lhs.Add(rhs);
            return lhs;
        }
        public static List<Bill> operator + (Bill lhs, List<Bill> rhs)
        {
            rhs.Add(lhs);
            return rhs;
        }


    }

    public enum Denomination
    {
        One = 1,
        Two = 2,
        Five = 5,
        Ten = 10,
        Twenty = 20,
        Fifty = 50,
        Hundred = 100
    }

}
