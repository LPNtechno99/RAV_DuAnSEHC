﻿#pragma checksum "..\..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C3129D4AE1122B40FC440CC65D01868828F7C6E57127F533027B903D59519B67"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AssyChargeSEHC;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using ZedGraph;


namespace AssyChargeSEHC {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 93 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuAdd;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuEdit;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuSetCurrent;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuLogs;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuChangePass;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuLogin;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuSignOut;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbbModelList;
        
        #line default
        #line hidden
        
        
        #line 152 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbModelInfo;
        
        #line default
        #line hidden
        
        
        #line 155 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabControlMain;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbStVolMin;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbStVolMax;
        
        #line default
        #line hidden
        
        
        #line 202 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbChVolMin;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbChVolMax;
        
        #line default
        #line hidden
        
        
        #line 206 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbChCurMin;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbChCurMax;
        
        #line default
        #line hidden
        
        
        #line 210 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelVoltageStandby;
        
        #line default
        #line hidden
        
        
        #line 212 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelIRLeft;
        
        #line default
        #line hidden
        
        
        #line 214 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelIRCenter;
        
        #line default
        #line hidden
        
        
        #line 216 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelIRRight;
        
        #line default
        #line hidden
        
        
        #line 218 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelVoltage;
        
        #line default
        #line hidden
        
        
        #line 220 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelCurrent;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeVoltageStandby;
        
        #line default
        #line hidden
        
        
        #line 224 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeIRLeft;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeIRCenter;
        
        #line default
        #line hidden
        
        
        #line 226 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeIRRight;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeVoltage;
        
        #line default
        #line hidden
        
        
        #line 228 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeCurrent;
        
        #line default
        #line hidden
        
        
        #line 275 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgResultList;
        
        #line default
        #line hidden
        
        
        #line 325 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZedGraph.ZedGraphControl graphIRLeft;
        
        #line default
        #line hidden
        
        
        #line 330 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZedGraph.ZedGraphControl graphIRCenter;
        
        #line default
        #line hidden
        
        
        #line 335 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZedGraph.ZedGraphControl graphIRRight;
        
        #line default
        #line hidden
        
        
        #line 352 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelFinalJudgement;
        
        #line default
        #line hidden
        
        
        #line 361 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMessage;
        
        #line default
        #line hidden
        
        
        #line 377 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgQRCode;
        
        #line default
        #line hidden
        
        
        #line 403 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonReset;
        
        #line default
        #line hidden
        
        
        #line 404 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelOK;
        
        #line default
        #line hidden
        
        
        #line 406 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelNG;
        
