using ProductCatalogue.Models;
using System.Collections.Generic;

namespace ProductCatalogue.Services
{
    public interface IDataProvider
    {
        bool Delete(IItem item);

        List<IItem> GetAllItems();

        IItem GetItemById(string id);

        bool Insert(IItem item);

        bool Update(IItem item);
    }
}