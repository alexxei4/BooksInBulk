using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {

        void Update(Product obj);

        void Save();
        Product GetFirstOrDefault(Expression<Func<Product, bool>> filter, string? includeProperties = null);


    }
}
