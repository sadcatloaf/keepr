

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

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repository.GetVaultById(vaultId);

        if (vault == null) throw new Exception($"Invalid keep id: {vaultId}");

        if (vault.CreatorId != userId && vault.IsPrivate == true) throw new Exception($"Invalid vault id: {vaultId} 😉");
        return vault;
    }

    internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
    {
        Vault vault = GetVaultById(vaultId, userId);

        if (vault.CreatorId != userId) throw new Exception("You can not update anothers vault");

        vault.Img = vaultData.Img ?? vault.Img;
        vault.Name = vaultData.Name ?? vault.Name;
        vault.Description = vaultData.Description ?? vault.Description;
        vault.IsPrivate = vaultData.IsPrivate ?? vault.IsPrivate;

        _repository.UpdateVault(vault);

        return vault;

    }

    internal string DeleteVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId, userId);

        if (vault.CreatorId != userId) throw new Exception("You can not delete users Keep");
        _repository.DeleteVault(vaultId);
        return $"Deleted {vault.Name}";
    }

    internal List<Keep> GetKeepInPublicVault(int vaultId, string userId)
    {

        Vault vault = _repository.GetVaultById(vaultId);

        // TODO I have a vault 
        // I need to check if im allowed to see it. 😩
        if (vault.CreatorId != userId && vault.IsPrivate == true) throw new Exception($"Invalid vault is: {vaultId}😉");

        List<Keep> keeps = _repository.GetKeepInPublicVault(vaultId);

        return keeps;
    }

    internal List<Vault> GetProfileVaults(string profileId, string userId)
    {

        List<Vault> vaults = _repository.GetProfileVaults(profileId);
        // if(vaults.CreatorId != userId && vaults.IsPrivate == true) throw new Exception("You can not see other profiles private vaults");
        return vaults.FindAll(vault => vault.CreatorId == userId || vault.IsPrivate == false);
        // return vaults;
    }
}
// internal Restaurant GetRestaurantById(int restaurantId, string userId)
//   {
//     Restaurant restaurant = GetRestaurantById(restaurantId); // private method
//     if (restaurant.CreatorId != userId && restaurant.IsShutdown == true) throw new Exception($"Invalid restaurant id: {restaurantId} 😉");
//     return restaurant;
//   }
// NOTE this is an overload. two methods can share the same name, but different methods can run based on types of parameters, number of parameters, or a combination of the two
//   internal List<Restaurant> GetAllRestaurants(string userId)
//   {
//     List<Restaurant> restaurants = GetAllRestaurants(); // calls the private method

//     // FindAll is the C# list method equivalent of the js filter array method
//     // checks if the logged in user is the owner of the restaurant OR if the restaurant is not shut down
//     return restaurants.FindAll(restaurant => restaurant.CreatorId == userId || restaurant.IsShutdown == false);
//   }