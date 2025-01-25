
namespace keepr.Repositories;

public class VaultKeepsRepository
{
    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    private readonly IDbConnection _db;

    public VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
    INSERT INTO
    vaultKeeps(keep_id, vault_id, creator_id)
    VALUES(@KeepId, @VaultId, @CreatorId);
    
    SELECT
    vaultKeeps.*
    FROM vaultKeeps
    WHERE vaultKeeps.id = LAST_INSERT_ID();";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, vaultKeepData).SingleOrDefault();
        return vaultKeep;
    }
}
