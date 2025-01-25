namespace keepr.Services;

public class VaultKeepsService
{
    public VaultKeepsService(VaultKeepsRepository repository)
    {
        _repository = repository;
    }
    private readonly VaultKeepsRepository _repository;
    // private readonly VaultsService _vaultsService;

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData, string userId)
    {
        // Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId);
        // if (vault.CreatorId != userId) throw new Exception($"This is not your vaultkeep");

        VaultKeep vaultKeeps = _repository.CreateVaultKeep(vaultKeepData);
        return vaultKeeps;
    }
}

// internal Album CreateAlbum(Album albumData)
//   {
//     Album album = _repository.CreateAlbum(albumData);
//     return album;
//   }