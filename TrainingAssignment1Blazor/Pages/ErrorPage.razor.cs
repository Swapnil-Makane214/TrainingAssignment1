using Microsoft.AspNetCore.Components;
using System.Net;
namespace TrainingAssignment1Blazor.Pages
{
    public partial class ErrorPage
    {
        [Parameter]
        public string? ErrorMessage { get; set; } 
        protected override void OnInitialized()
        {
            if(string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = "Something Went Wrong";
            }
        }
    }
}
