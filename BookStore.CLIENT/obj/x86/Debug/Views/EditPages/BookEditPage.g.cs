#pragma checksum "C:\work\BookStore\BookStore.CLIENT\Views\EditPages\BookEditPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "DE80CBFE522986195EF6AEAC3C7FF45A1668D56BB3156267C7ACC4926F083303"
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
    partial class BookEditPage : 
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
            case 2: // Views\EditPages\BookEditPage.xaml line 13
                {
                    this.ViewModel = (global::BookStore.CLIENT.ViewModels.EditViewModels.BookEditViewModel)(target);
                }
                break;
            case 3: // Views\EditPages\BookEditPage.xaml line 234
                {
                    this.Notification = (global::Microsoft.Toolkit.Uwp.UI.Controls.InAppNotification)(target);
                }
                break;
            case 4: // Views\EditPages\BookEditPage.xaml line 212
                {
                    global::Windows.UI.Xaml.Controls.ListView element4 = (global::Windows.UI.Xaml.Controls.ListView)(target);
                    ((global::Windows.UI.Xaml.Controls.ListView)element4).SelectionChanged += this.ListView_SelectionChanged;
                }
                break;
            case 5: // Views\EditPages\BookEditPage.xaml line 176
                {
                    global::Windows.UI.Xaml.Controls.TextBox element5 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)element5).TextChanged += this.TextBox_TextChanged;
                }
                break;
            case 6: // Views\EditPages\BookEditPage.xaml line 148
                {
                    global::Windows.UI.Xaml.Controls.TextBox element6 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)element6).TextChanged += this.TextBox_TextChanged;
                }
                break;
            case 7: // Views\EditPages\BookEditPage.xaml line 88
                {
                    global::Windows.UI.Xaml.Controls.TextBox element7 = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                    ((global::Windows.UI.Xaml.Controls.TextBox)element7).TextChanged += this.TextBox_TextChanged;
                }
                break;
            case 8: // Views\EditPages\BookEditPage.xaml line 43
                {
                    global::Windows.UI.Xaml.Controls.ComboBox element8 = (global::Windows.UI.Xaml.Controls.ComboBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ComboBox)element8).SelectionChanged += this.ComboBox_SelectionChanged;
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

