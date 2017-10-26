using System;
using System.Collections;
using System.Windows.Input;

using Xamarin.Forms;

namespace Eggsclaim
{
    public class InfiniteListView : ListView
    {
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(nameof(LoadMoreCommandProperty), typeof(ICommand), typeof(InfiniteListView), default(ICommand));
        
        public ICommand LoadMoreCommand
        {
            get { return (ICommand) GetValue(LoadMoreCommandProperty); }
            set { SetValue(LoadMoreCommandProperty, value); }
        }
        
        public InfiniteListView() : base(ListViewCachingStrategy.RecycleElement)
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
        }

        private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var items = ItemsSource as IList;
            
            if (items != null && e.Item == items[items.Count - 1])
            {
                if(LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                {
                    LoadMoreCommand.Execute(null);
                }
            }
        }
    }
}
