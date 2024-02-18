using System.Collections.Generic;
using WPF.Application.MVVM.Model.DTOs;

namespace WPF.Application.MVVM.Model.Interfaces
{
    public interface IProductDataService
    {
        public List<ProductoDTO> GetProductos();
    }
}
