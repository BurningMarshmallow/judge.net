﻿using System.Web;
using Judge.Application.Interfaces;
using Judge.Data;
using Microsoft.Owin.Security;
using Microsoft.Practices.Unity;

namespace Judge.Application
{
    public sealed class ApplicationExtension : UnityContainerExtension
    {
        private readonly string _connectionString;

        public ApplicationExtension(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Initialize()
        {
            Container.AddExtension(new DataContainerExtension<PerRequestLifetimeManager>(_connectionString));

            Container.RegisterType<IAuthenticationManager>(new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));
            Container.RegisterType<ISecurityService, SecurityService>(new PerRequestLifetimeManager());

            Container.RegisterType<IProblemsService, ProblemsService>(new PerRequestLifetimeManager());
            Container.RegisterType<ISubmitSolutionService, SubmitSolutionService>(new PerRequestLifetimeManager());
            Container.RegisterType<ISubmitQueueService, SubmitQueueService>(new PerRequestLifetimeManager());
            Container.RegisterType<IContestsService, ContestsService>(new PerRequestLifetimeManager());
        }
    }
}
