using Application.Warehouse.Interface.DTOs.Stock;
using System.Collections.Generic;

namespace Application.Warehouse.Interface.Stock
{
    public interface IAreaApplication
    {
        IEnumerable<AreaDto> List();
    }
}
