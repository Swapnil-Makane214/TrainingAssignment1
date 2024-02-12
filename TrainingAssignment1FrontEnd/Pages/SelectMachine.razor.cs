using Microsoft.AspNetCore.Components;
using System.Text.Json;
using TrainingAssignment1FrontEnd.Models;

namespace TrainingAssignment1FrontEnd.Pages
{
    public partial class SelectMachine
    {
        //public string? machineName { get; set; }
        private string? url = "https://localhost:7168/api/MachineAsset/GetMachines/";
        HttpClient httpClient = new HttpClient();
        List<Machine> machines = new();
        public string? selectedValue { get; set; }
        protected override void OnInitialized()
        {
            try
            {
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    machines = JsonSerializer.Deserialize<List<Machine>>(result)!;
                }
                else
                {
                    throw new Exception("No Data Found") { HResult = -1 };
                }
            }
            catch (Exception ex)
            {

                if(ex.HResult== -1)
                NavigationManager.NavigateTo($"/ErrorPage/{ex.Message}", true);
                NavigationManager.NavigateTo($"/ErrorPage/", true);
            }
        }
        protected void HandleSelectionChange()
        {
            NavigationManager.NavigateTo($"/AssetsForMachine/{selectedValue}", true);
        }
    }
}
