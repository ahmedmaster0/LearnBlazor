using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Pages
{
    public partial class CustomLoading
    {
        [Parameter] public bool IsLoading { get; set; } = false;
    }
}
