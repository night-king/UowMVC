using UowMVC.Domain;
using UowMVC.Repository;
using UowMVC.SDK;
using UowMVC.Service.Interfaces;
using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace UowMVC.Web
{
    /// <summary>
    /// Autofac注册配置
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// 容器私有
        /// </summary>
        private static readonly IContainer container;

        /// <summary>
        /// 锁
        /// 防并发
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static AutofacConfig()
        {
            if (container == null)
            {
                lock (locker)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterModule(new AutofacEntityFrameworkModule());
                    var executingAssembly = Assembly.GetExecutingAssembly();
                    var assemblies = AssemblyHelper.Search(HttpContext.Current.Server.MapPath("~/bin/"), new string[] { "*.Service.*.dll" })
                                       .Concat(new Assembly[] { executingAssembly }).ToArray();
                    builder
                     .RegisterAssemblyTypes(assemblies)
                     .Where(t => t.Name.EndsWith("Service"))
                     .AsImplementedInterfaces()
                     .InstancePerLifetimeScope();

                    builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

                    builder
                   .RegisterControllers(executingAssembly)
                   .InstancePerRequest();
                    container = builder.Build();
                    DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacConfig.Container));
                   
                }
            }
        }

        /// <summary>
        /// 获取容器
        /// </summary>
        public static IContainer Container
        {
            get
            {
                return container;
            }
        }
    }
}