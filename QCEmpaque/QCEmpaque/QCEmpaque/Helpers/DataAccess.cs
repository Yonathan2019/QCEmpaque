namespace QCEmpaque.Helpers
{
    using QCEmpaque.ModelsLocales;
    using Interfaces;
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xamarin.Forms;
    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            this.connection = new SQLiteConnection(
            // config.Platform,
            Path.Combine(config.DirectoryDB, "qcemp.db3"));
            connection.CreateTable<UserLocal>();
            connection.CreateTable<MenuLocal>();           
        }

        public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }
        public void InsertAll<T>(List<T> model)
        {
            this.connection.InsertAll(model);
        }
        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }
        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }
        public void DeleteTrum<T>()
        {
            var mapping = connection.GetMapping<T>();
            this.connection.DeleteAll(mapping);
        }
        public T First<T>(bool WithChildren) where T : new()
        {
            return connection.Table<T>().FirstOrDefault();
        }
        public T Last<T>(bool WithChildren) where T : new()
        {
            return connection.Table<T>().LastOrDefault();
        }
        public List<T> GetList<T>() where T : new()
        {
            var mapping = connection.GetMapping<T>();
            var result = connection.Query<T>(string.Format("Select * from {0}", mapping.TableName));
            return result;
        }
        public T Find<T>(int pk) where T : new()
        {
            var mapping = connection.GetMapping<T>();
            var result = connection.Query<T>(string.Format("Select * from {0}", mapping.TableName));
            return result.FirstOrDefault(m => m.GetHashCode() == pk);
        }
        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
