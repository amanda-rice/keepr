using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }

    internal List<Keep> Get()
    {
      return _repo.Get();
    }

    internal Keep Get(int id)
    {
      Keep found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      found.Views++;
      return _repo.Edit(found);
    }
    internal List<Keep> GetProfileKeeps(string id, bool isYourProfile)
    {
      List<Keep> keeps;
        keeps = _repo.GetYourProfileKeeps(id);
      return keeps;
    }
    internal Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }

    internal Keep Edit(Keep updatedKeep)
    {
      Keep original = Get(updatedKeep.Id);
      if (original.CreatorId != updatedKeep.CreatorId)
      {
        throw new Exception("You can't edit this keep!");
      }
      original.Name = updatedKeep.Name ?? original.Name;
      original.Description = updatedKeep.Description ?? original.Description;
      original.Img = updatedKeep.Img ?? original.Img;
      original.Views = updatedKeep.Views != 0 ? updatedKeep.Views : original.Views;
      original.Shares = updatedKeep.Shares != 0 ? updatedKeep.Shares : original.Shares;
      original.Keeps = updatedKeep.Keeps != 0 ? updatedKeep.Keeps : original.Keeps;
      _repo.Edit(original);
      return original;
    }
    internal void Delete(int id, string userId)
    {
      Keep deleteItem = Get(id);
      if(deleteItem.CreatorId != userId)
      {
        throw new Exception("You can only delete your own keeps");
      }
      _repo.Delete(id);
    }

    internal List<KeepByVaultModel> GetKeepsByVaultId(int id, string userId, Vault vault)
    {
        if(vault.CreatorId != userId && vault.IsPrivate == true)
        {
        throw new Exception("You don't have access to this vault");
      }
      List<KeepByVaultModel> keeps =  _repo.GetKeepsByVaultId(id);
      return keeps;
    }
    internal List<KeepByVaultModel> GetKeepsByVaultId(int id, Vault vault)
    {
        if(vault.IsPrivate == true)
        {
        throw new Exception("You don't have access to this vault");
      }
      List<KeepByVaultModel> keeps =  _repo.GetKeepsByVaultId(id);
      return keeps;
    }
  }
}