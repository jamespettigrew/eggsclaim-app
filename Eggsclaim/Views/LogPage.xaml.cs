using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Eggsclaim
{
    public partial class LogPage : ContentPage
    {
        LogViewModel viewModel;

        public LogPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new LogViewModel();
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            viewModel.LoadLatestCommand.Execute(null);
            if (viewModel.LogItems.Count > 0)
                ItemsListView.ScrollTo(viewModel.LogItems[0], ScrollToPosition.Start, true);
        }
    }
}
