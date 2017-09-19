using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Dta.Spy.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        public RelayCommand FileOpenCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "UI Depot File|*.xml";
                    bool? result = ofd.ShowDialog();
                    if (result != true)
                    {
                        return;
                    }
                    ViewModelLocator.Instance.DepotTree.Load(ofd.FileName);
                });
            }
        }
    }
}