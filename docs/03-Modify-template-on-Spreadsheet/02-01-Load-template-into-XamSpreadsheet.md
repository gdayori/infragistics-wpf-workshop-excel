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
 - "Save template" button linking to SaveWorkbook command defined in the ViewModel

## Load the template Excel file.



## Next
[02-02 Layout screen with XamTileManager](02-02-Layout-screen-with-XamTileManager.md)
