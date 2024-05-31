using VouchersProgram.Data.Models;
using VouchersProgram.Data.Services;
using VouchersProgram.Data.Interfaces;
using Syncfusion.Blazor.Navigations;
using Syncfusion.Blazor.Popups;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Grids;
using Humanizer;
using VouchersProgram.Shared;
using Microsoft.AspNetCore.Routing.Matching;

namespace VouchersProgram.Pages
{
    public partial class VouchersPage
    {

        VWS_Header voucheraddedit = new VWS_Header();

        IEnumerable<Supplier> suppliers;
        IEnumerable<Deparment> deparments;
        IEnumerable<Teams> teams;
        IEnumerable<PayMethod> paymeths;
        IEnumerable<Invoice> invoices;
        IEnumerable<Banks> banks;
        IEnumerable<Currency> currency;
        IEnumerable<VWS_Line> vlinesbyVSHeader;

        private string UserName;


        SfGrid<VWS_Line> VoucherLinesGrid;
        public List<VWS_Line> voucherLines = new List<VWS_Line>();
        private List<ItemModel> ToolbarItems = new List<ItemModel>();

        SfDialog DialogAddeditVoucherLine;
        public VWS_Line addeditVoucherLine = new VWS_Line();

        private List<int> VoucherLinesToBeDeleted = new List<int>();

        WarningPage Warning;
        string WarningHeaderMessage = "";
        string WarningContentMessage = "";

        private int tmpVSLineID { get; set; } = 0;

        private int selectedVSLineID { get; set; } = 0;


        string pagetitle = "Create a voucher";

        string dialogHeaderText = "";

        [Parameter]
        public int VSHeaderID { get; set; }

        public bool IsEnabled { get; set; } = true;

        public bool supplierEnabled { get; set; } = true;

        public async Task OnChangeDeparment(Syncfusion.Blazor.DropDowns.SelectEventArgs<Deparment> args)
        { 
            this.voucheraddedit.VSHeaderDeparmentID = args.ItemData.DeparmentID;
            this.voucheraddedit.DeparmentName = args.ItemData.DeparmentName;
        }

        public async Task OnChangeTeam(Syncfusion.Blazor.DropDowns.SelectEventArgs<Teams> args)
        {
            this.voucheraddedit.VSHeaderTeamID = args.ItemData.DepTeamID;
        }

        public async Task OnChangeSupplier2(Syncfusion.Blazor.DropDowns.SelectEventArgs<Supplier> args)
        {
            
            this.voucheraddedit.VSHeaderSupplierID = args.ItemData.SupplierID;
            this.voucheraddedit.SupplierPayToName = args.ItemData.SupplierPayToName;
            this.voucheraddedit.VSHeaderSupplierContact = args.ItemData.SupplierContact;
            this.voucheraddedit.VSHeaderSuppPayMethodID = args.ItemData.SupplierPayMethID;
            this.voucheraddedit.VSHeaderSupplierBankAcc = args.ItemData.SupplierBankAccount;
            this.voucheraddedit.VSHeaderSupplierBankID = args.ItemData.SupplierBankID;
            this.voucheraddedit.VSHeaderSupplierBankName = args.ItemData.SupplierBankName;
            this.voucheraddedit.VSHeaderSupplierCID = args.ItemData.SupplierCurrencyID;
            IsEnabled = false;

            invoices = await InvoiceService.InvoicesBySupplier(args.ItemData.SupplierID);

        }

        public async Task OnChangePayMethod(Syncfusion.Blazor.DropDowns.SelectEventArgs<PayMethod> args)
        {
            this.voucheraddedit.VSHeaderSuppPayMethodID = args.ItemData.PayMethodID;
            this.voucheraddedit.PayMethDescription = args.ItemData.PayMethDescription;
        }

        public async Task OnChangeInvoice(Syncfusion.Blazor.DropDowns.SelectEventArgs<Invoice> args)
        {

            this.addeditVoucherLine.VSLineInvoiceBuyer = args.ItemData.InvoiceBuyer;
            this.addeditVoucherLine.VSLineInvoiceFile = args.ItemData.IvoiceFile;
            this.addeditVoucherLine.VSLineInvRNumber = args.ItemData.InvoiceRecordNumber;
            this.addeditVoucherLine.VSLineInvNumber = args.ItemData.InvoiceNumber;
            this.addeditVoucherLine.VSLineInvoiceQuantity = args.ItemData.InvoicePQty;
            this.addeditVoucherLine.VSLineInvoiceUnitPrice = args.ItemData.InvoicePUnitPrice;
            this.addeditVoucherLine.VSLineInvoiceTotal = args.ItemData.InvoiceTotal;           
            VSLineCalc();

        }

