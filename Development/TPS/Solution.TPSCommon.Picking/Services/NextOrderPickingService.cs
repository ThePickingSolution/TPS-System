using Business.Domain.Picking;
using Business.Domain.Services;
using Repository.Picking.Interface.OrderPickings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.TPSCommon.Picking.Services
{
    /// <summary>
    /// TPSCommon INextOrderPickingService Implementation
    /// </summary>
    public class NextOrderPickingService : INextOrderPickingService
    {

        private readonly IOrderPickingQuery baseQuery;
        private const string SECTOR = "SECTOR";

        public NextOrderPickingService(IOrderPickingQuery _query) {
            baseQuery = _query;
        }

        /// <summary>
        /// The order pickings are ordered by date and the oldest one that matches the following criteria is returned:
        ///     1. Must contain one detail of key "SECTOR" and value equals the parameter sector
        ///     2. Status equals PENDING or READY
        /// </summary>
        /// <param name="sector"></param>
        /// <returns></returns>
        public OrderPicking NextOrderPicking(string sector) {
            var query = baseQuery.New();
            bool ascendingOrdering = true;

            return query
                .FilterByDetail(SECTOR, sector)
                .FilterByOrStatus(new List<PickingStatus>() { PickingStatus.PENDING, PickingStatus.READY })
                .OrderByDate(ascendingOrdering)
                .FirstOrDefault();
        }
    }
}
