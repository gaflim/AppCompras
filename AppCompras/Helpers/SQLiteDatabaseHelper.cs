using AppCompras.Models;
using SQLite;

namespace AppCompras.Helpers;

public class SQLiteDatabaseHelper
{
    readonly SQLiteAsyncConnection _con;

    public SQLiteDatabaseHelper(string path)
    {
        _con = new SQLiteAsyncConnection(path);
        _con.CreateTableAsync<Produto>().Wait();
    }

    public Task<int> Insert(Produto p)
    {
        return _con.InsertAsync(p);
    }

    public Task<List<Produto>> Update(Produto p)
    {
        var sql = "UPDATE Produto SET Categoria=?, Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
        
        return _con.QueryAsync<Produto>(
            sql, p.Categoria, p.Descricao, p.Quantidade, p.Preco, p.Id
        );
    }

    public Task<int> Delete(int id)
    {
        return _con.Table<Produto>().DeleteAsync(i => i.Id == id);
    }

    public Task<List<Produto>> GetAll()
    {
        return _con.Table<Produto>().ToListAsync();
    }

    public Task<List<Produto>> Search(string query)
    {
        var sql = "SELECT * FROM Produto WHERE Descricao LIKE '%"+query+"%'";
        return _con.QueryAsync<Produto>(sql);
    }
    
    public Task<List<Produto>> SearchByCat(string q)
    {
        var sql = "SELECT * FROM Produto WHERE Categoria = '"+q+"'";
        return _con.QueryAsync<Produto>(sql);
    }
    
    public Task<List<dynamic>> ShowCats()
    {
        var sql = "SELECT Categoria FROM Produto GROUP BY Categoria";
        return _con.QueryAsync<dynamic>(sql);
    }
    
    public Task<List<Produto>> GetCat(string categoria)
    {
        string sql = "SELECT * FROM Produto WHERE Categoria = ?";
        return _con.QueryAsync<Produto>(sql, categoria);
    }
}