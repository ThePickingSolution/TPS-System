using Business.Domain.Warehouse.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Warehouse.Interface.Sectors
{
    public interface ISectorRepository
    {
        IEnumerable<Sector> Get();
    }
}
