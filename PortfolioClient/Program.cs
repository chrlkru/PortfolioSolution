using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;            // ���� ����������� MudBlazor
// using Blazored.FluentValidation;  // ���� ������ ������������ �����

namespace PortfolioClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // �������� �������� �����������
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // HttpClient ��� ������ API
            builder.Services.AddScoped(sp =>
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // ���� ����������� MudBlazor � ������������ �������
            builder.Services.AddMudServices();

            // ���� ����������� FluentValidation �� �������, ���������� ����������� using
            // � ���������� <FluentValidationValidator /> � �����,
            // �� �������������� �������� ����� �� �����.

            await builder.Build().RunAsync();
        }
    }
}
