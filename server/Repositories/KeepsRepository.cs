

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

    internal void DeleteKeep(int id)
    {
        string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { id });

        if (rowsAffected != 1) throw new Exception($"{rowsAffected} were deleted and that bad juju");
    }

    // internal List<Keep> GetKeepInPublicVault(int vaultId)
    // {
    //     string sql = @"
    //     SELECT
    //     keeps.*,
    //     accounts.*
    //     FROM keeps
    //     JOIN accounts ON accounts.id = keeps.creator_id
    //     JOIN vaultKeeps ON vaultKeeps.keep_id = keeps.id
    //     JOIN vaults ON vaults.id = vaultKeeps.vault_id
    //     WHERE vaultKeeps.vault_id = @vaultId AND vaults.is_private = false;";

    //     List<Keep> keeps = _db.Query(sql, (Keep keep, Account account, int vaultKeepId) =>
    //     {
    //         keep.Creator = account;
    //         keep.VaultKeepId = vaultKeepId;
    //         return keep;
    //     }, new { vaultId }, splitOn: "VaultKeepId").ToList();
    //     return keeps;
    // }

    // internal List<Keep> GetKeepInPublicVault(int vaultId)
    // {
    //     string sql = @"
    //     SELECT
    //     keeps.*,
    //     accounts.*
    //     FROM keeps
    //     JOIN accounts ON accounts.id = keeps.creator_id
    //     JOIN vaultKeeps ON vaultKeeps.keep_id = keeps.id
    //     JOIN vaults ON vaults.id = vaultKeeps.vault_id
    //     WHERE vaultKeeps.vault_id = @vaultId AND vaults.is_private = false;";

    //     List<Keep> keeps = _db.Query(sql, (Keep keep, Account account) =>
    //     {
    //         keep.Creator = account;
    //         return keep;
    //     }, new { vaultId }).ToList();
    //     return keeps;
    // }
}
