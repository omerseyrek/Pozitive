using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SampleArch.Model.Core
{
    public class MVCSessionManager : ISessionManager
    {

        public string GetUserProfileLang()
        {
            var profile = HttpContext.Current.Session["_userProfile"];

            if (profile != null)
            {
                return ((UserProfileViewModel)profile).LanguageCode.ToUpper();
            }
            return string.Empty;
        }


        public UserProfileViewModel GetUserProfile()
        {
            var profile = HttpContext.Current.Session["_userProfile"];

            if (profile != null)
            {
                return (UserProfileViewModel)profile;
            }
            return null;
        }
    }
}
