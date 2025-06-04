using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Models
{
    public class ProposalBasicDetailModel
    {
        public int Propid { get; set; }
        public int ProposalId { get; set; }
        public string Quoteid { get; set; }
        public string PolicyNo { get; set; }
        public string LeadNumber { get; set; }
        public string NameOfBank { get; set; }
        public string LoanAccNo { get; set; }
        public string SurveyDate { get; set; }
        public string SurveyTime { get; set; }
        public string LoanAmount { get; set; }
        public string CustomerName { get; set; }
        public string CustomerGender { get; set; }
        public string CustomerAddress { get; set; }
        public string CreateBy { get; set; }
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
        public string Age { get; set; }
        public string Horns { get; set; }
        public string MilkYield { get; set; }
        public string DrContactNo { get; set; }
        public string DrContactName { get; set; }
        public string RegistrationNo { get; set; }
        public string HealthCertificateIssueDate { get; set; }
        public string DateOfDeath { get; set; }
        public string TimeOfDeath { get; set; }
        public string CauseOfDeath { get; set; }
        public string Tagging { get; set; }
        public string TaggingDate { get; set; }
        public string FatherHusbandName { get; set; }
        public string CustomerDOB { get; set; }
        public string CustomerMobile { get; set; }
        public string Block { get; set; }
        public string PostOffice { get; set; }
        public string CustomerAge { get; set; }
        public string GramPanchayat { get; set; }
        public string NoOfCattlesFunde { get; set; }
        public string NumberOfCattleToBeInsured { get; set; }
        public string SchemeID { get; set; }
        public string SumInsured { get; set; }
        public string CentralShareAmount { get; set; }
        public string StateShareAmount { get; set; }
        public string TotalGovtShareAmount { get; set; }
        public string BeneficiaryContributionAmount { get; set; }
        public string PaymentMode { get; set; }
        public string PolicyPeriod { get; set; }
        public string PremiumRate { get; set; }
        public string PremiumAmount { get; set; }
      
    }

    public class CreateProposalResult
    {
        public string PropId { get; set; }
        public string QuoteId { get; set; }
        public string LeadNumber { get; set; }
        public string SumInsured { get; set; }
        public string Premium { get; set; }
        public string TotalPremium { get; set; }
        public string ReturnCattleToBeInsured { get; set; }
    }
    public class BoState
    {
        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string CountryId { get; set; }
        public string StateName { get; set; }
        public string StateType { get; set; }
        public string USGICode { get; set; }

    }

    public class BoDistrict
    {
        public int DistrictId { get; set; }
        public string StateCode { get; set; }
        public string DistrictName { get; set; }
        public string FarmerPremiumPercentage { get; set; }
        public string GovPremiumPercentage { get; set; }

    }

    public class BoTehsil
    {
        public int TehsilId { get; set; }
        public string DistrictCode { get; set; }
        public string TehsilName { get; set; }

    }

    public class BoBlock
    {
        public int HobliId { get; set; }
        public string TehsilCode { get; set; }
        public string HobliName { get; set; }

    }
    public class BoCity
    {
        public int CityId { get; set; }
        public string StateCode { get; set; }
        public string CityName { get; set; }

    }

    public class BreadtypeList
    {
        public int SeqNo { get; set; }
        public string BreedCode { get; set; }
        public string BreedName { get; set; }
    }
    public class SpeciesList
    {
        public int SeqNo { get; set; }
        public string CattleCode { get; set; }
        public string CattleName { get; set; }
    }
    public class GetAllSchemeModel
    {
        public int SchemeID { get; set; }
        public string SchemeName { get; set; }
    }
    public class GetStateWiseSchemesModel
    {
        public int CentralShare { get; set; }
        public int StateShare { get; set; }
        public int TotalGovtShare { get; set; }
        public int BeneficiaryContribution { get; set; }
        // public string StateShare { get; set; }
        //  public string BeneficiaryContribution { get; set; }
    }

}
