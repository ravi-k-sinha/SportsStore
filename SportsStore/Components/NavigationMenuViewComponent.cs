﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {

        private IProductRepository repository;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            repository = repo;
        }
        
        public IViewComponentResult Invoke()
        {
            return View(
                repository.Products
                    .Select(x => x.Category)
                    .Distinct()
                    .OrderBy(x => x));
        }
    }
}