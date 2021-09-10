using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Keep> Get()
    {
      string sql = @"
        SELECT
          a.*,
          k.*
        FROM keeps k
        JOIN accounts a ON k.creatorId = a.id
        ";
      return _db.Query<Profile, Keep, Keep>(sql, (profile, keep) =>
      {
        keep.Creator = profile;
        return keep;
      }, splitOn: "id").ToList();
    }
    internal Keep Get(int id)
    {
      string sql = @"
        SELECT
          a.*,
          k.*
        FROM keeps k
        JOIN accounts a ON a.id = k.creatorId
        WHERE k.id = @id;
        ";
      return _db.Query<Profile, Keep, Keep>(sql, (profile, keep) =>
      {
        keep.Creator = profile;
        return keep;
      }, new { id }, splitOn: "id").FirstOrDefault();
    }
    internal Keep Create(Keep newKeep)
    {
      string sql = @"
        INSERT INTO keeps
        (name, description, img, views, shares, keeps, creatorId)
        VALUES
        (@Name, @Description, @Img, @Views, @Shares, @Keeps, @CreatorId);
        SELECT LAST_INSERT_ID();
      ";
      newKeep.Id = _db.ExecuteScalar<int>(sql, newKeep);
      Keep toReturn = Get(newKeep.Id);
      return toReturn;
    }

    public Keep Edit(Keep updated)
    {
      string sql = @"
      UPDATE keeps
      SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views,
        shares = @Shares,
        keeps = @Keeps
      WHERE id = @Id
      ";
      _db.Execute(sql, updated);
      return updated;
    }
    public void Delete(int id)
    {
      string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}