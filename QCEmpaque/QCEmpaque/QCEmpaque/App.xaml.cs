using QCEmpaque.Helpers;
using QCEmpaque.ModelsLocales;
using QCEmpaque.Services;
using QCEmpaque.ViewModels;
using QCEmpaque.Views;
using Xamarin.Forms;

namespace QCEmpaque
{
    public partial class App : Application
    {
        #region Propiedades

        public static NavigationPage Navigator
        {
            get;
            internal set;
        }
        public static MasterPage Master
        {
            get;
            internal set;
        }

        #endregion
        public App()
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(Settings.User) && Settings.IdUser > 0)
            {
                var dataServives = new DataService(); //Revisamos la data grabada
                var user = dataServives.First<UserLocal>(false); //obtenemos el usuario

                var mainViewModel = MainViewModel.GetInstance();
                var encryp = new Encryptor();
                mainViewModel.IdUser = Settings.IdUser;
                mainViewModel.User = Encryptor.Encriptar(Settings.User, "mko10ag0rti2o0ax");
                mainViewModel.UserName = Settings.UserName;
                mainViewModel.IdPerfil = Settings.IdPerfil;
                //mainViewModel.IdFinca = Settings.IdFinca;
                mainViewModel.UserLocal = user;
                mainViewModel.LoadMenu();
                MainViewModel.GetInstance().Home = new HomeViewModel();
                //MainPage = new NavigationPage(new NavierasPage());
                MainPage = new MasterPage();
            }
            else
            {
                MainPage = new LoginPage();
            }
        }
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
