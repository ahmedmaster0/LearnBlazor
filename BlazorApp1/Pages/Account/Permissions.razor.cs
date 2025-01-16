
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages.Account
{
    public partial class Permissions
    {
        [Parameter] public string? UserId { get; set; }

        public List<string> lst_permissions { get; set; } = new();

        protected override Task OnInitializedAsync()
        {
            if (!lst_permissions.Any())
            {
                lst_permissions.Add("CanEditUser");
                lst_permissions.Add("CanDeleteUser");
            }


            return base.OnInitializedAsync();
        }
    }
}
