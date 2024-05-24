using _3._Data.Models;

namespace _3._Data;

public interface IClientData
{
    Task<Boolean> SaveAsync(Client data);
    Task<Boolean> UpdateAsync(Client data,int id);
    Task<Boolean> DeleteAsync(int id);
    Task<List<Client>> getAllAsync();
    Task<Client> GetByIdAsync(int id);
    Task<Client> GetByEmailAsync(string email);
}