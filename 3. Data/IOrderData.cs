using _3._Data.Models;

namespace _3._Data;

public interface IOrderData
{
    Task<Boolean> SaveAsync(Order data);
    Task<Boolean> UpdateAsync(Order data,int id);
    Task<Boolean> DeleteAsync(int id);
    Task<List<Order>> getAllAsync();
    Task<Order> GetByIdAsync(int Id);
    Task<int> GetOrdersToday(int clientId);
}