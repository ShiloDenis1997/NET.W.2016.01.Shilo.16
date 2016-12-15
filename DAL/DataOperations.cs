using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DAL
{
    public static class DataOperations
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
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Adds new ingradient category
        /// </summary>
        /// <param name="name"></param>
        /// <returns>true if succesfully added, false if not</returns>
        public static bool AddIngredientCategory(string name)
        {
            using (var ctx = new ShawarmaModel())
            {
                ctx.IngradientCategory.Add(new IngradientCategory { CategoryName = name });
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Sells one shawarma by decreasing total weight of ingradients
        /// </summary>
        /// <param name="shawarmaName"></param>
        /// /// <returns>true if succesfully added, false if not</returns>
        public static bool SellShawarma(string shawarmaName)
        {
            using (var ctx = new ShawarmaModel())
            {
                Shawarma shawarma = ctx.Shawarma.FirstOrDefault
                    (sh => sh.ShawarmaName == shawarmaName);
                if (shawarma == null)
                    return false;
                foreach (var recipe in shawarma.ShawarmaRecipe)
                {
                    if (recipe.Weight > recipe.Ingradient.TotalWeight)
                        return false;
                    recipe.Ingradient.TotalWeight -= recipe.Weight;
                }
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Adds new recipe of shawarma
        /// </summary>
        /// <param name="shawarmaName"></param>
        /// <param name="cookingTime"></param>
        /// <param name="ingradientNames"></param>
        /// <param name="weights"></param>
        /// <returns></returns>
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
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Adds selling point category to database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>true if succesfully added, false if not</returns>
        public static bool AddSellingPointCategory(string name)
        {
            using (var ctx = new ShawarmaModel())
            {
                ctx.SellingPointCategory.Add(new SellingPointCategory
                        { SellingPointCategoryName = name });
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Adds selling point to database
        /// </summary>
        /// <returns>true if succesfully added, false if not</returns>
        public static bool AddSellingPoint(string title, string address, string categoryName)
        {
            using (var ctx = new ShawarmaModel())
            {
                SellingPoint point = new SellingPoint {ShawarmaTitle = title, Address = address};
                SellingPointCategory category = ctx.SellingPointCategory
                    .FirstOrDefault(c => c.SellingPointCategoryName == categoryName);
                if (category == null)
                    return false;
                point.SellingPointCategoryId = category.SellingPointCategoryId;
                ctx.SellingPoint.Add(point);
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Set's or create new price for shawarma in specific <paramref name="sellingPointTitle"/>
        /// </summary>
        /// <returns>true if succesfully added, false if not</returns>
        public static bool SetNewPrice
            (string shawarmaName, decimal price, string sellingPointTitle, string comment)
        {
            using (var ctx = new ShawarmaModel())
            {
                Shawarma shawarma = ctx.Shawarma.FirstOrDefault
                    (sh => sh.ShawarmaName == shawarmaName);
                if (shawarma == null)
                    return false;
                SellingPoint sp = ctx.SellingPoint.FirstOrDefault
                    (s => s.ShawarmaTitle == sellingPointTitle);
                if (sp == null)
                    return false;
                PriceController pc = ctx.PriceController.FirstOrDefault
                    (p => p.ShawarmaId == shawarma.ShawarmaId
                          && p.SellingPointId == sp.SellingPointId);
                if (pc != null)
                {
                    pc.Price = price;
                    pc.Comment = comment;
                }
                else
                {
                    pc = new PriceController
                    {
                        Price = price,
                        Comment = comment,
                        SellingPointId = sp.SellingPointId,
                        ShawarmaId = shawarma.ShawarmaId
                    };
                    ctx.PriceController.Add(pc);
                }
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Adds new seller to specific selling point
        /// </summary>
        /// <param name="name"></param>
        /// <param name="shawarmaTitle"></param>
        /// <returns></returns>
        public static bool AddSeller(string name, string shawarmaTitle)
        {
            Seller seller = new Seller {SellerName = name};
            using (var ctx = new ShawarmaModel())
            {
                SellingPoint sp = ctx.SellingPoint.FirstOrDefault
                    (selPoint => selPoint.ShawarmaTitle == shawarmaTitle);
                if (sp == null)
                    return false;
                seller.SellingPointId = sp.SellingPointId;
                ctx.Seller.Add(seller);
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Add new time controller data to database
        /// </summary>
        /// <param name="sellerName"></param>
        /// <param name="workStart"></param>
        /// <param name="workEnd"></param>
        /// <returns></returns>
        public static bool AddTimeController
            (string sellerName, DateTime workStart, DateTime workEnd)
        {
            using (var ctx = new ShawarmaModel())
            {
                Seller seller = ctx.Seller.FirstOrDefault
                    (sel => sel.SellerName == sellerName);
                if (seller == null)
                    return false;
                ctx.TimeController.Add(new TimeController
                {
                    SellerId = seller.SellerId,
                    WorkStart = workStart,
                    WorkEnd = workEnd
                });
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Adds new Order to database
        /// </summary>
        /// <param name="shawarmaName"></param>
        /// <param name="date"></param>
        /// <param name="sellerName"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static bool AddOrder
            (string shawarmaName, DateTime date, string sellerName, int quantity)
        {
            using (var ctx = new ShawarmaModel())
            {
                Shawarma shawarma = ctx.Shawarma.FirstOrDefault
                    (sh => sh.ShawarmaName == shawarmaName);
                if (shawarma == null)
                    return false;
                Seller seller = ctx.Seller.FirstOrDefault
                    (sel => sel.SellerName == sellerName);
                if (seller == null)
                    return false;
                OrderHeader orderHeader = ctx.OrderHeader.FirstOrDefault
                    (oh => oh.OrderDate == date && oh.SellerId == seller.SellerId);
                if (orderHeader == null)
                    orderHeader = new OrderHeader
                    {
                        OrderDate = date,
                        SellerId = seller.SellerId
                    };
                ctx.OrderHeader.Add(orderHeader);
                OrderDetails orderDetails = new OrderDetails
                {
                    OrderHeaderId = orderHeader.OrderHeaderId,
                    ShawarmaId = shawarma.ShawarmaId,
                    Quantity = quantity
                };
                ctx.OrderDetails.Add(orderDetails);
                return Commit(ctx);
            }
        }

        /// <summary>
        /// Saves changes of <paramref name="ctx"/>
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns>true if successfully saved, otherwise returns false</returns>
        private static bool Commit(DbContext ctx)
        {
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
