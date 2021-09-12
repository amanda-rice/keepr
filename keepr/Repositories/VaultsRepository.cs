using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;
    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Vault> Get()
    {
      string sql = @"
        SELECT
          a.*,
          v.*
        FROM vaults v
        JOIN accounts a ON v.creatorId = a.id
        ";
      return _db.Query<Profile, Vault, Vault>(sql, (profile, vault) =>
      {
        vault.Creator = profile;
        return vault;
      }, splitOn: "id").ToList();
    }
    internal Vault Get(int id)
    {
      string sql = @"
        SELECT
          a.*,
          v.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.id = @id;
        ";
      return _db.Query<Profile, Vault, Vault>(sql, (profile, vault) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }
    internal List<Vault> GetYourProfileVaults(string accountId)
    {
      string sql = @"
        SELECT
          a.*,
          v.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.creatorId = @accountId;
        ";
      return _db.Query<Profile, Vault, Vault>(sql, (profile, vault) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { accountId }, splitOn: "id").ToList();
    }
    internal List<Vault> GetProfileVaults(string accountId)
    {
      string sql = @"
        SELECT
          a.*,
          v.*
        FROM vaults v
        JOIN accounts a ON a.id = v.creatorId
        WHERE v.creatorId = @accountId AND v.isPrivate = false;
        ";
      return _db.Query<Profile, Vault, Vault>(sql, (profile, vault) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { accountId }, splitOn: "id").ToList();
    }
    internal Vault Create(Vault newVault)
    {
      string sql = @"
        INSERT INTO vaults
        (name, description, img, isPrivate, creatorId)
        VALUES
        (@Name, @Description, @Img, @IsPrivate, @CreatorId);
        SELECT LAST_INSERT_ID();
      ";
      newVault.Id = _db.ExecuteScalar<int>(sql, newVault);
      Vault toReturn = Get(newVault.Id);
      return toReturn;
    }

    public Vault Edit(Vault updated)
    {
      string sql = @"
      UPDATE vaults
      SET
        name = @Name,
        description = @Description,
        img = @Img,
        isPrivate = @IsPrivate
      WHERE id = @Id
      ";
      _db.Execute(sql, updated);
      return updated;
    }
    public void Delete(int id)
    {
      string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
    internal List<VaultByKeepModel> GetVaultsByKeepId(int keepId)
    {
      string sql = @"
      SELECT
        a.*,
        v.*,
        vk.id AS vaultKeepId
      FROM vaultKeep vk
      JOIN vaults v ON vk.vaultId = v.id
      JOIN accounts a ON v.creatorId = a.id
      WHERE vk.keepId = @keepId;
      ";
      return _db.Query<Profile, VaultByKeepModel, VaultByKeepModel>(sql, (prof, vbkm) =>
      {
        vbkm.Creator = prof;
        return vbkm;
      }, new { keepId }, splitOn: "id").ToList();
    }

  }
}