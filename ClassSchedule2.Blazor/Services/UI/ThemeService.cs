using Microsoft.JSInterop;

namespace ClassSchedule2.Blazor.Services.UI
{
    public class ThemeService
    {
        private readonly IJSRuntime _jsRuntime;

        public bool IsDarkMode { get; private set; }
        public bool IsInitialized { get; private set; }

        public event Action? OnThemeChanged;

        public ThemeService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync()
        {
            if (IsInitialized)
            {
                // Hvis vi allerede har hentet fra localStorage én gang i denne session,
                // skal vi blot gen-anvende klassen på DOM'en uden at spørge JS/localStorage igen.
                await ApplyThemeAsync();
                return;
            }

            try
            {
                var savedTheme = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "theme");

                if (!string.IsNullOrEmpty(savedTheme))
                {
                    IsDarkMode = savedTheme == "dark";
                }
                else
                {
                    IsDarkMode = await _jsRuntime.InvokeAsync<bool>("eval", "window.matchMedia('(prefers-color-scheme: dark)').matches");
                }

                IsInitialized = true;
                await ApplyThemeAsync();
                OnThemeChanged?.Invoke();
            }
            catch (JSException)
            {
                // Håndterer prerendering
            }
        }

        public async Task ToggleThemeAsync()
        {
            IsDarkMode = !IsDarkMode;
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "theme", IsDarkMode ? "dark" : "light");
            await ApplyThemeAsync();
            OnThemeChanged?.Invoke();
        }

        private async Task ApplyThemeAsync()
        {
            if (IsDarkMode)
            {
                await _jsRuntime.InvokeVoidAsync("eval", "document.documentElement.classList.add('dark')");
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("eval", "document.documentElement.classList.remove('dark')");
            }
        }
    }
}
