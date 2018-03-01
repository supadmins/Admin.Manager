using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Manager.App_Start
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => new GroupCustomerRepository()).As<IGroupCustomerRepository>().InstancePerLifetimeScope();
            //builder.Register(c => new JXC_MaterialTypeRepository()).As<IJXC_MaterialTypeRepository>().InstancePerLifetimeScope();
            //builder.Register(c => new JXC_MaterialRepository()).As<IJXC_MaterialRepository>().InstancePerLifetimeScope();
            //builder.Register(c => new JXC_ShopCartRepository()).As<IJXC_ShopCartRepository>().InstancePerLifetimeScope();
            //builder.Register(c => new JXC_ShopRepository()).As<IJXC_ShopRepository>().InstancePerLifetimeScope();
            //builder.Register(c => new JXC_CustomerLevelPriceRepository()).As<IJXC_CustomerLevelPriceRepository>().InstancePerLifetimeScope();
            //builder.Register(c => new JXC_CustomerLevelRepository()).As<IJXC_CustomerLevelRepository>().InstancePerLifetimeScope();
            //builder.Register(c => new JXC_OrderBillRepository()).As<IJXC_OrderBillRepository>().InstancePerLifetimeScope();
            //builder.Register(c => new JXC_OrderBillDetailRepository()).As<IJXC_OrderBillDetailRepository>().InstancePerLifetimeScope();

            //builder.Register(c => new SendSmsRepository()).As<ISendSmsRepository>().InstancePerLifetimeScope();


        }
    }
}