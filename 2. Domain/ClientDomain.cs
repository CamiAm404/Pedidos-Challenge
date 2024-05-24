using _3._Data;
using _3._Data.Models;
using _4._Shared;

namespace _2._Domain;

public class ClientDomain : IClientDomain
{
    private IClientData _clientData;
    public ClientDomain(IClientData clientData)
    {
        _clientData = clientData;
    }
    public async Task<Boolean> SaveAsync(Client data)
    {
        //Bussiness rules
        var clientExts = await _clientData.GetByEmailAsync(data.Email);
        if (clientExts != null)
            throw new Exception("Email already registered");
        
        return await _clientData.SaveAsync(data);
    }
    
    public async Task<Boolean> UpdateAsync(Client data,int id)
    {
        var clientExts = await _clientData.GetByIdAsync(id);
        
        //Bussiness rules
        return  await _clientData.UpdateAsync(data,id);
    }
    
    public  async Task<Boolean> DeleteAsync(int id)
    {
        //Bussiness rules
        return await _clientData.DeleteAsync(id);
    }
}