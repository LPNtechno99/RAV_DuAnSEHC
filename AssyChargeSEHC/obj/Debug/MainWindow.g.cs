﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FA30BDEDC2619A9A7F1E3169A9D7A859C7002DA1E35114119ECC4A1F2C2AB14E"
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
        
        
        #line 93 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuAdd;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuEdit;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuSetCurrent;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuLogs;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuChangePass;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuLogin;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuSignOut;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuRegister;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem mnuAbouts;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbbModelList;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbModelInfo;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabControlMain;
        
        #line default
        #line hidden
        
        
        #line 206 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbStVolMin;
        
        #line default
        #line hidden
        
        
        #line 208 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbStVolMax;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbChVolMin;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbChVolMax;
        
        #line default
        #line hidden
        
        
        #line 217 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbChCurMin;
        
        #line default
        #line hidden
        
        
        #line 219 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbChCurMax;
        
        #line default
        #line hidden
        
        
        #line 221 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelVoltageStandby;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelIRLeft;
        
        #line default
        #line hidden
        
        
        #line 225 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelIRCenter;
        
        #line default
        #line hidden
        
        
        #line 227 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelIRRight;
        
        #line default
        #line hidden
        
        
        #line 229 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelVoltage;
        
        #line default
        #line hidden
        
        
        #line 231 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelCurrent;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeVoltageStandby;
        
        #line default
        #line hidden
        
        
        #line 235 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeIRLeft;
        
        #line default
        #line hidden
        
        
        #line 236 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeIRCenter;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeIRRight;
        
        #line default
        #line hidden
        
        
        #line 238 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeVoltage;
        
        #line default
        #line hidden
        
        
        #line 239 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelJudgeCurrent;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chbStandbyVol;
        
        #line default
        #line hidden
        
        
        #line 249 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chbIRLeft;
        
        #line default
        #line hidden
        
        
        #line 257 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chbIRCenter;
        
        #line default
        #line hidden
        
        
        #line 265 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chbIRRight;
        
        #line default
        #line hidden
        
        
        #line 273 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chbCharVol;
        
        #line default
        #line hidden
        
        
        #line 280 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox chbCharCur;
        
        #line default
        #line hidden
        
        
        #line 289 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgResultList;
        
        #line default
        #line hidden
        
        
        #line 339 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZedGraph.ZedGraphControl graphIRLeft;
        
        #line default
        #line hidden
        
        
        #line 344 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZedGraph.ZedGraphControl graphIRCenter;
        
        #line default
        #line hidden
        
        
        #line 349 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ZedGraph.ZedGraphControl graphIRRight;
        
        #line default
        #line hidden
        
        
        #line 366 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelFinalJudgement;
        
        #line default
        #line hidden
        
        
        #line 375 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtMessage;
        
        #line default
        #line hidden
        
        
        #line 402 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblStartTime;
        
        #line default
        #line hidden
        
        
        #line 407 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblEndTime;
        
        #line default
        #line hidden
        
        
        #line 436 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelOK;
        
        #line default
        #line hidden
        
        
        #line 438 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelNG;
        
        #line default
        #line hidden
        
        
        #line 440 "..\..\MainWindow.xaml"
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
            
            #line 1 "..\..\MainWindow.xaml"
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
            
            #line 10 "..\..\MainWindow.xaml"
            ((AssyChargeSEHC.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 10 "..\..\MainWindow.xaml"
            ((AssyChargeSEHC.MainWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 57 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CommandBinding_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 58 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.CommandBinding_Executed_1);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 59 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF2);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 60 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF3);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 61 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF4);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 62 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF5);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 63 "..\..\MainWindow.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Event_PushF6);
            
            #line default
            #line hidden
            return;
            case 9:
            this.mnuAdd = ((System.Windows.Controls.MenuItem)(target));
            
            #line 93 "..\..\MainWindow.xaml"
            this.mnuAdd.Click += new System.Windows.RoutedEventHandler(this.mnuAddEdit_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.mnuEdit = ((System.Windows.Controls.MenuItem)(target));
            
            #line 95 "..\..\MainWindow.xaml"
            this.mnuEdit.Click += new System.Windows.RoutedEventHandler(this.mnuAddEdit_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.mnuSetCurrent = ((System.Windows.Controls.MenuItem)(target));
            
            #line 105 "..\..\MainWindow.xaml"
            this.mnuSetCurrent.Click += new System.Windows.RoutedEventHandler(this.mnuSetCurrent_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.mnuLogs = ((System.Windows.Controls.MenuItem)(target));
            
            #line 108 "..\..\MainWindow.xaml"
            this.mnuLogs.Click += new System.Windows.RoutedEventHandler(this.mnuLogs_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.mnuChangePass = ((System.Windows.Controls.MenuItem)(target));
            
            #line 111 "..\..\MainWindow.xaml"
            this.mnuChangePass.Click += new System.Windows.RoutedEventHandler(this.mnuChangePass_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            this.mnuLogin = ((System.Windows.Controls.MenuItem)(target));
            
            #line 114 "..\..\MainWindow.xaml"
            this.mnuLogin.Click += new System.Windows.RoutedEventHandler(this.mnuLogin_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.mnuSignOut = ((System.Windows.Controls.MenuItem)(target));
            
            #line 117 "..\..\MainWindow.xaml"
            this.mnuSignOut.Click += new System.Windows.RoutedEventHandler(this.mnuSignOut_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.mnuRegister = ((System.Windows.Controls.MenuItem)(target));
            
            #line 120 "..\..\MainWindow.xaml"
            this.mnuRegister.Click += new System.Windows.RoutedEventHandler(this.mnuRegister_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.mnuAbouts = ((System.Windows.Controls.MenuItem)(target));
            
            #line 122 "..\..\MainWindow.xaml"
            this.mnuAbouts.Click += new System.Windows.RoutedEventHandler(this.mnuAbouts_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            this.cbbModelList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 154 "..\..\MainWindow.xaml"
            this.cbbModelList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbbModelList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 19:
            this.lbModelInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 20:
            this.tabControlMain = ((System.Windows.Controls.TabControl)(target));
            return;
            case 21:
            this.lbStVolMin = ((System.Windows.Controls.Label)(target));
            return;
            case 22:
            this.lbStVolMax = ((System.Windows.Controls.Label)(target));
            return;
            case 23:
            this.lbChVolMin = ((System.Windows.Controls.Label)(target));
            return;
            case 24:
            this.lbChVolMax = ((System.Windows.Controls.Label)(target));
            return;
            case 25:
            this.lbChCurMin = ((System.Windows.Controls.Label)(target));
            return;
            case 26:
            this.lbChCurMax = ((System.Windows.Controls.Label)(target));
            return;
            case 27:
            this.labelVoltageStandby = ((System.Windows.Controls.Label)(target));
            return;
            case 28:
            this.labelIRLeft = ((System.Windows.Controls.Label)(target));
            return;
            case 29:
            this.labelIRCenter = ((System.Windows.Controls.Label)(target));
            return;
            case 30:
            this.labelIRRight = ((System.Windows.Controls.Label)(target));
            return;
            case 31:
            this.labelVoltage = ((System.Windows.Controls.Label)(target));
            return;
            case 32:
            this.labelCurrent = ((System.Windows.Controls.Label)(target));
            return;
            case 33:
            this.labelJudgeVoltageStandby = ((System.Windows.Controls.Label)(target));
            return;
            case 34:
            this.labelJudgeIRLeft = ((System.Windows.Controls.Label)(target));
            return;
            case 35:
            this.labelJudgeIRCenter = ((System.Windows.Controls.Label)(target));
            return;
            case 36:
            this.labelJudgeIRRight = ((System.Windows.Controls.Label)(target));
            return;
            case 37:
            this.labelJudgeVoltage = ((System.Windows.Controls.Label)(target));
            return;
            case 38:
            this.labelJudgeCurrent = ((System.Windows.Controls.Label)(target));
            return;
            case 39:
            this.chbStandbyVol = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 40:
            this.chbIRLeft = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 41:
            this.chbIRCenter = ((System.Windows.Controls.CheckBox)(target));
            
            #line 258 "..\..\MainWindow.xaml"
            this.chbIRCenter.Unchecked += new System.Windows.RoutedEventHandler(this.chbIRCenter_Unchecked);
            
            #line default
            #line hidden
            return;
            case 42:
            this.chbIRRight = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 43:
            this.chbCharVol = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 44:
            this.chbCharCur = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 45:
            this.dgResultList = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 46:
            this.graphIRLeft = ((ZedGraph.ZedGraphControl)(target));
            return;
            case 47:
            this.graphIRCenter = ((ZedGraph.ZedGraphControl)(target));
            return;
            case 48:
            this.graphIRRight = ((ZedGraph.ZedGraphControl)(target));
            return;
            case 49:
            this.labelFinalJudgement = ((System.Windows.Controls.Label)(target));
            return;
            case 50:
            this.txtMessage = ((System.Windows.Controls.TextBox)(target));
            return;
            case 51:
            this.lblStartTime = ((System.Windows.Controls.Label)(target));
            return;
            case 52:
            this.lblEndTime = ((System.Windows.Controls.Label)(target));
            return;
            case 53:
            this.labelOK = ((System.Windows.Controls.Label)(target));
            return;
            case 54:
            this.labelNG = ((System.Windows.Controls.Label)(target));
            return;
            case 55:
            this.labelTotal = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

