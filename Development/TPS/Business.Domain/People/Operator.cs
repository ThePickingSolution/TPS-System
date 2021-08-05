using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.People
{
    public class Operator
    {
        public Guid Id { get; protected set; }
        public string Username { get; protected set; }
        public string Name { get; protected set; }

        public Operator(Guid id) {
            Id = id;
        }
        public Operator(Guid id, string username, string name) {
            Id = id;
            Username = username;
        }
    }
}
