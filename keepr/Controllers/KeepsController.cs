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
    public class KeepsController : ControllerBase
    {
    private readonly KeepsService _kService;
    private readonly VaultKeepsService _vService;
    private readonly VaultsService _vaService;
    public KeepsController(KeepsService kService, VaultKeepsService vService, VaultsService vaService)
    {
      _kService = kService;
      _vService = vService;
      _vaService = vaService;
    }
    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
      try
      {
        List<Keep> keeps = _kService.Get();
        return Ok(keeps);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      try
      {
        Keep keep = _kService.Get(id);
        return Ok(keep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<VaultByKeepModel>>> GetVaultsByKeepIdAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Keep keep = _kService.Get(id);
        List<VaultByKeepModel> vault;
        if(userInfo != null){
        vault = _vaService.GetVaultsByKeepId(id, userInfo.Id, keep);
        return Ok(vault);
        }
        else{
          return BadRequest("You are not logged in");
        }
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep newKeep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newKeep.CreatorId = userInfo.Id;
        Keep keep = _kService.Create(newKeep);
        return Ok(keep);
      }
      catch (Exception err)
      {
        return BadRequest(err.Message);
      }
    }
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit([FromBody] Keep updatedKeep, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updatedKeep.CreatorId = userInfo.Id;
        updatedKeep.Id = id;
        Keep keep = _kService.Edit(updatedKeep);
        return Ok(keep);
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
        _kService.Delete(id, userInfo.Id);
        return Ok("Deleted");
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
  }
}