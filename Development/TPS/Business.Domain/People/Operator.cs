using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.People
{
    public class Operator
    {
        public string Username { get; protected set; }
        public string Name { get; protected set; }

        public Operator(string username) {
            Username = username;
        }
        public Operator(string username, string name)
        {
            Username = username;
            Name = name;
        }
    }
}
