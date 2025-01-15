using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace BlazorApp1.Authentication
{
    public class CustomAuthentication : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anynoums = new ClaimsPrincipal(new ClaimsIdentity());
        public CustomAuthentication(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task UpdateAuthenticateState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if(userSession is not null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userSession.UserName),
                    new Claim(ClaimTypes.Role,userSession.Role),
                }));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anynoums;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var _sessionResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
                var _userSession = _sessionResult.Success ? _sessionResult.Value : null;
                if (_userSession == null) return await Task.FromResult(new AuthenticationState(_anynoums));

                var claimPrincple = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name,_userSession.UserName),
                new Claim(ClaimTypes.Role,_userSession.Role),
            }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimPrincple));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new AuthenticationState(_anynoums));
            }
        }
    }
}
