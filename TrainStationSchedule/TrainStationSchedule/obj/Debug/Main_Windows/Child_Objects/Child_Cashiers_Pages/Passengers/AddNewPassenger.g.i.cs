﻿#pragma checksum "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Cashiers_Pages\Passengers\AddNewPassenger.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C51D3EA19AAA34200313795B35D03D2F20565A29438ADCAB834AE72618699AF8"
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
using TrainStationSchedule.Main_Windows.Child_Objects.Child_Cashiers_Pages.Passengers;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Converters;
using Xceed.Wpf.Toolkit.Core;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Mag.Converters;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace TrainStationSchedule.Main_Windows.Child_Objects.Child_Cashiers_Pages.Passengers {
    
    
    /// <summary>
    /// AddNewPassenger
    /// </summary>
    public partial class AddNewPassenger : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 30 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Cashiers_Pages\Passengers\AddNewPassenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FullNameBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Cashiers_Pages\Passengers\AddNewPassenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.MaskedTextBox PassportBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Cashiers_Pages\Passengers\AddNewPassenger.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.MaskedTextBox PhoneBox;
        
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
            System.Uri resourceLocater = new System.Uri("/TrainStationSchedule;component/main_windows/child_objects/child_cashiers_pages/p" +
                    "assengers/addnewpassenger.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Cashiers_Pages\Passengers\AddNewPassenger.xaml"
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
            this.FullNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.PassportBox = ((Xceed.Wpf.Toolkit.MaskedTextBox)(target));
            return;
            case 3:
            this.PhoneBox = ((Xceed.Wpf.Toolkit.MaskedTextBox)(target));
            return;
            case 4:
            
            #line 42 "..\..\..\..\..\..\Main_Windows\Child_Objects\Child_Cashiers_Pages\Passengers\AddNewPassenger.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPassenger_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

