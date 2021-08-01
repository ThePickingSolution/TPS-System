using Business.Domain.Administration.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Administration.Interface.People
{
    public interface IUserRepository
    {
        User Get(Guid id);
        User Get(string username);

        User Create(User user);
        User Update(User user);
        void Delete(Guid id);
    }
}
