using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Models
{
    public class ClaimIntimationModel
    {
        [PrimaryKey, AutoIncrement]
        public int ClaimIntimationId { get; set;}
        public string SeqNo { get; set; }
        public string LeadNumber { get; set; }
        public string DateOfDeath { get; set; }
        public string TimeOfDeath { get; set; }
        public string CauseOfDeath { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryMobileNo { get; set; }
        public string CustomerBankName { get; set;}
        public string CustomerIFSCCode { get; set;}
        public string CustomerAccountNumber { get; set;}
        public string CustomerPanCard { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyDate { get; set; }
        public string AadharNumber { get; set; }  //Add Exectra

    }

    public class ClaimIntimationResponceModel
    {
        public string PropId { get; set; }
        public string QuoteId { get; set; }
        public string LeadNumber { get; set; }
        public double SumInsured { get; set; }
        public double Premium { get; set; }
        public string TotalPremium { get; set; }
    }

}
