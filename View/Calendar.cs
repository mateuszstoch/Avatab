using Avatab.Model;
using Avatab.ViewModel;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;


namespace Avatab.View;

public partial class Calendar : ContentView
{


    private CollectionView _hoursCollection;
    private AbsoluteLayout _eventLayout;
    private AbsoluteLayout _lineLaypout;

    public Calendar()
    {
        var scrollView = new ScrollView { Orientation = ScrollOrientation.Vertical };

        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = 80 },
                new ColumnDefinition { Width = GridLength.Star }
            }
        };



        _hoursCollection = new CollectionView
        {
            ItemsSource = GenerateHours(),
            ItemTemplate = new DataTemplate(() =>
            {
                var label = new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center };
                label.SetBinding(Label.TextProperty, ".");
                return new Border
                {

                    Padding = 0,
                    HeightRequest = this.HeightRequest / 12.0,
                    Stroke = Colors.Transparent,
                    Content = label

                };
            })
        };
        grid.Add(_hoursCollection);
        Grid.SetColumn(_hoursCollection, 0);

        _eventLayout = new AbsoluteLayout { BackgroundColor = Colors.Transparent };
        grid.Add(_eventLayout);
        Grid.SetColumn(_eventLayout, 1);

        _lineLaypout = new AbsoluteLayout { BackgroundColor = Colors.Transparent };
        grid.Add(_lineLaypout);
        Grid.SetColumnSpan(_lineLaypout, 2);

        _lineLaypout = new AbsoluteLayout { BackgroundColor = Colors.Transparent };
        grid.Add(_lineLaypout);
        Grid.SetColumnSpan(_lineLaypout, 2);

        scrollView.Content = grid;
        Content = scrollView;



        this.SizeChanged += (_, _) =>
        {
            if (BindingContext is CalendarViewModel vm)
            {
                vm.UpdateEventPositions(_eventLayout.Height, _eventLayout.Width);
                RenderEvents(vm.Events);
            }
        };

    }



    private void RenderEvents(ObservableCollection<CalendarEvent> events)
    {
        _eventLayout.Children.Clear();
        double hourHeight = _eventLayout.Height / 96;

        for (int i = 0; i <= 96; i++)
        {

            bool mainLine = i % 4 == 0;

            var line = new BoxView
            {
                Color = mainLine ? Colors.Black : Colors.Gray,
                HeightRequest = 1,
                HorizontalOptions = LayoutOptions.Fill
            };


            AbsoluteLayout.SetLayoutBounds(line, new Rect(0, i * hourHeight, 1, 1));
            AbsoluteLayout.SetLayoutFlags(line, AbsoluteLayoutFlags.WidthProportional);
            if (mainLine)
            {
                _lineLaypout.Children.Add(line);
            }
            else
            {
                _eventLayout.Children.Add(line);
            }
        }


        foreach (var ev in events)
        {
            var border = new Border
            {
                BackgroundColor = Color.FromRgba(255, 0, 255, 0.3),
                StrokeShape = new RoundRectangle { CornerRadius = 8 },
                Padding = 5,
                Content = new Label
                {
                    Text = ev.title,
                    TextColor = Colors.White

                },
                Stroke = Color.FromRgba(255, 0, 255, 1)

            };

            AbsoluteLayout.SetLayoutBounds(border, ev.layoutBounds);
            AbsoluteLayout.SetLayoutFlags(border, AbsoluteLayoutFlags.None);
            _eventLayout.Children.Add(border);
        }
    }

    private ObservableCollection<string> GenerateHours()
    {
        var hours = new ObservableCollection<string>();
        for (int i = 0; i < 24; i++)
            hours.Add($"{i:00}:00");
        return hours;
    }
}

