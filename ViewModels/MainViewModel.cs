using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using TEAM2067_SourceAmerica_Project.Commands;

namespace TEAM2067_SourceAmerica_Project.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class MainViewModel : BaseViewModel
    {
        private BaseViewModel _currentView;

        public BaseViewModel CurrentView { get; set; }

        public ICommand ChangeViewCommand { get; set; }

        public MainViewModel()
        {
            this.ChangeViewCommand = new ChangeViewCommand(this);
        }
    }
}
