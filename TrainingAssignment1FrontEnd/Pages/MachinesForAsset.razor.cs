using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace TrainingAssignment1FrontEnd.Pages
{
    public partial class MachinesForAsset
    {
        [Parameter]
        public string? assetName { get; set; }
        private string? url = "https://localhost:7168/api/GetMachinesUsesAsset/";
        HttpClient httpClient = new HttpClient();
        List<string> machinesForAsset = new();
        protected override void OnInitialized()
        {
            if(string.IsNullOrEmpty(assetName))
            {
                NavigationManager!.NavigateTo($"/ErrorPage/Asset Name is required", true);
            }
            try
            {
                
                var response = httpClient.GetAsync(url + assetName).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    machinesForAsset = JsonSerializer.Deserialize<List<string>>(result)!;
                }
                else
                {
                    NavigationManager!.NavigateTo($"/ErrorPage/No Data Found", true);
                }
            }
            catch
            {
                NavigationManager!.NavigateTo($"/ErrorPage");
            }
        }
    }
}
