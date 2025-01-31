

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

    // TODO also add the count select here with the GROUP BY clause
    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
        SELECT
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creator_id = accounts.id
        LEFT JOIN vaultKeeps ON keeps.id = vaultKeeps.keep_id
        GROUP BY(keeps.id);";

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
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        LEFT JOIN vaultKeeps ON keeps.id = vaultKeeps.keep_id
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

    internal List<Keep> GetProfileKeeps(string profileId)
    {
        string sql = @"
        SELECT
        accounts.*,
        keeps.*
        FROM accounts
        JOIN keeps ON keeps.creator_id = accounts.id
        WHERE accounts.id = @profileId;";

        List<Keep> keeps = _db.Query(sql, (Account account, Keep keep) =>
        {
            keep.Creator = account;
            return keep;
        }, new { profileId }).ToList();
        return keeps;
    }

    internal void IncrementViews(Keep keep)
    {
        string sql = @"
        UPDATE keeps
        SET views = @Views
        WHERE id = @Id
        LIMIT 1;";

        int rowsAffected = _db.Execute(sql, keep);

        if (rowsAffected != 1) throw new Exception($"{rowsAffected} WERE UPDATED AND THATS BAD JUJU");
    }
}
