﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34011
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Positive.Model.Languages {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CRM {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CRM() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SampleArch.Model.Languages.CRM", typeof(CRM).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Firma Adı.
        /// </summary>
        public static string FirmName {
            get {
                return ResourceManager.GetString("FirmName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Messis No.
        /// </summary>
        public static string MersisNo {
            get {
                return ResourceManager.GetString("MersisNo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not.
        /// </summary>
        public static string Note {
            get {
                return ResourceManager.GetString("Note", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vergi No.
        /// </summary>
        public static string TaxNumber {
            get {
                return ResourceManager.GetString("TaxNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Vergi Dairesi.
        /// </summary>
        public static string TaxOffice {
            get {
                return ResourceManager.GetString("TaxOffice", resourceCulture);
            }
        }
    }
}