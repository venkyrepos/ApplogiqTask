using ApplogiqTask.DataContext;
using ApplogiqTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        //[HttpGet("GetTransactionData")]
        //public async Task<ActionResult> GetTransactionData([FromQuery] string address)
        //{
        //    EtherTransactionData data = new();
        //    string userResult = string.Empty;

        //    try
        //    {
        //        string etherURL = $"{_configuration.GetValue<string>("EtherScanSite")}&address={address}&apikey={_configuration.GetValue<string>("EtherAPIKey")}";

        //        RestRequest request = new RestRequest(etherURL, Method.Get);
        //        RestResponse response = await _restClient.ExecuteAsync(request);


        //        if (response.Content != null)
        //        {
        //            //if (content["result"] is Result[])
        //            //{
        //            var etherResult = JsonConvert.DeserializeObject<dynamic>(response.Content);
        //            var statusValue = etherResult.result.First.result;
        //            if (etherResult.result.Count() != 0)
        //            {
        //                userResult += "{\"Result\":[";
        //                foreach (var item in etherResult.result)
        //                {
        //                    userResult += JsonConvert.SerializeObject(item) + ",";
        //                }
        //                userResult = userResult.Substring(0, userResult.Length - 1);
        //                userResult += "]}";
        //            }
        //            //}
        //            //else
        //            //{
        //            //    userResult = content["result"].ToString();
        //            //}

        //            //data.Id = Convert.ToInt32(content["id"]);
        //            //data.Status = content["status"].ToString();
        //            //data.Message = content["message"].ToString();
        //            //data.SerializedResults = userResult;
        //        }
        //        await _dbContext.EtherTransactionData.AddAsync(data);
        //        _dbContext.SaveChanges();

        //        return Ok(response.Content);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("GetTransactionData")]
        public async Task<ActionResult> GetTransactionData([FromQuery] string address)
        {
            try
            {
                EtherTransactionResponseData data = new();
                string etherURL = $"{_configuration.GetValue<string>("EtherScanSite")}&address={address}&apikey={_configuration.GetValue<string>("EtherAPIKey")}";

                RestRequest request = new RestRequest(etherURL, Method.Get);
                RestResponse response = await _restClient.ExecuteAsync(request);

                var etherResult = JsonConvert.DeserializeObject<EtherTransactionResponseData>(response.Content);
                var responseResult = HandleResponse(etherResult);
                data.serializedTransaction += "{\"Result\":[";
                foreach (var item in responseResult.result)
                {
                    data.serializedTransaction += JsonConvert.SerializeObject(item) + ",";
                }
                data.serializedTransaction = data.serializedTransaction.Substring(0, data.serializedTransaction.Length - 1);
                data.serializedTransaction += "]}";
                data.status = responseResult.status;
                data.message = responseResult.message;

                await _dbContext.EtherTransactionResponseData.AddAsync(data);
                _dbContext.SaveChanges();

                return Ok(responseResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        static dynamic HandleResponse(EtherTransactionResponseData response)
        {
            var data = new
            {
                status = response.status,
                message = response.message,
                result = response.result is JArray ?
                JsonConvert.DeserializeObject<ResultData[]>(response.result.ToString()) :
                response.result
            };

            return data;
        }

        [HttpGet("GetTransactions/{id:Guid}")]
        public ActionResult GetTransactions([FromRoute] Guid id)
        {
            try
            {
                var response = _dbContext.EtherTransactionResponseData.Where(y => y.Id.Equals(id)).FirstOrDefault();
                if (response != null)
                {
                    var etherResult = JsonConvert.DeserializeObject<Result>(response.serializedTransaction);
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