using Application.User.Interface.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Interface.Users
{
    public interface IUserApplication
    {
        UserDto Find(string username);
    }
}
