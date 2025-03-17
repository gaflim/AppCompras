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
        string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
        
        return _con.QueryAsync<Produto>(
            sql, p.Descricao, p.Quantidade, p.Preco, p.Id
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
        string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%"+query+"%'";
        return _con.QueryAsync<Produto>(sql);
    }
}