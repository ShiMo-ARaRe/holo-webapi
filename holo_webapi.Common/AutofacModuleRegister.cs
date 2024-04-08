using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace holo_webapi.Common
{
    public class AutofacModuleRegister : Autofac.Module
        //继承自Autofac.Module的类AutofacModuleRegister，它用于在Autofac容器中注册依赖项注入（Dependency Injection）。
    {
        //重写Autofac管道Load方法，该方法是Autofac管道中的一个钩子方法，在容器构建时会被调用。
        //在这里注册注入
        protected override void Load(ContainerBuilder builder)
            //通过ContainerBuilder对象builder来注册我们的依赖项
        {
            //程序集注入业务服务
            var IAppServices = Assembly.Load("holo_webapi.Service"); // 包含接口的程序集
            var AppServices = Assembly.Load("holo_webapi.Service");  // 包含实现类的程序集

            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(IAppServices, AppServices)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces();
            /* 通过调用builder.RegisterAssemblyTypes方法，将IAppServices和AppServices两个程序集中符合条件的类型进行注册。
               .Where方法用于筛选出满足特定条件的类型，这里的条件是类型名称以"Service"结尾。
               .AsImplementedInterfaces方法表示将这些类型以它们实现的接口作为服务接口进行注册。*/
        }
    }

}
