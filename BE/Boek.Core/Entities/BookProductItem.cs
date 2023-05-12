using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class BookProductItem
    {
        public int Id { get; set; }
        public Guid? ParentBookProductId { get; set; }
        public int? BookId { get; set; }
        public byte? Format { get; set; }
        public int? DisplayIndex { get; set; }
        public bool? WithPdf { get; set; }
        public decimal? PdfExtraPrice { get; set; }
        public int? DisplayPdfIndex { get; set; }
        public bool? WithAudio { get; set; }
        public decimal? AudioExtraPrice { get; set; }
        public int? DisplayAudioIndex { get; set; }

        public virtual Book Book { get; set; }
        public virtual BookProduct ParentBookProduct { get; set; }
    }
}
