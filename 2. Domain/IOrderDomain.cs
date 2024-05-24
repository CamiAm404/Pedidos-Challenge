using _3._Data;

namespace _2._Domain;

public interface IOrderDomain
{
    Task<Boolean> SaveAsync(Order data);
    Task<Boolean> UpdateAsync(Order data,int id);
    Task<Boolean> DeleteAsync(int id);
}