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

        [Route("api/entries")]
        [HttpGet]
        public Task<Page<Entry>> GetEntries([FromUri]int index = 0, [FromUri]int maxSize = 20, [FromUri]string search = null)
        {
            return business.GetEntries(index, maxSize);
        }
    }
}
