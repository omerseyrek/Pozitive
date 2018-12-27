using Kendo.Mvc.UI;
using SampleArch.Model;
using SampleArch.Model.Core;
using SampleArch.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Positive
{
    public class ToolBox
    {
        public static List<TreeViewItemModel> ConvertToTreeViewItemModel(List<SampleArch.Model.Menu> Menu)
        {
            List<TreeViewItemModel> menus = new List<TreeViewItemModel>();
            foreach (SampleArch.Model.Menu mvm in Menu)
            {
                TreeViewItemModel menuModel = new TreeViewItemModel();
                ConvertToTreeViewItemModel(mvm, menuModel);
                menus.Add(menuModel);
            }

            return menus;
        }
        
        public static void ConvertToTreeViewItemModel(SampleArch.Model.Menu Menu, TreeViewItemModel model)
        {
           KeyValuePair<string, string> kvp = new KeyValuePair<string, string>("naw-url", Menu.URL); 

            model.Id = Menu.Id.ToString();
            model.HtmlAttributes.Add(kvp);
            model.Text = Menu.Description;

            if (Menu.SubMenu != null && Menu.SubMenu.Count > 0)
            {
                model.HasChildren = true;
                model.Items = new List<TreeViewItemModel>();
                foreach (SampleArch.Model.Menu submenu in Menu.SubMenu)
                {
                    TreeViewItemModel subMenuModel = new TreeViewItemModel();
                    ConvertToTreeViewItemModel(submenu, subMenuModel);
                    model.Items.Add(subMenuModel);
                }
            }
            else
            {
               // model.HasChildren = false;
            }
        }


        public static UserProfileViewModel  GetUserProfile()
        {
            var profile = HttpContext.Current.Session["_userProfile"];

            if (profile != null)
            {
                return (UserProfileViewModel)profile;
            }
            return null;
        }



        public static string  GetUserProfileLang()
        {
            var profile = HttpContext.Current.Session["_userProfile"];

            if (profile != null)
            {
                return ((UserProfileViewModel)profile).LanguageCode;
            }
            return string.Empty;
        }



        public static CultureInfo GetUserProfileCulture()
        {
            var profile = HttpContext.Current.Session["_userProfile"];

            if (profile != null)
            {
                string lang = ((UserProfileViewModel)profile).LanguageCode.ToUpper();

                if (lang == EnumDefinitions.Language.EN.ToString())
                {
                    return new CultureInfo("en-GB");
                }
                else if ( lang == EnumDefinitions.Language.TR.ToString())
                {
                    return new CultureInfo("tr-TR");
                }
            }
            return null;
        }
    }
}