﻿using FinalCoffee1.Modules.Admin.service;
using FinalCoffee1.common.helperServices;
using Microsoft.Extensions.Logging;
using FinalCoffee1.Modules.Coffee.service;
using FinalCoffee1.Modules.Staff.service;
namespace FinalCoffee1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<AdminService>();
            builder.Services.AddSingleton<CoffeeService>();
            builder.Services.AddSingleton<NotificationService>();
            builder.Services.AddSingleton<SessionService>();
            builder.Services.AddSingleton<AuthenticationService>();
            builder.Services.AddSingleton<ActionService>();
            builder.Services.AddSingleton<StaffService>();
            return builder.Build();
        }
    }
}
