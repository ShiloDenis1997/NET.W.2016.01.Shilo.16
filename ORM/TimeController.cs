namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TimeController")]
    public partial class TimeController
    {
        public int TimeControllerId { get; set; }

        public int SellerId { get; set; }

        public DateTime WorkStart { get; set; }

        public DateTime WorkEnd { get; set; }

        public virtual Seller Seller { get; set; }
    }
}
