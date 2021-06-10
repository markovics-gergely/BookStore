using BookStore.CLIENT.API;
using BookStore.CLIENT.Services;
using BookStore.CLIENT.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace BookStore.CLIENT.ViewModels
{
    public class MemberPageViewModel : ViewModelBase
    {
        public ObservableCollection<Member> Members { get; set; } =
            new ObservableCollection<Member>();

        public MemberPageViewModel()
        {
        }

        public async Task loadListAsync()
        {
            var memberService = new MemberService();
            var members = await memberService.GetAllMemberAsync();
            foreach (var m in members)
            {
                Members.Add(m);
            }
        }
    }
}

