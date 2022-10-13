using Azure.Identity;

namespace Microsoft.eShopWeb.Web.Extensions;

public static class ConfigurationManagerExtensions
{
    public static void AddKeyVault(
        this ConfigurationManager configuration,
        string vaultNamePath = "VaultName",
        string identityClientIdPath = "ManagedIdentityClientId")
    {
        var vaultName = configuration[vaultNamePath];
        if (vaultName is null)
        {
            throw new InvalidOperationException(
                $"\"{vaultNamePath}\" is not provided in application settings to configure Azure Key Vault");
        }

        var clientId = configuration[identityClientIdPath];
        if (clientId is null)
        {
            throw new InvalidOperationException(
                $"\"{identityClientIdPath}\" is not provided in application settings to configure Azure Key Vault");
        }

        var vaultUri = new Uri($"https://{vaultName}.vault.azure.net/");
        var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            ManagedIdentityClientId = clientId
        });
        configuration.AddAzureKeyVault(vaultUri, credential);
    }
}
