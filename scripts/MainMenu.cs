using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Godot;

public partial class MainMenu : Control
{
    private static string SERVER_URL = "http://localhost:5000";

    private static System.Net.Http.HttpClient client = new() { BaseAddress = new Uri(SERVER_URL), };

    private async void OnCreatePressed()
    {
        using StringContent jsonContent =
            new(JsonSerializer.Serialize(new { }), Encoding.UTF8, "application/json");

        using HttpResponseMessage response = await client.PostAsync("create", jsonContent);

        if (!response.IsSuccessStatusCode)
        {
            // TODO handle failure
            GD.PrintErr("Failed to create lobby");
            return;
        }
        var jsonRespoonse = await response.Content.ReadAsStringAsync();
        GD.Print($"Hosting: {jsonRespoonse}");
    }

    private void OnExitPressed()
    {
        GetTree().Quit();
    }
}
