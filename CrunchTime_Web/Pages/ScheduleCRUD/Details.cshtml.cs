using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CrunchTime_Web;
using Microsoft.AspNetCore.Authorization;

namespace CrunchTime_Web.Pages.ScheduleCRUD
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        public DetailsModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

      public ScheduleModel ScheduleModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ScheduleModel == null)
            {
                return NotFound();
            }

            var schedulemodel = await _context.ScheduleModel.FirstOrDefaultAsync(m => m.ScheduleModelID == id);
            if (schedulemodel == null)
            {
                return NotFound();
            }
            else 
            {
                ScheduleModel = schedulemodel;
            }
            return Page();
        }
    }
}
