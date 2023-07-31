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
    public class DeleteModel : PageModel
    {
        private readonly CrunchTime_Web.UserData _context;

        public DeleteModel(CrunchTime_Web.UserData context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.ModuleModel == null)
            {
                return NotFound();
            }
            var modulemodel = await _context.ModuleModel.FindAsync(id);

            if (modulemodel != null)
            {
                ModuleModel = modulemodel;
                _context.ModuleModel.Remove(ModuleModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
