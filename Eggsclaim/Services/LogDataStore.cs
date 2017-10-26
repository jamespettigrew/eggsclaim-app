using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using SQLite;

using Eggsclaim.Models;

namespace Eggsclaim
{
    public class LogDataStore
    {
        private readonly SQLiteAsyncConnection _database;
        public EventHandler OnStoreUpdated;
        
        public LogDataStore()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbPath = Path.Combine(path, "eggsclaim.db");
            _database = new SQLiteAsyncConnection(dbPath);
        }
        
        private async Task CreateTables()
        {
            await _database.CreateTableAsync<EggsStatus>();
        }

        public async Task<int> AddItemAsync(EggsStatus item)
        {
            await CreateTables();
            var insertTask = await _database.InsertAsync(item);
            OnStoreUpdated?.Invoke(this, EventArgs.Empty);
            return insertTask;
        }

        public async Task<IEnumerable<EggsStatus>> GetItemsBeforeAsync(int index, int limit)
        {
            await CreateTables();
            var list = await _database.Table<EggsStatus>()
                                      .Where(i => i.SequenceId < index)
                                      .OrderByDescending(x => x.SequenceId)
                                      .Take(limit)
                                      .ToListAsync();
            return list.AsEnumerable();
        }

        public async Task<IEnumerable<EggsStatus>> GetItemsAfterAsync(int index, int limit)
        {
            await CreateTables();
            var list = await _database.Table<EggsStatus>()
                                      .Where(i => i.SequenceId > index)
                                      .Take(limit)
                                      .ToListAsync();
            return list.AsEnumerable();
        }
        
        public async Task<IEnumerable<EggsStatus>> GetLatestAsync(int limit)
        {
            await CreateTables();
            var list = await _database.Table<EggsStatus>()
                                      .OrderByDescending(x => x.SequenceId)
                                      .Take(limit)
                                      .ToListAsync();
            return list.AsEnumerable();
        }
    }
}
