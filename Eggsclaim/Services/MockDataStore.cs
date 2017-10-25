using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Eggsclaim.Models;

namespace Eggsclaim
{
    public class MockDataStore : IDataStore<EggsStatusUpdate>
    {
        List<EggsStatusUpdate> items;

        public MockDataStore()
        {
            items = new List<EggsStatusUpdate>();
        }

        public async Task<bool> AddItemAsync(EggsStatusUpdate item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(EggsStatusUpdate item)
        {
            //var _item = items.Where((EggsStatusUpdate arg) => arg.Id == item.Id).FirstOrDefault();
            //items.Remove(_item);
            //items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            //var _item = items.Where((EggsStatusUpdate arg) => arg.Id == id).FirstOrDefault();
            //items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<EggsStatusUpdate> GetItemAsync(string id)
        {
            return new EggsStatusUpdate(new DateTime(), true);
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<EggsStatusUpdate>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
