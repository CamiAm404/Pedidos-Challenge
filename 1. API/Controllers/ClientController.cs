using _1._API.Request;
using _2._Domain;
using _3._Data;
using _3._Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace _1._API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientData _clientData;
        private readonly IClientDomain _clientDomain;
        private readonly IMapper _mapper;
        
        public ClientController(IClientData clientData,IClientDomain clientDomain,IMapper mapper)
        {
            _clientData = clientData;
            _clientDomain = clientDomain;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult>  GetAsync()
        {
            var data = await _clientData.getAllAsync();
            var result = _mapper.Map<List<Client>,List<ClientResponse>>(data);
            return Ok(result);
        }
        
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            var data = await _clientData.GetByIdAsync(id);
            var result = _mapper.Map<Client,ClientResponse>(data);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ClientRequest  data)
        {
            try
            {
                var client = _mapper.Map<ClientRequest, Client>(data);
                var result = await _clientDomain.SaveAsync(client);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id,[FromBody] ClientRequest  data)
        {
            try
            {
                var client = _mapper.Map<ClientRequest, Client>(data);
                var result = await _clientDomain.UpdateAsync(client,id);
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
            await _clientDomain.DeleteAsync(id);
            
            return Ok();
        }
    }
}