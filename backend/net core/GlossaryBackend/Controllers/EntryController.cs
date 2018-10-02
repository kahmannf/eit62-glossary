using GlossaryDefinition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GlossaryBackend.Controllers
{
    public class EntryController : ApiController
    {
        readonly IEntryBusiness business = WebApiApplication.GetEntryBusiness();

        [Route("api/entry/all")]
        [HttpGet]
        public Task<Page<Entry>> GetEntries([FromUri]int pageNumber = 0, [FromUri]int pageSize = 20)
        {
            return business.GetEntries(pageNumber, pageSize);
        }
    }
}
