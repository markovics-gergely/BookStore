#pragma checksum "C:\work\BookStore\BookStore.CLIENT\Views\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A8081F909C9A4AAEBE3E6ECC00E03BF3D1D7BF96DACF546959356FE8ECCE73BA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore.CLIENT.Views
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\MainPage.xaml line 13
                {
                    this.ViewModel = (global::BookStore.CLIENT.ViewModels.MainPageViewModel)(target);
                }
                break;
            case 3: // Views\MainPage.xaml line 25
                {
                    this.Navigation = (global::Windows.UI.Xaml.Controls.NavigationView)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationView)this.Navigation).ItemInvoked += this.Navigation_Invoked;
                }
                break;
            case 4: // Views\MainPage.xaml line 110
                {
                    this.InnerFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                    ((global::Windows.UI.Xaml.Controls.Frame)this.InnerFrame).NavigationFailed += this.InnerFrame_NavigationFailed;
                }
                break;
            case 5: // Views\MainPage.xaml line 102
                {
                    global::Windows.UI.Xaml.Controls.NavigationViewItem element5 = (global::Windows.UI.Xaml.Controls.NavigationViewItem)(target);
                    ((global::Windows.UI.Xaml.Controls.NavigationViewItem)element5).Tapped += this.NavigationViewItem_Tapped;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

