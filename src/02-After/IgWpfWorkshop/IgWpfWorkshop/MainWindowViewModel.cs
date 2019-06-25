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
using Infragistics.Documents.Excel;

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

            var filePath = "../../Files/TemplateExcel.xlsx";
            Workbook wb = Workbook.Load(filePath);
            Worksheet ws = wb.Worksheets["Sheet1"];

            // ********************
            // Get data from vm Sales Records
            // ********************
            List<Sale> salesRecordsOrdered = _vm.SalesRecords
                                                    .OrderBy(rec => rec.City)
                                                    .ThenBy(rec => rec.ProductName)
                                                    .ThenBy(rec => rec.Date)
                                                    .ToList<Sale>();

            Dictionary<string, int> qtyByProduct = (from x in _vm.SalesRecords
                                                    group x by x.ProductName into A
                                                    select new { product = A.Key, NumberOfUnits = A.Sum(a => (int)a.NumberOfUnits) })
                                                  .ToDictionary(a => a.product, a => a.NumberOfUnits);

            // ********************
            // Marge data into Excel
            // ********************
            // Marge data for header part
            ws.GetCell("IssuedDate").Value = DateTime.Now;
            ws.GetCell("IssuedBy").Value = "Satoru Yamaguchi";
            ws.GetCell("Dept").Value = "Sales Group";
            // Marge data for quantity list by product in header part
            for (var i = 1; i <= 4; i++)
            {
                var productName = ws.GetCell("ProductName" + i.ToString()).Value.ToString();
                if (qtyByProduct.ContainsKey(productName))
                    ws.GetCell("ProductQty" + i.ToString()).Value = qtyByProduct[productName];
            }
            // Marge data for record part
            for (var rowIndex = 0; rowIndex < salesRecordsOrdered.Count(); rowIndex++)
            {
                ws.Rows[11 + rowIndex].Cells[1].Value = salesRecordsOrdered[rowIndex].City;
                ws.Rows[11 + rowIndex].Cells[2].Value = salesRecordsOrdered[rowIndex].ProductName;
                ws.Rows[11 + rowIndex].Cells[3].Value = salesRecordsOrdered[rowIndex].Date;
                ws.Rows[11 + rowIndex].Cells[4].Value = salesRecordsOrdered[rowIndex].SalesPerson;
                ws.Rows[11 + rowIndex].Cells[5].Value = salesRecordsOrdered[rowIndex].NumberOfUnits;
            }

            // ↓↓↓ Added ↓↓↓
            try
            {
                // Export the Excel object
                var exportedFilePath = "../../Files/ExportedExcel.xlsx";
                wb.Save(exportedFilePath);

                // Open the exported Excel file
                var stFilePath = System.IO.Path.GetFullPath(exportedFilePath);
                System.Diagnostics.Process.Start(stFilePath);
            }
            catch (Exception) { Console.WriteLine("Exception occurred"); }
            // ↑↑↑ Added ↑↑↑
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
