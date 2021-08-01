using Business.Domain.Administration.People;
using Database.Administration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Administration.People
{
    internal static class UserObjectMapper
    {
        public static User ToDomain(this UserEntity entity)
        {
            if (entity == null)
                return null;

            var model = new User() {
                Id = entity.Id,
                Username = entity.Username,
                Name = entity.Name,
                Active = entity.Active.HasValue && entity.Active.Value
            };

            return model;
        }

        public static UserEntity ToEntity(this User model) {
            if (model == null)
                return null;

            var entity = new UserEntity() {
                Id = model.Id,
                Username = model.Username,
                Name = model.Name,
                Active = model.Active
            };

            return entity;
        }
    }
}
