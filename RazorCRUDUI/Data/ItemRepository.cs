using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using RazorCRUDUI.Data;
using RazorCRUDUI.Migrations;
using RazorCRUDUI.Models;

namespace RazorRepoUI.Data
{
    public class ItemRepository: IItemRepository
    {
        private readonly ItemsContext _context;
        public ItemRepository(ItemsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ItemModel>> GetItemsAsync(String? SearchString)
        {
            //select all items from database
            var items = from i in _context.Items select i;

            //if search string is not null or empty, filter by search string
            if (!String.IsNullOrEmpty(SearchString))
            {
                items = items.Where(s => s.Name.Contains(SearchString));
            }

            return await items.ToListAsync();
        }

        public async Task<ItemModel?> GetItemByIdAsync(int id)
        {
            return await _context.Items.FirstOrDefaultAsync(i => i.ID == id);
        }

        public async Task<bool> UpdateItemAsync(ItemModel item)
        {
            _context.Attach(item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var itemmodel = await _context.Items.FindAsync(id);

            if (itemmodel != null)
            {
                _context.Items.Remove(itemmodel);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ItemModel> InsertItemAsync(ItemModel item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
