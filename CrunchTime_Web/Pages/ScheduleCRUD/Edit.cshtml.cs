using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrunchTime_Web;
using Microsoft.AspNetCore.Authorization;

namespace CrunchTime_Web.Pages.ScheduleCRUD
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        public EditModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

        [BindProperty]
        public ScheduleModel ScheduleModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ScheduleModel == null)
            {
                return NotFound();
            }

            var schedulemodel =  await _context.ScheduleModel.FirstOrDefaultAsync(m => m.ScheduleModelID == id);
            if (schedulemodel == null)
            {
                return NotFound();
            }
            ScheduleModel = schedulemodel;
           ViewData["ModuleModelID"] = new SelectList(_context.ModuleModel, "ModuleModelID", "ModuleModelID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ScheduleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleModelExists(ScheduleModel.ScheduleModelID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ScheduleModelExists(int id)
        {
          return _context.ScheduleModel.Any(e => e.ScheduleModelID == id);
        }
    }
}
