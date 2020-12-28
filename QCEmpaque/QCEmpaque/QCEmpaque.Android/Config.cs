[assembly: Xamarin.Forms.Dependency(typeof(QCEmpaque.Droid.Config))]
namespace QCEmpaque.Droid
{
    using QCEmpaque.Interfaces;
    using SQLite.Net.Interop;
    using SQLite.Net.Platform.XamarinAndroid;
    using System;

    public class Config : IConfig
    {
        private string directoryDB;
        private ISQLitePlatform platform;

        public string DirectoryDB
        {
            get
            {
                if (string.IsNullOrEmpty(directoryDB))
                {
                    directoryDB = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }

                return directoryDB;
            }
        }

        public ISQLitePlatform Platform
        {
            get
            {
                if (platform == null)
                {
                    platform = new SQLitePlatformAndroid();
                }

                return platform;

            }
        }
    }
}