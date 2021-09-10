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
      return found;
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
        throw new Exception("Hands off Buddy");
      }
      // NOTE these are the same kind of evaluation ('??' Null Coalesing Operator)
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
  }
}