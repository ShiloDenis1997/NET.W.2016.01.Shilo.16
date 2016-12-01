namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShawarmaRecipe")]
    public partial class ShawarmaRecipe
    {
        public int ShawarmaRecipeId { get; set; }

        public int ShawarmaId { get; set; }

        public int IngradientId { get; set; }

        public int Weight { get; set; }

        public virtual Ingradient Ingradient { get; set; }

        public virtual Shawarma Shawarma { get; set; }
    }
}
