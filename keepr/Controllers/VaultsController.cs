using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [ApiController]
    [Route("/api/[controller]")]
    public class VaultsController : ControllerBase
    {
    private readonly VaultsService _vaService;
    private readonly VaultKeepsService _vService;
    private readonly KeepsService _kService;
    public VaultsController(VaultsService vaService, VaultKeepsService vService, KeepsService kService)
    {
      _vaService = vaService;
      _vService = vService;
      _kService = kService;
    }
    [HttpGet]
    public ActionResult<List<Vault>> Get()
    {
      try
      {
        List<Vault> vaults = _vaService.Get();
        return Ok(vaults);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> Get(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault;
        if(userInfo != null)
        {
            vault = _vaService.Get(id, userInfo.Id);
        }
        else
        {
            vault = _vaService.Get(id, false);
        }
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<KeepByVaultModel>>> GetKeepsByVaultIdAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vaService.Get(id, true);
        List<KeepByVaultModel> keep;
        if(userInfo != null){
        keep = _kService.GetKeepsByVaultId(id, userInfo.Id, vault);
        }
        else{
        keep = _kService.GetKeepsByVaultId(id, vault);
        }
        return Ok(keep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault newVault)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newVault.CreatorId = userInfo.Id;
        Vault vault = _vaService.Create(newVault);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit([FromBody] Vault updatedVault, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedVault.CreatorId = userInfo.Id;
        updatedVault.Id = id;
        Vault vault = _vaService.Edit(updatedVault);
        return Ok(vault);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
        try
        {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vaService.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
  }
}