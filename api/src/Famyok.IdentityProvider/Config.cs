using Duende.IdentityModel;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Famyok.IdentityProvider;

public class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            // new("famyokapi", new List<string> {  }),
            new("roles") { UserClaims = new List<string> { JwtClaimTypes.Role }},
        };

    public static IEnumerable<Client> Clients =>
        new Client[] {
            new () {
                ClientName = "Famyok Client",
                ClientId = "famyokclient",
                AllowedGrantTypes = new List<string> {
                    GrantType.ClientCredentials,
                    GrantType.ResourceOwnerPassword
                },
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = new List<string> { 
                    "famyokapi",
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OpenId,
                    "roles",
                },
                AllowOfflineAccess = true,
            }
        };
}