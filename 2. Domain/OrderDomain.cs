using _3._Data;
using _4._Shared;

namespace _2._Domain;

public class OrderDomain : IOrderDomain
{
    private IOrderData _orderData;
    public OrderDomain(IOrderData orderData)
    {
        _orderData = orderData;
    }
    public async Task<Boolean> SaveAsync(Order data)
    {
        // Get the number of orders the client has made today
        var ordersToday = await _orderData.GetOrdersToday(data.ClientId);
        if (ordersToday >= CONSTANTS.MAX_ORDERS_PER_DAY)
            throw new Exception("Client has reached the maximum number of orders for today");

        return await _orderData.SaveAsync(data);
    }
    
    public async Task<Boolean> UpdateAsync(Order data,int id)
    {
        var orderExts = await _orderData.GetByIdAsync(id);
        
        if (orderExts.ClientId != data.ClientId)
            throw new Exception("Update ClientId is not allowed");
        
        return await _orderData.UpdateAsync(data,id);
    }
    
    public  async Task<Boolean> DeleteAsync(int id)
    {
        return await _orderData.DeleteAsync(id);
    }
}