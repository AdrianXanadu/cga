﻿#pragma checksum "..\..\..\..\Views\ModifyLocationWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "83D2968BFFC6C69B6938C552D077F851B0917613"
//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using CGA_Client.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CGA_Client.Views {
    
    
    /// <summary>
    /// ModifyLocationWindow
    /// </summary>
    public partial class ModifyLocationWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 54 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_id;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_name;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_name_native;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBox_coordinates;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBox_isCapital;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox_locations;
        
        #line default
        #line hidden
        
        
        #line 88 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox_countries;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_save;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Views\ModifyLocationWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button_delete;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.2.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CGA_Client;V1.0.0.0;component/views/modifylocationwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\ModifyLocationWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.2.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.textBox_id = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.textBox_name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.textBox_name_native = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textBox_coordinates = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.checkBox_isCapital = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 6:
            this.listBox_locations = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.listBox_countries = ((System.Windows.Controls.ListBox)(target));
            return;
            case 8:
            this.button_save = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.button_delete = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

