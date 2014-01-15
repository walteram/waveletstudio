using System;

namespace WaveletStudio.Designer.Documents
{
    public class CurrentUserDiscoverer
    {
        public string Discover()
        {
            var currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
            if (currentUser != null)
            {
                return currentUser.Name;
            }
            return Environment.UserName;
        }
    }
}
