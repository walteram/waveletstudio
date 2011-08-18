using WaveletStudio.MainApplication.Properties;

namespace WaveletStudio.MainApplication
{
    public static class ApplicationUtils
    {
        public static string GetResourceString(string name)
        {
            var resource = Resources.ResourceManager.GetObject(name.ToLower()) ?? "";
            if (resource.GetType() != typeof(string)) 
            {
                resource = Resources.ResourceManager.GetString(name.ToLower() + "_text") ?? "";
            }
            if (resource.ToString() == "")
            {
                resource = name;
            }
            return resource.ToString();
        }
    }
}
