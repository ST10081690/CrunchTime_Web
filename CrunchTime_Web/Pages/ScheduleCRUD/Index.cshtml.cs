using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrunchTime_Web;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CrunchTime_Web.Pages.ScheduleCRUD
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        //getting current weekday
        public DayOfWeek getDay = DateTime.Today.DayOfWeek;

        //variable holding current weekday
        public string Day;

        //variable to store current user's id
        public string currentUser;

        public IndexModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

        public IList<ScheduleModel> ScheduleModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //finding user identity
            var identity = (ClaimsIdentity)User.Identity;
            var findUser = identity.FindFirst(ClaimTypes.NameIdentifier);

            //saving user id
            currentUser = findUser.Value;

            //copying weekday
            Day = getDay.ToString();

            if (_context.ScheduleModel != null)
            {
                ScheduleModel = await _context.ScheduleModel
                .Include(s => s.Module).ToListAsync();
            }
        }
    }
}
