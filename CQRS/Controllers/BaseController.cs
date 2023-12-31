using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CQRS.Controllers;

[ApiController]
public abstract class BaseController(IMediator mediator) : ControllerBase
{
    protected readonly IMediator Mediator = mediator;
}