using Business.Domain.Picking;
using System;

namespace Application.Picking.Interface.DTOs.Params
{
    public class OrderPickingParams
    {

        public bool FilterByContainer { get; private set; }
        public string Container { get; private set; }


        public bool FilterByStatus { get; private set; }
        public PickingStatus Status { get; private set; }


        public bool FilterByOperator { get; private set; }
        public string Operator { get; private set; }


        public bool FilterByArea { get; private set; }
        public string Area { get; private set; }


        public void SetContainerFilter(string container)
        {
            this.Container = container;
            this.FilterByContainer = true;
        }
        public bool SetStatusFilter(PickingStatus status) {
            if (!Enum.IsDefined(typeof(PickingStatus), status))
                return false;

            this.Status = status;
            this.FilterByStatus = true;
            return true;
        }
        public void SetOperatorFilter(string _operator)
        {
            this.Operator = _operator;
            this.FilterByOperator = true;
        }

        public void SetAreaFilter(string area)
        {
            Area = area;
            FilterByArea = true;
        }
    }
}
