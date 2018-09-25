namespace SocNetwork.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading;
    using System.Web;
    using System.Web.Mvc;
    using DAL.ConnectionStrings;
    using DAL.Entities.Friends;
    using DAL.Entities.Users;
    using DAL.Repositories;
    using SocNetwork.Helpers;
    using SocNetwork.Models;

    public class FriendController : Controller
    {
        private FriendsRepository friendsRepository;
        private UserRepository userRepository;
        
        public FriendController()
        {
            this.friendsRepository = new FriendsRepository(ConnectionString.GetConnectionString());
            this.userRepository = new UserRepository(ConnectionString.GetConnectionString());
        }

        [Authorize]
        public ActionResult Index()
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            List<Friends> relList = this.friendsRepository.GetFriendsByRelation(userId, (int)Relation.Friends);
            List<User> friends = relList.ToUserList(userId);

            return this.View(new FriendsViewModel(new User(), friends));
        }

        public ActionResult Get(int userId)
        {
            int id = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            if (id == userId)
            {
                return this.RedirectToAction("Index");
            }

            List<Friends> relList = this.friendsRepository.GetFriendsByRelation(userId, (int)Relation.Friends);
            List<User> friends = relList.ToUserList(userId);

            User user = this.userRepository.Get(userId);

            return this.View(new FriendsViewModel(user, friends));
        }

        [Authorize]
        public ActionResult GetIncomingRequest()
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            List<Friends> relList = this.friendsRepository.GetFriendsByRelation(userId, (int)Relation.Incoming);
            List<User> friends = relList.ToUserList(userId);

            return this.View(new FriendsViewModel(new User(), friends));
        }

        [Authorize]
        public ActionResult GetOutgoingRequest()
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);
            List<Friends> relList = this.friendsRepository.GetFriendsByRelation(userId, (int)Relation.Outgoing);
            List<User> friends = relList.ToUserList(userId);

            return this.View(new FriendsViewModel(new User(), friends));
        }

        [Authorize]
        public ActionResult SendRequest(int fid)
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);

            try
            {
                Friends fr = this.friendsRepository.Get(userId, fid);
                if (fr != null)
                {
                    if ((fr.UserId == userId && fr.RelationId == (int)Relation.Incoming) || (fr.FriendId == userId && fr.RelationId == (int)Relation.Outgoing))
                    {
                        this.friendsRepository.Save(new Friends(userId, fid, (int)Relation.Friends));
                    }
                }
                else
                {
                    this.friendsRepository.Save(new Friends(userId, fid, (int)Relation.Outgoing));
                }

                return this.RedirectToRoute("User", new { id = fid });
            }
            catch
            {
                return this.RedirectToRoute("User", new { id = fid });
            }
        }

        [Authorize]
        public ActionResult AcceptRequest(int fid)
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);

            try
            {
                Friends fr = this.friendsRepository.Get(userId, fid);
                if (fr != null)
                {
                    if ((fr.UserId == userId && fr.RelationId == (int)Relation.Incoming) || (fr.FriendId == userId && fr.RelationId == (int)Relation.Outgoing))
                    {
                        this.friendsRepository.Save(new Friends(userId, fid, (int)Relation.Friends));
                    }
                }

                return this.RedirectToRoute("Incoming friend request");
            }
            catch
            {
                return this.RedirectToRoute("Incoming friend request");
            }
        }

        [Authorize]
        public ActionResult DismissRequest(int fid)
        {
            int userId = int.Parse(Thread.CurrentPrincipal.Identity.Name);

            try
            {
                Friends fr = this.friendsRepository.Get(userId, fid);
                if (fr != null)
                {
                    this.friendsRepository.Delete(fr.UserId, fr.FriendId);
                }

                return this.RedirectToRoute("Friends");
            }
            catch
            {
                return this.RedirectToRoute("Friends");
            }
        }
    }
}
