﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PL2D.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Escape")]
        public global::Microsoft.Xna.Framework.Input.Keys Exit {
            get {
                return ((global::Microsoft.Xna.Framework.Input.Keys)(this["Exit"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("W")]
        public global::Microsoft.Xna.Framework.Input.Keys MoveUp {
            get {
                return ((global::Microsoft.Xna.Framework.Input.Keys)(this["MoveUp"]));
            }
            set {
                this["MoveUp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("S")]
        public global::Microsoft.Xna.Framework.Input.Keys MoveDown {
            get {
                return ((global::Microsoft.Xna.Framework.Input.Keys)(this["MoveDown"]));
            }
            set {
                this["MoveDown"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("A")]
        public global::Microsoft.Xna.Framework.Input.Keys MoveLeft {
            get {
                return ((global::Microsoft.Xna.Framework.Input.Keys)(this["MoveLeft"]));
            }
            set {
                this["MoveLeft"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D")]
        public global::Microsoft.Xna.Framework.Input.Keys MoveRight {
            get {
                return ((global::Microsoft.Xna.Framework.Input.Keys)(this["MoveRight"]));
            }
            set {
                this["MoveRight"] = value;
            }
        }
    }
}
