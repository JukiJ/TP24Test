using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TP24.Core.Dtos;
using TP24.Core.Interfaces.Services;

namespace TP24.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceivablesController : ControllerBase
    {
        private readonly IReceivableService _receivableService;

        public ReceivablesController(IReceivableService receivableService)
        {
            _receivableService = receivableService;
        }


        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var summary = await _receivableService.GetReceivableSummary();

            return Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Post(List<CreateReceivableDto> receivables)
        {
            return Ok(await _receivableService.AddReceivablesAsync(receivables));
        }

    }
}
