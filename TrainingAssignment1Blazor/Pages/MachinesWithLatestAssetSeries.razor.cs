using Microsoft.AspNetCore.Components;
using System.Text.Json;

namespace TrainingAssignment1Blazor.Pages
{
    public partial class MachinesWithLatestAssetSeries
    {
        HttpClient client=new();
        private string url = "https://localhost:7107/api/MachinesWithLatestAssetSeries/";
        List<string> machines = new();
        protected override void OnInitialized()
        {
           try
            {

                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(result))
                    {
                        machines = JsonSerializer.Deserialize<List<string>>(result)!;
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
