using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Models
{
    public class UserRegisterModel
    {
        public int id { get; set; }
        public string userID { get; set; }
        public string userName { get; set; }
        public string department { get; set; }
        public string email { get; set; }
        public string phoneNo { get; set; }
        public string mobile { get; set; }
        public string password { get; set; }

        public string State { get; set; }
        public string District { get; set; }
        public string AadhaarNumber { get; set; }
        public string PanCardNo { get; set; }
        public string Bank { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string SelfPhotoPath { get; set; }
        public string AadharPhotoPath { get; set; }
        public string PanPhotoPath { get; set; }
        public string PassbookChequePath { get; set; }
        public string imeiNo { get; set; }
    }
}
