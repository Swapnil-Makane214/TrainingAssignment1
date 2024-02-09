﻿using Microsoft.AspNetCore.Components;
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
                if(string.IsNullOrEmpty(assetName))
                {
                    throw new Exception($"Asset Name is required") { HResult=-1};
                }
                
                var response = httpClient.GetAsync(url + assetName).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    machinesForAsset = JsonSerializer.Deserialize<List<string>>(result)!;
                }
                else
                {
                    throw new Exception("No Data Found") { HResult = -1 };
                }
            }
            catch(Exception ex)
            {
                
                if(ex.HResult== -1)
                NavigationManager.NavigateTo($"/ErrorPage/{ex.Message}", true);
                NavigationManager.NavigateTo($"/ErrorPage/", true);
            }
        }
    }
}