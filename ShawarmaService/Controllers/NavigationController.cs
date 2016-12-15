using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShawarmaService.Controllers
{
    public class NavigationInfo
    {
        public string Controller { get; set; }
        public string LinkName { get; set; }

    }
    
    public class NavigationController : Controller
    {
        public ActionResult Menu()
        {
            NavigationInfo[] info =
            {
                new NavigationInfo {Controller = "Shawarmas", LinkName = "Shawarma"},
                new NavigationInfo {Controller = "Ingradients", LinkName = "Ingradients"},
                new NavigationInfo {Controller = "IngradientCategories", LinkName = "Ingradients"},
                new NavigationInfo {Controller = "SellingPoints", LinkName = "Selling points"},
                new NavigationInfo {Controller = "Sellers", LinkName = "Sellers"},
                new NavigationInfo {Controller = "SellingPointCategories", LinkName = "Selling point categories"},
                new NavigationInfo {Controller = "PriceControllers", LinkName = "Price controller"},
                new NavigationInfo {Controller = "OrderHeaders", LinkName = "Order header"},
                new NavigationInfo {Controller = "OrderDetails", LinkName = "Order details"},
                new NavigationInfo {Controller = "TimeControllers", LinkName = "Time controller"},
                new NavigationInfo {Controller = "ShawarmaRecipes", LinkName = "Shawarma recipes"},
            };
            return PartialView("_Menu", info);
        }
    }
}