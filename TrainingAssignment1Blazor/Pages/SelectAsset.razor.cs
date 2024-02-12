using Microsoft.AspNetCore.Components;
using System.Text.Json;
using TrainingAssignment1Blazor.Models;

namespace TrainingAssignment1Blazor.Pages
{
    public partial class SelectAsset
    {
        string url = "https://localhost:7107/api/MachineAsset/GetAssets/";
        HttpClient httpClient = new HttpClient();
        //List<Asset> assets = new();
        public string? selectedValue { get; set; }
        HashSet<string> set = new();
        protected override void OnInitialized()
        {
            try
            {
                var response = httpClient.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    List<Asset> assets = JsonSerializer.Deserialize<List<Asset>>(result)!;
                    

                    foreach (var asset in assets) 
                    {
                        set.Add(asset.assetName!);
                    }
                }
                else
                {
                    throw new Exception("No Data Found") { HResult = -1 };
                }
            }
            catch (Exception ex)
            {

                if (ex.HResult == -1)
                    NavigationManager.NavigateTo($"/ErrorPage/{ex.Message}", true);
                NavigationManager.NavigateTo($"/ErrorPage/", true);
            }
        }
        protected void HandleSelectionChange()
        {
            NavigationManager.NavigateTo($"/MachinesForAsset/{selectedValue}", true);
        }
    }
}
