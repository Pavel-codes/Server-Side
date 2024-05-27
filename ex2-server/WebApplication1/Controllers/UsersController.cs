﻿using Microsoft.AspNetCore.Mvc;
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
            return user.getUsers();
        }

        //// GET api/<UsersController>/5
        //[HttpGet("{id}")]
        //public bool GetInstructorId(User user)
        //{
        //    return user.getInstructorId;
        //}

        // POST api/<UsersController>
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

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
