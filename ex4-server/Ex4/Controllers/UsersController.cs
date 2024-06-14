
using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        User user = new User();
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return WebApplication1.BL.User.GetUsers();
        }
        //GET api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var ThisUser = user.GetUser(id);
            if (ThisUser == null)
            {
                return NotFound();
            }
            return ThisUser;
        }

        //POST api/<UsersController>
        [HttpPost]
        public bool Post([FromBody] User user)
        {
            return user.Registration();
        }

        // POST api/<UsersController>/login
        [HttpPost("login")]
        public User Login([FromBody] Login value)
        {
            return WebApplication1.BL.User.Login(value);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}
