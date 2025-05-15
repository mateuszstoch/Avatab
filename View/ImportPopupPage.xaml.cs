using Avatab.ViewModel;
using CommunityToolkit.Maui.Views;

namespace Avatab.View;

public partial class ImportPopupPage : Popup
{
	public ImportPopupPage(ImportPopupViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    private void OnCloseClicked(object sender, EventArgs e)
    {
        Close();
    }

}