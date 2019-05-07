using System;
using System.Collections.Generic;

namespace ThesisProject.Models
{
    public partial class Image
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string Extn { get; set; }
        public int? FkModuleId { get; set; }

        public Module FkModule { get; set; }
    }
}
