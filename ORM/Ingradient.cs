namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ingradient")]
    public partial class Ingradient
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ingradient()
        {
            ShawarmaRecipe = new HashSet<ShawarmaRecipe>();
        }

        public int IngradientId { get; set; }

        [Required]
        [StringLength(255)]
        public string IngradientName { get; set; }

        public int TotalWeight { get; set; }

        public int CategoryId { get; set; }

        public virtual IngradientCategory IngradientCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShawarmaRecipe> ShawarmaRecipe { get; set; }
    }
}
