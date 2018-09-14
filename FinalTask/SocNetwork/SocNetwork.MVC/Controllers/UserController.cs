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
    public class UserController : Controller
    {
        UserRepository repo = new UserRepository(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        public ActionResult Control()
        {
            return View();
        }

        public ActionResult Get(int id)
        {
            User user = repo.Get(id);
            if (user != null)
            {
                ViewBag.User = user;
            }

            return View();
        }

        public ActionResult GetAll(int num = 20)
        {
            List<User> users = repo.GetAll(num);
            if (users != null)
            {
                ViewBag.Users = users;
            }

            return View();
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                User user = new User();
                user.Firstname = collection.Get("Firstname");
                user.Surname = collection.Get("Lastname");
                user.Password = collection.Get("Password");
                user.Email = collection.Get("Email");
                user.UserRole = UserRole.User;

                if (repo.Save(user))
                {
                    User u = repo.GetAll().Find(e => e.Email.Equals(user.Email));
                    return Redirect(string.Format("/user/{0}", u.Id));
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View(id);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            try
            {
                int.TryParse(collection.Get("ID"), out int id);

                User user = repo.Get(id);
                if (user != null)
                {
                    repo.Delete(id);
                }

                return Redirect("/users");
            }
            catch
            {
                return Redirect("/users");
            }
        }
    }
}
