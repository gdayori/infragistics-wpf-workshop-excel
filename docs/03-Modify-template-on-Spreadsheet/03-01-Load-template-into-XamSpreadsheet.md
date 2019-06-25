## Load the template Excel into XamSpreadsheet

## Check SpreadsheetViewModel.cs

Open SpreadsheetViewModel.cs and check what's in the class.

```cs
...
public ICommand SaveWorkbook { get; set; }

public SpreadsheetViewModel()
{
    SaveWorkbook = new SaveWorkbookCommand(this);
}
...
    
```

This class has
 - SaveWorkbookCommand to save a workbook object back to the template Excel file.

## Check Spreadsheet.xaml

Open Spreadsheet.xaml and check what's in the xaml.
```xml
<Window
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:local="clr-namespace:IgWpfWorkshop"
        xmlns:vm="clr-namespace:IgWpfWorkshop.ViewModel" xmlns:ig="http://schemas.infragistics.com/xaml" xmlns:Custom="http://infragistics.com/DataPresenter" x:Class="IgWpfWorkshop.Spreadsheet"
        mc:Ignorable="d"
        Title="Change the template of Shipping List" Height="600" Width="1000"
        >
    <Window.DataContext>
        <vm:SpreadsheetViewModel/>
    </Window.DataContext>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="25px"/>
            <RowDefinition Height="*"/>
        </Grid.RowDefinitions>

        <Button Content="Save template" Command="{Binding SaveWorkbook}" Grid.Column="0"/>
    </Grid>
</Window>
```

This xaml has
 - SpreadsheetViewModel bound to this view
 - Grid Row definitions to split and layout the screen
 - "Save template" button bound to SaveWorkbook command defined in the ViewModel

## Load the template Excel file into Workbook object

Open SpreadsheetViewModel.cs. Define Workbook property to be bound to the View, load the template Excel file like you did in the section 2 and set it to the Workbook property as below.

SpreadsheetViewModel.cs

```cs
...
    public class SpreadsheetViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand SaveWorkbook { get; set; }

        public SpreadsheetViewModel()
        {
            SaveWorkbook = new SaveWorkbookCommand(this);
            // ↓↓↓ Added ↓↓↓
            var filePath = "../../Files/TemplateExcel.xlsx";
            workbook = Workbook.Load(filePath);
            // ↑↑↑ Added ↑↑↑
        }

        // ↓↓↓ Added ↓↓↓
        // Workbook object to be bound to Spreadsheet control
        private Workbook workbook;
        public Workbook Workbook
        {
            get { return workbook; }
        }
        // ↑↑↑ Added ↑↑↑
    }
...
```

## Add XamSpreadsheet in Spreadsheet.xaml

Open Spreadsheet.xaml and drag XamSpreadsheet from your Toolbox and drop into the designer. Reset the layout of XamSpreadsheet and bind Workbook to the Spreadsheet.

Spreadsheet.xaml

```xml
・・・
        <Button Content="Save template" Command="{Binding SaveWorkbook}" Grid.Column="0"/>
        <!-- ↓↓↓ Added ↓↓↓ -->
        <ig:XamSpreadsheet Grid.Row="1" Workbook="{Binding Path=Workbook}"/>
        <!-- ↑↑↑ Added ↑↑↑ -->
    </Grid>
</Window>
```

## Check the result

Run the app and check the result.

![](../assets/03-01-01.png)



## Next
[03 02 Modify-template](03-02-Modify-template.md)
