﻿#pragma checksum "..\..\BookManagementWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2FC5B2F929FAD7E834C18CF10ACA08F796CB6C9FF0512E5C8DAD49CA101D94E2"
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


namespace LMS {
    
    
    /// <summary>
    /// BookManagementWindow
    /// </summary>
    public partial class BookManagementWindow : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\BookManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid BookDataGrid;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\BookManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrevBtn;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\BookManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PageInfoTextBlock;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\BookManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button NextBtn;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\BookManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteBookBtn;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\BookManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\BookManagementWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SearchPlaceholder;
        
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
            System.Uri resourceLocater = new System.Uri("/LMS;component/bookmanagementwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BookManagementWindow.xaml"
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
            this.BookDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 2:
            this.PrevBtn = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\BookManagementWindow.xaml"
            this.PrevBtn.Click += new System.Windows.RoutedEventHandler(this.PreviousBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PageInfoTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.NextBtn = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\BookManagementWindow.xaml"
            this.NextBtn.Click += new System.Windows.RoutedEventHandler(this.NextBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 84 "..\..\BookManagementWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBook_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 86 "..\..\BookManagementWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditBook_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DeleteBookBtn = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\BookManagementWindow.xaml"
            this.DeleteBookBtn.Click += new System.Windows.RoutedEventHandler(this.DeleteBook_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 99 "..\..\BookManagementWindow.xaml"
            this.SearchTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.SearchTextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 100 "..\..\BookManagementWindow.xaml"
            this.SearchTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.SearchTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SearchPlaceholder = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            
            #line 107 "..\..\BookManagementWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SearchBook_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

