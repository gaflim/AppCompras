using Android.AdServices.AppSetIds;
using Exception = Java.Lang.Exception;

namespace AppCompras.Models;

using SQLite;

public class Produto
{
    private string? _descricao;
    private string? _categoria;
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    
    public string? Categoria
    {
        get => _categoria;
        set
        {
            if (value == null) 
                throw new Exception("Preencha a categoria");
            _categoria = value;
        }
    }
    public string Descricao
    {
        get => _descricao;
        set
        {
            if (value == null) 
                throw new Exception("Preencha a descrição");
            _descricao = value;
        }
    }
    public double Quantidade { get; set; }
    public double Preco { get; set; }
    public double Total
    {
        get => Quantidade * Preco;
    }
}