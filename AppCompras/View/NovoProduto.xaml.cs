using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCompras.Models;

namespace AppCompras.View;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
    }

    private async void MenuItem_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            Produto p = new Produto
            {
                Categoria = categoria.SelectedItem.ToString(),
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_qnt.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };
            await App.Db.Insert(p);
            await DisplayAlert("Sucesso", "Registro Inserido", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
    }
}