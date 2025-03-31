using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Security.Identity;
using Android.Widget;
using AppCompras.Models;

namespace AppCompras.View;

public partial class ListaProduto : ContentPage
{
    private ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();
    
    public ListaProduto()
    {
        InitializeComponent();
        LstProdutos.ItemsSource = Lista;
    }
    
    protected override async void OnAppearing()
    {
        try
        {
            Lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => Lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
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
            LstProdutos.IsRefreshing = true;
            Lista.Clear();
            List<Produto> tmp = await App.Db.Search(q);
            tmp.ForEach(i => Lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
        finally
        {
            LstProdutos.IsRefreshing = false;
        }
    }
    
    private void Somar(object? sender, EventArgs e)
    {
        double soma = Lista.Sum(i => i.Total);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "Ok");
    }

    private async void Remover(object? sender, EventArgs e)
    {
        try
        {
            MenuItem? selecionado = sender as MenuItem;
            Produto p = selecionado.BindingContext as Produto;
            bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                Lista.Remove(p);
            }
        } 
        catch (Exception ex)
        {
            await DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
    }

    private void LstProdutos_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto? p = e.SelectedItem as Produto;
            Navigation.PushAsync(new EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
    }

    private async void LstProdutos_OnRefreshing(object? sender, EventArgs e)
    {
        try
        {
            Lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => Lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
        finally
        {
            LstProdutos.IsRefreshing = false;
        }
    }
    
    private async void PickerCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            var categoria = FilterCat.SelectedItem.ToString();
            Lista.Clear();
            if (categoria != null && categoria != "Filtrar")
            {
                List<Produto> produtosFiltrados = await App.Db.GetCat(categoria);
                produtosFiltrados.ForEach(i => Lista.Add(i));
            }
            else
            {
                OnAppearing();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
    }
    private void SomarPorCategoria_Clicked(object sender, EventArgs e)
    {
        try
        {
            var categoria = FilterCat.SelectedItem.ToString();
            var produtosFiltrados = Lista.Where(i => i.Categoria == categoria).ToList();
            
            double? soma = produtosFiltrados.Sum(i => i.Total);
            
            var msg = produtosFiltrados.Count > 0
                ? $"O total da categoria {categoria} é {soma:C}"
                : "Nenhum produto encontrado nesta categoria.";
            
            DisplayAlert("Total por Categoria", msg, "OK");
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}