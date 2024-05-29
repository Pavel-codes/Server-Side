using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        Course course = new Course();
        // GET: api/<CoursesController>
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return course.Read();
        }

        // GET api/<CoursesController>/5
        [HttpGet("{title}")]
        public Course Get(string title)
        {
            return course.getCourseByTitle(title);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var course = Course.CoursesList.FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound("Course not found");
            }
            return Ok(course);
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

        //POST api/<CoursesController>
        [HttpPost]
        public bool Post([FromBody] Course value)
        {
            return value.Insert();
        }


        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Course updatedCourse)
        {
            try
            {
                // Find the course with the given id
                Course courseToUpdate = course.Read().FirstOrDefault(c => c.Id == id);

                // If the course is not found, return NotFound
                if (courseToUpdate == null)
                {
                    return NotFound("Course not found");
                }

                // Update the course properties with the new data
                courseToUpdate.Title = updatedCourse.Title;
                courseToUpdate.Url = updatedCourse.Url;
                courseToUpdate.Rating = updatedCourse.Rating;
                courseToUpdate.NumberOfReviews = updatedCourse.NumberOfReviews;
                courseToUpdate.InstructorsId = updatedCourse.InstructorsId;
                courseToUpdate.ImageReference = updatedCourse.ImageReference;
                courseToUpdate.Duration = updatedCourse.Duration;
                courseToUpdate.LastUpdate = updatedCourse.LastUpdate;

                // Return Ok with a success message
                return Ok("Course updated successfully");
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error with the exception message
                return StatusCode(500, ex.Message);
            }
        }

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

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id) // need to fix the exception
        {
            try
            {
                Course course = new Course();
                course.DeleteById(id);
                return Ok("Course Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Return 500 Internal Server Error with the exception message
            }
        }

        [HttpPost("addCourseToUser/{userId}")]
        public IActionResult AddCourseToUser(int userId, [FromBody] Course course)
        {
            if (Course.AddCourseToUser(userId, course))
            {
                return Ok("Course added to user successfully");
            }
            return BadRequest("Failed to add course to user");
        }

        [HttpGet("user/{userId}")] //
        public ActionResult<IEnumerable<Course>> GetUserCourses(int userId)
        {
            var user = WebApplication1.BL.User.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            var courses = user.GetCourses();
            return Ok(courses);
        }
    }
}