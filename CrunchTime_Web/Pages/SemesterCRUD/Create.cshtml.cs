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
using Microsoft.AspNetCore.Authorization;

namespace CrunchTime_Web.Pages.SemesterCRUD
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        //variable to hold current user id
        public string currentUser;

        public CreateModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SemesterModel SemesterModel { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.SemesterModel.Add(SemesterModel);
            await _context.SaveChangesAsync();

            //calling method to save user id
            SaveUserID();

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
            command.CommandText = "update [SemesterModel] set UserID = @sOwner where SemesterModelID = @thisID";

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
            command.Parameters.AddWithValue("@sOwner", currentUser);
            command.Parameters.AddWithValue("@thisID", SemesterModel.SemesterModelID);

            //executing command
            command.ExecuteNonQuery();

            //closing database connection
            dbCon.Close();

        }
    }
}
