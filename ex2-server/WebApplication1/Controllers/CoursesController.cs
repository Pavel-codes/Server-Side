﻿using Microsoft.AspNetCore.Mvc;
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

        // POST api/<CoursesController>
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
                if (string.IsNullOrEmpty(updatedCourse.ImageReference))
                {
                    updatedCourse.ImageReference = "https://www.clio.com/wp-content/uploads/2024/03/Journal-Entry-Accounting-1-750x422.png";
                }
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

        [HttpPost("addCourseToUser/{userId}")]
        public IActionResult AddCourseToUser(int userId, [FromBody] Course course)
        {
            if (Course.AddCourseToUser(userId, course))
            {
                return Ok("Course added to user successfully");
            }
            return BadRequest("Failed to add course to user");
        }

        [HttpDelete("deleteByCourseFromUserList/{userId}")]
        public IActionResult DeleteByCourseFromUserList(int userId, [FromQuery] int coursid)
        {
            try
            {
                if (Course.DeleteCourse(userId, coursid))
                {
                    return Ok("Course deleted from user successfully");
                }
                return BadRequest("Failed to delete course from user list");
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
                return NotFound("No courses found for the specified duration range and user.");
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
                return NotFound("No courses found for the specified rating range and user.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}