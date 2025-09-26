namespace Avatab.View;

public partial class PersonListPage : ContentPage
{
    public PersonListPage(PersonListPage vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}