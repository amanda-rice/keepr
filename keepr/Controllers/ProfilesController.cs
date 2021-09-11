using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keepr.Models;
using keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ProfilesController : ControllerBase
    {
    private readonly AccountService _aService;
    private readonly VaultsService _vService;
    private readonly KeepsService _kService;
    public ProfilesController(AccountService aService, VaultsService vService, KeepsService kService)
    {
      _aService = aService;
      _vService = vService;
      _kService = kService;
    }
    [HttpGet]
    public ActionResult<List<Profile>> Get()
    {
      try
      {
        List<Profile> profiles = _aService.GetProfiles();
        return Ok(profiles);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}")]
    public ActionResult<Profile> Get(string id)
    {
      try
      {
        Profile profile = _aService.GetProfile(id);
        return Ok(profile);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<List<Vault>>> GetProfileVaultsAsync(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults;
        if(userInfo != null && userInfo.Id == id)
        {
            vaults = _vService.GetProfileVaults(id, true);
        }
        else
        {
            vaults = _vService.GetProfileVaults(id, false);
        }
        return Ok(vaults);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
    [HttpGet("{id}/keeps")]
    public async Task<ActionResult<List<Keep>>> GetProfileKeepsAsync(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Keep> keeps;
        if(userInfo != null && userInfo.Id == id)
        {
            keeps = _kService.GetProfileKeeps(id, true);
        }
        else
        {
            keeps = _kService.GetProfileKeeps(id, false);
        }
        return Ok(keeps);
      }
        catch (Exception err)
        {
        return BadRequest(err.Message);
      }
    }
  }
}