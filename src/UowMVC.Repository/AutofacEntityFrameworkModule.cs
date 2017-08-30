using UowMVC.SDK;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Repository
{
    public class AutofacEntityFrameworkModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(db =>
            {
                var ctx = new DefaultDataContext();
                return ctx;
            })
            .InstancePerLifetimeScope()
            .AsSelf()
            .OnActivated(x =>
            {

            })
            .OnRelease(db =>
            {
                //if (db.ChangeTracker.HasChanges() == false)
                //{
                //    db.Dispose();
                //    return;
                //}
                //var entitiesErrors = db.GetValidationErrors().ToList();
                //if (entitiesErrors.Count > 0)
                //{
                //    //TODO logged
                //    //foreach (var e in entitiesErrors)
                //    //{
                //    //    LogWriter.Default.WriteError(e);
                //    //}
                //    db.Dispose();
                //}
                //else
                //{
                //    db.SaveChanges();
                //}
            });
        }
    }
}
