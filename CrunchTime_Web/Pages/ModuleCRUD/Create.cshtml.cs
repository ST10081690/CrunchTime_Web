using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrunchTime_Web;
using Microsoft.Data.SqlClient;
using System.Security.Claims;
using CrunchTime_Web.Pages.SemesterCRUD;
using StudyTime;
using CrunchTime_Web.Pages.ScheduleCRUD;
using Microsoft.AspNetCore.Authorization;

namespace CrunchTime_Web.Pages.ModuleCRUD
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        //variable to store current user id
        public string currentUser;

        //variable to store calculated self-study hours
        public double selfStudy;

        //variable to store number of weeks for  respective semester
        public double foundSemesterWeeks;

        public CreateModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["SemesterModelID"] = new SelectList(_context.SemesterModel, "SemesterModelID", "SemesterName");
            return Page();
        }

        [BindProperty]
        public ModuleModel ModuleModel { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ModuleModel.Add(ModuleModel);
            await _context.SaveChangesAsync();

            //calling method to save user id
            SaveUserID();

            //calling method fetch number of weeks in semester
            GetWeeks();

            //calling method to calculate self-study hours for module
            CalculateSelfStudyHours();

            //calling method to save self-study hours to database
            SaveStudyHours();

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
            command.CommandText = "update [ModuleModel] set UserID = @modOwner where ModuleModelID = @thisID";

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
            command.Parameters.AddWithValue("@modOwner", currentUser);
            command.Parameters.AddWithValue("@thisID", ModuleModel.ModuleModelID);

            //executing command
            command.ExecuteNonQuery();

            //closing database connection
            dbCon.Close();

        }

        //method to find corresponding module's self-study hours
        public void GetWeeks()
        {
            //creating sql connection
            SqlConnection dbCon = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CrunchTime_WebData;Integrated Security=True");

            //creating sql command
            SqlCommand findHours = new SqlCommand("select [WeeksInSemester] from [SemesterModel] where [SemesterModelID] = @sID", dbCon);

            //opening connection to database
            dbCon.Open();

            //adding parameters to command
            findHours.Parameters.AddWithValue("@sID", ModuleModel.SemesterModelID);

            //executing reader
            SqlDataReader read = findHours.ExecuteReader();

            //running if reader finds a result
            if (read.Read() == true)
            {
                //saving data
                foundSemesterWeeks = (double)read[0]; ;


            }

        }


        //method to calculate self-study hours
        public void CalculateSelfStudyHours()
        {
            //class library object reference
            StudyHour study = new StudyHour();

            //calculating self study hours
            selfStudy = study.SelfStudyHours(ModuleModel.ModuleCredits, foundSemesterWeeks, ModuleModel.ClassHours);

        }


        public void SaveStudyHours()
        {

            //creating sql connection
            SqlConnection dbCon = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb;Initial Catalog=CrunchTime_WebData;Integrated Security=True");

            //creating sql command
            SqlCommand command = new SqlCommand();

            //specifying command type
            command.CommandType = System.Data.CommandType.Text;

            //query statement to update self study hours in table
            command.CommandText = "update [ModuleModel] set SelfStudyHours = @hours where ModuleModelID = @mID";

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
            command.Parameters.AddWithValue("@hours", selfStudy);
            command.Parameters.AddWithValue("@mID", ModuleModel.ModuleModelID);

            //executing command
            command.ExecuteNonQuery();

            //closing database connection
            dbCon.Close();

        }
    }
}
