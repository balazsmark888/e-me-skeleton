using Xamarin.Forms;

namespace e_me.mobile.Settings
{
    public static class ApplicationSettings
    {
        public static T GetSettingOrDefault<T>(string key)
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                return (T)Application.Current.Properties[key];
            }

            return default;
        }

        public static T SetSetting<T>(string key, T value)
        {
            var current = GetSettingOrDefault<T>(key);
            Application.Current.Properties[key] = value;
            return current;
        }

        public static T RemoveSetting<T>(string key)
        {
            var current = GetSettingOrDefault<T>(key);
            if (Application.Current.Properties.ContainsKey(key))
            {
                Application.Current.Properties.Remove(key);
            }

            return current;
        }
    }
}