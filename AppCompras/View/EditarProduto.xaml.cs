using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCompras.Models;

namespace AppCompras.View;

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent();
    }

    private async void Button_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            Produto? produtoAnexado = BindingContext as Produto; 
            Produto p = new Produto
            {
                Id = produtoAnexado.Id,
                Categoria = categoria.SelectedItem.ToString(),
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_qnt.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };
            await App.Db.Update(p);
            await DisplayAlert("Sucesso", "Registro Atualizado", "Ok");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Algo deu errado", ex.Message, "Ok");
        }
    }
}