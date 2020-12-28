namespace QCEmpaque.iOS
{
    using QCEmpaque.Interfaces;
    using SQLite.Net.Interop;
    using SQLite.Net.Platform.XamarinIOS;
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
                    platform = new SQLitePlatformIOS();
                }

                return platform;

            }
        }
    }
}