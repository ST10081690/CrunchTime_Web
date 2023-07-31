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

namespace CrunchTime_Web.Pages.ModuleCRUD
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        //variable to store current user's id
        public string currentUser;

        public IndexModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

        public IList<ModuleModel> ModuleModel { get;set; } = default!;

        public async Task OnGetAsync()
        {
            //finding user identity
            var identity = (ClaimsIdentity)User.Identity;
            var findUser = identity.FindFirst(ClaimTypes.NameIdentifier);

            //saving user id
            currentUser = findUser.Value;

            if (_context.ModuleModel != null)
            {
                ModuleModel = await _context.ModuleModel
                .Include(m => m.Semester).ToListAsync();
            }
        }
    }
}
