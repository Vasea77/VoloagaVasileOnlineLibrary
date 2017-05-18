using OnlineLibrary.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Omu.ValueInjecter;
using OnlineLibrary.Models;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Clases;

namespace OnlineLibrary.Controllers
{
    public class UsersController : Controller
    {
        private IUserService service;

        public UsersController(IUserService service)
        {
            this.service = service;
        }

        // GET: Users
        public ActionResult Index(int? page, string sortorder)
        {
            const int pageSize = 4;
            var pageNumber = page.HasValue ? page.Value : 1;
            ViewBag.Count = service.Count();

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = ViewBag.Count / pageSize;
            if (ViewBag.TotalPages * pageSize < ViewBag.Count)
            {
                ViewBag.TotalPages++;}
            ViewBag.PageSize = pageSize;

            List<UserViewModel> userList = new List<UserViewModel>();

            ViewBag.FirstNameSortParam = string.IsNullOrEmpty(sortorder) ? "FirstName_desc" : "";
            ViewBag.LastNameSortParam = sortorder == "LastName" ? "LastName_desc" : "LastName";

            ViewBag.CurrentSortOrder = sortorder;
            var userDbList = service.GetUser(pageNumber,pageSize,sortorder);
            

            userList.InjectFrom(userDbList);
            
            return View(userList);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return RedirectToAction("Index");
            //}
            User user = service.FindUser(id);
            UserViewModel model = new UserViewModel();
            model.InjectFrom(user);
            return View(model);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            CreateUserViewModel model = new CreateUserViewModel();
            return View(model);
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //use the service to insert in db
                    User newUser = new User();
                    newUser.InjectFrom(model);
                                       
                    newUser.Salt = Guid.NewGuid().ToString();
                    string hashedPassword = Security.HashSHA1(newUser.Password + newUser.Salt);
                    newUser.Password = hashedPassword;

                    service.CreateUser(newUser);


                    //service.SaveChanges();


                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            User user = service.FindUser(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            EditUserViewModel model = new EditUserViewModel();
            model.InjectFrom(user);
            //model.FirstName = user.FirstName;
            //model.LastName = user.LastName;
            //model.Email = user.Email;
            return View(model);
            
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email")] EditUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = service.FindUser(model.ID);
                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    TryUpdateModel(user);
                    service.UpdateUser(user);
                }
            }
            catch
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            User user = service.FindUser(id);
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                User user = service.FindUser(id);
                service.DeleteUser(user);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}