using System.ComponentModel.DataAnnotations;

namespace StudentAttendanceSystem.Application.DTOs
{
    public class ScanQrCodeDto
    {
        [Required(ErrorMessage = "QR Code is required")]
        public string QRCode { get; set; }

        [Required(ErrorMessage = "Student ID is required")]
        public int StudentId { get; set; }
    }
}
