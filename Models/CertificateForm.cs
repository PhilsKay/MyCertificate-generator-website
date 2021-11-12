using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PdfDownload.Models
{
    public class CertificateForm
    {
        [Required(ErrorMessage ="Yo? Enter Name")]
        public string Name { get; set; }
    }
}
