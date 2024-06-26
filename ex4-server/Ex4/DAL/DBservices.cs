
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebApplication1.BL;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Reflection.PortableExecutable;


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
                course.IsActive = Convert.ToBoolean(reader["isActive"]);

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




    public List<Course> GetCoursesOfUser(int userId)
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

        cmd = CreateCommandWithStoredProcedureGetCoursesFromUser("SP_GetCoursesFromUser", con, userId);
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
                course.IsActive = Convert.ToBoolean(reader["isActive"]);

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

    private SqlCommand CreateCommandWithStoredProcedureGetCoursesFromUser(String spName, SqlConnection con, int userId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@UserId", userId);

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

        cmd.Parameters.AddWithValue("@p_isActive", course.IsActive);

        return cmd;
    }

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

        cmd = CreateCommandWithStoredProcedureAddCourseToUser("SP_AddCourseToUser", con, userId, course);             // create the command

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

    private SqlCommand CreateCommandWithStoredProcedureAddCourseToUser(String spName, SqlConnection con, int userId, Course course)
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
                course.IsActive = Convert.ToBoolean(reader["isActive"]);

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
                course.IsActive = Convert.ToBoolean(reader["isActive"]);

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

        if (spName == "SP_GetCoursesByDurationRangeForUser")
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
    public int EditCourse(int id, Course course)
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

        cmd = CreateCommandWithStoredProcedureEditCourse("SP_EditCourse", con, id, course);             // create the command

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

    private SqlCommand CreateCommandWithStoredProcedureEditCourse(String spName, SqlConnection con, int id, Course course)
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

        if (string.IsNullOrEmpty(course.ImageReference))
        {
            course.ImageReference = "https://www.clio.com/wp-content/uploads/2024/03/Journal-Entry-Accounting-1-750x422.png";
        }
        cmd.Parameters.AddWithValue("@p_imageReference", course.ImageReference);

        cmd.Parameters.AddWithValue("@p_isActive", course.IsActive);

        return cmd;
    }

    public int Registration(User user)
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

    public User Login(Login login)
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
        cmd = CreateCommandWithStoredProcedureLogin("SP_Login", con, login);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            if (reader.Read())
            {
                User user = new User();
                user.Id = Convert.ToInt32(reader["id"]);
                user.Name = reader["name"].ToString();
                user.Email = reader["email"].ToString();
                user.Password = reader["password"].ToString();
                user.IsAdmin = Convert.ToBoolean(reader["isAdmin"]);
                user.IsActive = Convert.ToBoolean(reader["isActive"]);
                return user;
            }
            return null;
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

    private SqlCommand CreateCommandWithStoredProcedureLogin(String spName, SqlConnection con, Login login)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_userEmail", login.Email);

        cmd.Parameters.AddWithValue("@p_passwordHash", login.Password);

        return cmd;
    }
    public string InstructorName(int InstructorId)
    {
        SqlConnection con;
        SqlCommand cmd;
        string instructorName = "";
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
        cmd = CreateCommandWithStoredProcedureGetInstructorsNameById("SP_GetInstructorNameById", con, InstructorId);
        try
        {

            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            while (reader.Read())
            {
                instructorName = reader["displayName"].ToString();
            }
            return instructorName;
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

    private SqlCommand CreateCommandWithStoredProcedureGetInstructorsNameById(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_id", id);

        return cmd;
    }

    public List<Instructor> ReadInstructors()
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
        cmd = CreateCommandWithStoredProcedureGetInstructors("SP_GetInstructors", con);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Instructor> instructors = new List<Instructor>();
            while (reader.Read())
            {
                Instructor instructor = new Instructor();
                instructor.Id = Convert.ToInt32(reader["id"]);
                instructor.Title = reader["title"].ToString();
                instructor.Name = reader["displayName"].ToString();
                instructor.Image = reader["image"].ToString();
                instructor.JobTitle = reader["job_title"].ToString();

                instructors.Add(instructor);
            }
            return instructors;
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

    private SqlCommand CreateCommandWithStoredProcedureGetInstructors(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        return cmd;
    }

    // Method to get courses by instructor
    public List<Course> GetCoursesByInstructor(int instructorId)
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
        // use a stored predures "SP_GetCoursesByInstructor" to get all the courses of that instructor
        cmd = CreateCommandWithStoredProcedureGetCoursesByInstructor("SP_GetCoursesByInstructor ", con, instructorId);
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
                course.IsActive = Convert.ToBoolean(reader["isActive"]);

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

    private SqlCommand CreateCommandWithStoredProcedureGetCoursesByInstructor(String spName, SqlConnection con, int instructorId)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_instructorID", instructorId);

        return cmd;
    }



    public User GetUser(int userId)
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

        cmd = CreateCommandWithStoredProcedureGetUser("SP_GetUser ", con, userId);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            User user = new User();
            while (reader.Read())
            {

                user.Id = Convert.ToInt32(reader["id"]);
                user.Name = reader["name"].ToString();
                user.Email = reader["email"].ToString();
                user.Password = reader["password"].ToString();
                user.IsAdmin = Convert.ToBoolean(reader["isAdmin"]);
                user.IsActive = Convert.ToBoolean(reader["isActive"]);
            }
            return user;
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


    private SqlCommand CreateCommandWithStoredProcedureGetUser(String spName, SqlConnection con, int userId)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_id", userId);

        return cmd;
    }

    public List<Object> GetTopFiveCourses()
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
            throw ex;
        }

        // Use the stored procedure "SP_GetTop5Courses" to get the top 5 courses
        cmd = CreateCommandWithStoredProcedure("SP_GetTop5Courses", con);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection); // execute the command
            List<Object> topCourses = new List<Object>();
            while (reader.Read())
            {
               topCourses.Add( new {
                    id = Convert.ToInt32(reader["id"]),
                    title = reader["name"].ToString(),
                    rating = Convert.ToDouble(reader["rating"]),
                    numOfRegisters = Convert.ToInt32(reader["NumberOfUsers"]),
                    image = reader["image"].ToString()
               });
            }
            return topCourses;
        }
        catch (Exception ex)
        {
            // write to log
            throw ex;
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

    private SqlCommand CreateCommandWithStoredProcedure(string spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand
        {
            Connection = con,              // assign the connection to the command object
            CommandText = spName,          // can be Select, Insert, Update, Delete 
            CommandTimeout = 10,           // Time to wait for the execution, the default is 30 seconds
            CommandType = CommandType.StoredProcedure // the type of the command, can also be text
        };
        return cmd;
    }

    public int changeActiveStatus(int courseId)
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

        cmd = CreateCommandWithStoredProcedureChangeCourseStatus("SP_ChangeCourseStatus", con, courseId);             // create the command
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

    private SqlCommand CreateCommandWithStoredProcedureChangeCourseStatus(String spName, SqlConnection con, int courseId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_CourseId", courseId);

        return cmd;
    }

    public int DeleteCourse(int courseId)
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

        cmd = CreateCommandWithStoredProcedureDeleteCourse("SP_DeleteCourse", con, courseId);             // create the command
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

    private SqlCommand CreateCommandWithStoredProcedureDeleteCourse(String spName, SqlConnection con, int courseId)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        cmd.Parameters.AddWithValue("@p_CourseId", courseId);

        return cmd;
    }

}
