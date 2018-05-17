using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronwoodLibrary
{
    public class Courses
    {
        public int courseID = -1;
        public string CourseName = "";
        public string CourseTitle = "";
        public int CourseCredit = -1;
        public int DepartmentID = -1;

        public static List<String> GetAllCourseName(out string status )
        {
            status = "No course found.";

            List<String> courseList = null;

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = Data.con;

            cmd.CommandText = "Select CourseName from UniversityCourse";

            try
            {
                Data.con.Open();

                System.Data.SqlClient.SqlDataReader records = cmd.ExecuteReader(); 

                if (records.Read())
                {
                    courseList = new List<String>();

                    while (records.Read())
                    {
                        courseList.Add(records[0].ToString());
                    }

                    status = "Course Found";
                }
            }
            catch(Exception ex)
            {
                status = ex.Message;
            }
            finally
            {
                if (Data.con.State == System.Data.ConnectionState.Open)
                {
                    Data.con.Close();
                }
            }
            return courseList;
        }
        
        public static Courses GetCourse(int id, out string status)
        {
            status = "Unable to find course";

            Courses courses = null;

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = Data.con;

            cmd.CommandText = "Select * FROM UniversityCourse WHERE CourseID = " + id;

            try
            {
                Data.con.Open();

                System.Data.SqlClient.SqlDataReader records = cmd.ExecuteReader(System.Data.CommandBehavior.SingleRow); //https://msdn.microsoft.com/en-us/library/kddf8ah6.aspx

                if (records.Read())
                {
                    courses = new Courses();

                    courses.courseID = Convert.ToInt32(records[0]);
                    courses.CourseName = records[1].ToString();
                    courses.CourseTitle = records[2].ToString();
                    courses.CourseCredit = Convert.ToInt16(records[3]);
                    courses.DepartmentID = Convert.ToInt32(records[4]);

                    status = "Course Found";
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            finally
            {
                if (Data.con.State == System.Data.ConnectionState.Open)
                {
                    Data.con.Close();
                }
            }
            return courses;
        }

        public static bool UpdateCourse(Courses courses, out string status )
        {
            status = "Unable to update/insert Course";

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

            cmd.Connection = Data.con;

            string sqlStatement = (courses.courseID == -1) ? "Insert UniversityCourse (CourseName, CourseTitle, CourseCredit, DepartmentID) " +
                                                        "Values (@CourseName, @CourseTitle, @CourseCredit, @DepartmentID"
                                                        : "Update UniversityCourse SET " +
                                                        "CourseName = @CourseName" + 
                                                        "CourseTitle = @CourseTitle" +
                                                        "CourseCredit = @CourseCredit" +
                                                        "DepartmentID = @DepartmentID" +
                                                        "Where CourseID = @ID";
            cmd.CommandText = sqlStatement;

            if (courses.courseID > 0)
            cmd.Parameters.AddWithValue("@ID", courses.courseID);
            cmd.Parameters.AddWithValue("@CourseName", courses.CourseName);
            cmd.Parameters.AddWithValue("@CourseTitle", courses.CourseTitle);
            cmd.Parameters.AddWithValue("@CourseCredit", courses.CourseCredit);
            cmd.Parameters.AddWithValue("@DepartmentID", courses.DepartmentID);

            try
            {
                Data.con.Open();

                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    status = "Record Successfully insert/updated";
                    return true;
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            finally
            {
                if (Data.con.State == System.Data.ConnectionState.Open)
                {
                    Data.con.Close();
                }
            }
            return false;

        }
        public static bool DeleteCourse(Courses courses, out string status)
        {
            status = "Unable to delete Course";

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("DeleteCourse", Data.con);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", courses.courseID);

            try
            {
                Data.con.Open();

                int count = cmd.ExecuteNonQuery();

                if (count > 0)
                {
                    status = "Record Deleted";
                    return true;
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            finally
            {
                if (Data.con.State == System.Data.ConnectionState.Open)
                {
                    Data.con.Close();
                }
            }
            return false;
        }

    }
}
