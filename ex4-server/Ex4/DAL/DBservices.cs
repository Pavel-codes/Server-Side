using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebApplication1.BL;
using System.Data.SqlClient;


/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }


    // Method to get all Courses
    public List<Course> ReadCourses()
    {

        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        // use a stored predures "spGetStudent" to get all the students from the student table
        cmd = CreateCommandWithStoredProcedureGetCourses("SP_GetCourses", con);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Course> courses = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.Id = Convert.ToInt32(reader["id"]);
                course.Title = reader["title"].ToString();
                course.Url = reader["url"].ToString();
                course.Rating = Convert.ToDouble(reader["rating"]);
                course.NumberOfReviews = Convert.ToInt32(reader["num_reviews"]);
                course.LastUpdate = reader["last_update_date"].ToString();
                course.Duration = reader["duration"].ToString();
                course.InstructorsId = Convert.ToInt32(reader["instructors_id"]);
                course.ImageReference = reader["image"].ToString();

                courses.Add(course);
            }
            return courses;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }


    }

    private SqlCommand CreateCommandWithStoredProcedureGetCourses(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // Insert New Course to the Corses table 
    //--------------------------------------------------------------------------------------------------
    public int InsertNewCourse(Course course)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureCreateCourse("SP_CreateCourse", con, course);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //---------------------------------------------------------------------------------
    // Create Course SqlCommand using a stored procedure
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureCreateCourse(String spName, SqlConnection con, Course course)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_title", course.Title);

        cmd.Parameters.AddWithValue("@p_url", course.Url);

        cmd.Parameters.AddWithValue("@p_rating", course.Rating);

        cmd.Parameters.AddWithValue("@p_numberOfReviews", course.NumberOfReviews);

        cmd.Parameters.AddWithValue("@p_lastUpdate", course.LastUpdate);

        cmd.Parameters.AddWithValue("@p_duration", course.Duration);

        cmd.Parameters.AddWithValue("@p_instructorId", course.InstructorsId);

        if (string.IsNullOrEmpty(course.ImageReference))
        {
            course.ImageReference = "https://www.clio.com/wp-content/uploads/2024/03/Journal-Entry-Accounting-1-750x422.png";
        }
        cmd.Parameters.AddWithValue("@p_imageReference", course.ImageReference);
       

        return cmd;
    }

    ////--------------------------------------------------------------------------------------------------
    //// This method update a student to the student table 
    ////--------------------------------------------------------------------------------------------------
    //public int Update(Student student)
    //{

    //    SqlConnection con;
    //    SqlCommand cmd;

    //    try
    //    {
    //        con = connect("myProjDB"); // create the connection
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }

    //    cmd = CreateCommandWithStoredProcedure("spUpdateStudent1", con,student);             // create the command

    //    try
    //    {
    //        int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //        return numEffected;
    //    }
    //    catch (Exception ex)
    //    {
    //        // write to log
    //        throw (ex);
    //    }

    //    finally
    //    {
    //        if (con != null)
    //        {
    //            // close the db connection
    //            con.Close();
    //        }
    //    }

    //}

    // Method to add a course to a user
    
    public int AddCourseToUser(int userId, Course course)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureAddCourseToUser("SP_AddCourseToUser", con,userId, course);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateCommandWithStoredProcedureAddCourseToUser(String spName, SqlConnection con,int userId, Course course)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        cmd.Parameters.AddWithValue("@CourseId", course.Id);

        return cmd;
    }

    public int DeleteCourseFromUser(int userId, int courseId)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureDeleteCourseFromUser("SP_DeleteCourseFromUser", con, userId, courseId);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateCommandWithStoredProcedureDeleteCourseFromUser(String spName, SqlConnection con, int userId, int courseId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

        cmd.Parameters.AddWithValue("@CourseId", courseId);

        return cmd;
    }

    // Method to get courses by duration range for a user
    public List<Course> GetByDurationRangeForUser(int userId, double fromDuration, double toDuration)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        // use a stored predures "spGetStudent" to get all the students from the student table
        cmd = CreateCommandWithStoredProcedureGetCoursesByRatingOrDurationRangeForUser("SP_GetCoursesByDurationRangeForUser", con, userId, fromDuration, toDuration);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Course> courses = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.Id = Convert.ToInt32(reader["id"]);
                course.Title = reader["title"].ToString();
                course.Url = reader["url"].ToString();
                course.Rating = Convert.ToDouble(reader["rating"]);
                course.NumberOfReviews = Convert.ToInt32(reader["num_reviews"]);
                course.LastUpdate = reader["last_update_date"].ToString();
                course.Duration = reader["duration"].ToString();
                course.InstructorsId = Convert.ToInt32(reader["instructors_id"]);
                course.ImageReference = reader["image"].ToString();

                courses.Add(course);
            }
            return courses;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Method to get courses by rating range for a user
    public List<Course> GetByRatingRangeForUser(int userId, double fromRating, double toRating)
    {
        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        // use a stored predures "spGetStudent" to get all the students from the student table
        cmd = CreateCommandWithStoredProcedureGetCoursesByRatingOrDurationRangeForUser("SP_GetCoursesByRatingRangeForUser", con, userId, fromRating, toRating);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Course> courses = new List<Course>();
            while (reader.Read())
            {
                Course course = new Course();
                course.Id = Convert.ToInt32(reader["id"]);
                course.Title = reader["title"].ToString();
                course.Url = reader["url"].ToString();
                course.Rating = Convert.ToDouble(reader["rating"]);
                course.NumberOfReviews = Convert.ToInt32(reader["num_reviews"]);
                course.LastUpdate = reader["last_update_date"].ToString();
                course.Duration = reader["duration"].ToString();
                course.InstructorsId = Convert.ToInt32(reader["instructors_id"]);
                course.ImageReference = reader["image"].ToString();

                courses.Add(course);
            }
            return courses;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    private SqlCommand CreateCommandWithStoredProcedureGetCoursesByRatingOrDurationRangeForUser(String spName, SqlConnection con, int userId, double from, double to)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        if(spName == "SP_GetCoursesByDurationRangeForUser")
        {
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@FromDuration", from);
            cmd.Parameters.AddWithValue("@ToDuration", to);
        }
        else
        {
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@FromRating", from);
            cmd.Parameters.AddWithValue("@ToRating", to);
        }    

        return cmd;
    }


    // update course
    public int EditCourse(int id,Course course)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureEditCourse("SP_EditCourse", con,id, course);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    private SqlCommand CreateCommandWithStoredProcedureEditCourse(String spName, SqlConnection con,int id, Course course)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_courseID", id);

        cmd.Parameters.AddWithValue("@p_title", course.Title);

        cmd.Parameters.AddWithValue("@p_url", course.Url);

        cmd.Parameters.AddWithValue("@p_lastUpdate", course.LastUpdate);

        cmd.Parameters.AddWithValue("@p_duration", course.Duration);

        cmd.Parameters.AddWithValue("@p_instructorId", course.InstructorsId);
       
        if (string.IsNullOrEmpty(course.ImageReference))
        {
            course.ImageReference = "https://www.clio.com/wp-content/uploads/2024/03/Journal-Entry-Accounting-1-750x422.png";
        }
        cmd.Parameters.AddWithValue("@p_imageReference", course.ImageReference);

        return cmd;
    }

    public int Registation(User user)
    {
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureRegistration("SP_Register", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    private SqlCommand CreateCommandWithStoredProcedureRegistration(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_userName", user.Name);

        cmd.Parameters.AddWithValue("@p_email", user.Email);

        cmd.Parameters.AddWithValue("@p_passwordHash", user.Password);

        cmd.Parameters.AddWithValue("@p_isAdmin", user.IsAdmin);

        cmd.Parameters.AddWithValue("@p_isActive", user.IsActive);

        return cmd;
    }

   
    //// Method to login a user
    //public User Login(string userName, string passwordHash)
    //{
    //    using (SqlConnection con = Connect())
    //    {
    //        SqlCommand cmd = new SqlCommand("Login", con)
    //        {
    //            CommandType = CommandType.StoredProcedure
    //        };
    //        cmd.Parameters.AddWithValue("@p_userName", userName);
    //        cmd.Parameters.AddWithValue("@p_passwordHash", passwordHash);

    //        SqlDataReader reader = cmd.ExecuteReader();

    //        if (reader.Read())
    //        {
    //            User user = new User
    //            {
    //                Id = (int)reader["UserID"],
    //                Name = reader["UserName"].ToString(),
    //                Email = reader["Email"].ToString(),
    //                //Role = reader["Role"].ToString()
    //            };
    //            return user;
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //}


    //// Method to insert an instructor into the database
    //public int InsertInstructor(Instructor instructor)
    //{
    //    using (SqlConnection con = Connect())
    //    {
    //        SqlCommand cmd = new SqlCommand("SP_CreateInstructor", con)
    //        {
    //            CommandType = CommandType.StoredProcedure
    //        };
    //        cmd.Parameters.AddWithValue("@Name", instructor.Name);
    //        cmd.Parameters.AddWithValue("@Title", instructor.Title);
    //        cmd.Parameters.AddWithValue("@JobTitle", instructor.JobTitle);
    //        cmd.Parameters.AddWithValue("@Image", instructor.Image);
    //        return cmd.ExecuteNonQuery();
    //    }
    //}


    //public User GetUser(int userId)
    //{
    //    using (SqlConnection con = Connect())
    //    {
    //        SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE UserID = @UserId", con);
    //        cmd.Parameters.AddWithValue("@UserId", userId);

    //        using (SqlDataReader reader = cmd.ExecuteReader())
    //        {
    //            if (reader.Read())
    //            {
    //                return new User
    //                {
    //                    Id = (int)reader["UserID"],
    //                    Name = (string)reader["UserName"],
    //                    Email = (string)reader["Email"],
    //                    Password = (string)reader["Password"],
    //                   /* Role = (string)reader["Role"*/]
    //                };
    //            }
    //        }
    //    }
    //    return null;
    //}

    //// Method to get courses for a specific user
    //public List<Course> GetUsersCourses(int userId)
    //{
    //    using (SqlConnection con = Connect())
    //    {
    //        SqlCommand cmd = new SqlCommand("GetUsersCourses", con)
    //        {
    //            CommandType = CommandType.StoredProcedure
    //        };
    //        cmd.Parameters.AddWithValue("@UserId", userId);

    //        SqlDataReader reader = cmd.ExecuteReader();
    //        List<Course> courses = new List<Course>();

    //        while (reader.Read())
    //        {
    //            Course course = new Course
    //            {
    //                Id = (int)reader["CourseID"],
    //                Title = reader["Title"].ToString(),
    //                Url = reader["URL"].ToString(),
    //                Rating = (double)reader["Rating"],
    //                NumberOfReviews = (int)reader["NumberOfReviews"],
    //                InstructorId = (int)reader["InstructorID"],
    //                ImageReference = reader["ImageReference"].ToString(),
    //                Duration = reader["Duration"].ToString(),
    //                LastUpdate = (DateTime)reader["LastUpdate"]
    //            };
    //            courses.Add(course);
    //        }
    //        return courses;
    //    }
    //}


    //// Method to get courses by instructor ID
    //public List<Course> GetCoursesByInstructor(int instructorId)
    //{
    //    using (SqlConnection con = Connect())
    //    {
    //        SqlCommand cmd = new SqlCommand("GetCoursesByInstructor", con)
    //        {
    //            CommandType = CommandType.StoredProcedure
    //        };
    //        cmd.Parameters.AddWithValue("@p_instructorID", instructorId);

    //        SqlDataReader reader = cmd.ExecuteReader();
    //        List<Course> courses = new List<Course>();

    //        while (reader.Read())
    //        {
    //            Course course = new Course
    //            {
    //                Id = (int)reader["CourseID"],
    //                Title = reader["Title"].ToString(),
    //                Url = reader["URL"].ToString(),
    //                Rating = (double)reader["Rating"],
    //                NumberOfReviews = (int)reader["NumberOfReviews"],
    //                InstructorId = (int)reader["InstructorID"],
    //                ImageReference = reader["ImageReference"].ToString(),
    //                Duration = reader["Duration"].ToString(),
    //                LastUpdate = (DateTime)reader["LastUpdate"]
    //            };
    //            courses.Add(course);
    //        }
    //        return courses;
    //    }
    //}

    //public List<User> GetUsers()
    //{
    //    List<User> users = new List<User>();

    //    using (SqlConnection con = Connect())
    //    {
    //        SqlCommand cmd = new SqlCommand("SELECT * FROM Users", con);

    //        using (SqlDataReader reader = cmd.ExecuteReader())
    //        {
    //            while (reader.Read())
    //            {
    //                users.Add(new User
    //                {
    //                    Id = (int)reader["UserID"],
    //                    Name = (string)reader["UserName"],
    //                    Email = (string)reader["Email"],
    //                    Password = (string)reader["PasswordHash"],
    //                    IsAdmin = (string)reader["Role"] == "admin",
    //                    IsActive = true
    //                });
    //            }
    //        }
    //    }

    //    return users;
    //}


}
