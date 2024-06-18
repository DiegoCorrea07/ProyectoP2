using ProyectoP2.ViewModels;

namespace ProyectoP2
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }


    }

}
