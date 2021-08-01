using Business.Domain.Administration.Exceptions;
using Business.Domain.Administration.People;
using Business.Domain.Administration.Validations;
using Database.Administration;
using Repository.Administration.Interface.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Administration.People
{
    public class UserRepository : IUserRepository
    {
        private readonly AdministrationDbContext _dbContext;
        private readonly IUserValidation _validation;

        public UserRepository(AdministrationDbContext dbContext,
            IUserValidation validation) {
            _dbContext = dbContext;
            _validation = validation;
        }

        public User Create(User user) {
            AdministrationException exception;
            user.Validator = _validation;
            if (!user.IsValidToCreate(out exception)) {
                throw exception;
            }

            var entity = user.ToEntity();
            entity.Create();

            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();
            return Get(entity.Username);
        }

        public void Delete(Guid id) {
            var entity = _dbContext.Users
                .FirstOrDefault(user => user.Id == id && user.Active.HasValue);
            if (entity == null)
                throw new RegistryNotFoundException("User not found");

            entity.Delete();
            _dbContext.Users.Update(entity);
            _dbContext.SaveChanges();
        }

        public User Get(Guid id)
        {
            return _dbContext.Users
                .FirstOrDefault(user => user.Id == id && user.Active.HasValue)
                .ToDomain();
        }

        public User Get(string username)
        {
            return _dbContext.Users
                .FirstOrDefault(user => user.Username == username && user.Active.HasValue)
                .ToDomain();
        }

        public User Update(User user) {
            var entity = _dbContext.Users
               .FirstOrDefault(_user => _user.Id == user.Id && _user.Active.HasValue);
            if (entity == null)
                throw new RegistryNotFoundException("User not found");

            AdministrationException exception;
            user.Validator = _validation;
            if (!user.IsValidToEdit(out exception)) {
                throw exception;
            }

            entity.CopyToEdit(user.ToEntity());
            _dbContext.Users.Update(entity);
            _dbContext.SaveChanges();
            return entity.ToDomain();
        }
    }
}
