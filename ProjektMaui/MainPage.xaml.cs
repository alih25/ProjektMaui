using ProjektMaui.ViewModel;

namespace ProjektMaui
    
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

    }
}