using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrunchTime_Web;
using Microsoft.AspNetCore.Authorization;

namespace CrunchTime_Web.Pages.SemesterCRUD
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        public DetailsModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

      public SemesterModel SemesterModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SemesterModel == null)
            {
                return NotFound();
            }

            var semestermodel = await _context.SemesterModel.FirstOrDefaultAsync(m => m.SemesterModelID == id);
            if (semestermodel == null)
            {
                return NotFound();
            }
            else 
            {
                SemesterModel = semestermodel;
            }
            return Page();
        }
    }
}
