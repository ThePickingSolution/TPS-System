using Business.Domain.Administration.Exceptions;
using Business.Domain.Administration.People;
using Microsoft.AspNetCore.Mvc;
using Repository.Administration.Interface.People;
using System;

namespace API.Administration.Controllers {
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserRepository repository;

        public UserController(IUserRepository _repository) {
            repository = _repository;
        }


        [HttpGet]
        public IActionResult Get(Guid? id = null, string username = null) {
            if (id.HasValue && !string.IsNullOrEmpty(username)) {
                return BadRequest(@"Inform only one param");
            }

            User user = null;
            if (id.HasValue) {
                user = repository.Get(id.Value);
                return user != null ? Ok(user) : NoContent();
            } else if (!string.IsNullOrEmpty(username)) {
                user = repository.Get(username);
                return user != null ? Ok(user) : NoContent();
            }
            return BadRequest(
@"Inform one param
?id:Guid 
?username:string");

        }
    
        [HttpPost]
        public IActionResult Create(User user) {
            try {
                return Ok(this.repository.Create(user));
            } catch (AdministrationException ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Edit(User user) {
            try {
                return Ok(this.repository.Update(user));
            } catch (AdministrationException ex) {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public IActionResult Delete(Guid id) {
            try {
                this.repository.Delete(id);
                return Ok($"User {id} deleted");
            } catch (AdministrationException ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
