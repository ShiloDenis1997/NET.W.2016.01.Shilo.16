﻿using System;
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
        /// <param name="name"></param>
        /// <param name="categoryName"></param>
        /// <param name="weight"></param>
        /// <returns>true if succesfully added, false if not</returns>
        public static bool AddIngredient
            (string name, string categoryName, int weight)
        {
            using (var ctx = new ShawarmaModel())
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
                    return false;
                }
            }
        }

        public static bool AddIngredientCategory(string name)
        {
            using (var ctx = new ShawarmaModel())
            {
                ctx.IngradientCategory.Add(new IngradientCategory { CategoryName = name });
                try
                {
                    ctx.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }

        public static bool SellShawarma(string shawarmaName)
        {

            return true;
        }

        public static bool AddRecipe
            (string shawarmaName, int cookingTime, 
                string[] ingradientNames, int[] weights)
        {
            Shawarma shawarma = new Shawarma { ShawarmaName = shawarmaName, CookingTime = cookingTime };
            using (var ctx = new ShawarmaModel())
            {
                ctx.Shawarma.Add(shawarma);
                IQueryable<Ingradient> ingradients = ctx.Ingradient.Where
                    (ingr => ingradientNames.Contains(ingr.IngradientName));
                if (ingradients.Count() != ingradientNames.Length)
                    return false;
                for (int i = 0; i < ingradientNames.Length; i++)
                {
                    ShawarmaRecipe sr = new ShawarmaRecipe
                    {
                        IngradientId = ingradients.First
                            (ing => ing.IngradientName == ingradientNames[i]).IngradientId,
                        ShawarmaId = shawarma.ShawarmaId,
                        Weight = weights[i]
                    };
                    ctx.ShawarmaRecipe.Add(sr);
                }
                try
                {
                    ctx.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }
    }
}
