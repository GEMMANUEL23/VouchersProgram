using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Navigations;
using VouchersProgram.Data.Models;
using VouchersProgram.Data.Services;
using VouchersProgram.Shared;
namespace VouchersProgram.Pages
{
    public partial class Index
    {
        IEnumerable<VWS_Header> vsheader;
        private List<ItemModel> Toolbaritems = new List<ItemModel>();
        private int selectedVSHeaderID { get; set; } = 0;

        private int VSHeaderID;

        VWS_Header voucherHeader = new VWS_Header();


        WarningPage Warning;
        string WarningHeaderMessage = "";
        string WarningContentMessage = "";



        protected override async Task OnInitializedAsync()
        {
            vsheader = await VWS_HeaderService.VWSHeader_List();

            Toolbaritems.Add(new ItemModel() { Text = "Create", TooltipText = "Add a new voucher", PrefixIcon = "e-add" });
            Toolbaritems.Add(new ItemModel() { Text = "Edit", TooltipText = "Edit selected voucher", PrefixIcon = "e-edit" });
            
        }

        public void ToolbarClickHandler(Syncfusion.Blazor.Navigations.ClickEventArgs args)
        {
            if (args.Item.Text == "Create")
            {
                //Code for adding goes here
                VSHeaderID = 0;
                NavigationManager.NavigateTo($"/voucher/{VSHeaderID}");

            }

            if (args.Item.Text == "Edit")
            {
                //Code for editing
                if (selectedVSHeaderID == 0)
                {
                    WarningHeaderMessage = "Warning!";
                    WarningContentMessage = "Please select an Order from the grid.";
                    Warning.OpenDialog();
                }
                else
                {
                    NavigationManager.NavigateTo($"/voucher/{selectedVSHeaderID}");
                }

            }

            
        }

        public void RowSelectHandler(RowSelectEventArgs<VWS_Header> args)
        {
            selectedVSHeaderID = args.Data.VSHeaderID;
            
        }

    }
}
