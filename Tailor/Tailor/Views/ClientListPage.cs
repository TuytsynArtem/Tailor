using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tailor.Models;
using Xamarin.Forms;

namespace Tailor.Views
{
    public class ClientListPage : ContentPage
    {
        private ListView _list;
        private Client _client;

        string _path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DB.db");
        public ClientListPage()
        {

            StackLayout stackLayout = new StackLayout();
            this.Title = "Список клиентов";
            var data = new SQLiteConnection(_path);


            _list = new ListView();
            try
            {
                _list.ItemsSource = data.Table<Client>().OrderBy(k => k.Name).ToList();
                _list.ItemSelected += _listView_ItemSelected;
            }
            catch 
            { }

            stackLayout.Children.Add(_list);
            #region кнопки
            Button button = new Button();

            button = new Button();
            button.BorderColor = Color.LightSeaGreen;
            button.CornerRadius = 25;
            button.BorderWidth = 1.5;
            button.BackgroundColor = Color.YellowGreen;
            button.Text = "Добавить Клиента";
            button.Clicked += Button_Clicked;
            stackLayout.Children.Add(button);

            button = new Button();
            button.BorderColor = Color.LightSeaGreen;
            button.CornerRadius = 25;
            button.BorderWidth = 1.5;
            button.BackgroundColor = Color.YellowGreen;
            button.Text = "Редактирование Клиентов";
            button.Clicked += Button_Clicked2;
            stackLayout.Children.Add(button);

            button = new Button();
            button.BorderColor = Color.LightSeaGreen;
            button.CornerRadius = 25;
            button.BorderWidth = 1;
            button.BackgroundColor = Color.YellowGreen;
            button.Text = "Выкройка";
            button.Clicked += _button_Clicked22;
            stackLayout.Children.Add(button);
            #endregion
            Content = stackLayout;
        }
        private async void Button_Clicked2(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new ClientEditPage());
            }
            catch
            {
                await DisplayAlert(null, "Список клиентов пуст", "Продолжить");
            }
        }
        private async void _button_Clicked22(object sender, EventArgs e)
        {
            try
            {
                await DisplayAlert(null, "Выкройка пользователя " + Convert.ToString(_client.Name), "Продолжить");

                await Navigation.PushAsync(new VikPage(_client));
            }
            catch
            {
                await DisplayAlert(null, "Выберите клиента", "Продолжить");
                await Navigation.PushAsync(new ClientListPage());
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientAddPage());
        }
        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _client = (Client)e.SelectedItem;
        }
    }
}