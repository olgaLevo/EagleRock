using EagleRock.Api.Core;
using EagleRock.Api.Data.Helpers;
using EagleRock.Api.Data.Interfaces;
using EagleRock.Api.Models.Input;
using EagleRock.Api.Models.Output;
using Microsoft.AspNetCore.Mvc;

namespace EagleRock.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EagleBotController : ControllerBase
    {
        private readonly IEagleBotRepository _eagleBotRepository;

        public EagleBotController(IEagleBotRepository eagleBotRepository)
        {
            _eagleBotRepository = eagleBotRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EagleBotOutputModel[]>> GetAll()
        {
            var records = await _eagleBotRepository.GetAllAsync();

            return EagleBotHelper.ToEagleBotOutputModel(records);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(EagleBootInputItem model)
        {
            var record = model.ToEagleBootRecord();

            var addResult = await _eagleBotRepository.AddAsync(record);

            if (addResult.IsSuccess)
            {
                return StatusCode(StatusCodes.Status201Created);
            }

            if (addResult.IsDuplicate)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            ModelState.AddValidationResultErrors(addResult.ValidationResult.Errors);

            return ValidationProblem();
        }
    }
}