        public async Task OnChangeCurrency(Syncfusion.Blazor.DropDowns.SelectEventArgs<Currency> args)
        {
            this.voucheraddedit.VSHeaderSupplierCID = args.ItemData.CurrencyID;
            this.voucheraddedit.CurrencySymbol = args.ItemData.CurrencySymbol;
        
        }

        public async Task OnChangeBank(Syncfusion.Blazor.DropDowns.SelectEventArgs<Banks> args)
        {
            this.voucheraddedit.VSHeaderSupplierBankID = args.ItemData.BankID;
            this.voucheraddedit.VSHeaderSupplierBankName = args.ItemData.BankDescription;


        }

        protected override async Task OnInitializedAsync()
        {
            suppliers = await SupplierService.SupplierList();
            deparments = await DeparmentService.DeparmentList();
            teams = await TeamsService.TeamsList();
            paymeths = await PayMethodService.PayMethList();
            banks = await BanksService.BankList();
            currency = await CurrencyService.CurrencyList();

            voucheraddedit.VSHeaderVEDate = DateTime.Today;
            voucheraddedit.PayDate = DateTime.Today;

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                UserName = user.Identity.Name;
            }
            else
            {
                UserName = "The user is NOT authenticated.";
            }

            voucheraddedit.VSHeaderRequestedBy = UserName;


            ToolbarItems.Add(new ItemModel() { Text = "Add", TooltipText = "Add a new Tax Rate", PrefixIcon = "e-add" });
            ToolbarItems.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit selected Tax Rate", PrefixIcon = "e-edit" });
            ToolbarItems.Add(new ItemModel() { Text = "Delete", TooltipText = "Delete selected Tax Rate", PrefixIcon = "e-delete" });

            if (VSHeaderID == 0)
            {
                pagetitle = "Create a voucher";
            }
            else
            {
                pagetitle = "Edit selected Voucher";
                voucheraddedit = await VWS_HeaderService.VWSHeader_GetOne(VSHeaderID);
                vlinesbyVSHeader = await VWS_LineService.VWSLine_GetbyVSHeader(VSHeaderID);
                voucherLines = vlinesbyVSHeader.ToList();
                IsEnabled = false;
                supplierEnabled = false;


            }
        }

        public async Task ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            invoices = await InvoiceService.InvoicesBySupplier(voucheraddedit.VSHeaderSupplierID);

            if (args.Item.Text == "Add")
            {
                dialogHeaderText = "Add Invoice";

                //Code for adding goes here
                addeditVoucherLine = new VWS_Line();
                await this.DialogAddeditVoucherLine.ShowAsync();
            }

            if (args.Item.Text == "Edit")
            {
                dialogHeaderText = "Edit Invoice info";
                //Code for adding goes here
                if (selectedVSLineID == 0)
                {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "Please select a Voucher Line from the grid.";
                    Warning.OpenDialog();
                }

                else
                {
                    addeditVoucherLine = voucherLines.Where(x => x.VSLineID == selectedVSLineID).FirstOrDefault();
                    StateHasChanged();
                    await this.DialogAddeditVoucherLine.ShowAsync();
                }

            }

