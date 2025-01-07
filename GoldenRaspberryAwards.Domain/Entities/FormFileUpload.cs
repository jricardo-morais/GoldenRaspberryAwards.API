using Microsoft.AspNetCore.Http;

namespace GoldenRaspberryAwards.Domain.Entities
{
    public class FormFileUpload
    {
        public IFormFile File { get; set; }
    }
}
