﻿#pragma checksum "..\..\FunWithTransforms.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "0630A06BB02FA9CC286BF7481F4635EC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace RenderingWithShapes {
    
    
    /// <summary>
    /// FunWithTransforms
    /// </summary>
    public partial class FunWithTransforms : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\FunWithTransforms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSkew;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\FunWithTransforms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRotate;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\FunWithTransforms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFlip;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\FunWithTransforms.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas myCanvas;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RenderingWithShapes;component/funwithtransforms.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FunWithTransforms.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btnSkew = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\FunWithTransforms.xaml"
            this.btnSkew.Click += new System.Windows.RoutedEventHandler(this.btnSkew_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnRotate = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\FunWithTransforms.xaml"
            this.btnRotate.Click += new System.Windows.RoutedEventHandler(this.btnRotate_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnFlip = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\FunWithTransforms.xaml"
            this.btnFlip.Click += new System.Windows.RoutedEventHandler(this.btnFlip_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 14 "..\..\FunWithTransforms.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.myCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

