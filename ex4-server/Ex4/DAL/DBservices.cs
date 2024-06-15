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




}
