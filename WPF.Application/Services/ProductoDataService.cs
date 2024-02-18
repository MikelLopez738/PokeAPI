using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using WPF.Application.Model;
using WPF.Application.MVVM.Model.DTOs;
using WPF.Application.MVVM.Model.Interfaces;
using Newtonsoft.Json;
using System.Windows.Documents;
using System.Threading.Tasks;

namespace WPF.Application.Services
{
    public class ProductoDataService : IProductDataService
    {
        public List<ProductoDTO> GetProductos()
        {
            List<ProductoDTO> productos = new();
            using (var context = new AplicacionWpfContext())
            {
                var products = context.Products.ToList();
                foreach (var item in products)
                {
                    productos.Add(new ProductoDTO()
                    {
                        ProductId = item.ProductId,
                        Amount = item.Amount,
                        Name = item.Name,
                        Price = item.Price
                    });
                }
            }

            return productos;
        }

        
    }
}
