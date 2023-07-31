using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrunchTime_Web;
using Microsoft.Data.SqlClient;
using StudyTime;
using CrunchTime_Web.Pages.ModuleCRUD;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Threading;

namespace CrunchTime_Web.Pages.ScheduleCRUD
{

    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        //variable to store current user id
        public string currentUser;

        //variable to store self-study hours found in database
        public double foundSelfStudy;

        //variable to store remaining self-study hours
        public double remainingStudyHours;

        //variable to hold drop-down list's selection
        public string daySelection;

        public CreateModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ModuleModelID"] = new SelectList(_context.ModuleModel, "ModuleModelID", "ModuleName");
            return Page();
        }

        [BindProperty]
        public ScheduleModel ScheduleModel { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ScheduleModel.Add(ScheduleModel);
            await _context.SaveChangesAsync();

            //calling method to save user id
            SaveUserID();

            //calling method to find self study hours for module
            FindSelfStudyHours();

            //calling method to calculate remaining study hours
            CalculateStudyHours();

            //calling method to save remaining study hours
            SaveRemainingStudyHours();

            //calling method to save module weekday
            SaveWeekday();

            return RedirectToPage("./Index");
        }

        //method to save current user id
        public void SaveUserID()
        {

            //creating sql connection
            SqlConnection dbCon = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CrunchTime_WebData;Integrated Security=True");

            //creating sql command
            SqlCommand command = new SqlCommand();

            //specifying command type
            command.CommandType = System.Data.CommandType.Text;

            //query statement to update userID in table
            command.CommandText = "update [ScheduleModel] set UserID = @schOwner where ScheduleModelID = @thisID";

            //finding user identity
            var identity = (ClaimsIdentity)User.Identity;
            var findUser = identity.FindFirst(ClaimTypes.NameIdentifier);

            //saving user id
            currentUser = findUser.Value;

            //setting command connection to database
            command.Connection = dbCon;

            //opening database connection
            dbCon.Open();

            //adding parameters to command
            command.Parameters.AddWithValue("@schOwner", currentUser);
            command.Parameters.AddWithValue("@thisID", ScheduleModel.ScheduleModelID);

            //executing command
            command.ExecuteNonQuery();

            //closing database connection
            dbCon.Close();

        }

        //method to find this module's self study hours
        public void FindSelfStudyHours()
        {
            //creating sql connection
            SqlConnection dbCon = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CrunchTime_WebData;Integrated Security=True");

            //creating sql command
            SqlCommand findHours = new SqlCommand("select [SelfStudyHours] from [ModuleModel] where [ModuleModelID] = @mID", dbCon);

            //opening connection to database
            dbCon.Open();

            //adding parameters to command
            findHours.Parameters.AddWithValue("@mID", ScheduleModel.ModuleModelID);

            //executing reader
            SqlDataReader read = findHours.ExecuteReader();

            //running if reader finds a result
            if (read.Read() == true)
            {
                //saving data
                foundSelfStudy = (double)read[0];

            }

            //closing reader
            read.Close();

            //closing database connection
            dbCon.Close();

        }

        //method to calculate remaining study hours
        public void CalculateStudyHours()
        {
            //class library object reference
            StudyHour study = new StudyHour();

            //calculating self study hours
            remainingStudyHours = study.RemainingStudyHours(foundSelfStudy, ScheduleModel.HoursStudied);

        }

        //method to save remaining study hours
        public void SaveRemainingStudyHours()
        {

            //creating sql connection
            SqlConnection dbCon = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CrunchTime_WebData;Integrated Security=True");

            //creating sql command
            SqlCommand command = new SqlCommand();

            //specifying command type
            command.CommandType = System.Data.CommandType.Text;

            //query statement to update self study hours in table
            command.CommandText = "update [ScheduleModel] set RemainingStudyHours = @hours where ScheduleModelID = @sID";


            //setting command connection to database
            command.Connection = dbCon;

            //opening database connection
            dbCon.Open();

            //adding parameters to command
            command.Parameters.AddWithValue("@hours", remainingStudyHours);
            command.Parameters.AddWithValue("@sID", ScheduleModel.ScheduleModelID);

            //executing command
            command.ExecuteNonQuery();

            //closing database connection
            dbCon.Close();

        }

        //method to save module weekday
        public void SaveWeekday()
        {

            //creating sql connection
            SqlConnection dbCon = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CrunchTime_WebData;Integrated Security=True");

            //creating sql command
            SqlCommand command = new SqlCommand();

            //specifying command type
            command.CommandType = System.Data.CommandType.Text;

            //query statement to update module weekday in table
            command.CommandText = "update [ScheduleModel] set ModuleWeekDay = @day where ScheduleModelID = @sID";


            //setting command connection to database
            command.Connection = dbCon;

            //opening database connection
            dbCon.Open();

            //saving drop-down list selection
            daySelection = Request.Form["weekday"];

            //adding parameters to command
            command.Parameters.AddWithValue("@day", daySelection);
            command.Parameters.AddWithValue("@sID", ScheduleModel.ScheduleModelID);

            //executing command
            command.ExecuteNonQuery();

            //closing database connection
            dbCon.Close();

        }
    }
}
//-----------------------------------------------------END-OF-CLASS---------------------------------------------------------//