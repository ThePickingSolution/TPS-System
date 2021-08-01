using Business.Domain.Administration.Exceptions;
using Business.Domain.Administration.Validations;
using Infrastructure.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Administration.People
{
    public class User
    {
        private IUserValidation _validator;

        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }

        public IUserValidation Validator {
            set {
                if (value == null)
                    throw new AdmNullValidatorException("User validator is null");
                _validator = value;
            }
        }


        public bool IsValidToCreate(out AdministrationException exception) {
            if (Username.IsNullOrEmpty()) {
                exception = new MissingUserValueException("Missing user name");
            } else if (Name.IsNullOrEmpty()) {
                exception = new MissingUserValueException("Missing name");
            }else if (!_validator.IsUniqueUsername(this)) {
                exception = new UsernameNotUniqueException($"User name {Username} already exists");
            } else {
                exception = null;
                return true;
            }
            return false;
        }
        public bool IsValidToEdit(out AdministrationException exception) {
            if (Username.IsNullOrEmpty()) {
                exception = new MissingUserValueException("Missing user name");
            } else if (Name.IsNullOrEmpty()) {
                exception = new MissingUserValueException("Missing name");
            } else if (!_validator.IsUniqueUsername(this)) {
                exception = new UsernameNotUniqueException($"User name {Username} already exists");
            } else {
                exception = null;
                return true;
            }
            return false;
        }
    }
}
