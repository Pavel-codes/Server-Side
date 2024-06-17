
using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly DBservices dbservices = new DBservices();

        Course course = new Course();

        // GET: api/Courses
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return new Course().Read();
        }

        // GET api/<CoursesController>/5
        [HttpGet("{title}")]
        public Course Get(string title)
        {
            return course.GetCourseByTitle(title);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var courseById = course.Id;
            if (course == null)
            {
                return NotFound("Course not found");
            }
            return Ok(course);
        }

        // GET: api/Courses/user/{userId}
        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<Course>> GetUserCourses(int userId)
        {
            var userById = dbservices.GetUser(userId);
            if (userById == null)
            {
                return NotFound();
            }
            var userCourses = userById.GetCourses();
            return Ok(userCourses);
        }

        // POST: api/Courses/NewCourse
        [HttpPost("NewCourse")]
        public IActionResult PostNewCourse([FromBody] Course value)
        {
            bool result = value.InsertNewCourse();
            if (!result)
            {
                return NotFound(new { message = "Course could not be inserted." });
            }
            return Ok(new { message = "Course inserted successfully." });
        }

        // POST api/<CoursesController>
        [HttpPost("NewCourse")]
        public IActionResult PostNewCourse([FromBody] Course value)
        {
            bool result = value.InsertNewCourse();
            if (!result)
            {
                return NotFound("Course could not be inserted.");
            }
            return Ok("Course inserted successfully.");
        }


        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course updatedCourse)
        {
            try
            {
                bool result = new Course().UpdateCourse(id, updatedCourse);
                if (result)
                {
                    return Ok(new { message = "Course updated." });
                }
                else
                {
                    return NotFound(new { message = "Course could not be updated." });
                }
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error with the exception message
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("addCourseToUser/{userId}")]
        public IActionResult AddCourseToUser(int userId, [FromBody] Course course)
        {
            if (Course.AddCourseToUser(userId, course))
            {
                return Ok(new { message = "Course added to user successfully" });
            }
            return BadRequest(new { message = "Failed to add course to user" });
        }

        [HttpDelete("deleteByCourseFromUserList/{userId}")]
        public IActionResult DeleteByCourseFromUserList(int userId, [FromQuery] int coursid)
        {
            try
            {
                if (Course.DeleteCourse(userId, coursid))
                {
                    return Ok(new { message = "Course deleted from user successfully" });
                }
                return BadRequest(new { message = "Failed to delete course from user list" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("searchByDurationForUser/{userId}")]
        public IActionResult GetByDurationRangeForUser(int userId, [FromQuery] double fromDuration, [FromQuery] double toDuration)
        {
            try
            {
                List<Course> courses = Course.GetByDurationRangeForUser(userId, fromDuration, toDuration);

                if (courses.Any())
                {
                    return Ok(courses);
                }
                return NotFound(new { message = "No courses found for the specified duration range and user." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("searchByRatingForUser/{userId}")]
        public IActionResult GetByRatingRangeForUser(int userId, [FromQuery] double fromRating, [FromQuery] double toRating)
        {
            try
            {
                List<Course> courses = Course.GetByRatingRangeForUser(userId, fromRating, toRating);

                if (courses.Any())
                {
                    return Ok(courses);
                }
                return NotFound(new { message = "No courses found for the specified rating range and user." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }


}
