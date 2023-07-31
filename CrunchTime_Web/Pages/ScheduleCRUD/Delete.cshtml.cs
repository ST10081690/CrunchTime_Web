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
    public class DeleteModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        public DeleteModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ScheduleModel == null)
            {
                return NotFound();
            }
            var schedulemodel = await _context.ScheduleModel.FindAsync(id);

            if (schedulemodel != null)
            {
                ScheduleModel = schedulemodel;
                _context.ScheduleModel.Remove(ScheduleModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
