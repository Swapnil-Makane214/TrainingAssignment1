using Microsoft.AspNetCore.Components;
using System.Text.Json;
using TrainingAssignment1Blazor.Models;

namespace TrainingAssignment1Blazor.Pages
{
    public partial class AssetsForMachine
    {
        [Parameter]
        public string? machineName { get; set; } =string.Empty;
        Machine? machine  = new();
        string? url = "https://localhost:7107/api/GetMachine/";
        HttpClient httpClient = new HttpClient();
        protected override void OnInitialized()
        {
            if (string.IsNullOrEmpty(machineName))
            {
                NavigationManager.NavigateTo($"/ErrorPage/Machine Name is required", true); 
            }
            try
            {
                var response=httpClient.GetAsync(url+machineName).Result;
                if(response.IsSuccessStatusCode)
                {
                    var result=response.Content.ReadAsStringAsync().Result;
                    if (!String.IsNullOrEmpty(result))
                    {
                        machine = JsonSerializer.Deserialize<Machine>(result);
                    }
                }
                else
                {
                    NavigationManager.NavigateTo($"/ErrorPage/No Data Found", true); 
                }
            }
            catch
            { 
                NavigationManager.NavigateTo($"/ErrorPage", true);
            }
        }
    }
}
