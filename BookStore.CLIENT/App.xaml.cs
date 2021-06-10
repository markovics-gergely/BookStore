using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BookStore.CLIENT
{
    [Bindable]
    sealed partial class App : Template10.Common.BootStrapper
    {
        /// <summary>
        /// B2C tenant name
        /// </summary>
        private static readonly string TenantName = "bookstoreb2c";
        private static readonly string Tenant = $"{TenantName}.onmicrosoft.com";
        private static readonly string AzureAdB2CHostname = $"{TenantName}.b2clogin.com";

        /// <summary>
        /// ClientId for the application which initiates the login functionality (this app)  
        /// </summary>
        private static readonly string ClientId = "bb711873-765b-4ee2-ad4e-675044e0f396";

        /// <summary>
        /// Should be one of the choices on the Azure AD B2c / [This App] / Authentication blade
        /// </summary>
        private static readonly string RedirectUri = $"https://{TenantName}.b2clogin.com/oauth2/nativeclient";

        /// <summary>
        /// From Azure AD B2C / UserFlows blade
        /// </summary>
        public static string PolicySignUpSignIn = "B2C_1_signupsignin";
        public static string PolicyResetPassword = "b2c_1_reset";

        public static string[] ApiScopes = { "https://bookstoreb2c.onmicrosoft.com/api/allusers" };

        /// <summary>
        /// URL for API which will receive the bearer token corresponding to this authentication
        /// </summary>
        public static string ApiEndpoint = "https://localhost:5000/api/author";

        // Shouldn't need to change these:
        private static string AuthorityBase = $"https://{AzureAdB2CHostname}/tfp/{Tenant}/";
        public static string AuthoritySignUpSignIn = $"{AuthorityBase}{PolicySignUpSignIn}/oauth2/v2.0/authorize";
        public static string AuthorityResetPassword = $"{AuthorityBase}{PolicyResetPassword}";

        public static IPublicClientApplication PublicClientApp { get; private set; }
        public static string BearerToken { get; set; }

        public App()
        {
            InitializeComponent();
            RequestedTheme = Windows.UI.Xaml.ApplicationTheme.Light;

            PublicClientApp = PublicClientApplicationBuilder.Create(ClientId)
                .WithB2CAuthority(AuthoritySignUpSignIn)
                .WithRedirectUri(RedirectUri)
                .Build();
            
            TokenCacheHelper.Bind(PublicClientApp.UserTokenCache);
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            await NavigationService.NavigateAsync(typeof(Views.MainPage));
        }

        public static string admintoken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsifQ.eyJpc3MiOiJodHRwczovL2Jvb2tzdG9yZWIyYy5iMmNsb2dpbi5jb20vYTY4MzBjOTUtYjQzYy00ZTY2LTgxNzYtYzNmZjhjZWVlNzQ2L3YyLjAvIiwiZXhwIjoxNjIzMTQ2NzE1LCJuYmYiOjE2MjMxNDMxMTUsImF1ZCI6IjU3N2U5NmI1LTc2NjgtNDE2OC1hYThkLWEzMjAwODgxNTZjZSIsIm9pZCI6Ijg2MDRkNTNiLTUxMzQtNDNjMy1iMjNhLTBjNTEzYzc3YTQ2ZCIsInN1YiI6Ijg2MDRkNTNiLTUxMzQtNDNjMy1iMjNhLTBjNTEzYzc3YTQ2ZCIsImdpdmVuX25hbWUiOiJHZXJnZWx5IiwiZmFtaWx5X25hbWUiOiJNYXJrb3ZpY3MiLCJlbWFpbHMiOlsiZ2VyZ2VseS5tYXJrb3ZpY3MyMEBnbWFpbC5jb20iXSwidGZwIjoiQjJDXzFfc2lnbnVwc2lnbmluIiwic2NwIjoiYWxsdXNlcnMiLCJhenAiOiI5M2U5OTU1Yy05M2RhLTRkYTEtOGIzNy0xZWVlYzVlNjI2NDQiLCJ2ZXIiOiIxLjAiLCJpYXQiOjE2MjMxNDMxMTV9.cl9X9ki6mKcGnW29rmfHEI88JTl6c5otrqBrEZCso3sPoK36KH6_5KuZoK9gmthB3kYWaSLsrhhUUEGka0D7VuTYFfeUm-3ocweEFGZjBb7yigzr4ghH3_mkdU99sUuhMqy9nBO8IKci2A7nC2ndxbcWcRyuMtppP0LypZOVyN9aO_9V231w5zp-5_e9r-sIxV7c4GAufio_NQRef1en4VPNaVbIiR3sD3AJMo6NUY88GD-6QxsuiC2gVZq8V1K7uv7fWW_jOE0ihQbCl7p4mhJI2j6drYb3z28S7VNNbM7YAW7JvdmtAHO7nH7XLNsJ4icov6TSViwuoIksyMAdNw";

    }
}
