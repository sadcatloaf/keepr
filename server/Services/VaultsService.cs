

namespace keepr.Services;

public class VaultsService
{
    public VaultsService(VaultsRepository repository)
    {
        _repository = repository;
    }
    private readonly VaultsRepository _repository;

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repository.CreateVault(vaultData);
        return vault;
    }

    internal Vault GetVaultById(int vaultId)
    {
        Vault vault = _repository.GetVaultById(vaultId);

        if (vault == null) throw new Exception($"Invalid keep id: {vaultId}");
        return vault;
    }

    internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
    {
        Vault vault = GetVaultById(vaultId);

        if (vault.CreatorId != userId) throw new Exception("You can not update anothers keep");

        vault.Img = vaultData.Img ?? vault.Img;
        vault.Name = vaultData.Name ?? vault.Name;
        vault.Description = vaultData.Description ?? vault.Description;
        vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;

        _repository.UpdateVault(vault);

        return vault;

    }

    internal string DeleteVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId);

        if (vault.CreatorId != userId) throw new Exception("You can not delete users Keep");
        _repository.DeleteVault(vaultId);
        return $"Deleted {vault.Name}";
    }

    internal async Task<List<Keep>> GetKeepInPublicVault(int vaultId)
    {
        return await _repository.GetKeepInPublicVault(vaultId);
    }

    internal List<Vault> GetProfileVaults(string profileId)
    {
        List<Vault> vaults = _repository.GetProfileVaults(profileId);
        return vaults;
    }
}