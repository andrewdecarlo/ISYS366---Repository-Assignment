﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorCRUDUI.Data;
using RazorCRUDUI.Models;
using RazorRepoUI.Data;

namespace RazorCRUDUI.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly IItemRepository _repo;

        //Pass IItemRepository into constructor
        public IndexModel(IItemRepository repo)
        {
            _repo = repo;
        }

        //Iterable list of items in the database
        public IList<ItemModel> ItemModel { get;set; } = default!;


        //Properties for search bar
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }


        public async Task OnGetAsync()
        {
            ItemModel = (IList<ItemModel>) await _repo.GetItemsAsync(SearchString);
        }

    }
}
