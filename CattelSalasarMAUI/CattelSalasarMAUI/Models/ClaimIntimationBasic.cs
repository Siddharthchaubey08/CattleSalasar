using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Models
{
    public class ClaimIntimationBasic
    {
        public ProposerData ProposerData { get; set; }
        public List<object> Cattle { get; set; }
        public Premium Premium { get; set; }
    }
    public class Premium
    {
        public int SeqNo { get; set; }
        public string PropID { get; set; }
        public string QuoteId { get; set; }
        public string LeadNumber { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyMode { get; set; }
        public string PolicyType { get; set; }
        public string PolicyIssueDate { get; set; }
        public string PolicyStrDate { get; set; }
        public string PolicyEndDate { get; set; }
        public string SumInsured { get; set; }
       // public string Premium { get; set; }
        public string TotalPremium { get; set; }
        public string CreateBy { get; set; }
        public string CreateDtim { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDtim { get; set; }
        public string IGST { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string UGST { get; set; }
    }

    public class ProposerData
    {
        public int SeqNo { get; set; }
        public string Propid { get; set; }
        public string Quoteid { get; set; }
        public string PolicyNo { get; set; }
        public string LeadNumber { get; set; }
        public string NameOfBank { get; set; }
        public string LoanAccNo { get; set; }
        public string SurveyDate { get; set; }
        public string SurveyTime { get; set; }
        public string NoOfCattlesFunded { get; set; }
        public string LoanAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDtim { get; set; }
        public string UpdateDtim { get; set; }
        public string UpdateBy { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Remark { get; set; }
        public string InspectionDate { get; set; }
        public string InspectedBy { get; set; }
        public string InpectionType { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string AadharNumber { get; set; }
        public string TypeofAnimal { get; set; }
        public string Species { get; set; }
        public string Age { get; set; }
        public string BodyColour { get; set; }
        public string Horns { get; set; }
        public string SwitchOfTail { get; set; }
        public string ApproxAgeOfAnimal { get; set; }
        public string MilkYield { get; set; }
        public string OtherIdentificationMark { get; set; }
        public string DrContactNo { get; set; }
        public string DrContactName { get; set; }
        public string RegistrationNo { get; set; }
        public string HealthCertificateIssueDate { get; set; }
        public string TagNo { get; set; }
        public string DateOfLastCalving { get; set; }
        public string SymptomsObservedWithDate { get; set; }
        public string DateOfDeath { get; set; }
        public string TimeOfDeath { get; set; }
        public string CauseOfDeath { get; set; }
        public string Category { get; set; }
        public string Sex { get; set; }
        public string Breadtype { get; set; }
        public string MilkingStatus { get; set; }
        public string PregnancyStatus { get; set; }
        public string Vaccination { get; set; }
        public string Tagging { get; set; }
        public string TaggingDate { get; set; }
        public string TypeTagging { get; set; }
        public string ChipUsedForIdentification { get; set; }
        public string SumInsured { get; set; }
        public string TagCondition { get; set; }
        public string FatherHusbandName { get; set; }
        public string CustomerDOB { get; set; }
        public string CustomerMobile { get; set; }
        public string Block { get; set; }
        public string PostOffice { get; set; }
        public string CustomerAge { get; set; }
        public string BreedName { get; set; }
        public string NumberOfCattleToBeInsured { get; set; }
        public string PremiumRate { get; set; }
        public string PremiumAmount { get; set; }
        public string Ownership { get; set; }
        public Premium Premium { get; set; }
    }

}
