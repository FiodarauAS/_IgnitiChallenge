using IgnitiChallenge.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.WPF.State
{
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
    }
}
