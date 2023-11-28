﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ApplogiqTask.Models
{
    public class EtherTransactionResponseData
    {
        [Key]
        public int Id { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public Result[] result { get; set; }
    }
    public class EtherTransactionRetrieveData
    {
        public Result[] result { get; set; }
    }

    public class Result
    {
        [Key]
        public int Id { get; set; }
        public string blockNumber { get; set; }
        public string timeStamp { get; set; }
        public string hash { get; set; }
        public string nonce { get; set; }
        public string blockHash { get; set; }
        public string transactionIndex { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string value { get; set; }
        public string gas { get; set; }
        public string gasPrice { get; set; }
        public string isError { get; set; }
        public string txreceipt_status { get; set; }
        public string input { get; set; }
        public string contractAddress { get; set; }
        public string cumulativeGasUsed { get; set; }
        public string gasUsed { get; set; }
        public string confirmations { get; set; }
        public string methodId { get; set; }
        public string functionName { get; set; }
    }
}
