﻿using ProyectoP2.Views;

namespace ProyectoP2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            var logueado = Preferences.Get("logueado", string.Empty);
            if (string.IsNullOrEmpty(logueado))
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new AppShell();
            }

        }
    }
}
