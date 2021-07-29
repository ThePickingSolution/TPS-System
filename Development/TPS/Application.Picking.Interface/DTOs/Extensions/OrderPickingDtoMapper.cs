using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Picking.Interface.DTOs.Extensions
{
    public static class OrderPickingDtoMapper
    {
        public static OrderPickingDto ParseDto(this OrderPicking model)
        {
            return new OrderPickingDto()
            {
                Id = model.Id,
                Area = model.Area,
                Container = model.Container,
                Description = model.Description,
                Operator = model.Operator == null ? string.Empty : model.Operator.Username,
                ScanContainer = model.WithContainer,
                Status = model.Status,
                Details = model.Details.Select(s => new OrderPickingDetailDto(s.Key,s.Value)).ToList()
            };
        }
    
        public static IEnumerable<OrderPickingDto> ParseDtos(this IEnumerable<OrderPicking> models)
        {
            return models.Select(ParseDto);
        }
    }
}
