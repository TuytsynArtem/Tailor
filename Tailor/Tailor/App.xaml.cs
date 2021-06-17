using System;
using Tailor.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tailor
{
    public partial class App : Application
    {
        public App()
        {
            MainPage = new Xamarin.Forms.NavigationPage(new ClientListPage());
            InitializeComponent();
            


        }



        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}