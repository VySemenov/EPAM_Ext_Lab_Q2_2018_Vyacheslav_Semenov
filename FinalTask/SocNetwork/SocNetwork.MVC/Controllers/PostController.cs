﻿namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using DAL.ConnectionStrings;
    using DAL.Entities.Posts;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;
    using SocNetwork.Helpers;
    using SocNetwork.Models;
    using SocNetwork.Models.ViewModels.Post;

    public class PostController : Controller
    {
        private IPostRepository postRepository;

        public PostController(IPostRepository repo)
        {
            this.postRepository = repo;
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(CreatePostViewModel model)
        {
            try
            {
                int uid = int.Parse(Thread.CurrentPrincipal.Identity.Name);
                Post post = new Post()
                {
                    AuthorId = uid,
                    PageId = model.PageId,
                    PublicationDate = DateTime.Now,
                    Text = model.Text
                };

                if (this.postRepository.Save(post))
                {
                    return this.RedirectToRoute("User", new { id = post.PageId });
                }
                else
                {
                    return this.RedirectToRoute("User", new { id = post.PageId });
                }
            }
            catch
            {
                return this.RedirectToRoute("User", new { id = model.PageId });
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(EditPostViewModel model)
        {
            int uid = int.Parse(Thread.CurrentPrincipal.Identity.Name);

            Post post = this.postRepository.Get(model.PostId);
            if (post != null)
            {
                if (!RoleAuth.IsInRole(uid, (int)UserRole.Admin) && !RoleAuth.IsInRole(uid, (int)UserRole.Moderator))
                {
                    if (post.AuthorId != uid)
                    {
                        return this.RedirectToRoute("User", new { id = post.PageId });
                    }
                }

                post.Text = model.Text;

                this.postRepository.Save(post);
                return this.RedirectToRoute("User", new { id = post.PageId });
            }

            return this.RedirectToRoute("User", new { id = post.PageId });
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            int uid = int.Parse(Thread.CurrentPrincipal.Identity.Name);

            Post post = this.postRepository.Get(id);
            if (post != null)
            {
                if (!RoleAuth.IsInRole(uid, (int)UserRole.Admin) && !RoleAuth.IsInRole(uid, (int)UserRole.Moderator))
                {
                    if (post.AuthorId != uid && post.PageId != uid)
                    {
                        return this.RedirectToRoute("User", new { id = post.PageId });
                    }
                }

                this.postRepository.Delete(id);
                return this.RedirectToRoute("User", new { id = post.PageId });
            }

            return this.RedirectToRoute("User", new { id = post.PageId });
        }
    }
}