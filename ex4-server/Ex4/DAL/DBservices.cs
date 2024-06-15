using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebApplication1.BL;

public class DBservices
{
    private readonly string connectionString;

    public DBservices()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        connectionString = configuration.GetConnectionString("myProjDB");
    }

    private SqlConnection Connect()
    {
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        return con;
    }

    // Method to get all courses
    public List<Course> GetAllCourses()
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("GetAllCourses", con)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = cmd.ExecuteReader();
            List<Course> courses = new List<Course>();

            while (reader.Read())
            {
                Course course = new Course
                {
                    Id = (int)reader["CourseID"],
                    Title = reader["Title"].ToString(),
                    Url = reader["URL"].ToString(),
                    Rating = (double)reader["Rating"],
                    NumberOfReviews = (int)reader["NumberOfReviews"],
                    InstructorId = (int)reader["InstructorID"],
                    ImageReference = reader["ImageReference"].ToString(),
                    Duration = reader["Duration"].ToString(),
                    LastUpdate = (DateTime)reader["LastUpdate"]
                };
                courses.Add(course);
            }
            return courses;
        }
    }

    // Method to add a course to a user
    public int AddCourseToUser(int userId, int courseId)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("SP_AddCourseToUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@CourseId", courseId);
            return cmd.ExecuteNonQuery();
        }
    }
    // Method to delete a course from a user
    public int DeleteCourseFromUser(int userId, int courseId)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("SP_DeleteCourseFromUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@CourseId", courseId);
            return cmd.ExecuteNonQuery();
        }
    }

    // Method to get courses by duration range for a user
    public List<Course> GetByDurationRangeForUser(int userId, double fromDuration, double toDuration)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("SP_GetCoursesByDurationRangeForUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@FromDuration", fromDuration);
            cmd.Parameters.AddWithValue("@ToDuration", toDuration);

            SqlDataReader reader = cmd.ExecuteReader();
            List<Course> courses = new List<Course>();

            while (reader.Read())
            {
                Course course = new Course
                {
                    Id = (int)reader["CourseID"],
                    Title = reader["Title"].ToString(),
                    Url = reader["URL"].ToString(),
                    Rating = (double)reader["Rating"],
                    NumberOfReviews = (int)reader["NumberOfReviews"],
                    InstructorId = (int)reader["InstructorID"],
                    ImageReference = reader["ImageReference"].ToString(),
                    Duration = reader["Duration"].ToString(),
                    LastUpdate = (DateTime)reader["LastUpdate"]
                };
                courses.Add(course);
            }
            return courses;
        }
    }

    // Method to get courses by rating range for a user
    public List<Course> GetByRatingRangeForUser(int userId, double fromRating, double toRating)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("SP_GetCoursesByRatingRangeForUser", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@FromRating", fromRating);
            cmd.Parameters.AddWithValue("@ToRating", toRating);

            SqlDataReader reader = cmd.ExecuteReader();
            List<Course> courses = new List<Course>();

            while (reader.Read())
            {
                Course course = new Course
                {
                    Id = (int)reader["CourseID"],
                    Title = reader["Title"].ToString(),
                    Url = reader["URL"].ToString(),
                    Rating = (double)reader["Rating"],
                    NumberOfReviews = (int)reader["NumberOfReviews"],
                    InstructorId = (int)reader["InstructorID"],
                    ImageReference = reader["ImageReference"].ToString(),
                    Duration = reader["Duration"].ToString(),
                    LastUpdate = (DateTime)reader["LastUpdate"]
                };
                courses.Add(course);
            }
            return courses;
        }
    }

    // Method to insert a course into the database
    public int InsertCourse(Course course)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("SP_CreateCourse", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@p_title", course.Title);
            cmd.Parameters.AddWithValue("@p_url", course.Url);
            cmd.Parameters.AddWithValue("@p_rating", course.Rating);
            cmd.Parameters.AddWithValue("@p_numberOfReviews", course.NumberOfReviews);
            cmd.Parameters.AddWithValue("@p_instructorID", course.InstructorId);
            cmd.Parameters.AddWithValue("@p_imageReference", course.ImageReference);
            cmd.Parameters.AddWithValue("@p_duration", course.Duration);
            cmd.Parameters.AddWithValue("@p_lastUpdate", course.LastUpdate);
            return cmd.ExecuteNonQuery();
        }
    }

    // Method to update a course
    public int UpdateCourse(Course course)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("EditCourse", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@p_courseID", course.Id);
            cmd.Parameters.AddWithValue("@p_title", course.Title);
            cmd.Parameters.AddWithValue("@p_url", course.Url);
            cmd.Parameters.AddWithValue("@p_rating", course.Rating);
            cmd.Parameters.AddWithValue("@p_numberOfReviews", course.NumberOfReviews);
            cmd.Parameters.AddWithValue("@p_instructorID", course.InstructorId);
            cmd.Parameters.AddWithValue("@p_imageReference", course.ImageReference);
            cmd.Parameters.AddWithValue("@p_duration", course.Duration);
            cmd.Parameters.AddWithValue("@p_lastUpdate", course.LastUpdate);
            return cmd.ExecuteNonQuery();
        }
    }
    public bool RegisterUser(User user)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmdCheck = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", con);
            cmdCheck.Parameters.AddWithValue("@Email", user.Email);
            int count = (int)cmdCheck.ExecuteScalar();

            if (count > 0)
            {
                return false;
            }

            SqlCommand cmdInsert = new SqlCommand("SP_Register", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmdInsert.Parameters.AddWithValue("@p_userName", user.Name);
            cmdInsert.Parameters.AddWithValue("@p_passwordHash", user.Password);
            cmdInsert.Parameters.AddWithValue("@p_email", user.Email);
            cmdInsert.Parameters.AddWithValue("@p_isAdmin", user.IsAdmin);
            cmdInsert.Parameters.AddWithValue("@p_isActive", user.IsActive);
            return cmdInsert.ExecuteNonQuery() > 0;
        }
    }
    // Method to login a user
    public User Login(string userName, string passwordHash)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("Login", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@p_userName", userName);
            cmd.Parameters.AddWithValue("@p_passwordHash", passwordHash);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                User user = new User
                {
                    Id = (int)reader["UserID"],
                    Name = reader["UserName"].ToString(),
                    Email = reader["Email"].ToString(),
                    //Role = reader["Role"].ToString()
                };
                return user;
            }
            else
            {
                return null;
            }
        }
    }


    // Method to insert an instructor into the database
    public int InsertInstructor(Instructor instructor)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("SP_CreateInstructor", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@Name", instructor.Name);
            cmd.Parameters.AddWithValue("@Title", instructor.Title);
            cmd.Parameters.AddWithValue("@JobTitle", instructor.JobTitle);
            cmd.Parameters.AddWithValue("@Image", instructor.Image);
            return cmd.ExecuteNonQuery();
        }
    }

   
    public User GetUser(int userId)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserID = @UserId", con);
            cmd.Parameters.AddWithValue("@UserId", userId);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User
                    {
                        Id = (int)reader["UserID"],
                        Name = (string)reader["UserName"],
                        Email = (string)reader["Email"],
                        Password = (string)reader["Password"],
                       /* Role = (string)reader["Role"*/]
                    };
                }
            }
        }
        return null;
    }

    // Method to get courses for a specific user
    public List<Course> GetUsersCourses(int userId)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("GetUsersCourses", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@UserId", userId);

            SqlDataReader reader = cmd.ExecuteReader();
            List<Course> courses = new List<Course>();

            while (reader.Read())
            {
                Course course = new Course
                {
                    Id = (int)reader["CourseID"],
                    Title = reader["Title"].ToString(),
                    Url = reader["URL"].ToString(),
                    Rating = (double)reader["Rating"],
                    NumberOfReviews = (int)reader["NumberOfReviews"],
                    InstructorId = (int)reader["InstructorID"],
                    ImageReference = reader["ImageReference"].ToString(),
                    Duration = reader["Duration"].ToString(),
                    LastUpdate = (DateTime)reader["LastUpdate"]
                };
                courses.Add(course);
            }
            return courses;
        }
    }


    // Method to get courses by instructor ID
    public List<Course> GetCoursesByInstructor(int instructorId)
    {
        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("GetCoursesByInstructor", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Parameters.AddWithValue("@p_instructorID", instructorId);

            SqlDataReader reader = cmd.ExecuteReader();
            List<Course> courses = new List<Course>();

            while (reader.Read())
            {
                Course course = new Course
                {
                    Id = (int)reader["CourseID"],
                    Title = reader["Title"].ToString(),
                    Url = reader["URL"].ToString(),
                    Rating = (double)reader["Rating"],
                    NumberOfReviews = (int)reader["NumberOfReviews"],
                    InstructorId = (int)reader["InstructorID"],
                    ImageReference = reader["ImageReference"].ToString(),
                    Duration = reader["Duration"].ToString(),
                    LastUpdate = (DateTime)reader["LastUpdate"]
                };
                courses.Add(course);
            }
            return courses;
        }
    }

    public List<User> GetUsers()
    {
        List<User> users = new List<User>();

        using (SqlConnection con = Connect())
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)reader["UserID"],
                        Name = (string)reader["UserName"],
                        Email = (string)reader["Email"],
                        Password = (string)reader["PasswordHash"],
                        IsAdmin = (string)reader["Role"] == "admin",
                        IsActive = true
                    });
                }
            }
        }

        return users;
    }

}
