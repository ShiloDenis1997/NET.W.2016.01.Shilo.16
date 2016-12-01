﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace DatabaseQueries
{
    class MainQueries
    {
        static void Main(string[] args)
        {
            using (ShawarmaModel ctx = new ShawarmaModel())
            {
                PrintMenu();
                Console.WriteLine("Enter command number: ");
                string ans = Console.ReadLine();
                do
                {
                    switch (ans)
                    {
                        case "0":
                            goto endWork;
                        case "1":
                            AddIngredient(ctx);
                            break;
                        case "2":
                            AddIngredientCategory(ctx);
                            break;
                        case "3":
                            ShowIngradients(ctx);
                            break;
                        case "4":
                            ShowCategories(ctx);
                            break;
                        case "5":
                            AddShawarmaRecipe(ctx);
                            break;
                        case "6":
                            ShowShawarma(ctx);
                            break;
                    }
                    PrintMenu();
                    Console.WriteLine("Enter command number: ");
                    ans = Console.ReadLine();
                } while (true);
                endWork:
                ;
            }
        }

        public static void PrintMenu()
        {
            Console.WriteLine("-------MENU-------\n" +
                              "\t0 - Exit\n" +
                              "\t1 - Add ingradient\n" +
                              "\t2 - Add ingradient category\n" +
                              "\t3 - Show ingradients\n" +
                              "\t4 - Show categories\n" +
                              "\t5 - Add shawarma recipe\n" +
                              "\t6 - Show shawarma\n");
        }

        public static void AddShawarmaRecipe(ShawarmaModel ctx)
        {
            Console.WriteLine("Enter shawarma name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter cooking time: ");
            string cookingTimeString = Console.ReadLine();
            int cookingTime;
            if (!int.TryParse(cookingTimeString, out cookingTime))
            {
                Console.WriteLine("Invalid cooking time");
                return;
            }
            List<string> ingradients = new List<string>();
            List<int> weights = new List<int>();
            string ans;
            Console.WriteLine("Enter ingadient name or 0 to finish");
            ans = Console.ReadLine();
            while (ans != "0")
            {
                ingradients.Add(ans);
                Console.WriteLine("Enter weight");
                int weight;
                while (!int.TryParse(Console.ReadLine(), out weight))
                    Console.WriteLine("Invalid weight has been entered. Try again: ");
                weights.Add(weight);
                Console.WriteLine("Enter next ingradient name or 0");
                ans = Console.ReadLine();
            }
            if (DataOperations.AddRecipe
                (ctx, name, cookingTime, ingradients.ToArray(), weights.ToArray()))
                Console.WriteLine("Added succesfully");
            else
                Console.WriteLine("Cannot add shawarma");
        }

        public static void ShowShawarma(ShawarmaModel ctx)
        {
            Console.WriteLine("Shawarma:");
            foreach (var shawarma in ctx.Shawarma)
            {
                Console.WriteLine("\t" + shawarma.ShawarmaName + ":");
                foreach (var recipe in shawarma.ShawarmaRecipe)
                {
                    Console.WriteLine($"\t\t{recipe.Ingradient.IngradientName} " +
                                      $"- {recipe.Weight}g");
                }
            }    
        }

        public static void AddIngredientCategory(ShawarmaModel ctx)
        {
            Console.WriteLine("Enter category name:");
            string name = Console.ReadLine();
            if (DataOperations.AddIngredientCategory(ctx, name))
                Console.WriteLine("Added succesfully");
            else
                Console.WriteLine("Cannot add category");
        }

        public static void ShowCategories(ShawarmaModel ctx)
        {
            Console.WriteLine("Categories:");
            foreach (var category in ctx.IngradientCategory)
            {
                Console.WriteLine("\t" + category.CategoryName);
            }
        }

        public static void ShowIngradients(ShawarmaModel ctx)
        {
            foreach (var category in ctx.IngradientCategory)
            {
                Console.WriteLine("Category: " + category.CategoryName);
                foreach (var ingr in category.Ingradient)
                {
                    Console.WriteLine($"\tName: {ingr.IngradientName}");
                    Console.WriteLine($"\tTotal weight: {ingr.TotalWeight}\n");
                }
            }
        }

        public static void AddIngredient(ShawarmaModel ctx)
        {
            Console.WriteLine("Enter ingradient category: ");
            string categoryName = Console.ReadLine();
            Console.WriteLine("Enter ingradient name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter weight: ");
            string weightString = Console.ReadLine();
            int weight;
            if (!int.TryParse(weightString, out weight))
            {
                Console.WriteLine("Invalid weight");
                return;
            }
            if (DataOperations.AddIngredient(ctx, name, categoryName, weight))
                Console.WriteLine("Added succesfully");
            else
            {
                Console.WriteLine("Cannot add an ingradient");
            }
        }
    }
}
