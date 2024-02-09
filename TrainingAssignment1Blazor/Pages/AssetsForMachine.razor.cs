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
            try
            {
                if (string.IsNullOrEmpty(machineName))
                {
                    throw new Exception($"Machine Name is required") { HResult = -1 };
                }
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
                    throw new Exception("No Data Found") { HResult = -1 };
                }
            }
            catch(Exception ex) 
            {
                if (ex.HResult == -1)
                    NavigationManager.NavigateTo($"/ErrorPage/{ex.Message}", true);
                NavigationManager.NavigateTo($"/ErrorPage/", true);
            }
        }
    }
}
