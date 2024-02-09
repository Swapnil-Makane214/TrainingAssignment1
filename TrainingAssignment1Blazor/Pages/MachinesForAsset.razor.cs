using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace TrainingAssignment1Blazor.Pages
{
    public partial class MachinesForAsset
    {
        [Parameter]
        public string? assetName { get; set; }
        private string? url = "https://localhost:7107/api/GetMachinesUsesAsset/";
        HttpClient httpClient = new HttpClient();
        List<string> machinesForAsset = new();
        protected override void OnInitialized()
        {
            try
            {
                var response = httpClient.GetAsync(url + assetName).Result;
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (!String.IsNullOrEmpty(result))
                    {
                        machinesForAsset = JsonSerializer.Deserialize<List<string>>(result)!;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
