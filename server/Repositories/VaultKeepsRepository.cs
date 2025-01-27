
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

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        string sql = "SELECT * FROM vaultKeeps WHERE id = @vaultKeepId;";
        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).SingleOrDefault();
        return vaultKeep;
    }
    internal void DeleteVaultKeep(int vaultKeepId)
    {
        string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";

        int rowsAffected = _db.Execute(sql, new { vaultKeepId });
        if (rowsAffected != 1) throw new Exception($"{rowsAffected} were deleted and that is bad juju");
    }

}
