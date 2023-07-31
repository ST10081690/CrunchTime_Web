using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CrunchTime_Web.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CT_User class
public class CrunchTimeUser : IdentityUser
{
    //creating properties for user details
    //username
    public string UsernameByUser { get; set; }

    //first name
    public string FirstName { get; set; }

    //surname
    public string Surname { get; set; }

    //user's study field
    public string StudyField { get; set; }
}

