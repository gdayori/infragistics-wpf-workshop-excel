using System;
using System.Collections.Generic;
using System.ComponentModel;
using Infragistics.Samples.Data.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infragistics.Documents.Excel;
using System.IO;
using System.Windows.Input;
using System.Windows;

namespace IgWpfWorkshop.ViewModel
{
    public class SpreadsheetViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveWorkbook { get; set; }

        public SpreadsheetViewModel()
        {
            SaveWorkbook = new SaveWorkbookCommand(this);
        }

    }


    public class SaveWorkbookCommand : ICommand
    {
        SpreadsheetViewModel _vm;
        public SaveWorkbookCommand()
        {
        }

        public SaveWorkbookCommand(SpreadsheetViewModel vm)
        {
            _vm = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
        }
    }
}
