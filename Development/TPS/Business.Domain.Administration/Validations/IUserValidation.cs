using Business.Domain.Administration.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Administration.Validations
{
    public interface IUserValidation
    {
        bool IsUniqueUsername(User user);
    }

    public class MockUserValidation : IUserValidation
    {
        public bool IsUniqueUsername(User user) {
            return true;
        }
    }
}
