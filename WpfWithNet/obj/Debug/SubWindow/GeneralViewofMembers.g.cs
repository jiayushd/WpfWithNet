﻿#pragma checksum "..\..\..\SubWindow\GeneralViewofMembers.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "157EE41F7D5D7042A36CCD29E3F2D89FE26994B9"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
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
using WpfWithNet.SubWindow;


namespace WpfWithNet.SubWindow {
    
    
    /// <summary>
    /// GeneralViewofMembers
    /// </summary>
    public partial class GeneralViewofMembers : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 9 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition gridcolumn1;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgGeneralViewofMembers;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbIndicator;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgDetailViewofMember;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfWithNet;component/subwindow/generalviewofmembers.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
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
            
            #line 8 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            ((WpfWithNet.SubWindow.GeneralViewofMembers)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.gridcolumn1 = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 4:
            this.dgGeneralViewofMembers = ((System.Windows.Controls.DataGrid)(target));
            
            #line 21 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            this.dgGeneralViewofMembers.MouseLeave += new System.Windows.Input.MouseEventHandler(this.dgGeneralViewofMembers_MouseLeave);
            
            #line default
            #line hidden
            
            #line 22 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            this.dgGeneralViewofMembers.MouseEnter += new System.Windows.Input.MouseEventHandler(this.dgGeneralViewofMembers_MouseEnter);
            
            #line default
            #line hidden
            return;
            case 11:
            this.tbIndicator = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.dgDetailViewofMember = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 5:
            
            #line 67 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDomesdic_Click);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 78 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonGlobal_Click);
            
            #line default
            #line hidden
            break;
            case 7:
            
            #line 89 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonTodo_Click);
            
            #line default
            #line hidden
            break;
            case 8:
            
            #line 100 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonFirstVirsion_Click);
            
            #line default
            #line hidden
            break;
            case 9:
            
            #line 111 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonOAtotal_Click);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 127 "..\..\..\SubWindow\GeneralViewofMembers.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonOAin30_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

