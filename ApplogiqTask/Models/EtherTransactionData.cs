using Newtonsoft.Json;

using System.ComponentModel.DataAnnotations;

namespace ApplogiqTask.Models
{
    public class EtherTransactionData
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string SerializedResults { get; set; }

        //public List<TransactionResult> GetResults()
        //{
        //    return JsonConvert.DeserializeObject<List<TransactionResult>>(SerializedResults);
        //}

        //public void SetResults(List<TransactionResult> results)
        //{
        //    SerializedResults = JsonConvert.SerializeObject(results);
        //}
    }

    //public class TransactionResult
    //{
    //    public string BlockNumber { get; set; }
    //    public string TimeStamp { get; set; }
    //    public string Hash { get; set; }
    //    public string Nonce { get; set; }
    //    public string BlockHash { get; set; }
    //    public string TransactionIndex { get; set; }
    //    public string From { get; set; }
    //    public string To { get; set; }
    //    public string Value { get; set; }
    //    public string Gas { get; set; }
    //    public string GasPrice { get; set; }
    //    public string IsError { get; set; }
    //    public string TxReceiptStatus { get; set; }
    //    public string Input { get; set; }
    //    public string ContractAddress { get; set; }
    //    public string CumulativeGasUsed { get; set; }
    //    public string GasUsed { get; set; }
    //    public string Confirmations { get; set; }
    //    public string MethodId { get; set; }
    //    public string FunctionName { get; set; }
    //}
}
