using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Models
{
    public class EditProposalModel
    {
        public ProposerDetailModel proposerDetailModel { get; set; }
        public List<AnimalDatum> animalData { get; set; }
        public List<ProposalImage> proposalImages { get; set; }
    }
    public class ProposerDetailModel
    {
        public int Propid { get; set; }
        public int AnimalDataID { get; set; }
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
        public string CustomerGender { get; set; }
        public List<string> AnimalDataList { get; set; }
    }
    public class AnimalDatum
    {
        public int Propid { get; set; }
        public int AnimalDataID { get; set; }
        public string Breadtype { get; set; }
        public string TypeofAnimal { get; set; }
        public string Category { get; set; }
        public string Species { get; set; }
        public string Sex { get; set; }
        public string BreedName { get; set; }
        public string BodyColour { get; set; }
        public string SwitchOfTail { get; set; }
        public string ApproxAgeOfAnimal { get; set; }
        public string MilkingStatus { get; set; }
        public string MilkYield { get; set; }
        public string PregnancyStatus { get; set; }
        public string OtherIdentificationMark { get; set; }
        public string Vaccination { get; set; }
        public string DrContactName { get; set; }
        public string DrContactNo { get; set; }
        public string RegistrationNo { get; set; }
        public string HealthCertificateIssueDate { get; set; }
        public string TagNo { get; set; }
        public string TypeTagging { get; set; }
        public string Ownership { get; set; }
        public string PaymentRefNo { get; set; }
        public string PremiumRate { get; set; }
        public string SumInsured { get; set; }
        public string PremiumAmount { get; set; }
        public string CentralShareAmount { get; set; }
        public string StateShareAmount { get; set; }
        public string TotalGovtShareAmount { get; set; }
        public string BeneficiaryContributionAmount { get; set; }
    }

    public class ProposalImage
    {
        public int ImageId { get; set; }
        public int PropId { get; set; }
        public string ImageName { get; set; }
        public string FileType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CreatedDate { get; set; }
        public string TimeStamp { get; set; }
        public string ImageCapson { get; set; }
        public string CompassDegrees { get; set; }
        public string UpdatedDate { get; set; }
        public string IsDeleted { get; set; }
        public string NewFileName { get; set; }
    }

}
