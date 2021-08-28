using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Picking.Interface.DTOs.Extensions
{
    public static class PickingItemDtoMapper
    {
        public static PickingItemDto ParseDto(this PickingItem model)
        {
            return new PickingItemDto()
            {
                Id = model.Id,
                Barcode = model.Barcode,
                SKU = model.SKU,
                Status = model.Status,
                Details = model.Details.Select(s => new PickingItemDetailDto(s.Key,s.Value)).ToList()
            };
        }
    
        public static IEnumerable<PickingItemDto> ParseDtos(this IEnumerable<PickingItem> models)
        {
            return models.Select(ParseDto);
        }
    }
}
