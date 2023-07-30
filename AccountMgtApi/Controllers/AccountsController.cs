using AccountMgtApi.Models;
using AccountMgtApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        private readonly IUseBsonDocumentServices _useBsonDocumentServices;
        private readonly ITransactionServices _transfer;

        public AccountsController(IAccountServices accountServices, 
            IUseBsonDocumentServices useBsonDocumentServices,
            ITransactionServices transfer) 
        { 
            _accountServices = accountServices;
            _useBsonDocumentServices = useBsonDocumentServices;
            _transfer = transfer;
        }

        [HttpPost]
        [Route(nameof(AddAccount))]
        public ActionResult AddAccount()
        {
            _useBsonDocumentServices.AddAccount();
            
            return Ok();
        }

        [HttpPost]
        [Route(nameof(AddAccounts))]
        public ActionResult AddAccounts()
        {
            _useBsonDocumentServices.AddAccounts();

            return Ok();
        }

        [HttpGet]
        [Route(nameof(GetAllAccounts))]
        public ActionResult<List<Account>> GetAllAccounts()
        {
            var accounts = _accountServices.SearchAllAccounts();

            return Ok(accounts);
        }

        [HttpGet]
        [Route(nameof(SearchAccountByAccountId))]
        public ActionResult<Account> SearchAccountByAccountId([FromQuery] string accountId)
        {
            var account = _accountServices.SearchAccountByAccountId(accountId);

            return Ok(account);
        }

        [HttpGet]
        [Route(nameof(SearchAccountsByAcountType))]
        public ActionResult<Account> SearchAccountsByAcountType([FromQuery] string accountType)
        {
            var account = _accountServices.SearchAccountsByAcountType(accountType);

            return Ok(account);
        }

        [HttpPut]
        [Route(nameof(UpdateBalanceByAccountId))]
        public ActionResult UpdateBalanceByAccountId(string accountId, decimal balance)
        {
            var result = _accountServices.UpdateBalanceByAccountId(accountId, balance);

            return Ok(result);
        }

        [HttpPut]
        [Route(nameof(Transfer))]
        public ActionResult Transfer(
            [FromQuery] string fromId, 
            [FromQuery] string toId, 
            [FromQuery] string transferId, 
            [FromQuery] decimal transferAmount)
        {
            _transfer.PerformeTransfer(fromId, toId, transferId, transferAmount);

            return Ok();
        }
    }
}
