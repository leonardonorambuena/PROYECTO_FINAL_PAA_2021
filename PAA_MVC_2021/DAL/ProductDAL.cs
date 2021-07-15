using PAA_MVC_2021.Models.Entities;
using PAA_MVC_2021.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PAA_MVC_2021.DAL
{
    public class ProductDAL
    {
        public static async Task<List<Product>> GetProducts(ProductIndexViewModel vm, ApplicationDbContext db)
        {
            var queryProduct = db.Products.AsQueryable();

            if (vm.ProductCode != null)
                queryProduct = queryProduct.Where(x => x.ProductCode == vm.ProductCode);
            if (vm.ProductName != null)
                queryProduct = queryProduct.Where(x => x.ProductName == vm.ProductName);
            if (vm.PlatformId != null)
                queryProduct = queryProduct.Where(x => x.PlatformId == vm.PlatformId);
            if (!string.IsNullOrEmpty(vm.Search))
                queryProduct = queryProduct.Where(x => x.ProductName.Contains(vm.Search) || x.ProductCode == vm.Search);

            switch (vm.Sort)
            {
                case 1:
                    queryProduct = queryProduct.OrderBy(x => x.ProductCode);
                    break;
                case -1:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductCode);
                    break;
                case 2:
                    queryProduct = queryProduct.OrderBy(x => x.ProductName);
                    break;
                case -2:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductName);
                    break;
                case 3:
                    queryProduct = queryProduct.OrderBy(x => x.ProductPrice);
                    break;
                case -3:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductPrice);
                    break;
                case 4:
                    queryProduct = queryProduct.OrderBy(x => x.ProducStock);
                    break;
                case -4:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProducStock);
                    break;
                default:
                    queryProduct = queryProduct.OrderByDescending(x => x.ProductName);
                    break;
            }

            return await queryProduct
                            .Include(x => x.ProductPlatform)
                            .ToListAsync();

        }

    }
}