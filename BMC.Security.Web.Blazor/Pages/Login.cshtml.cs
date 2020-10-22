using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BMC.Security.Web.Blazor.Data;
using System.Linq;
using BMC.Security.Tools;

namespace BMC.Security.Web.Blazor.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        bool Check(string Email, string Password)
        {
            var enc = new Encryption();
            var isAdm = (from a in AppConstants.AdminAccounts
                         where a.Email.ToLower() == Email.ToLower()
                         && a.Password == enc.Encrypt(Password)
                         select a).FirstOrDefault();

            if (isAdm == null)
            {
                //toastService.ShowError("Sorry, login invalid");
                return false;
            }
            return true;
        }
        public string ReturnUrl { get; set; }
        public async Task<IActionResult>OnGetAsync(string paramUsername, string paramPassword)
        {
          
            string returnUrl = Url.Content("~/");
            try
            {
                // Clear the existing external cookie
                await HttpContext
                    .SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch { }
            bool isAuthenticate = true;
            isAuthenticate = Check(paramUsername, paramPassword);
            // In this example we just log the user in
            // (Always log the user in for this demo)
            if (isAuthenticate)
            {
                // *** !!! This is where you would validate the user !!! ***
                // In this example we just log the user in
                // (Always log the user in for this demo)
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, paramUsername),
                new Claim(ClaimTypes.Role, "Administrator"),
            };
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    RedirectUri = this.Request.Host.Value
                };
                try
                {
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                }
            }
            if (!isAuthenticate) returnUrl = "/auth/login?result=false";
            return LocalRedirect(returnUrl);
        }
    }
}
