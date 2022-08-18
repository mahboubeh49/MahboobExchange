using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer
{
    public static class IdentityConfiguration
    {
        public static List<TestUser> FakeUsers =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "6101ebfd-71db-44e5-8561-23317edd785c",
                    Username = "matt",
                    Password = "123456",
                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "matt"),
                        new Claim(JwtClaimTypes.FamilyName, "damon")
                    }
                }
            };

        public static IEnumerable<ApiScope> Scopes =>
            new List<ApiScope>
            {
                        new ApiScope("ReadApis", "Read Permission"),
                        new ApiScope("WriteApis", "Write Permission")
            };

        public static IEnumerable<IdentityResource> Resources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "ExchangeKnabAdmin",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("PasswordSecret".Sha256()) },
                    AllowedScopes = new List<string>
                    {
                        "ReadApis",
                        "WriteApis"
                    },
                    AccessTokenLifetime = 3600, 
                    IdentityTokenLifetime = 3600, 
                    UpdateAccessTokenClaimsOnRefresh = false,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysSendClientClaims = true,
                    Enabled = true,
                },

            };

    }
}
