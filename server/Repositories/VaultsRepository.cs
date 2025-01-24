
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
}

// t; set; }
//     public DateTime CreatedAt { get; set; }
//     public DateTime UpdatedAt { get; set; }
//     public string Name { get; set; }
//     public string Description { get; set; }
//     public string Img { get; set; }
//     public bool? IsPrivate { get; set; }
//     public string CreatorId { get; set; }
//     public Account Creator { get; set; }
// }