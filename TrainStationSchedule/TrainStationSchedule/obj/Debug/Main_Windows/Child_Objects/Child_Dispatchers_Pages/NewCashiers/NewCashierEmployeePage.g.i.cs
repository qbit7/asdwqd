﻿#pragma checksum "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D4E8298311EDC9F5A5E89D34EF16535E9217E52EE0A8FD4122BB9FB3C0F6A052"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
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
using TrainStationSchedule.Main_Windows.Child_Objects.Child_Dispatchers_Pages.NewCashiers;


namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Dispatchers_Pages.NewCashiers {
    
    
    /// <summary>
    /// NewCashierEmployeePage
    /// </summary>
    public partial class NewCashierEmployeePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CashierSurNameBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CashierNameBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CashierPatronymicBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox InputLoginBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox InputPasswordBox;
        
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
            System.Uri resourceLocater = new System.Uri("/TrainStationSchedule;component/main_windows/child_objects/child_dispatchers_page" +
                    "s/newcashiers/newcashieremployeepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml"
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
            this.CashierSurNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.CashierNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.CashierPatronymicBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.InputLoginBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.InputPasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 6:
            
            #line 29 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddCashierButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 31 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Dispatchers_Pages\NewCashiers\NewCashierEmployeePage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveCashierButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

