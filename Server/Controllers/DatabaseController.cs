using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.Models;

namespace Server.Controllers
{
    [Route("db/")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly DatabaseServices _databaseService;

        public DatabaseController(DatabaseServices databaseServices)
        {
            _databaseService = databaseServices;
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<User>> GetAll() => _databaseService.Get();

        [HttpGet]
        [Route("Get")]
        public ActionResult<User> Get(int id)
        {
            var user = _databaseService.Get(id);

            if(user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet]
        [Route("GetHighest")]
        public ActionResult<User> GetHighest()
        {
            var user = _databaseService.GetHighest();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult<User> Create(User user)
        {
            _databaseService.Create(user);

            return Ok();
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update(int id, User userIn)
        {
            var user = _databaseService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _databaseService.Update(id, userIn);

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            var user = _databaseService.Get(id);

            if (user == null)
            {
                return NotFound();
            }

            _databaseService.Delete(user.userId);

            return NoContent();
        }

    }
}
