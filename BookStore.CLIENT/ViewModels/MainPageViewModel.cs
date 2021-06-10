using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using System.Reflection;
using Windows.UI.Xaml.Media.Animation;
using Template10.Common;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Xaml;

namespace BookStore.CLIENT.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private bool _isLoggedIn = false;
        public bool IsLoggedIn
        {
            get
            {
                return _isLoggedIn;
            }
            set
            {
                Set(ref _isLoggedIn, value);
            }
        }
        private string _name = "Sign in";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                Set(ref _name, value);
            }
        }

        private NavigationViewItem _lastItem;
        public void Navigation_Invoked(NavigationView sender, NavigationViewItemInvokedEventArgs args, Frame InnerFrame)
        {
            var item = args.InvokedItemContainer as NavigationViewItem;
            if (item == null || item == _lastItem)
                return;
            var clickedView = item.Tag?.ToString() ?? "Book";

            if (!NavigateToView(clickedView, InnerFrame)) return;
            _lastItem = item;

        }

        private bool NavigateToView(string clickedView, Frame InnerFrame)
        {
            var view = Assembly.GetExecutingAssembly()
                .GetType($"BookStore.CLIENT.Views.{clickedView}Page");

            if (string.IsNullOrWhiteSpace(clickedView) || view == null)
            {
                return false;
            }
            
            InnerFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());
            
            return true;
        }
        public void InnerFrame_NavigationFailed(object sender, Windows.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {

        }

        public async void LoginStart()
        {
            AuthenticationResult authResult = null;
            IEnumerable<IAccount> accounts = await App.PublicClientApp.GetAccountsAsync();
            try
            {
                IAccount currentUserAccount = GetAccountByPolicy(accounts, App.PolicySignUpSignIn);
                authResult = await App.PublicClientApp.AcquireTokenSilent(App.ApiScopes, currentUserAccount)
                    .ExecuteAsync();

                DisplayBasicTokenInfo(authResult);
                UpdateSignInState(true);
            }
            catch (MsalUiRequiredException ex)
            {
                authResult = await App.PublicClientApp.AcquireTokenInteractive(App.ApiScopes)
                    .WithAccount(GetAccountByPolicy(accounts, App.PolicySignUpSignIn))
                    .WithPrompt(Prompt.SelectAccount)
                    .ExecuteAsync();
                DisplayBasicTokenInfo(authResult);
                UpdateSignInState(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Users:{string.Join(",", accounts.Select(u => u.Username))}{Environment.NewLine}Error Acquiring Token:{Environment.NewLine}{ex}");
            }
            App.admintoken = authResult.AccessToken;
        }
        private void DisplayBasicTokenInfo(AuthenticationResult authResult)
        {
            if (authResult != null)
            {
                Debug.WriteLine($"Name: {authResult.Account.Username}" + Environment.NewLine);
                Debug.WriteLine($"Token Expires: {authResult.ExpiresOn.ToLocalTime()}" + Environment.NewLine);
                Debug.WriteLine($"Id Token: {authResult.IdToken}" + Environment.NewLine);
                Debug.WriteLine($"Tenant Id: {authResult.TenantId}" + Environment.NewLine);
                
            }
        }
        private IAccount GetAccountByPolicy(IEnumerable<IAccount> accounts, string policy)
        {
            foreach (var account in accounts)
            {
                string userIdentifier = account.HomeAccountId.ObjectId.Split('.')[0];
                if (userIdentifier.EndsWith(policy.ToLower())) return account;
            }

            return null;
        }
        private void UpdateSignInState(bool signedIn)
        {
            if (signedIn)
            {
                IsLoggedIn = true;
                Name = "Sign out";
            }
            else
            {
                IsLoggedIn = false;
                Name = "Sign in";
            }
        }

        public async void SignOut(object sender, RoutedEventArgs e)
        {
            IEnumerable<IAccount> accounts = await App.PublicClientApp.GetAccountsAsync();

            try
            {
                while (accounts.Any())
                {
                    await App.PublicClientApp.RemoveAsync(accounts.FirstOrDefault());
                    accounts = await App.PublicClientApp.GetAccountsAsync();
                }
                UpdateSignInState(false);

            }
            catch (MsalException ex)
            {
                Debug.WriteLine($"Error signing-out user: {ex.Message}");
            }
        }
    }
}
