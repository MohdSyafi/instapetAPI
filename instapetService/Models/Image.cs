using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Models
{
    public class Image
    {
        public int PostId { get; set; }

        public string Name { get; set; } = "";
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ImageId { get; set; }

        public string Location { get; set; } = "";
    }
}
