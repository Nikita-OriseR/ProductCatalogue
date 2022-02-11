using System;

namespace ProductCatalogue.Models
{
    public interface IItem
    {
        Guid Id { get; set; }
        Guid IdPrice { get; set; }
        int Code { get; set; }
        string Name { get; set; }
        string BarCode { get; set; }
        Decimal Quantity { get; set; }
        string Model { get; set; }
        string Sort { get; set; }
        string Color { get; set; }
        string Size { get; set; }
        string Wight { get; set; }
        DateTime DateChanges { get; set; }
    }
}