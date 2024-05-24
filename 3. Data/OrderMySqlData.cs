using _3._Data.Context;
using Microsoft.EntityFrameworkCore;

namespace _3._Data;

public class OrderMySqlData : IOrderData
{
    private OrdersDBContext _ordersDbContext;
    public OrderMySqlData(OrdersDBContext ordersDbContext)
    {
        _ordersDbContext = ordersDbContext;
    }
    public async Task<Boolean> SaveAsync(Order data)
    {
        data.IsActive = true;

        using (var transaction = await _ordersDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                _ordersDbContext.Orders.Add(data);
                await _ordersDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        return true;
    }
    
    public async Task<Boolean> UpdateAsync(Order data,int id)
    {
        using (var transaction = await _ordersDbContext.Database.BeginTransactionAsync())
        {
            var orderToUpdate = _ordersDbContext.Orders.Where(t => t.Id == id).FirstOrDefault();
            orderToUpdate.ClientId = data.ClientId;
            orderToUpdate.CreatedDate = data.CreatedDate;
            orderToUpdate.Amount = data.Amount;
            
            _ordersDbContext.Orders.Update(orderToUpdate);
            await _ordersDbContext.SaveChangesAsync(); 
            await transaction.CommitAsync();
        }

        return true;
    }
    
    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _ordersDbContext.Database.BeginTransactionAsync())
        {
            var orderToDelete = _ordersDbContext.Orders.Where(t => t.Id == id).FirstOrDefault();
            orderToDelete.IsActive = false;
            await _ordersDbContext.SaveChangesAsync(); 
            await transaction.CommitAsync();
        }

        return true;
    }
    
    public async Task<List<Order>> getAllAsync()
    {
        return await _ordersDbContext.Orders.Where(t=>t.IsActive).ToListAsync();
    }
    
    public async Task<Order> GetByIdAsync(int Id)
    {
        return await _ordersDbContext.Orders.Where(t => t.Id == Id).FirstOrDefaultAsync();
    }
    
    public async Task<int> GetOrdersToday(int clientId)
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        
        return await _ordersDbContext.Orders
            .Where(o => o.ClientId == clientId && o.CreatedDate >= today && o.CreatedDate < tomorrow)
            .CountAsync();
        //return await _ordersDbContext.Orders.Where(t => t.ClientId == clientId && t.CreatedDate.Date == DateTime.Now.Date).CountAsync();
    }
}