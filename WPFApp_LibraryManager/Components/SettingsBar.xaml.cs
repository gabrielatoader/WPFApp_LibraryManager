using System.Windows;
using System.Windows.Controls;
using ControlzEx.Theming;
using MahApps.Metro.Controls;

namespace WPFApp_LibraryManager.Components
{
    public partial class SettingsBar : UserControl
    {
        public SettingsBar()
        {
            InitializeComponent();
        }
        
        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;

            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn)
                {
                    SetThemeSettings("Dark", ThemeAccentColor_Cbo.SelectedValue.ToString());
                }
                else
                {
                    SetThemeSettings("Light", ThemeAccentColor_Cbo.SelectedValue.ToString());
                }
            }
        }

        private void ThemeAccentColor_Cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThemeSwitch_Tgl.IsOn)
            {
                SetThemeSettings("Dark", ThemeAccentColor_Cbo.SelectedValue.ToString());
            }
            else
            {
                SetThemeSettings("Light", ThemeAccentColor_Cbo.SelectedValue.ToString());
            }
        }

        private void SetThemeSettings(string colorTheme, string colorAccent)
        {
            string themeSettings = $"{colorTheme}.{colorAccent}";

            ThemeManager.Current.ChangeTheme(Application.Current.MainWindow, themeSettings);
        }
    }
}
