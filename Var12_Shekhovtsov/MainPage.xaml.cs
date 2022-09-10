using Var12_Shekhovtsov.ViewModel;
namespace Var12_Shekhovtsov;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
        BindingContext = new ComponentViewModel();
    }
}

