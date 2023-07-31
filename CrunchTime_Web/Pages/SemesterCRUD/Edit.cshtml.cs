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

namespace CrunchTime_Web.Pages.SemesterCRUD
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
        public SemesterModel SemesterModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.SemesterModel == null)
            {
                return NotFound();
            }

            var semestermodel =  await _context.SemesterModel.FirstOrDefaultAsync(m => m.SemesterModelID == id);
            if (semestermodel == null)
            {
                return NotFound();
            }
            SemesterModel = semestermodel;
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

            _context.Attach(SemesterModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SemesterModelExists(SemesterModel.SemesterModelID))
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

        private bool SemesterModelExists(int id)
        {
          return _context.SemesterModel.Any(e => e.SemesterModelID == id);
        }
    }
}
