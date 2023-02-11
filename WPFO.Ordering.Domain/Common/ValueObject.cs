using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace WPFO.Ordering.Domain.Common
{
    //for equals two instances
    // Example
    // var one = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
    // var two = new Address("1 Microsoft Way", "Redmond", "WA", "US", "98052");
    // 
    // Console.WriteLine(EqualityComparer<Address>.Default.Equals(one, two)); // True
    // Console.WriteLine(object.Equals(one, two)); // True
    // Console.WriteLine(one.Equals(two)); // True
    // Console.WriteLine(one == two); // True

    //public class Address : ValueObject
    //{
    //    public String Street { get; private set; }
    //    public String City { get; private set; }
    //    public String State { get; private set; }
    //    public String Country { get; private set; }
    //    public String ZipCode { get; private set; }
    //
    //    public Address() { }
    //
    //    public Address(string street, string city, string state, string country, string zipcode)
    //    {
    //        Street = street;
    //        City = city;
    //        State = state;
    //        Country = country;
    //        ZipCode = zipcode;
    //    }
    //
    //    protected override IEnumerable<object> GetEqualityComponents()
    //    {
    //        // Using a yield return statement to return each element one at a time
    //        yield return Street;
    //        yield return City;
    //        yield return State;
    //        yield return Country;
    //        yield return ZipCode;
    //    }
    //}
    public abstract class ValueObject
    {
        
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (left is null ^ right is null)
                return false;
            return left?.Equals(right)!=false;
        }

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !(EqualOperator(left, right));
        }

        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if(obj==null || obj.GetType() != GetType())
                return false;
            
            var other= (ValueObject)obj;
            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents().Select(x => x != null ? x.GetHashCode() : 0).
                Aggregate((x, y) => x ^ y);
        }


    }
}
