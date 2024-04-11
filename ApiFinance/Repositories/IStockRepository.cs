using ApiFinance.Dtos.Stock;
using ApiFinance.Helpers;
using ApiFinance.Models;
using Mysqlx.Crud;

namespace ApiFinance.Repositories
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetStocksAsync(QueryObject query);
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequest dtoStock);
        Task<Stock?> DeleteAsync(int id);
        Task<bool> StockExists(int id);
    }
}
