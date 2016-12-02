using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using ORM;

namespace DatabaseQueries
{
    class MainQueries
    {
        static void Main(string[] args)
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
                        AddIngredient();
                        break;
                    case "2":
                        AddIngredientCategory();
                        break;
                    case "3":
                        ShowIngradients();
                        break;
                    case "4":
                        ShowCategories();
                        break;
                    case "5":
                        AddShawarmaRecipe();
                        break;
                    case "6":
                        ShowShawarma();
                        break;
                    case "7":
                        SellShawarma();
                        break;
                    case "8":
                        AddSellingPointCategory();
                        break;
                    case "9":
                        AddSellingPoint();
                        break;
                    case "10":
                        ShowSellingPointCategories();
                        break;
                    case "11":
                        ShowSellingPoints();
                        break;
                    case "12":
                        AddPrice();
                        break;
                    case "13":
                        ShowPrices();
                        break;
                    case "14":
                        AddSeller();
                        break;
                    case "15":
                        ShowSellers();
                        break;
                    case "16":
                        AddOrder();
                        break;
                    case "17":
                        AddTimeController();
                        break;
                    case "18":
                        ShowReportBySellingPoint();
                        break;
                    case "19":
                        ShowReportBySeller();
                        break;
                }
                PrintMenu();
                Console.WriteLine("Enter command number: ");
                ans = Console.ReadLine();
            } while (true);
            endWork:
            ;
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
                              "\t6 - Show shawarma\n" +
                              "\t7 - Sell shawarma\n" +
                              "\t8 - Add selling point category\n" +
                              "\t9 - Add selling point\n" +
                              "\t10 - Show selling point categories\n" +
                              "\t11 - Show selling points\n" +
                              "\t12 - Set price\n" +
                              "\t13 - Show prices\n" +
                              "\t14 - Add seller-cooker\n" +
                              "\t15 - Show sellers\n" +
                              "\t16 - Add order\n" +
                              "\t17 - Add time controller\n" +
                              "\t18 - Show report by selling point\n" +
                              "\t19 - Show report by seller\n");
        }

        public static void ShowReportBySellingPoint()
        {
            Console.WriteLine("Enter start date (dd.mm.yyyy hh:mm)");
            string startDateStr = Console.ReadLine();
            Console.WriteLine("Enter end date (dd.mm.yyyy hh:mm)");
            string endDateStr = Console.ReadLine();
            DateTime startDate, endDate;
            if (!DateTime.TryParse(startDateStr, out startDate))
            {
                Console.WriteLine("Wrong date");
                return;
            }
            if (!DateTime.TryParse(endDateStr, out endDate))
            {
                Console.WriteLine("Wrong date");
                return;
            }
            using (var ctx = new ShawarmaModel())
            {
                foreach (var selPoint in ctx.SellingPoint)
                {
                    Console.WriteLine("Shawarma title: " + selPoint.ShawarmaTitle);
                    foreach (var seller in selPoint.Seller)
                    {
                        foreach (var orderHeader 
                            in seller.OrderHeader.Where
                                (oh => oh.OrderDate >= startDate && oh.OrderDate <= endDate))
                        {
                            Console.WriteLine("\tDate: " + orderHeader.OrderDate);
                            decimal earned = 0m;
                            foreach (var detail in orderHeader.OrderDetails)
                            {
                                try
                                {
                                    earned += detail.Quantity*
                                              detail.Shawarma.PriceController.First(
                                                  pc => pc.SellingPointId == selPoint.SellingPointId).Price;
                                }
                                catch
                                {
                                    Console.WriteLine("Some shawarma has no cost in some selling point");
                                }
                            }
                            Console.WriteLine("\t\tEarned: " + earned);
                        }
                    }
                }
            }
        }

        public static void ShowReportBySeller()
        {
            Console.WriteLine("Enter start date (dd.mm.yyyy hh:mm)");
            string startDateStr = Console.ReadLine();
            Console.WriteLine("Enter end date (dd.mm.yyyy hh:mm)");
            string endDateStr = Console.ReadLine();
            DateTime startDate, endDate;
            if (!DateTime.TryParse(startDateStr, out startDate))
            {
                Console.WriteLine("Wrong date");
                return;
            }
            if (!DateTime.TryParse(endDateStr, out endDate))
            {
                Console.WriteLine("Wrong date");
                return;
            }
            using (var ctx = new ShawarmaModel())
            {
                foreach (var seller in ctx.Seller)
                {
                    Console.WriteLine("Seller name: " + seller.SellerName);
                    TimeSpan sumWorkTime = new TimeSpan(0);
                    foreach (var timeController in seller.TimeController)
                    {
                        sumWorkTime += timeController.WorkEnd - timeController.WorkStart;
                    }
                    Console.WriteLine("\tWorking time: " + sumWorkTime.TotalHours + "h");
                    int cookingTime = 0;
                    foreach (var orderHeader in seller.OrderHeader
                        .Where(oh => oh.OrderDate >= startDate && oh.OrderDate <= endDate))
                        foreach (var detail in orderHeader.OrderDetails)
                        {
                            cookingTime += detail.Quantity*detail.Shawarma.CookingTime;
                        }
                    Console.WriteLine("\tCooking time: " + cookingTime + "min");
                }
            }
        }

        public static void AddTimeController()
        {
            Console.WriteLine("Enter seller name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter work start date (dd.mm.yyyy hh:mm)");
            string startDateStr = Console.ReadLine();
            Console.WriteLine("Enter work end date (dd.mm.yyyy hh:mm)");
            string endDateStr = Console.ReadLine();
            DateTime startDate, endDate;
            if (!DateTime.TryParse(startDateStr, out startDate))
            {
                Console.WriteLine("Wrong date");
                return;
            }
            if (!DateTime.TryParse(endDateStr, out endDate))
            {
                Console.WriteLine("Wrong date");
                return;
            }
            if (endDate < startDate)
            {
                Console.WriteLine("End date < start date. Invalid input");
                return;
            }
            if (DataOperations.AddTimeController(name, startDate, endDate))
                Console.WriteLine("Added");
            else
                Console.WriteLine("Can't add");
        }

        public static void AddOrder()
        {
            Console.WriteLine("Enter seller name:");
            string selName = Console.ReadLine();
            Console.WriteLine("Enter shawarma name:");
            string shName = Console.ReadLine();
            Console.WriteLine("Enter quantity: ");
            string quantityStr = Console.ReadLine();
            int quantity;
            if (!int.TryParse(quantityStr, out quantity))
            {
                Console.WriteLine("Wrong quantity");
                return;
            }
            Console.WriteLine("Enter order date (dd.mm.yyyy hh:mm)");
            string date = Console.ReadLine();
            DateTime orderDate;
            if (!DateTime.TryParse(date, out orderDate))
            {
                Console.WriteLine("Wrong date");
                return;
            }
            if (DataOperations.AddOrder(shName, orderDate, selName, quantity))
                Console.WriteLine("Added");
            else
            {
                Console.WriteLine("Can't add");
            }
        }

        public static void ShowSellers()
        {
            using (var ctx = new ShawarmaModel())
            {
                Console.WriteLine("Sellers:");
                foreach (var seller in ctx.Seller)
                {
                    Console.WriteLine("\tName: " + seller.SellerName);
                    Console.WriteLine("\tWorks in: " + seller.SellingPoint.ShawarmaTitle);
                }
            }
        }
        public static void AddSeller()
        {
            Console.WriteLine("Enter seller's name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Enter selling point shawarma title: ");
            string title = Console.ReadLine();
            if (DataOperations.AddSeller(name, title))
                Console.WriteLine("Added");
            else
                Console.WriteLine("Can't add");
        }

        public static void ShowPrices()
        {
            using (var ctx = new ShawarmaModel())
            {
                foreach (var sp in ctx.SellingPoint)
                {
                    Console.WriteLine($"Selling point: {sp.ShawarmaTitle}");
                    foreach (var pc in sp.PriceController)
                        Console.WriteLine($"Shawarma name: {pc.Shawarma.ShawarmaName}\n" +
                                          $"\tPrice: {pc.Price:C}" +
                                          $"\tComment: {pc.Comment}");
                }
            }
        }
        public static void AddPrice()
        {
            Console.WriteLine("Enter shawarma name: ");
            string shName = Console.ReadLine();
            Console.WriteLine("Enter selling point title: ");
            string spTitle = Console.ReadLine();
            Console.WriteLine("Enter price: ");
            string strPrice = Console.ReadLine();
            decimal price;
            if (!decimal.TryParse(strPrice, out price))
            {
                Console.WriteLine("Invalid price format");
                return;
            }
            Console.WriteLine("Enter comment:");
            string comment = Console.ReadLine();
            if (DataOperations.SetNewPrice(shName, price, spTitle, comment))
                Console.WriteLine("Setted!");
            else
                Console.WriteLine("Can't set this price");
        }
        public static void ShowSellingPointCategories()
        {
            using (var ctx = new ShawarmaModel())
            {
                Console.WriteLine("Categories:");
                foreach (var category in ctx.SellingPointCategory)
                {
                    Console.WriteLine("\t" + category.SellingPointCategoryName);
                }
            }
        }

        public static void ShowSellingPoints()
        {
            using (var ctx = new ShawarmaModel())
            {
                foreach (var category in ctx.SellingPointCategory)
                {
                    Console.WriteLine("Category: " + category.SellingPointCategoryName);
                    foreach (var selPoint in category.SellingPoint)
                    {
                        Console.WriteLine($"\tTitle: {selPoint.ShawarmaTitle}\n" +
                                          $"\tAddress: {selPoint.Address}\n");
                    }
                    
                }
            }
        }
        public static void AddSellingPoint()
        {
            Console.WriteLine("Enter selling point address");
            string address = Console.ReadLine();
            Console.WriteLine("Enter selling point shawarma title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter selling point category");
            string category = Console.ReadLine();
            if (DataOperations.AddSellingPoint(title, address, category))
                Console.WriteLine("Added!");
            else
                Console.WriteLine("Can't add");
        }

        public static void AddSellingPointCategory()
        {
            Console.WriteLine("Enter selling category");
            string category = Console.ReadLine();
            if (DataOperations.AddSellingPointCategory(category))
                Console.WriteLine("Added!");
            else
                Console.WriteLine("Can't add. Maybe already exists");
        }

        public static void AddShawarmaRecipe()
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
                (name, cookingTime, ingradients.ToArray(), weights.ToArray()))
                Console.WriteLine("Added succesfully");
            else
                Console.WriteLine("Cannot add shawarma");
        }

        public static void SellShawarma()
        {
            Console.WriteLine("Enter shawarma name:");
            string name = Console.ReadLine();
            if (DataOperations.SellShawarma(name))
                Console.WriteLine("Selled! Your earned money:)");
            else
                Console.WriteLine("Can't sell shaurma, maybe not enought ingradients :(");
        }

        public static void ShowShawarma()
        {
            Console.WriteLine("Shawarma:");

            using (var ctx = new ShawarmaModel())
            {
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
        }

        public static void AddIngredientCategory()
        {
            Console.WriteLine("Enter category name:");
            string name = Console.ReadLine();
            if (DataOperations.AddIngredientCategory(name))
                Console.WriteLine("Added succesfully");
            else
                Console.WriteLine("Cannot add category");
        }

        public static void ShowCategories()
        {
            Console.WriteLine("Categories:");
            using (var ctx = new ShawarmaModel())
            {
                foreach (var category in ctx.IngradientCategory)
                {
                    Console.WriteLine("\t" + category.CategoryName);
                }
            }
        }

        public static void ShowIngradients()
        {
            using (var ctx = new ShawarmaModel())
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
        }

        public static void AddIngredient()
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
            if (DataOperations.AddIngredient(name, categoryName, weight))
                Console.WriteLine("Added succesfully");
            else
            {
                Console.WriteLine("Cannot add an ingradient");
            }
        }
    }
}
