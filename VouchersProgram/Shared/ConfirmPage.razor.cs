using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Popups;

namespace VouchersProgram.Shared
{
    public partial class ConfirmPage
    {
        SfDialog DialogConfirm;

        public bool IsVisible { get; set; } = false;

        [Parameter]
        public string ConfirmHeaderMessage { get; set; }
        [Parameter]
        public string ConfirmContentMessage { get; set; }

        [Parameter]
        public EventCallback<bool> ConfirmationChanged { get; set; }

        public void OpenDialog()
        {
            this.IsVisible = true;
            this.StateHasChanged();

        }

        protected async Task OnConfirmationChange(bool value)
        {
            this.IsVisible = false;
            await ConfirmationChanged.InvokeAsync(value);

        }
    }
}
