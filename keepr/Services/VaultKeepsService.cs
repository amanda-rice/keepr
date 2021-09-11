using System;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly KeepsRepository _kRepo;
    public VaultKeepsService(VaultKeepsRepository repo, KeepsRepository kRepo)
    {
      _repo = repo;
      _kRepo = kRepo;
    }

    // internal List<VaultKeep> Get()
    // {
    //   return _repo.Get();
    // }
    internal VaultKeep Get(int id)
    {
      VaultKeep found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      return found;
    }
    // internal VaultKeep Get(int id, string userId)
    // {
    //   VaultKeep found = _repo.Get(id);
    //   if (found == null)
    //   {
    //     throw new Exception("Invalid ID");
    //   }
    //   if(found.CreatorId != userId && found.IsPrivate == true)
    //   {
    //     throw new Exception("You don't have access to this vaultKeep");
    //   }
    //   return found;
    // }
    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      VaultKeep toReturn = _repo.Create(newVaultKeep);
      Keep keep = _kRepo.Get(toReturn.Id);
      keep.Keeps = keep.Keeps + 1;
      _kRepo.Edit(keep);
      return toReturn;
    }

    // internal VaultKeep Edit(VaultKeep updatedVaultKeep)
    // {
    //   VaultKeep original = Get(updatedVaultKeep.Id, true);
    //   if (original.CreatorId != updatedVaultKeep.CreatorId)
    //   {
    //     throw new Exception("You can't edit someone else's vaultKeep");
    //   }
    //   original.Name = updatedVaultKeep.Name ?? original.Name;
    //   original.Description = updatedVaultKeep.Description ?? original.Description;
    //   original.Img = updatedVaultKeep.Img ?? original.Img;
    //   original.IsPrivate = updatedVaultKeep.IsPrivate ?? original.IsPrivate;
    //   _repo.Edit(original);
    //   return original;
    // }
    internal void Delete(int id, string userId)
    {
      VaultKeep deleteItem = Get(id);
      if(deleteItem.CreatorId != userId)
      {
        throw new Exception("You can only delete your own vaultKeeps");
      }
      _repo.Delete(id);
    }
  }
}