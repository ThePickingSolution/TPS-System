using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Administration.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }


        public bool? Active { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }


        public UserEntity() {

        }

        public void CopyToEdit(UserEntity user) {
            this.Username = user.Username;
            this.Name = user.Name;
            this.Active = user.Active;
        }

        public void Create() {
            Id = new Guid();
            this.Active = true;
            this.CreatedOn = DateTime.Now;
        }
        public void Delete() {
            this.Active = null;
            this.DeletedOn = DateTime.Now;
        }


    }
}
