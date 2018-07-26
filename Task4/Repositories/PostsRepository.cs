namespace Task4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Task4.Repositories;

    public class PostsRepository : IBaseRepository<Post>
    {
        private static volatile PostsRepository instance;
        private static object syncRoot = new object();

        private List<Post> posts = new List<Post>();

        private PostsRepository()
        {
        }

        public static PostsRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new PostsRepository();
                        }
                    }
                }

                return instance;
            }
        }

        public Post Get(int id)
        {
            Post post = this.posts.Find(p => p.Id == id);
            return post;
        }

        public List<Post> GetAll()
        {
            return this.posts;
        }

        public bool Save(Post entity)
        {
            if (entity == null)
            {
                return false;
            }

            Post post = this.posts.Find(p => p.Id == entity.Id);
            if (post == null)
            {
                this.posts.Add(entity);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            Post post = this.posts.Find(p => p.Id == id);
            if (post == null)
            {
                return false;
            }

            this.posts.Remove(post);
            return true;
        }
    }
}