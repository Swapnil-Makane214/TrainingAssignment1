﻿using System.Text.Json;

namespace TrainingAssignment1Blazor.Pages
{
    public partial class MachinesWithLatestAssetSeries
    {
        HttpClient client=new();
        private string url = "https://localhost:7107/api/MachinesWithLatestAssetSeries/";
        List<string> machines = new();
        protected override void OnInitialized()
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
        }
    }
}
