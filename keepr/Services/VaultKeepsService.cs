using System;
using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;

namespace keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    private readonly KeepsRepository _kRepo;
    private readonly VaultsRepository _vRepo;
    public VaultKeepsService(VaultKeepsRepository repo, KeepsRepository kRepo, VaultsRepository vRepo)
    {
      _repo = repo;
      _kRepo = kRepo;
      _vRepo = vRepo;
    }
    internal VaultKeep Get(int id)
    {
      VaultKeep found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid ID");
      }
      return found;
    }

    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      Vault vault = _vRepo.Get(newVaultKeep.VaultId);
      if(vault.CreatorId != newVaultKeep.CreatorId){
        throw new Exception("This isn't your keep!");
      }
      List<KeepByVaultModel> existingKeeps = _kRepo.GetKeepsByVaultId(newVaultKeep.VaultId);
      if(existingKeeps.Find(k => k.Id == newVaultKeep.KeepId) != null){
        throw new Exception("This Keep already exists in this Vault");
      }
      VaultKeep toReturn = _repo.Create(newVaultKeep);
      Keep keep = _kRepo.Get(newVaultKeep.KeepId);
      keep.Keeps = keep.Keeps + 1;
      _kRepo.Edit(keep);
      return toReturn;
    }
    internal void Delete(int id, string userId)
    {
      VaultKeep deleteItem = Get(id);
      if(deleteItem.CreatorId != userId)
      {
        throw new Exception("You can only delete your own vaultKeeps");
      }
      Keep keep = _kRepo.Get(deleteItem.KeepId);
      keep.Keeps = keep.Keeps - 1;
      _kRepo.Edit(keep);
      _repo.Delete(id);
    }
  }
}