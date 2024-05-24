using _3._Data.Models;

namespace _2._Domain;

public interface IClientDomain
{
    Task<Boolean> SaveAsync(Client data);
    Task<Boolean> UpdateAsync(Client data,int id);
    Task<Boolean> DeleteAsync(int id);
}