using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;            // если используете MudBlazor
// using Blazored.FluentValidation;  // если будете валидировать формы

namespace PortfolioClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            // Привязка корневых компонентов
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // HttpClient для вызова API
            builder.Services.AddScoped(sp =>
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // Если используете MudBlazor — регистрируем сервисы
            builder.Services.AddMudServices();

            // Если подключаете FluentValidation на клиенте, добавляйте необходимые using
            // и компоненты <FluentValidationValidator /> в формы,
            // но дополнительных сервисов здесь не нужно.

            await builder.Build().RunAsync();
        }
    }
}
