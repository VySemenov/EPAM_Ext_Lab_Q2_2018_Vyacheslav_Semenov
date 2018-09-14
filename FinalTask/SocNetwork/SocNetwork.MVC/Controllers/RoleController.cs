using DAL.Entities.Users;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocNetwork.Controllers
{
    public class RoleController : Controller
    {
        RoleRepository repo = new RoleRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public ActionResult Control()
        {
            return View();
        }

        public ActionResult Get(int id)
        {
            UserRole role = repo.Get(id);
            if (role != UserRole.None)
            {
                ViewBag.UserRole = role;
            }

            return View();
        }

        public ActionResult GetAll()
        {
            List<UserRole> roles = repo.GetAll();
            if (roles != null)
            {
                ViewBag.UserRole = roles;
            }

            return View();
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                throw new NotImplementedException();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            throw new NotImplementedException();

            return View();
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                throw new NotImplementedException();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
            return View();
        }

        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                throw new NotImplementedException();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
