using Exchange.Api.Common.Models;
using Exchange.Application.Commands.Conversion;
using Exchange.Application.Common.Dto;
using Exchange.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Net;

namespace Exchange.Api.Controllers
{

    public class ExchangeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ExchangeController> _logger;

        public ExchangeController(IMediator mediator, ILogger<ExchangeController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [Authorize]
        [RequiredScope("WriteApis")]
        [HttpPost]
        [ProducesResponseType(typeof(List<CurrencyResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("api/exchange")]
        public async Task<IActionResult> GetPriceConversion([FromBody] ConversionCommand command)
        {
            _logger.LogInformation("ConversionCommand was received.");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [RequiredScope("ReadApis", "WriteApis")]
        [HttpGet]
        [ProducesResponseType(typeof(List<ConversionHistoryResponse>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("api/exchange")]
        public async Task<IActionResult> GetConversionHistory()
        {
            _logger.LogInformation("ConversionHistoryQuery was received.");

            var result = await _mediator.Send(new ConversionHistoryQuery());
            return Ok(result);
        }
    }
}
