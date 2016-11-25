using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using WebRole1.Models;
using WebRole1.Resolver;

namespace WebRole1
{
    /// <summary>
    /// Web Config class
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers the specified configuration.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            var dbContext = new StoreDBContext();
            container.RegisterType<IProductsReviewsRepository, ProductsReviewsRepository>(new HierarchicalLifetimeManager(), new InjectionConstructor(dbContext));
            container.RegisterType<IProductsRepository, ProductsRepository>(new HierarchicalLifetimeManager(), new InjectionConstructor(dbContext));
            config.DependencyResolver = new UnityResolver(container);

            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}
