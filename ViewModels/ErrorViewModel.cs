using System;

namespace ReservationProject.ViewModels
{
    public class ErrorViewModel
    {
        public int ErrorViewModelID { get; set; }
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
