using Business.Domain.Warehouse.Stock;
using Database.Warehouse;
using Repository.Warehouse.Interface.Sectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Warehouse.Sectors
{
    public class SectorRepository : ISectorRepository
    {
        private readonly WarehouseDbContext _dbContext;

        public SectorRepository(WarehouseDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<Sector> Get() {
            return _dbContext.Sectors
                .Where(s => !s.IsDeleted)
                .Select(s => s.ToDomain());
        }
    }
}
