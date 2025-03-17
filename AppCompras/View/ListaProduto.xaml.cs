using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Widget;
using AppCompras.Models;

namespace AppCompras.View;

public partial class ListaProduto : ContentPage
{
    private ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();
    
    public ListaProduto()
    {
        InitializeComponent();

        lstProdutos.ItemsSource = Lista;
    }
    
    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();
        tmp.ForEach(i => Lista.Add(i));
    }
    
    private void AddNew(object? sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
    }

    private async void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;
            Lista.Clear();
            List<Produto> tmp = await App.Db.Search(q);
            tmp.ForEach(i => Lista.Add(i));
        }
        catch(Exception ex)
        {
            DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
    }

    private void Somar(object? sender, EventArgs e)
    {
        double soma = Lista.Sum(i => i.Total);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    private void Remover(object? sender, EventArgs e)
    {
        
    }
}