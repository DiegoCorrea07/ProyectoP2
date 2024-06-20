using ProyectoP2.ViewModels;

namespace ProyectoP2.Views;

public partial class InventarioPage : ContentPage
{
    private InventarioVM _viewModel;
    public InventarioPage(InventarioVM viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;

    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Recargar los productos cuando la vista aparezca
        if (_viewModel != null)
        {
            await _viewModel.ObtenerProductos();
        }
    }
}