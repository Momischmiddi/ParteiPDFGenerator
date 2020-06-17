using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudbobsPDFRendering.PDFCreators.Trip
{
    public class TripPDFModel
    {
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public double Costs { get; set; }
        public string ImageBlobURL { get; set; }
        public List<TravelMemberPDFModel> Members { get; set; }
    }
}
