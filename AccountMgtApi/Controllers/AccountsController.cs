using AccountMgtApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        public AccountsController(IAccountServices accountServices) 
        { 
            _accountServices = accountServices;
        }


    }
}
