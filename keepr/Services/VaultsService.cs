using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }

    internal List<Vault> Get()
    {
      return _repo.Get();
    }
    internal Vault Get(int id, bool internalCall)
    {
      Vault found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      if(!internalCall && found.IsPrivate == true)
      {
        throw new Exception("You can't access with vault");
      }
      return found;
    }
    internal Vault Get(int id, string userId)
    {
      Vault found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      if(found.CreatorId != userId && found.IsPrivate == true)
      {
        throw new Exception("You don't have access to this vault");
      }
      return found;
    }
    internal Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }
    internal List<Vault> GetProfileVaults(string id, bool isYourProfile)
    {
      List<Vault> vaults;
      if(isYourProfile){
        vaults = _repo.GetYourProfileVaults(id);
      }
      else
      {
        vaults =  _repo.GetProfileVaults(id);
      }
      return vaults;
    }
    internal Vault Edit(Vault updatedVault)
    {
      Vault original = Get(updatedVault.Id, true);
      if (original.CreatorId != updatedVault.CreatorId)
      {
        throw new Exception("You can't edit someone else's vault");
      }
      original.Name = updatedVault.Name ?? original.Name;
      original.Description = updatedVault.Description ?? original.Description;
      original.Img = updatedVault.Img ?? original.Img;
      original.IsPrivate = updatedVault.IsPrivate ?? original.IsPrivate;
      _repo.Edit(original);
      return original;
    }
    internal void Delete(int id, string userId)
    {
      Vault deleteItem = Get(id, true);
      if(deleteItem.CreatorId != userId)
      {
        throw new Exception("You can only delete your own vaults");
      }
      _repo.Delete(id);
    }
  }
}