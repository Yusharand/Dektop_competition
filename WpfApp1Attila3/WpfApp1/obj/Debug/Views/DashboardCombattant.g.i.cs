﻿#pragma checksum "..\..\..\Views\DashboardCombattant.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A89BF8C94DF58A6AE706C1B28D19ACA0AFE6E80D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.IconPacks;
using MahApps.Metro.IconPacks.Converter;
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
using WpfApp1.Views;


namespace WpfApp1.Views {
    
    
    /// <summary>
    /// DashboardCombattant
    /// </summary>
    public partial class DashboardCombattant : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 36 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse imageLogoDash;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock nomCompet;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lieuCompet;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnListeC;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnListeCombats;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnListeClub;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddButton;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveButton;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\Views\DashboardCombattant.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid combattantsDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/views/dashboardcombattant.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\DashboardCombattant.xaml"
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
            
            #line 17 "..\..\..\Views\DashboardCombattant.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\Views\DashboardCombattant.xaml"
            ((System.Windows.Controls.Border)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.imageLogoDash = ((System.Windows.Shapes.Ellipse)(target));
            return;
            case 3:
            this.nomCompet = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.lieuCompet = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            
            #line 53 "..\..\..\Views\DashboardCombattant.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Information_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnListeC = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\Views\DashboardCombattant.xaml"
            this.btnListeC.Click += new System.Windows.RoutedEventHandler(this.ListeCombattant_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnListeCombats = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\..\Views\DashboardCombattant.xaml"
            this.btnListeCombats.Click += new System.Windows.RoutedEventHandler(this.BtnListeCombats_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnListeClub = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\..\Views\DashboardCombattant.xaml"
            this.btnListeClub.Click += new System.Windows.RoutedEventHandler(this.BtnListeClub_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 78 "..\..\..\Views\DashboardCombattant.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.LogOut_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 84 "..\..\..\Views\DashboardCombattant.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.FormCombat_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 90 "..\..\..\Views\DashboardCombattant.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Retour_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.AddButton = ((System.Windows.Controls.Button)(target));
            
            #line 124 "..\..\..\Views\DashboardCombattant.xaml"
            this.AddButton.Click += new System.Windows.RoutedEventHandler(this.AjoutCombattant_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.RemoveButton = ((System.Windows.Controls.Button)(target));
            
            #line 130 "..\..\..\Views\DashboardCombattant.xaml"
            this.RemoveButton.Click += new System.Windows.RoutedEventHandler(this.TSupprimer_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.combattantsDataGrid = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

