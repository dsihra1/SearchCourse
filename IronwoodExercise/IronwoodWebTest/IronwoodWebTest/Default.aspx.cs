using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IronwoodLibrary;

namespace IronwoodWebTest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string status;
            Courses courses = Courses.GetCourse(6, out status);   //When searching for a record, enter an ID from 1 to 12 to find the record 
            if (courses != null)
                this.Label1.Text = courses.CourseName + "-" + courses.CourseTitle;
            this.Label2.Text = status;




        }
    }
}