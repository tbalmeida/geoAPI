using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchpadSept2020.Auth
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "geoapi",
                    DisplayName = "geoAPI",
                    Scopes = { "geoapi.scope" }
                }
            };
        
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("geoapi.scope", "geoAPI")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "mobile",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("EcBSm40SIOmHthHZZTSfWyZjAC2Y7vws".Sha256())
                    },
                    AllowedScopes = {"geoapi.scope", IdentityServerConstants.StandardScopes.OpenId}
                }
            };

    }
}
