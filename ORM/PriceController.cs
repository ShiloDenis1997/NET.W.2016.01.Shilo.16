namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PriceController")]
    public partial class PriceController
    {
        public int PriceControllerId { get; set; }

        public int ShawarmaId { get; set; }

        public decimal Price { get; set; }

        public int SellingPointId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Comment { get; set; }

        public virtual Shawarma Shawarma { get; set; }

        public virtual SellingPoint SellingPoint { get; set; }
    }
}
