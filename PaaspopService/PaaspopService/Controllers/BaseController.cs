using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PaaspopService.Common.DictionaryAsArrayResolver;

namespace PaaspopService.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        protected IMediator Mediator;
        protected JsonSerializerSettings JsonDictionaryAsArrayResolver;

        public BaseController()
        {
            Mediator = Mediator ?? (Mediator = HttpContext.RequestServices.GetService<IMediator>());
            JsonDictionaryAsArrayResolver = new JsonSerializerSettings
            {
                ContractResolver = new DictionaryAsArrayResolver()
            };
        }
    }
}