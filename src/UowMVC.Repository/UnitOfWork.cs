using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDbSet<T> Set<T>() where T : class;
        void Commit();

        DefaultDataContext Context { get; }
    }
    [Serializable]
    public class EntityValidationException : Exception
    {
        public EntityValidationException(string message)
            : base(message)
        {

        }
        public Dictionary<string, string> ValidationErrors { get; set; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private DefaultDataContext _context;
        public DefaultDataContext Context
        {
            get { return _context; }
        }
        public UnitOfWork(DefaultDataContext context)
        {
            _context = context;
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var errors = dbEx.EntityValidationErrors.SelectMany(x => x.ValidationErrors).GroupBy(x => x.PropertyName).ToDictionary(x => x.Key, x => string.Join(", ", x.Select(e => e.ErrorMessage)));
                throw new EntityValidationException("Properties : " + string.Join(",", errors.Keys.Select(x => x + " " + errors[x])))
                {
                    ValidationErrors = errors,
                };
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    if (_context != null)
                    {
                        _context.Dispose();
                        _context = null;
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~UnitOfWork() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }

    public static class UnitOfWorkExtensions
    {
        public static void Remove<T>(this IUnitOfWork uow, IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
            {
                uow.Set<T>().Remove(item);
            }
        }

        public static void Remove<T>(this IUnitOfWork uow, Expression<Func<T, bool>> selector) where T : class
        {
            foreach (var item in uow.Set<T>().Where(selector).ToList())
            {
                uow.Set<T>().Remove(item);
            }
        }

        public static void AddRange<T>(this IDbSet<T> dbSet, params T[] items) where T : class
        {
            foreach (var item in items)
            {
                dbSet.Add(item);
            }
        }
    }
}

