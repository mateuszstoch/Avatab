using Avatab.ViewModel;
using CommunityToolkit.Maui.Views;

namespace Avatab.View;

public partial class ImportPopupPage : Popup
{
    ImportPopupViewModel viewModel;
    public ImportPopupPage(ImportPopupViewModel vm)
    {
        InitializeComponent();
        viewModel = vm;
        BindingContext = vm;
    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        this.Close();
    }

    private void OnImportClicked(object sender, EventArgs e)
    {
        this.Close(viewModel.Lectures);
    }

}