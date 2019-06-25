using System;
using System.Collections.Generic;
using System.ComponentModel;
using Infragistics.Samples.Data.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Input;
using System.Windows;
using Infragistics.Documents.Excel;

namespace IgWpfWorkshop.ViewModel
{
    public class SpreadsheetViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveWorkbook { get; set; }

        public SpreadsheetViewModel()
        {
            SaveWorkbook = new SaveWorkbookCommand(this);
            workbook = Workbook.Load("../../Files/TemplateExcel.xlsx");
        }
        
        // Workbook object to be bound to Spreadsheet control
        private Workbook workbook;
        public Workbook Workbook
        {
            get { return workbook; }
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
            _vm.Workbook.Save("../../Files/TemplateExcel.xlsx");
        }
    }
}
