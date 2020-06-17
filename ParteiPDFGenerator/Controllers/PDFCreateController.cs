using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudbobsPDFRendering.PDFCreators;
using CloudbobsPDFRendering.PDFCreators.Trip;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudbobsPDFRendering.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PDFCreateController : ControllerBase
    {
        private readonly ILogger<PDFCreateController> _logger;
        public PDFCreateController(ILogger<PDFCreateController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateMemberListPDF")]
        public async Task<IActionResult> CreateMemberListPDF(MemberListPDFModel model)
        {
            var result = await MemberListPDFCreator.Create(model);

            if(result.Successfull)
            {
                return Ok(result.PayLoad);
            }
            else
            {
                return BadRequest(result.PayLoad);
            }
        }

        [HttpPost]
        [Route("CreateTripPDF")]
        public async Task<IActionResult> CreateTripPDF(TripPDFModel model)
        {
            var result = await TripPDFCreator.Create(model);

            if (result.Successfull)
            {
                return Ok(result.PayLoad);
            }
            else
            {
                return BadRequest(result.PayLoad);
            }
        }

        /**
         * Dummy-Model creators.
         */

        [HttpGet]
        [Route("GetMemberListPDFDummyModel")]
        public IActionResult GetMemberListPDFDummyModel()
        {
            var members = new List<MemberListMemberPDFModel>();

            for(int i=0; i<10; i++)
            {
                members.Add(new MemberListMemberPDFModel
                {
                    PreName = "Moritz",
                    LastName = "Schmidt",
                    Address = "Domänenstraße 16",
                    City = "Tettnang",
                    Contribution = 99.12,
                    DateOfBirth = new DateTime(1995, 11, 15),
                    Postal = "88069"
                });
            }

            return Ok(new MemberListPDFModel
            {
                Members = members
            });
        }

        [HttpGet]
        [Route("GetTripPDFDummyModel")]
        public IActionResult GetTripPDFDummyModel()
        {
            var members = new List<TravelMemberPDFModel>();

            for (int i = 0; i < 10; i++)
            {
                members.Add(new TravelMemberPDFModel
                {
                    PreName = "Moritz",
                    LastName = "Schmidt",
                    City = "Tettnang",
                    ActualCosts = 12.11,
                    TargetCosts = 9.33,
                    Stop = "Friedrichshafen"
                });
            }

            return Ok(new TripPDFModel
            {
                Destination = "Konstanz",
                Description = "Freiburg im Breisgau, eine Universitätsstadt im Schwarzwald im Südwesten Deutschlands," +
                "ist für sein mildes Klima und das wiederaufgebaute, von kleinen Bächen durchzogene" +
                "mittelalterliche Stadtzentrum bekannt.",
                Costs = 900.12,
                StartDate = DateTime.Now.AddDays(-2),
                EndDate = DateTime.Now.AddDays(5),
                ImageBlobURL = "https://cloudbobstorage.blob.core.windows.net/images/freiburg.jfif",
                Members = members
            });
        }
    }
}
