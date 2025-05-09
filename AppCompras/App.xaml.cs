﻿using System.Globalization;
using AppCompras.Helpers;
using AppCompras.View;

namespace AppCompras;

public partial class App : Application
{
    static SQLiteDatabaseHelper _db;

    public static SQLiteDatabaseHelper Db
    {
        get
        {
            if (_db == null)
            {
                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_sqlite_compras.db3"
                );
                _db = new SQLiteDatabaseHelper(path);
            }
            return _db;
        }
    }
    
    public App()
    {
        InitializeComponent();

        Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-br");
        
        MainPage = new NavigationPage(new ListaProduto());
    }
}