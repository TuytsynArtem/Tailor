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
    public class ClientEditPage : ContentPage
    {

        private ListView _listView;
        private Entry _id;
        private Entry _name;
        private Entry _lastname;
        private Entry _number;
        private Entry _mail;

        private Entry _DlinaIzdeliya;
        private Entry _Grud;
        private Entry _Sheya;
        private Entry _Plecho;
        private Entry _ShirinaPereda;
        private Entry _ShirinaSpiny;

        private Button button;

        Client _client = new Client();
        string _path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DB.db");

        public ClientEditPage()
        {
            this.Title = "Список клиентов";
            var data = new SQLiteConnection(_path);

            StackLayout stackLayout = new StackLayout();

            _listView = new ListView();
            _listView.ItemsSource = data.Table<Client>().OrderBy(l => l.Name).ToList();
            _listView.ItemSelected += _listView_ItemSelected;
            stackLayout.Children.Add(_listView);

            _id = new Entry();
            _id.Placeholder = "ID";
            _id.IsVisible = false;
            stackLayout.Children.Add(_listView);

            _name = new Entry();
            _name.Keyboard = Keyboard.Text;
            _name.Placeholder = "Имя";
            stackLayout.Children.Add(_name);

            _lastname = new Entry();
            _lastname.Keyboard = Keyboard.Text;
            _lastname.Placeholder = "Фамилия";
            stackLayout.Children.Add(_lastname);

            _number = new Entry();
            _number.Keyboard = Keyboard.Text;
            _number.Placeholder = "Номер";
            stackLayout.Children.Add(_number);

            _mail = new Entry();
            _mail.Keyboard = Keyboard.Text;
            _mail.Placeholder = "Почта";
            stackLayout.Children.Add(_mail);

            _DlinaIzdeliya = new Entry();
            _DlinaIzdeliya.Keyboard = Keyboard.Text;
            _DlinaIzdeliya.Placeholder = "Длина Изделия";
            stackLayout.Children.Add(_DlinaIzdeliya);

            _Grud = new Entry();
            _Grud.Keyboard = Keyboard.Text;
            _Grud.Placeholder = "Обхват Груди";
            stackLayout.Children.Add(_Grud);

            _Sheya = new Entry();
            _Sheya.Keyboard = Keyboard.Text;
            _Sheya.Placeholder = "Обхват Шеи";
            stackLayout.Children.Add(_Sheya);

            _Plecho = new Entry();
            _Plecho.Keyboard = Keyboard.Text;
            _Plecho.Placeholder = "Длина Плеча";
            stackLayout.Children.Add(_Plecho);

            _ShirinaPereda = new Entry();
            _ShirinaPereda.Keyboard = Keyboard.Text;
            _ShirinaPereda.Placeholder = "Ширина Переда";
            stackLayout.Children.Add(_ShirinaPereda);

            _ShirinaSpiny = new Entry();
            _ShirinaSpiny.Keyboard = Keyboard.Text;
            _ShirinaSpiny.Placeholder = "Ширина Спины";
            stackLayout.Children.Add(_ShirinaSpiny);

            button = new Button();
            button.BorderColor = Color.LightSeaGreen;
            button.CornerRadius = 25;
            button.BorderWidth = 1;
            button.BackgroundColor = Color.YellowGreen;
            button.Text = "Сохранить";
            button.Clicked += _button_Clicked;
            stackLayout.Children.Add(button);

            button = new Button();
            button.BorderColor = Color.LightSeaGreen;
            button.CornerRadius = 25;
            button.BorderWidth = 1;
            button.BackgroundColor = Color.YellowGreen;
            button.Text = "Удалить";
            button.Clicked += _deletebutton_Clicked;
            stackLayout.Children.Add(button);

            Content = stackLayout;
        }
        private async void _deletebutton_Clicked(object sender, EventArgs e)
        {

            var data = new SQLiteConnection(_path);
            await DisplayAlert(null, "Пользователь " + Convert.ToString(_client.Name) + " удалён", "Продолжить");
            data.Table<Client>().Delete(m => m.Id == _client.Id);
            data.Update(_client);
            await Navigation.PushAsync(new ClientListPage());

        }
        private async void _button_Clicked(object sender, EventArgs e)
        {
            var data = new SQLiteConnection(_path);
            Client client = new Client()
            {
                Id = Int32.Parse(_id.Text),
                Name = _name.Text,
                LastName = _lastname.Text,
                Number = _number.Text,
                Mail = _mail.Text,
                DlinaIzdeliya = _DlinaIzdeliya.Text,
                Grud = _Grud.Text,
                Sheya = _Sheya.Text,
                Plecho = _Plecho.Text,
                ShirinaPereda = _ShirinaPereda.Text,
                ShirinaSpiny = _ShirinaSpiny.Text
            };
            data.Update(client);
            await DisplayAlert(null, "Пользователь " + Convert.ToString(client.Name) + " изменён", "Продолжить");
            await Navigation.PushAsync(new ClientListPage());
        }
        private void _listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _client = (Client)e.SelectedItem;
            _id.Text = _client.Id.ToString();
            _name.Text = _client.Name;
            _lastname.Text = _client.LastName;
            _number.Text = _client.Number;
            _mail.Text = _client.Mail;
            _DlinaIzdeliya.Text = _client.DlinaIzdeliya;
            _Grud.Text = _client.Grud;
            _Sheya.Text = _client.Sheya;
            _Plecho.Text = _client.Plecho;
            _ShirinaPereda.Text = _client.ShirinaPereda;
            _ShirinaSpiny.Text = _client.ShirinaSpiny;
        }
    }
}