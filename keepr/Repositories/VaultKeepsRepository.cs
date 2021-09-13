using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal VaultKeep Get(int id)
    {
      string sql = "SELECT * FROM vaultKeep WHERE id = @id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
    internal VaultKeep Create(VaultKeep newVaultKeep)
    {
      string sql = @"
        INSERT INTO vaultKeep
        (creatorId, vaultId, keepId)
        VALUES
        (@CreatorId, @VaultId, @KeepId);
        SELECT LAST_INSERT_ID();
      ";
      newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
      VaultKeep toReturn = Get(newVaultKeep.Id);
      return toReturn;
    }
    public void Delete(int id)
    {
      string sql = "DELETE FROM vaultKeep WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}