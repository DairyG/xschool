using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XSchool.UCenter.Extensions
{
    public class ClientStore : IClientStore
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = new Client
            {
                ClientName = string.Empty,
                IdentityTokenLifetime = (int)TimeSpan.FromDays(30).TotalSeconds,
                ClientId = clientId,
                IncludeJwtId = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AccessTokenLifetime = (int)TimeSpan.FromDays(30).TotalSeconds,
                RefreshTokenUsage = TokenUsage.ReUse,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
                ClientSecrets = new List<Secret>
                {
                        new Secret("123456")
                },

                AllowedScopes = new List<string>
                    {
                        "ResourceAPI",
                        "DataAPI",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
            };
            return client;
        }
    }
}