        #line default
        #line hidden
        
        
        #line 408 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTotal;
        
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
            System.Uri resourceLocater = new System.Uri("/AssyChargeSEHC;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
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
            
            #line 10 "..\..\..\MainWindow.xaml"
            ((AssyChargeSEHC.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\MainWindow.xaml"
            ((AssyChargeSEHC.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 57 "..\..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CommandBinding_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 58 "..\..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CommandBinding_Executed_1);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 59 "..\..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF2);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 60 "..\..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF3);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 61 "..\..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF4);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 62 "..\..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF5);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 63 "..\..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF6);
            
            #line default
            #line hidden
            return;
            case 9:
            this.mnuAdd = ((System.Windows.Controls.MenuItem)(target));
            
            #line 93 "..\..\..\MainWindow.xaml"
            this.mnuAdd.Click += new System.Windows.RoutedEventHandler(this.mnuAddEdit_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.mnuEdit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 95 "..\..\..\MainWindow.xaml"
            this.mnuEdit.Click += new System.Windows.RoutedEventHandler(this.mnuAddEdit_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.mnuSetCurrent = ((System.Windows.Controls.MenuItem)(target));
            
            #line 105 "..\..\..\MainWindow.xaml"
            this.mnuSetCurrent.Click += new System.Windows.RoutedEventHandler(this.mnuSetCurrent_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.mnuLogs = ((System.Windows.Controls.MenuItem)(target));
            
            #line 108 "..\..\..\MainWindow.xaml"
            this.mnuLogs.Click += new System.Windows.RoutedEventHandler(this.mnuLogs_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.mnuChangePass = ((System.Windows.Controls.MenuItem)(target));
            
            #line 111 "..\..\..\MainWindow.xaml"
            this.mnuChangePass.Click += new System.Windows.RoutedEventHandler(this.mnuChangePass_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.mnuLogin = ((System.Windows.Controls.MenuItem)(target));
            
            #line 114 "..\..\..\MainWindow.xaml"
            this.mnuLogin.Click += new System.Windows.RoutedEventHandler(this.mnuLogin_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.mnuSignOut = ((System.Windows.Controls.MenuItem)(target));
            
            #line 117 "..\..\..\MainWindow.xaml"
            this.mnuSignOut.Click += new System.Windows.RoutedEventHandler(this.mnuSignOut_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.cbbModelList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 143 "..\..\..\MainWindow.xaml"
            this.cbbModelList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbbModelList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 17:
            this.lbModelInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 18:
            this.tabControlMain = ((System.Windows.Controls.TabControl)(target));
            return;
            case 19:
            this.lbStVolMin = ((System.Windows.Controls.Label)(target));
            return;
            case 20:
            this.lbStVolMax = ((System.Windows.Controls.Label)(target));
            return;
            case 21:
            this.lbChVolMin = ((System.Windows.Controls.Label)(target));
            return;
            case 22:
            this.lbChVolMax = ((System.Windows.Controls.Label)(target));
            return;
            case 23:
            this.lbChCurMin = ((System.Windows.Controls.Label)(target));
            return;
            case 24:
            this.lbChCurMax = ((System.Windows.Controls.Label)(target));
            return;
            case 25:
            this.labelVoltageStandby = ((System.Windows.Controls.Label)(target));
            return;
            case 26:
            this.labelIRLeft = ((System.Windows.Controls.Label)(target));
            return;
            case 27:
            this.labelIRCenter = ((System.Windows.Controls.Label)(target));
            return;
            case 28:
            this.labelIRRight = ((System.Windows.Controls.Label)(target));
            return;
            case 29:
            this.labelVoltage = ((System.Windows.Controls.Label)(target));
            return;
            case 30:
            this.labelCurrent = ((System.Windows.Controls.Label)(target));
            return;
            case 31:
            this.labelJudgeVoltageStandby = ((System.Windows.Controls.Label)(target));
            return;
            case 32:
            this.labelJudgeIRLeft = ((System.Windows.Controls.Label)(target));
            return;
            case 33:
            this.labelJudgeIRCenter = ((System.Windows.Controls.Label)(target));
            return;
            case 34:
            this.labelJudgeIRRight = ((System.Windows.Controls.Label)(target));
            return;
            case 35:
            this.labelJudgeVoltage = ((System.Windows.Controls.Label)(target));
            return;
            case 36:
            this.labelJudgeCurrent = ((System.Windows.Controls.Label)(target));
            return;
            case 37:
            this.dgResultList = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 38:
            this.graphIRLeft = ((ZedGraph.ZedGraphControl)(target));
            return;
            case 39:
            this.graphIRCenter = ((ZedGraph.ZedGraphControl)(target));
            return;
            case 40:
            this.graphIRRight = ((ZedGraph.ZedGraphControl)(target));
            return;
            case 41:
            this.labelFinalJudgement = ((System.Windows.Controls.Label)(target));
            return;
            case 42:
            this.txtMessage = ((System.Windows.Controls.TextBox)(target));
            return;
            case 43:
            this.imgQRCode = ((System.Windows.Controls.Image)(target));
            return;
            case 44:
            this.buttonReset = ((System.Windows.Controls.Button)(target));
            
            #line 403 "..\..\..\MainWindow.xaml"
            this.buttonReset.Click += new System.Windows.RoutedEventHandler(this.buttonReset_Click);
            
            #line default
            #line hidden
            return;
            case 45:
            this.labelOK = ((System.Windows.Controls.Label)(target));
            return;
            case 46:
            this.labelNG = ((System.Windows.Controls.Label)(target));
            return;
            case 47:
            this.labelTotal = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

