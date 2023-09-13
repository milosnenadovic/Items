using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Items.Shared.Response;
using MapsterMapper;
using MediatR;

namespace Items.Controllers.Base;

public class BaseController : Controller
{
    protected readonly ISender Mediator;
    protected readonly IMapper Mapper;

    protected BaseController(ISender mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (Request.Headers.ContainsKey("language-code"))
        {
            var languageCode = Request.Headers["language-code"].ToString();
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo(languageCode);
        }

        base.OnActionExecuting(context);
    }

    protected IActionResult ConvertToResponse<T>(IResponse<T> response)
    {
        if (response.IsSuccess)
            return Ok(response.Result);

        return BadRequest(new ErrorApiResponse()
        {
            Title = response.Message ?? string.Empty,
            Detail = response.DetailedMessage ?? string.Empty,
            ErrorCode = response.ErrorCode,
            ErrorCodes = new() { new() { Code = Enum.GetName(typeof(ErrorCodes), response.ErrorCode ?? 0) ?? string.Empty, Description = response.Message ?? string.Empty } }
        });
    }
}
