using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
    [Route("/api/vaultkeeps")]
    public class VaultKeepsController : ControllerBase
    {
    private readonly VaultKeepsService _vService;
    public VaultKeepsController( VaultKeepsService vService)
    {
      _vService = vService;
    }
    [HttpGet]
    // public ActionResult<List<VaultKeep>> Get()
    // {
    //   try
    //   {
    //     List<VaultKeep> vaultKeeps = _vService.Get();
    //     return Ok(vaultKeeps);
    //   }
    //     catch (Exception err)
    //     {
    //     return BadRequest(err.Message);
    //   }
    // }
    // [HttpGet("{id}")]
    // public async Task<ActionResult<VaultKeep>> Get(int id)
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     VaultKeep vaultKeep;
    //     if(userInfo != null)
    //     {
    //         vaultKeep = _vService.Get(id, userInfo.Id);
    //     }
    //     else
    //     {
    //         vaultKeep = _vService.Get(id, false);
    //     }
    //     return Ok(vaultKeep);
    //   }
    //   catch (Exception err)
    //   {
    //     return BadRequest(err.Message);
    //   }
    // }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVaultKeep.CreatorId = userInfo.Id;
        VaultKeep vaultKeep = _vService.Create(newVaultKeep);
        return Ok(vaultKeep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    // [HttpPut("{id}")]
    // [Authorize]
    // public async Task<ActionResult<VaultKeep>> Edit([FromBody] VaultKeep updatedVaultKeep, int id)
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     updatedVaultKeep.CreatorId = userInfo.Id;
    //     updatedVaultKeep.Id = id;
    //     VaultKeep vaultKeep = _vService.Edit(updatedVaultKeep);
    //     return Ok(vaultKeep);
    //   }
    //   catch (Exception err)
    //   {
    //     return BadRequest(err.Message);
    //   }
    // }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
        try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vService.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
  }
}