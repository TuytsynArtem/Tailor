using Tailor.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Tailor.Views
{
    public class ClientAddPage : ContentPage
    {

        private Entry _name;
        private Entry _lastname;
        private Entry _number;
        private Entry _email;

        private Entry _DlinaIzdeliya;
        private Entry _Grud;
        private Entry _Sheya;
        private Entry _Plecho;
        private Entry _ShirinaPereda;
        private Entry _ShirinaSpiny;

        private Button _button;

        string _path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DB.db");
        public ClientAddPage()
        {
            this.Title = "Добавление Клиентов";
            StackLayout stackLayout = new StackLayout();

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
            _number.Placeholder = "Номер телефона";
            stackLayout.Children.Add(_number);

            _email = new Entry();
            _email.Keyboard = Keyboard.Text;
            _email.Placeholder = "Почта";
            stackLayout.Children.Add(_email);

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

            _button = new Button();
            _button.BorderColor = Color.LightSeaGreen;
            _button.CornerRadius = 25;
            _button.BorderWidth = 1.5;
            _button.BackgroundColor = Color.YellowGreen;
            _button.Text = "Сохранить";
            _button.Clicked += _save_Clicked;
            stackLayout.Children.Add(_button);

            Content = stackLayout;
        }

        private async void _save_Clicked(object sender, EventArgs e)
        {
            var path = new SQLiteConnection(_path);
            path.CreateTable<Client>();

            var primarykey = path.Table<Client>().OrderByDescending(c => c.Id).FirstOrDefault();

            Client client = new Client()
            {
                Id = (primarykey == null ? 1 : primarykey.Id + 1),
                Name = _name.Text,
                LastName = _lastname.Text,
                Number = _number.Text,
                Mail = _email.Text,
                DlinaIzdeliya = _DlinaIzdeliya.Text,
                Grud = _Grud.Text,
                Sheya = _Sheya.Text,
                Plecho = _Plecho.Text,
                ShirinaPereda = _ShirinaPereda.Text,
                ShirinaSpiny = _ShirinaSpiny.Text
            };
            path.Insert(client);
            await DisplayAlert(null, client.Name + " Сохранено", "Продолжить");
            await Navigation.PushAsync(new ClientListPage());
        }
    }
}