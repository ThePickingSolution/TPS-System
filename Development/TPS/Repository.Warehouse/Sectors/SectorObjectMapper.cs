using Business.Domain.Warehouse.Stock;
using Database.Warehouse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Warehouse.Sectors
{
    internal static class SectorObjectMapper
    {
        public static Sector ToDomain(this SectorEntity entity) {
            if (entity == null)
                return null;


            return new Sector() {
                Id = entity.Id,
                Code = entity.Code,
                Name = entity.Name
            };
        }
    }
}
