namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellingPoint")]
    public partial class SellingPoint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SellingPoint()
        {
            PriceController = new HashSet<PriceController>();
            Seller = new HashSet<Seller>();
        }

        public int SellingPointId { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        public int SellingPointCategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string ShawarmaTitle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PriceController> PriceController { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Seller> Seller { get; set; }

        public virtual SellingPointCategory SellingPointCategory { get; set; }
    }
}
