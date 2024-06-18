using ProyectoP2.ViewModels;

namespace ProyectoP2.Views;

public partial class InventarioPage : ContentPage
{
	public InventarioPage(InventarioVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}