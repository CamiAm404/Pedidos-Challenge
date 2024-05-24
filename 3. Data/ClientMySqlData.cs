using _3._Data.Context;
using _3._Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3._Data;

public class ClientMySqlData :IClientData
{
    private OrdersDBContext _ordersDbContext;
    public ClientMySqlData (OrdersDBContext ordersDbContext)
    {
        _ordersDbContext = ordersDbContext;
    }
    public async Task<Boolean> SaveAsync(Client data)
    {
        data.IsActive = true;
    
        using (var transaction = await _ordersDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                _ordersDbContext.Clients.Add(data);
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
    
    public async Task<Boolean> UpdateAsync(Client data,int id)
    {

            using (var transaction = await _ordersDbContext.Database.BeginTransactionAsync())
            {
                var clientTpUpdate = _ordersDbContext.Clients.Where(t => t.Id == id).FirstOrDefault();
                clientTpUpdate.Name = data.Name;
                clientTpUpdate.Email = data.Email;

                _ordersDbContext.Clients.Update(clientTpUpdate);
                await _ordersDbContext.SaveChangesAsync(); // confirmar cambios
                await transaction.CommitAsync();
            }

        return true;
        
    }
    
    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _ordersDbContext.Database.BeginTransactionAsync())
        {
            var clientToDelete = _ordersDbContext.Clients.Where(t => t.Id == id).FirstOrDefault();
            clientToDelete.IsActive = false;
            await _ordersDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        return true;
    }
    
    public async Task<List<Client>> getAllAsync()
    {
        return await _ordersDbContext.Clients.Where(t=>t.IsActive).ToListAsync();
    }
    
    public async Task<Client> GetByIdAsync(int Id)
    {
        return await _ordersDbContext.Clients.Where(t=>t.Id == Id).FirstOrDefaultAsync();
    }
    
    public async Task<Client> GetByEmailAsync(string Email)
    {
        return await _ordersDbContext.Clients.Where(t=>t.Email == Email).FirstOrDefaultAsync();
    }
}