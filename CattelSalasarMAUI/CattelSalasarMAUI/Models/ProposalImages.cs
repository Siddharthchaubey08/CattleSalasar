using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CattelSalasarMAUI.Models
{
    public  class ProposalImages
    {
        [PrimaryKey, AutoIncrement]
        public int ImageId { get; set; }
        public int PropId { get; set; }
        public string ImgesPath { get; set; }
        public string ImageName { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string CreatedDate { get; set; }
        public string TimeStamp { get; set; }
        public string ImageCapson { get; set; }
        public string CustomerMobileNo { get; set; }
        public string CompassDegrees { get; set; }

        public ProposalImages()
        {
        }
    }
}
