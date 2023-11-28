using ApplogiqTask.DataContext;
using ApplogiqTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ApplogiqTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EtherTransactionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _dbContext;
        private RestClient _restClient = new();
        public EtherTransactionController(AppDBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpGet("GetTransactionData")]
        public async Task<ActionResult> GetTransactionData([FromQuery] string address)
        {
            try
            {
                string etherURL = $"{_configuration.GetValue<string>("EtherScanSite")}&address={address}&apikey={_configuration.GetValue<string>("EtherAPIKey")}";

                RestRequest request = new RestRequest(etherURL, Method.Get);
                RestResponse response = await _restClient.ExecuteAsync(request);

                var etherResult = JsonConvert.DeserializeObject<EtherTransactionResponseData>(response.Content);
                string userResult = string.Empty;

                if (etherResult.result.Count() != 0)
                {
                    userResult += "{\"Result\":[";
                    foreach (var item in etherResult.result)
                    {
                        userResult += JsonConvert.SerializeObject(item) + ",";
                    }
                    userResult = userResult.Substring(0, userResult.Length - 1);
                    userResult += "]}";
                    var check = userResult;
                }
                EtherTransactionData data = new()
                {
                    Id = etherResult.Id,
                    Status = etherResult.status,
                    Message = etherResult.message,
                    SerializedResults = userResult
                };
                await _dbContext.EtherTransactionData.AddAsync(data);
                _dbContext.SaveChanges();

                return Ok(etherResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTransactions/{id:int}")]
        public ActionResult GetTransactions([FromRoute] int id)
        {
            try
            {
                var response = _dbContext.EtherTransactionData.Where(y => y.Id.Equals(id)).FirstOrDefault();
                if (response != null)
                {
                    var etherResult = JsonConvert.DeserializeObject<EtherTransactionRetrieveData>(response.SerializedResults);
                    return Ok(etherResult);
                }
                return NotFound();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
