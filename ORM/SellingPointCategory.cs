namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellingPointCategory")]
    public partial class SellingPointCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SellingPointCategory()
        {
            SellingPoint = new HashSet<SellingPoint>();
        }

        public int SellingPointCategoryId { get; set; }

        [Required]
        [StringLength(255)]
        public string SellingPointCategoryName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellingPoint> SellingPoint { get; set; }
    }
}
