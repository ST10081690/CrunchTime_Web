using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrunchTime_Web;
using Microsoft.AspNetCore.Authorization;

namespace CrunchTime_Web.Pages.ModuleCRUD
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        public DetailsModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

      public ModuleModel ModuleModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ModuleModel == null)
            {
                return NotFound();
            }

            var modulemodel = await _context.ModuleModel.FirstOrDefaultAsync(m => m.ModuleModelID == id);
            if (modulemodel == null)
            {
                return NotFound();
            }
            else 
            {
                ModuleModel = modulemodel;
            }
            return Page();
        }
    }
}
//-----------------------------------------------------END-OF-CLASS---------------------------------------------------------//`