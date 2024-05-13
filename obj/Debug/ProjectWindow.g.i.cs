﻿#pragma checksum "..\..\ProjectWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2380D1C49C2B1C0587807632BA08AACDCB456E13BC61B0F250CC32E06B8557BD"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using HelixToolkit.Wpf;
using ProjPL3D;
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


namespace ProjPL3D {
    
    
    /// <summary>
    /// ProjectWindow
    /// </summary>
    public partial class ProjectWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\ProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid panelGrid;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox sceneNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox sceneListBox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\ProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal HelixToolkit.Wpf.HelixViewport3D viewport;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\ProjectWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock cameraPositionTextBlock;
        
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
            System.Uri resourceLocater = new System.Uri("/ProjPL3D;component/projectwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ProjectWindow.xaml"
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
            
            #line 14 "..\..\ProjectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExitButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.panelGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.sceneNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            
            #line 33 "..\..\ProjectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CreateButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 36 "..\..\ProjectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSceneButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.sceneListBox = ((System.Windows.Controls.ListBox)(target));
            
            #line 37 "..\..\ProjectWindow.xaml"
            this.sceneListBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SceneListBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.viewport = ((HelixToolkit.Wpf.HelixViewport3D)(target));
            return;
            case 8:
            this.cameraPositionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            
            #line 92 "..\..\ProjectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddCubeButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 93 "..\..\ProjectWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPyramidButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

