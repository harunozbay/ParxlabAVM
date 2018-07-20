﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ParxlabAVM.Identity;
using ParxlabAVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParxlabAVM.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class RoleAdminController : Controller
    {
        // GET: RoleAdmin - Rollerle alakalı işlerin yönetildiği controller

             
        private RoleManager<IdentityRole> roleManager;

        private UserManager<ApplicationUser> userManager;

        public RoleAdminController()
        {
            roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityDataContext()));
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDataContext()));

        }

        
        public ActionResult Index() //role index
        {
            return View(roleManager.Roles);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        public ActionResult Create([required] string name) //role oluşturma
        {
            if (ModelState.IsValid)
            {
                var result = roleManager.Create(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
            }

            return View(name);
        }

        [HttpPost]
        public ActionResult Delete(string id)   //rol silme
        {
            var role = roleManager.FindById(id);

            if (role != null)
            {
                var result = roleManager.Delete(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Role Bulunamadı" });
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {

            var role = roleManager.FindById(id);
            var members = new List<ApplicationUser>();
            var nonMembers = new List<ApplicationUser>();

            foreach (var user in userManager.Users.ToList())
            {
                var list = (userManager.IsInRole(user.Id, role.Name)) ? members : nonMembers;

                list.Add(user);
            }

            return View(new RoleEditModel()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers

            });
        }

        [HttpPost]
        public ActionResult Edit(RoleUpdateModel model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                foreach (var userId in model.IdsToAdd ?? new string[] { })
                {
                    result = userManager.AddToRole(userId,model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    result = userManager.RemoveFromRole(userId, model.RoleName);

                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }

                }

                return RedirectToAction("Index");

            }

            return View("Error",new string[] {"Aranılan rol yok"});

        }

    }

}