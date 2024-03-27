using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorCRUDUI.Data;
using RazorCRUDUI.Models;
using RazorRepoUI.Data;

namespace RazorCRUDUI.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly IItemRepository _repo;

        public CreateModel(IItemRepository repo)
        {
            _repo = repo;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ItemModel ItemModel { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repo.InsertItemAsync(ItemModel);

            return RedirectToPage("./Index");
        }
    }
}
