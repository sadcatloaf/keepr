

namespace keepr.Repositories;

public class KeepsRepository
{
    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;

    public Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO
        keeps(name, description, img, views, creator_id)
        VALUES(@Name, @Description, @Img, @Views, @CreatorId);

        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        WHERE keeps.id = LAST_INSERT_ID();";

        Keep keep = _db.Query(sql, (Keep keep, Account account) =>
        {
            keep.Creator = account;
            return keep;
        }, keepData).SingleOrDefault();

        return keep;
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id;";

        List<Keep> keeps = _db.Query(sql, (Keep keep, Account account) =>
        {
            keep.Creator = account;
            return keep;
        }).ToList();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        WHERE keeps.id = @keepId;";

        Keep keep = _db.Query(sql, (Keep keep, Account account) =>
        {
            keep.Creator = account;
            return keep;
        }, new { keepId }).SingleOrDefault();

        return keep;
    }

    internal void UpdateKeep(Keep updateData)
    {
        string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img
        WHERE id = @Id LIMIT 1;";

        int rowsAffected = _db.Execute(sql, updateData);

        if (rowsAffected != 1) throw new Exception($"{rowsAffected} were updated and that bad juju");
    }

    internal void DeleteKeep(int vaultKeepId)
    {
        string sql = "DELETE FROM keeps WHERE id = @vaultKeepId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { vaultKeepId });

        if (rowsAffected != 1) throw new Exception($"{rowsAffected} were deleted and that bad juju");
    }

}
