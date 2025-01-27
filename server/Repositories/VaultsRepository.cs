

namespace keepr.Repositories;

public class VaultsRepository
{
    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;

    public Vault CreateVault(Vault vaultData)
    {
        string sql = @"
    INSERT INTO
    vaults(name, description, img, creator_id)
    VALUES(@Name, @Description, @Img, @CreatorId);
    
    SELECT
    vaults.*,
    accounts.*
    FROM vaults
    JOIN accounts ON vaults.creator_id = accounts.id
    WHERE vaults.id = LAST_INSERT_ID();";

        Vault vault = _db.Query(sql, (Vault vault, Account account) =>
        {
            vault.Creator = account;
            return vault;
        }, vaultData).SingleOrDefault();
        return vault;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON vaults.creator_id = accounts.id
        WHERE vaults.id = @vaultId;";

        Vault vault = _db.Query(sql, (Vault vault, Account account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { vaultId }).SingleOrDefault();

        return vault;
    }

    internal void UpdateVault(Vault updateData)
    {
        string sql = @"
        UPDATE vaults
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        is_private = @IsPrivate
        WHERE id = @Id LIMIT 1;";

        int rowsAffected = _db.Execute(sql, updateData);

        if (rowsAffected != 1) throw new Exception($"{rowsAffected} were updated and that bad juju");
    }

    internal void DeleteVault(int vaultId)
    {
        string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { vaultId });

        if (rowsAffected != 1) throw new Exception($"{rowsAffected} were deleted and that bad juju");
    }

    public async Task<List<Keep>> GetKeepInPublicVault(int vaultId)
    {
        // SQL Query to get keeps in a public vault
        string sql = @"
        SELECT
            keeps.*,
            vaultKeeps.id AS vaultKeepId,
            accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creator_id
        JOIN vaultKeeps ON vaultKeeps.keep_id = keeps.id
        JOIN vaults ON vaults.id = vaultKeeps.vault_id
        WHERE vaultKeeps.vault_id = @vaultId AND vaults.is_private = false;";

        var keeps = await _db.QueryAsync(sql, (Keep keep, Account account) =>
            {
                keep.Creator = account;
                return keep;
            }, new { vaultId });

        return keeps.ToList();
    }
    internal List<Vault> GetProfileVaults(string profileId)
    {
        string sql = @"
        SELECT
        vaults.*,
        accounts.*
        FROM accounts
        JOIN vaults ON vaults.creator_id = accounts.id
        WHERE accounts.id = @profileId;";

        List<Vault> vaults = _db.Query(sql, (Vault vault, Account account) =>
        {
            vault.Creator = account;
            return vault;
        }, new { profileId }).ToList();
        return vaults;
    }

}

