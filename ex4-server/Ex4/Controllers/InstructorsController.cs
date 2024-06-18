using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorsController : ControllerBase
    {
        Instructor instructor = new Instructor();
        // GET: api/<InstructorsController>
        [HttpGet]
        public IEnumerable<Instructor> Get()
        {
            return instructor.Read();
        }

        // GET api/Instructors/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                string name = instructor.instructorNameById(id);
                if (string.IsNullOrEmpty(name))
                {
                    return NotFound(new { message = "Instructor not found" });
                }
                return Ok(new { name });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Get courses by instructor id
        [HttpGet("GetCoursesByInstructorId/{instructorId}")]
        public IActionResult GetByInstructorId(int instructorId)
        {
            try
            {
                List<Course> courses = Course.GetCoursesByInstructor(instructorId);

                if (courses == null)
                {
                    return NotFound(new { message = "No courses found for this instructor." });
                }
                else
                {
                    return Ok(courses);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // PUT api/<InstructorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InstructorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
