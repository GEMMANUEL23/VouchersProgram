using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Popups;
using VouchersProgram.Data.Models;
using Syncfusion.Blazor.Navigations;
using Microsoft.AspNetCore.Components;
using VouchersProgram.Data.Interfaces;
using VouchersProgram.Shared;

namespace VouchersProgram.Pages
{
    public partial class InvoicePage
    {
        

        IEnumerable<Invoice> invoices;
        IEnumerable<Supplier> suppliers;
        IEnumerable<Expense_Account> expense_accounts;
        IEnumerable<Currency> currency;

        private List<ItemModel> ToolbarItems = new List<ItemModel>();

        public int SelectedInvoiceId { get; set; } = 0;


        Invoice addeditInvoice = new Invoice();

        SfDialog DialogAddEditInvoice;
        string HeaderText = "";

        SfDialog DeleteInvoice;


        protected override async Task OnInitializedAsync()
        {
            invoices = await InvoiceService.InvoiceList();
            suppliers = await SupplierService.SupplierList();
            expense_accounts = await Expense_AccountService.Expense_Acc_List();
            currency = await CurrencyService.CurrencyList();

           


            ToolbarItems.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new Invoice", PrefixIcon = "e-add" });
            ToolbarItems.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit selected Invoice", PrefixIcon = "e-edit" });
            ToolbarItems.Add(new ItemModel() { Text = "Annul", TooltipText = "Annul selected Invoice", PrefixIcon = "e-delete" });
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Add")
            {
                HeaderText = "Add Invoice";

                //Code for adding goes here
                addeditInvoice = new Invoice();             // Ensures a blank form when adding
                addeditInvoice.InvoicePQty = 1;
                addeditInvoice.InvoiceRecordNumber = 1;
                addeditInvoice.InvoiceEmisionDate = DateTime.Today;

                await this.DialogAddEditInvoice.ShowAsync();
            }
            if (args.Item.Text == "Edit")
            {

            }
            if (args.Item.Text == "Annul")
            {
                await this.DeleteInvoice.ShowAsync();
            }

        }

        public void RowSelectHandler(RowSelectEventArgs<Invoice> args)
        {
            SelectedInvoiceId = args.Data.InvoiceID;
        }

        private void OnChangeSupplier(Syncfusion.Blazor.DropDowns.SelectEventArgs<Supplier> args)
        {
            this.addeditInvoice.InvoiceSupplierID = args.ItemData.SupplierID;
            this.addeditInvoice.SupplierAccCode = args.ItemData.SupplierAccCode;
            this.addeditInvoice.InvoiceSupplierCurrencyID = args.ItemData.SupplierCurrencyID;
            

        }


        private void OnChangeCurrency(Syncfusion.Blazor.DropDowns.SelectEventArgs<Currency> args)
        { 
            this.addeditInvoice.CurrencySymbol = args.ItemData.CurrencySymbol;  

        }

        private void OnChangeExpenseAcc(Syncfusion.Blazor.DropDowns.SelectEventArgs<Expense_Account> args)
        {
            this.addeditInvoice.InvoiceExpenseAccountID = args.ItemData.ExpenseAccountID;
            this.addeditInvoice.ExpenseAccountDescription = args.ItemData.ExpenseAccountDescription;

        }

        protected async Task InvoiceSave()
        {
            if (addeditInvoice.InvoiceID == 0)
            {
                int Success = await InvoiceService.Invoice_Insert(addeditInvoice.InvoiceRecordNumber, addeditInvoice.InvoiceSupplierID,
          addeditInvoice.InvoiceSupplierAccCode, addeditInvoice.InvoiceEmisionDate, addeditInvoice.InvoiceNumber, addeditInvoice.IvoiceFile, 
          addeditInvoice.InvoicePQty, addeditInvoice.InvoicePUnitPrice, addeditInvoice.InvoiceTotal, addeditInvoice.InvoiceSupplierCurrencyID, addeditInvoice.InvoiceBuyer,
          addeditInvoice.InvoiceExpenseAccountID);
            }

            invoices = await InvoiceService.InvoiceList();
            StateHasChanged();

            addeditInvoice.InvoiceRecordNumber = 1;
            addeditInvoice.InvoiceNumber = string.Empty;
            addeditInvoice.InvoiceBuyer = string.Empty;
            addeditInvoice.IvoiceFile = string.Empty;
            addeditInvoice.InvoicePQty = 0;
            addeditInvoice.InvoicePUnitPrice = 0;
            addeditInvoice.InvoiceTotal = 0;
            
            

        }

        private async Task CloseDialog()
        {
            await this.DialogAddEditInvoice.HideAsync();

        }

        public async void ConfirmDeleteNo()
        {
            await DeleteInvoice.HideAsync();
            SelectedInvoiceId = 0;
        }

        public async void ConfirmDeleteYes()
        {
            await DeleteInvoice.HideAsync();
            SelectedInvoiceId = 0;
        }
    }
}
