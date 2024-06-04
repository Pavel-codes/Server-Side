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
            return value.registration();
        }

        // POST api/<UsersController>/login
        [HttpPost("login")]
        public User Login([FromBody] Login value)
        {
            return WebApplication1.BL.User.login(value);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        
        //// DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id )
        //{
        //    try
        //    {
        //        if (user == null)
        //        {
        //            return BadRequest("User data is required.");
        //        }

        //        user.DeleteCourseById(id);
        //        return Ok("Course Deleted Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message); // Return 500 Internal Server Error with the exception message
        //    }
        //}

        [HttpGet("search")]
        public IEnumerable<Course> GetByDurationRange(double fromDuration, double toDuration)
        {
            Course courseByDuration = new Course();

            return courseByDuration.GetByDurationRange(fromDuration, toDuration);
        }

        [HttpGet("searchByRouting/fromRating/{fromRating}/toRating/{toRating}")]
        public IEnumerable<Course> GetByRatingRange(double fromRating, double toRating)
        {
            Course courseByRating = new Course();

            return courseByRating.GetByRatingRange(fromRating, toRating);
        }
    }
}