            if (args.Item.Text == "Delete")
            {
                //Code for adding goes here
                if (selectedVSLineID == 0)
                {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "Please select a Voucher Line from the grid.";
                    Warning.OpenDialog();
                }
                else 
                {
                    VoucherLineDelete();
                }


            }
        }

        public void RowSelectHandler(RowSelectEventArgs<VWS_Line> args)
        {
            selectedVSLineID = args.Data.VSLineID;

        }

        private void VSLineCalc()
        {

            addeditVoucherLine.VSLineInvoiceTotal = addeditVoucherLine.VSLineInvoiceQuantity * addeditVoucherLine.VSLineInvoiceUnitPrice;
        
        }


        private void VoucherLineSave()
        {
            if (addeditVoucherLine.VSLineID == 0)
            {
                if (addeditVoucherLine.VSLineInvoiceID == 0)
                {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "Please Select a Invoice.";
                    Warning.OpenDialog();
                }

                else
                {
                    tmpVSLineID = tmpVSLineID - 1;

                    voucherLines.Add(new VWS_Line
                    {
                        VSLineID = tmpVSLineID,
                        VSLineHeaderID = 0,
                        VSLineInvoiceID = addeditVoucherLine.VSLineInvoiceID,
                        VSLineInvRNumber = addeditVoucherLine.VSLineInvRNumber,
                        VSLineInvNumber = addeditVoucherLine.VSLineInvNumber,
                        VSLineInvoiceBuyer = addeditVoucherLine.VSLineInvoiceBuyer,
                        VSLineInvoiceFile = addeditVoucherLine.VSLineInvoiceFile,
                        VSLineDescription = addeditVoucherLine.VSLineDescription,
                        VSLineInvoiceQuantity = addeditVoucherLine.VSLineInvoiceQuantity,
                        VSLineInvoiceUnitPrice = addeditVoucherLine.VSLineInvoiceUnitPrice,
                        VSLineInvoiceTotal = addeditVoucherLine.VSLineInvoiceTotal
                    });

                    VoucherLinesGrid.Refresh();
                    StateHasChanged();


                    addeditVoucherLine.VSLineDescription = string.Empty;
                    addeditVoucherLine.VSLineInvoiceFile = string.Empty;
                    addeditVoucherLine.VSLineInvoiceBuyer = string.Empty;
                    addeditVoucherLine.VSLineInvoiceQuantity = 0;
                    addeditVoucherLine.VSLineInvoiceUnitPrice = 0;
                    addeditVoucherLine.VSLineInvoiceTotal = 0;

                    supplierEnabled = false;
                }
             
            }
         
        }

        protected async Task VoucherSave()
        {
            if (VSHeaderID == 0)
            {
                int HeaderID = await VWS_HeaderService.VWSHeader_Insert(voucheraddedit.VSHeaderVEDate, voucheraddedit.VSHeaderDeparmentID, voucheraddedit.VSHeaderTeamID, voucheraddedit.VSHeaderSupplierID,
                 voucheraddedit.SupplierPayToName, voucheraddedit.VSHeaderSupplierAccCode, voucheraddedit.VSHeaderSuppPayMethodID, voucheraddedit.VSHeaderSupplierBankID, voucheraddedit.VSHeaderSupplierBankAcc,
                 voucheraddedit.VSHeaderSupplierCID, voucheraddedit.VSHeaderSupplierCSymbol, voucheraddedit.VSHeaderSupplierContact, voucheraddedit.VSHeaderRequestedBy);

                foreach (var onevoucherline in voucherLines)
                {
                    onevoucherline.VSLineHeaderID = HeaderID;
                    bool Success = await VWS_LineService.VWSLine_Insert(onevoucherline);

                }

                NavigationManager.NavigateTo("/");
            }
            else
            {
                bool Success = await VWS_HeaderService.VWSHeader_Update(voucheraddedit);

                foreach (var onevoucherline in voucherLines)
                {
                    if (onevoucherline.VSLineID > 0)
                    { 
                      Success = await VWS_LineService.VWSLine_Update(onevoucherline);

                    }
                    else
                    {
                        onevoucherline.VSLineHeaderID = VSHeaderID;
                        Success = await VWS_LineService.VWSLine_Insert(onevoucherline);
                    }

                }

                foreach (var onevoucherline in VoucherLinesToBeDeleted)
                {
                    Success = await VWS_LineService.VSLineDeleteOne(onevoucherline);
                }

                VoucherLinesToBeDeleted.Clear();
                NavigationManager.NavigateTo("/");
            }
        }

        private async Task CloseDialog()
        {
            await this.DialogAddeditVoucherLine.HideAsync();
        }

        void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }

        private void VoucherLineDelete()
        {
            if (selectedVSLineID > 0)
            {
                VoucherLinesToBeDeleted.Add(selectedVSLineID);
            }

            var lineToRemove = voucherLines.Single(x => x.VSLineID == selectedVSLineID);
            voucherLines.Remove(lineToRemove);
            VoucherLinesGrid.Refresh();


            selectedVSLineID = 0;

        }
       
        
    }
}
