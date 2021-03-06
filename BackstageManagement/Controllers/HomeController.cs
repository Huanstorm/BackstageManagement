﻿using BackstageManagement.IServices;
using BackstageManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BackstageManagement.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IRolePermissionServices rolePermissionServices, 
            ILogServices logServices) : base(rolePermissionServices, logServices)
        {
            
        }
        public async Task<ActionResult> Index()
        {
            await Task.Run(() => { });
            
            return View();
        }
        
    }
}