using Avatab.ViewModel;

namespace Avatab.View;

public partial class PersonEditPage : ContentPage
{
    public PersonEditPage(PersonEditViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}