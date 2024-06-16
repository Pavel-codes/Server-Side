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
        public IEnumerable<User> Get()
        {
            return user.GetUsers();
        }
        //GET api/Users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            var user = WebApplication1.BL.User.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        //POST api/<UsersController>
        [HttpPost]
        public bool Post([FromBody] User value)
        {
            
            bool result = user.registration(value);
            if(result)
                return true;         
            return false;          

        }

        // POST api/<UsersController>/login
        [HttpPost("login")]
        public User Login([FromBody] Login value)
        {
            User nUser = user.login(value);
            if (nUser != null)
            {
                return nUser;
            }
            return null;
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

    }
}