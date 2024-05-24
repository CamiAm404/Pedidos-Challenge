using _1._API.Request;
using _2._Domain;
using _3._Data;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1._API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderData _orderData;
        private readonly IOrderDomain _orderDomain;
        private readonly IMapper _mapper;
    
        public OrderController(IOrderData orderData,IOrderDomain orderDomain,IMapper mapper)
        {
            _orderData = orderData;
            _orderDomain = orderDomain;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult>  GetAsync()
        {
            var data = await _orderData.getAllAsync();
            var result = _mapper.Map<List<Order>,List<OrderResponse>>(data);
            return Ok(result);
        }
        
        [HttpGet("{id}", Name = "GetOrder")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _orderData.GetByIdAsync(id);
            var result = _mapper.Map<Order,OrderResponse>(data);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] OrderRequest data)
        {
            try
            {
                var order = _mapper.Map<OrderRequest, Order>(data);
                var result = await _orderDomain.SaveAsync(order);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromBody] OrderRequest data)
        {
            try
            {
                var order = _mapper.Map<OrderRequest, Order>(data);
                var result = await _orderDomain.UpdateAsync(order,id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _orderDomain.DeleteAsync(id);
            return Ok();
        }
    }
}