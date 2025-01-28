namespace keepr.Services;

public class VaultKeepsService
{
    public VaultKeepsService(VaultKeepsRepository repository, VaultsService vaultsService)
    {
        _repository = repository;
        _vaultsService = vaultsService;
    }
    private readonly VaultKeepsRepository _repository;
    private readonly VaultsService _vaultsService;

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, userId);
        if (vault.CreatorId != userId) throw new Exception($"yes");
        VaultKeep vaultKeeps = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeeps;
    }

    private VaultKeep GetVaultKeepById(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = _repository.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null) throw new Exception($"Invalid vaultkeep id: {vaultKeepId}");
        return vaultKeep;
    }
    internal string DeleteVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId, userId);

        if (vaultKeep == null)
        {
            throw new Exception($"VaultKeep with ID {vaultKeepId} not found.");
        }

        Console.WriteLine($"CreatorId: {vaultKeep.CreatorId}, UserId: {userId}"); // Log for debugging

        if (vaultKeep.CreatorId != userId)
        {
            throw new Exception("You can not delete another user's vaultKeep.");
        }

        _repository.DeleteVaultKeep(vaultKeepId);
        return $"Deleted vaultKeep with ID {vaultKeepId}";
    }
}
