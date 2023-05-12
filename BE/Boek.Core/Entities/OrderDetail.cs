using System;
using System.Collections.Generic;

namespace Boek.Core.Entities
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? BookProductId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? Discount { get; set; }
        public bool? WithPdf { get; set; }
        public bool? WithAudio { get; set; }

        public virtual BookProduct BookProduct { get; set; }
        public virtual Order Order { get; set; }
    }
}
