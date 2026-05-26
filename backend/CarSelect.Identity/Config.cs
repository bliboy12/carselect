using Duende.IdentityServer.Models;

namespace CarSelect.Identity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource(
                name: "roles",
                userClaims: new[] {"role"}
            )
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("carselect.api.read", "CarSelect API Read"),
            new ApiScope("carselect.api.write", "CarSelect API Write")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // For React Frontend
            new Client
            {
                ClientId = "carselect-frontend",
                ClientName = "CarSelect React Frontend",

                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false, // no need to store any secrets in the frontend
                RedirectUris = {"http://localhost:5173/callback"},
                PostLogoutRedirectUris = {"http://localhost:5173"},
                AllowedCorsOrigins = { "http://localhost:5173" },
                AllowOfflineAccess = true, // allows the user to ask for a refresh token
                AllowedScopes = { "openid", "profile", "roles", "carselect.api.read", "carselect.api.write" }, // what is the frontend allowed to access
            },

            // Postman
            new Client
            {
                ClientId = "postman-client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("eenGrootGeheim".Sha256())},
                AllowedScopes = { "carselect.api.read" }
            },

            // m2m
            new Client
            {
                ClientId = "carselect-api-client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("carselect-api-secret".Sha256()) },
                AllowedScopes = { "carselect.api.read", "carselect.api.write"}
            }
        };
}
