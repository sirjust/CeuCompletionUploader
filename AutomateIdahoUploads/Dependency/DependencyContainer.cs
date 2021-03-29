﻿using CommonCode.Data;
using CommonCode.StaticData;
using Ninject.Modules;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomateIdahoUploads.Dependency
{
    public class DependencyContainer : NinjectModule
    {
        public override void Load()
        {
            Bind<ILoginInfo>().To<LoginInfo>();
            Bind<IWebDriver>().To<ChromeDriver>();
            //Bind<IUploader>().To<Uploader>();
            //Bind<IErrorHelper>().To<ErrorHelper>();
            //Bind<ILogger>().To<Logger>();
            //Bind<IEmailHelper>().To<EmailHelper>();
            //Bind<IValidationHelper>().To<ValidationHelper>();
            Bind<ICompletionRepository>().To<CompletionRepository>();
        }
    }
}