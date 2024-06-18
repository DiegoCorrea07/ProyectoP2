using ProyectoP2.ViewModels;

namespace ProyectoP2.Views;

public partial class HistoriaVentaPage : ContentPage
{
	public HistoriaVentaPage(HistorialVentaVM vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}