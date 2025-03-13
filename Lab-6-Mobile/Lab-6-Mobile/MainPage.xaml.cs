using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Collections.ObjectModel;

namespace Lab_6_Mobile
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        public string SelectedItem { get; set; }

        public MainPage()
        {
            InitializeComponent();

            Items = new ObservableCollection<string>
            {
                "Елемент 1", "Елемент 2", "Елемент 3", "Елемент 4", "Елемент 5"
            };

            BindingContext = this;
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.Count > 0)
            {
                SelectedItem = e.CurrentSelection[0].ToString();
                SelectedLabel.Text = $"Ви вибрали: {SelectedItem}";

                // Вивід у Toast
                await Toast.Make(SelectedItem, ToastDuration.Short).Show();
            }
        }

        private async void OnSwitchLayout(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SecondPage()); // Відкриваємо другу сторінку
        }
    }
}