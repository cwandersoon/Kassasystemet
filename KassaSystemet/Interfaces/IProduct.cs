using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystemet.Interfaces
{
    public interface IProduct
    {
        string ID { get; }
        string Name { get; }
        double Price { get; }
        PriceType PriceType { get; }
    }

    public enum PriceType
    {
        Styck,
        Kilo
    }
}
