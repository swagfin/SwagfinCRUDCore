using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SwagfinRESTServices;
namespace SwagfinUIXComponent
{
    public  class UIXPackageManager
    {
        protected string UIXBasePath { get; set; }

        public UIXPackageManager(string uixBasePath)
        {
            this.UIXBasePath = uixBasePath;
        }

        #region GetInstalledUIXPackages
        public List<UIXPackage> GetInstalledUIXPackages()
        {
            List<UIXPackage> Packages = new List<UIXPackage>();
            if (Directory.Exists(this.UIXBasePath) == false) return Packages;
            //If Directory Exists
            foreach (string dir in Directory.GetDirectories(this.UIXBasePath))
            {
                try
                {
                    string uix_xml = dir + "\\uix.json";
                    if (File.Exists(uix_xml))
                    {
                        var packageData = File.ReadAllText(uix_xml);
                        UIXPackage jsonData = JsonConvert.DeserializeObject<UIXPackage>(packageData);
                        //Update Directory
                        jsonData.UIX_InstallDirectory = dir;
                        if (jsonData != null)
                            Packages.Add(jsonData);
                    }
                }
                catch (Exception)
                {

                }

            }
            return Packages;
        }

        #endregion

        #region GetInstalledUIXPackagesAsync
        public async Task<List<UIXPackage>> GetInstalledUIXPackagesAsync()
        {
            return await  Task.Run(() => this.GetInstalledUIXPackages());
        }

        #endregion


        #region GetOnlineUIXPackagesAsync
        public async Task<List<UIXPackage>> GetOnlineUIXPackagesAsync(string SearchString="")
        {
            try
            {
                //Parameters are Optional and Are set my THe UIX Package Manager
                List<UrlParameter> DataParameters = new List<UrlParameter>
                {
                    new UrlParameter
                    {
                        Key="search",
                        Value=SearchString
                    }
                };

                string api_url = "http://apps.swagfinserver.com/api/uix_repository_api";
                string api_response = await API.GetResponceViaGETAsync(api_url, DataParameters);
                IEnumerable<UIXPackage> jsonData = JsonConvert.DeserializeObject<IEnumerable<UIXPackage>>(api_response);
                return jsonData.ToList(); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); 
            }

           
        }
        #endregion


    }
}
