using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace BookStore.CLIENT.Views
{
    public sealed partial class MemberListPage : Page
    {
        public MemberListPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.loadListAsync();
        }
    }
}