namespace SocNetwork
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using DAL.ConnectionStrings;
    using DAL.Repositories;
    using DAL.Repositories.Abstract;
    using DAL.Repositories.Concrete;
    using Ninject;

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            this.kernel = kernelParam;
            this.AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            this.kernel.Bind<IUserRepository>().To<UserRepository>();
            this.kernel.Bind<IUserDetailInfoRepository>().To<UserDetailInfoRepository>();
            this.kernel.Bind<IPostRepository>().To<PostRepository>();
            this.kernel.Bind<IRoleRepository>().To<RoleRepository>();
            this.kernel.Bind<IFriendsRepository>().To<FriendsRepository>();
            this.kernel.Bind<IDialogRepository>().To<DialogRepository>();
            this.kernel.Bind<IMessageRepository>().To<MessageRepository>();
        }
    }
}