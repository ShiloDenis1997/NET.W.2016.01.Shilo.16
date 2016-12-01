using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DatabaseQueries
{
    static class DataOperations
    {
        /// <summary>
        /// Addes new ingradient to database or if it's already exists
        /// increase <see cref="Ingradient.TotalWeight"/> by <paramref name="weight"/>
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="name"></param>
        /// <param name="categoryName"></param>
        /// <param name="weight"></param>
        /// <returns>true if succesfully added, false if not</returns>
        public static bool AddIngredient
            (ShawarmaModel ctx, string name, string categoryName, int weight)
        {
            Ingradient ingradient = ctx.Ingradient
                .FirstOrDefault(ingr => ingr.IngradientName == name);
            if (ingradient != null)
                ingradient.TotalWeight += weight;
            else
            {
                IngradientCategory category =
                    ctx.IngradientCategory
                        .FirstOrDefault(ingr => ingr.CategoryName == categoryName);
                if (category == null)
                    return false;
                int categoryId = category.CategoryId;
                ctx.Ingradient.Add(new Ingradient
                {
                    CategoryId = categoryId,
                    IngradientName = name,
                    TotalWeight = weight
                });
            }
            try
            {
                ctx.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                ctx.Ingradient.Remove(ingradient);
                return false;
            }
        }

        public static bool AddIngredientCategory(ShawarmaModel ctx, string name)
        {
            IngradientCategory ic = new IngradientCategory {CategoryName = name};
            ctx.IngradientCategory.Add(ic);
            try
            {
                ctx.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                ctx.IngradientCategory.Remove(ic);
                return false;
            }
        }

        public static bool AddRecipe
            (ShawarmaModel ctx, string shawarmaName, int cookingTime, 
                string[] ingradientNames, int[] weights)
        {
            Shawarma shawarma = new Shawarma { ShawarmaName = shawarmaName, CookingTime = cookingTime };
                ctx.Shawarma.Add(shawarma);
            Ingradient[] ingradients = ctx.Ingradient.Where
                (ingr => ingradientNames.Contains(ingr.IngradientName)).ToArray();
            if (ingradients.Length == 0)
                return false;
            ShawarmaRecipe[] shrs = new ShawarmaRecipe[ingradientNames.Length];
            for(int i = 0; i < ingradientNames.Length; i++)
            {
                ShawarmaRecipe sr = new ShawarmaRecipe
                {
                    IngradientId = ingradients.First
                        (ing => ing.IngradientName == ingradientNames[i]).IngradientId,
                    ShawarmaId = shawarma.ShawarmaId,
                    Weight = weights[i]
                };
                ctx.ShawarmaRecipe.Add(sr);
                shrs[i] = sr;
            }
            try
            {
                ctx.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                ctx.ShawarmaRecipe.RemoveRange(shrs);
                ctx.Shawarma.Remove(shawarma);
                return false;
            }
        }
    }
}
