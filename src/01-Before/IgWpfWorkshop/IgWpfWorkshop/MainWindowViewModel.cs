using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Infragistics.Samples.Data.Models;
using System.Diagnostics;

namespace IgWpfWorkshop.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ExportShippingList { get; set; }
        public ICommand OpenSpreadsheet { get; set; }

        public MainWindowViewModel()
        {
            //Get sales data to be bound to grid
            SalesDataSample salesDataSample = new SalesDataSample();
            salesRecords = salesDataSample.SalesData;

            // Commands
            ExportShippingList = new ExportShippingListCommand(this);
            OpenSpreadsheet = new OpenSpreadsheetCommand();
        }

        private ObservableCollection<Sale> salesRecords;
        public ObservableCollection<Sale> SalesRecords
        {
            get { return salesRecords; }
        }
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
    
    public class ExportShippingListCommand : ICommand
    {
        MainWindowViewModel _vm;
        public ExportShippingListCommand()
        {
        }
        public ExportShippingListCommand(MainWindowViewModel vm)
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

    public class OpenSpreadsheetCommand : ICommand
    {
        public OpenSpreadsheetCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            var newWindow = new Spreadsheet();
            newWindow.Show();
        }
    }
}
