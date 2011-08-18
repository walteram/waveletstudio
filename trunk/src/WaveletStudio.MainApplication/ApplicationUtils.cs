using WaveletStudio.MainApplication.Properties;

namespace WaveletStudio.MainApplication
{
    public static class ApplicationUtils
    {
        public static string GetResourceString(string name)
        {
            var key = name.ToLower().Replace(" ", "");
            var resource = Resources.ResourceManager.GetObject(key) ?? "";
            if (resource.GetType() != typeof(string)) 
            {
                resource = Resources.ResourceManager.GetString(key + "_text") ?? "";
            }
            if (resource.ToString() == "")
            {
                resource = name;
            }
            return resource.ToString();
        }
    }
}
