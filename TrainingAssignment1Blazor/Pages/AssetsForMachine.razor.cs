using Microsoft.AspNetCore.Components;
using System.Text.Json;
using TrainingAssignment1Blazor.Models;
using static System.Net.WebRequestMethods;

namespace TrainingAssignment1Blazor.Pages
{
    public partial class AssetsForMachine
    {
        [Parameter]
        public string? machineName { get; set; }
        Machine? machine  = new();
        string? url = "https://localhost:7107/api/GetMachine/";
        HttpClient httpClient = new HttpClient();
        protected override void OnInitialized()
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
        }
    }
}
