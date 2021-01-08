namespace QCEmpaque.Helpers
{
    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    //Esta clase es para ayudar a guadar el usuario y contrasena
    //Se agrega en esta carpeta Helpers si no existe se debe agregar
    public static class Settings
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        const string user = "User";
        const string username = "UserName";
        const string idUser = "IdUser";
        const string idPerfil = "idPerfil";
        const string idFinca = "";
        const string idEmpacadora = "";
        static readonly string stringDefault = string.Empty;
        static readonly int intDefault = 0;
        public static string User
        {
            get
            {
                return AppSettings.GetValueOrDefault(user, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(user, value);
            }
        }
        public static int IdUser
        {
            get
            {
                return AppSettings.GetValueOrDefault(idUser, intDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idUser, value);
            }
        }
        public static int IdPerfil
        {
            get
            {
                return AppSettings.GetValueOrDefault(idPerfil, intDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idPerfil, value);
            }
        }
        public static int IdFinca
        {
            get
            {
                return AppSettings.GetValueOrDefault(idFinca, intDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idFinca, value);
            }
        }
        public static int IdEmpacadora
        {
            get
            {
                return AppSettings.GetValueOrDefault(idEmpacadora, intDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(idEmpacadora, value);
            }
        }
        public static string UserName
        {
            get
            {
                return AppSettings.GetValueOrDefault(username, stringDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(username, value);
            }
        }
    }
}
