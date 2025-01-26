

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

    internal void DeleteVault(int id)
    {
        string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { id });

        if (rowsAffected != 1) throw new Exception($"{rowsAffected} were deleted and that bad juju");
    }

    public async Task<List<Keep>> GetKeepInPublicVault(int vaultId)
    {
        // SQL Query to get keeps in a public vault
        string sql = @"
                SELECT
                    keeps.*,
                    accounts.*,
                    vaultKeeps.id AS VaultKeepId
                FROM keeps
                JOIN accounts ON accounts.id = keeps.creator_id
                JOIN vaultKeeps ON vaultKeeps.keep_id = keeps.id
                JOIN vaults ON vaults.id = vaultKeeps.vault_id
                WHERE vaultKeeps.vault_id = @vaultId AND vaults.is_private = false;";

        var keeps = await _db.QueryAsync<Keep, Account, int, Keep>(
            sql,
            (keep, account, vaultKeepId) =>
            {
                keep.Creator = account;
                keep.VaultKeepId = vaultKeepId;
                return keep;
            },
            new { vaultId },
            splitOn: "id" // Dapper will split the result on 'id', as 'id' is the first column for 'accounts'
        );

        return keeps.ToList();

    }

}

