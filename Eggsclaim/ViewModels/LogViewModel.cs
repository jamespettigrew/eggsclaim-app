using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using Eggsclaim.Models;

namespace Eggsclaim
{
    public class LogViewModel : BaseViewModel
    {
        private LogDataStore DataStore => DependencyService.Get<LogDataStore>();
        public ObservableCollection<EggsStatus> LogItems { get; set; }
        public Command LoadOlderItemsCommand { get; set; }
        public Command LoadLatestCommand { get; set; }

        public LogViewModel()
        {
            Title = "Eggsclaim";
            LogItems = new ObservableCollection<EggsStatus>();
            LoadOlderItemsCommand = new Command(async () => await ExecuteLoadOlderItemsCommand());
            LoadLatestCommand = new Command(async () => await ExecuteLoadLatestItemsCommand());

            DataStore.OnStoreUpdated += (sender, e) => 
            {
                Device.BeginInvokeOnMainThread(() => LoadLatestCommand.Execute(null));
            };
        }
        
        private async Task ExecuteLoadOlderItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var items = await DataStore.GetItemsBeforeAsync(LogItems[LogItems.Count - 1].SequenceId, 50);
            foreach (var item in items)
            {
                LogItems.Add(item);
            }
            
            IsBusy = false;
        }
        
        private async Task ExecuteLoadLatestItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            
            var items = await DataStore.GetLatestAsync(50);
            LogItems.Clear();
            foreach (var item in items)
            {
                LogItems.Add(item);
            }
            
            IsBusy = false;
        }
    }
}
