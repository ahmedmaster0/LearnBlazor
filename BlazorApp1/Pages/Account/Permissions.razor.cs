
using BlazorApp1.ViewModels;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorApp1.Pages.Account
{
    public partial class Permissions
    {
        [Parameter] public string? UserId { get; set; }
        public bool _isLoading { get; set; } = false;
        public bool SelectAllCheckBox { get; set; } = false;
        [Inject] UserManager<ApplicationUser> userManager { get; set; }
        [Inject] public SweetAlertService alertService { get; set; }
        [Inject] public NavigationManager navigationManager { get; set; }

        public List<PermissionModel> lst_permissions { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            if (!lst_permissions.Any())
            {
                lst_permissions.Add(new PermissionModel
                {
                    text = "CanEditUser",
                    IsActive = false
                });
                lst_permissions.Add(new PermissionModel
                {
                    text = "CanDeleteUser",
                    IsActive = false
                });
                lst_permissions.Add(new PermissionModel
                {
                    text = "CanAddPermission",
                    IsActive = false
                });
            }


            var usr = await userManager.FindByIdAsync(UserId);
            IList<Claim> userClaims = await userManager.GetClaimsAsync(usr);

            var newlst = lst_permissions.Select(p => new PermissionModel
            {
                text = p.text,
                IsActive = userClaims.Any(m => m.Value.Equals(p.text)) ? true : false
            }).ToList();

            lst_permissions.Clear();
            lst_permissions.AddRange(newlst);

            //return base.OnInitializedAsync();
        }

        private void ShowSpinner(bool isLoading)
        {
            _isLoading = isLoading;
            StateHasChanged();
        }
        public async Task AssignPermission()
        {

            _isLoading = true;

            var usr = await userManager.FindByIdAsync(UserId);
            var claims = await userManager.GetClaimsAsync(usr);
            var result_remove = await userManager.RemoveClaimsAsync(usr,claims);
            if (result_remove.Succeeded)
            {
                var SelectedClaims = lst_permissions.Where(m => m.IsActive && !m.text.Equals("all")).ToList();
                List<Claim> AddedClaims =  SelectedClaims.Select(m => new Claim("Permission", m.text)).ToList();

                IdentityResult identityResult =  await userManager.AddClaimsAsync(usr, AddedClaims);
                if (identityResult.Succeeded)
                {
                    ShowSpinner(false);
                    var alert = await alertService.FireAsync(new SweetAlertOptions
                    {
                        Title = "suucess",
                        Text = "Permission Added Successfully, You Must Login In Again",
                        Icon = SweetAlertIcon.Success,
                        ConfirmButtonText = "Ok"
                    });
                    if(alert.IsConfirmed)
                    {
                        navigationManager.NavigateTo("/users");
                    }
                }
                else
                {
                    ShowSpinner(false);
                    await alertService.FireAsync(new SweetAlertOptions
                    {
                        Title = "error",
                        Text = identityResult.Errors.FirstOrDefault().Description,
                        Icon = SweetAlertIcon.Error,
                        ConfirmButtonText = "Ok"
                    });
                }
            }
            else
            {
                ShowSpinner(false);
                await alertService.FireAsync(new SweetAlertOptions
                {
                    Title = "error",
                    Text = result_remove.Errors.FirstOrDefault().Description,
                    Icon = SweetAlertIcon.Error,
                    ConfirmButtonText = "Ok"
                });
            }

        }

        public void fun_SelectAllCheckBox()
        {
            SelectAllCheckBox = !SelectAllCheckBox;

            foreach (var item in lst_permissions)
            {
                item.IsActive = SelectAllCheckBox;
            }
        }

    }
}
