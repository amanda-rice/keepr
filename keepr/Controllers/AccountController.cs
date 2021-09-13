using System;
using System.Threading.Tasks;
using keepr.Models;
using keepr.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Account>> Get()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                return Ok(_accountService.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut()]
        [Authorize]
        public async Task<ActionResult<Profile>> Edit([FromBody] Profile updatedAccount, int id)
        {
        try
        {
            Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
            updatedAccount.Id = userInfo.Id;
            Profile vault = _accountService.Update(updatedAccount);
            return Ok(vault);
        }
        catch (Exception err)
        {
            return BadRequest(err.Message);
        }
        }
    }


}