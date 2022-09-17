using Microsoft.AspNetCore.Components.WebView.Maui;
using Drag.Data;
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Components.Web;

namespace Drag;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
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
#endif

		builder.Services.AddSingleton<WeatherForecastService>();


        // For getting the window HWnd (There is probably a better way, but this works)
#if WINDOWS
		builder.ConfigureLifecycleEvents(events =>
        {
            events.AddWindows(wndLifeCycleBuilder =>
            {
                wndLifeCycleBuilder.OnWindowCreated(GetHWnd);
            });
        });
#endif

        return builder.Build();
	}

    // Get the window HWnd
    public static IntPtr HWnd;
    private static void GetHWnd(Microsoft.UI.Xaml.Window window)
    {
        HWnd = WindowNative.GetWindowHandle(window);
    }


    // Used in the app:
    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    public static void Drag()
    {
        SendMessage(MauiProgram.HWnd, 0xA1, (IntPtr)0x2, (IntPtr)0);
        ReleaseCapture();
    }
}
