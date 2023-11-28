using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicBankAPITests.Models
{
    public class BankingModels
    {

    }
    public class createAccountRequest
    {
        public double InitialBalance { get; set; }
        public string AccountName { get; set; }
        public string Address { get; set; }
    }
    public class createAccountResponse
    {
        public decimal newBalance { get; set; }
        public string accountName { get; set; }
        public string accountNumber { get; set; }
        public string message { get; set; }

    }
    public class depositAccountRequest
    {
        public decimal amount { get; set;}
        public string accountNumber { get; set; }

    }
    public class depositAccountResponse
    { 
        public string accountId { get; set; }
        public decimal newBalance { get; set; }
        public string message { get; set; }


    }

}
