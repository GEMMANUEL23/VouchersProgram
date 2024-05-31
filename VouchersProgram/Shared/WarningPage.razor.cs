using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Popups;

namespace VouchersProgram.Shared
{
    public partial class WarningPage
    {
        SfDialog DialogWarning;
        public bool IsVisible { get; set; } = false;

        [Parameter] public string WarningHeaderMessage { get; set; }
        [Parameter] public string WarningContentMessage { get; set; }

        public void OpenDialog()
        {
            this.IsVisible = true;
            this.StateHasChanged();
        }

        public void CloseDialog()
        {
            this.IsVisible = false;
            this.StateHasChanged();
        }

    }
}
