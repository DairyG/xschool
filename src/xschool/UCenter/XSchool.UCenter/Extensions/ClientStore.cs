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
        public Client GetClient(string clientId)
        {
            var client = new Client
            {
                ClientName = "第三方登录客户端",
                IdentityTokenLifetime = (int)TimeSpan.FromDays(30).TotalSeconds,
                ClientId = clientId,
                IncludeJwtId = true,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AccessTokenLifetime = (int)TimeSpan.FromDays(30).TotalSeconds,
                RefreshTokenUsage = TokenUsage.ReUse,
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowOfflineAccess = true,
                ClientSecrets = new List<Secret>
                {
                        new Secret("367CA1C1E7F64A2883B978DD7CEC043B".Sha256())
                },

                AllowedScopes = new List<string>
                 {
                    "UCenter",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            };
            return client;
        }

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = this.GetClient(clientId);
            return Task.FromResult(client);
        }
    }
}
