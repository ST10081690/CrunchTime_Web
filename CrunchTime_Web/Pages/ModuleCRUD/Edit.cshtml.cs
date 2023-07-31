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

namespace CrunchTime_Web.Pages.ModuleCRUD
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
        public ModuleModel ModuleModel { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.ModuleModel == null)
            {
                return NotFound();
            }

            var modulemodel =  await _context.ModuleModel.FirstOrDefaultAsync(m => m.ModuleModelID == id);
            if (modulemodel == null)
            {
                return NotFound();
            }
            ModuleModel = modulemodel;
           ViewData["SemesterModelID"] = new SelectList(_context.SemesterModel, "SemesterModelID", "SemesterModelID");
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

            _context.Attach(ModuleModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModuleModelExists(ModuleModel.ModuleModelID))
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

        private bool ModuleModelExists(int id)
        {
          return _context.ModuleModel.Any(e => e.ModuleModelID == id);
        }
    }
}
