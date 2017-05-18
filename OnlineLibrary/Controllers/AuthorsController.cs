using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Omu.ValueInjecter;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Models;
using OnlineLibrary.Services;

namespace OnlineLibrary.Controllers
{
    public class AuthorsController : Controller
    {
        private IAuthorService service;

        public AuthorsController(IAuthorService service)
        {
            this.service = service;
        }
        
        // GET: Authors
        public ActionResult Index(int? page, string sort = "", string sortdir = "")
        {
            const int pageSize = 2;
            var pageNumber = page.HasValue ? page.Value : 1;

            ViewBag.Count = service.Count();
            ViewBag.CurrentPage = pageNumber;
            decimal x = ViewBag.Count / pageSize;
            if (x * pageSize < ViewBag.Count)
            {
                ViewBag.TotalPages = x + 1;}
            else
            {
                ViewBag.TotalPages = x;
            }
            
            ViewBag.PageSize = pageSize;

            List<AuthorModel> authorList = new List<AuthorModel>();
            var authorDbList = service.GetAuthors(pageNumber, pageSize, "FirstName", " ");
            authorList.InjectFrom(authorDbList);

            return View(authorList);
        }

        // GET: Authors/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            AuthorModel model = new AuthorModel();
            return View(model);
        }

        // POST: Authors/Create
        [HttpPost]
        public ActionResult Create(AuthorModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Author newAuthor=new Author();
                    newAuthor.InjectFrom(model);
                    service.CreateAuthor(newAuthor);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: Authors/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Authors/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Authors/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Authors/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
