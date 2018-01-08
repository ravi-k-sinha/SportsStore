using System.Linq;

namespace SportsStore.Models
{
    interface IProductRespository
    {
        IQueryable<Product> Products { get; }
    }
}
