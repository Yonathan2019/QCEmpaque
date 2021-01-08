namespace QCEmpaque.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    //using Helps;
    using ModelsLocales;
    using Views;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;
    using QCEmpaque.Helpers;

    public class MainViewModel : BaseViewModel
    {
        #region Patron Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        #region Services
        private Services.ApiService apiService;
        private Services.DataService dataService;
        #endregion

        #region Propiedades
        private ObservableCollection<MenuItemViewModel> menuslist;
        public ObservableCollection<MenuItemViewModel> Menus
        {
            get { return this.menuslist; }
            set { SetValue(ref this.menuslist, value); }
        }

        public string User { get; set; }
        public string UserName { get; set; }
        public int IdUser { get; set; }
        public int IdPerfil { get; set; }
        public int IdEmpacadora { get; set; }
        public int IdFinca { get; set; }
        public bool conecte { get; set; }
        public UserLocal UserLocal { get; set; }
        public string Version { get; set; }

        #endregion

        #region Instancias a otras viewmodel
        public LoginViewModel Login { get; set; }
        public HomeViewModel Home { get; set; }
        public SincronizaViewModel Sincroniza { get; set; }
        public VidaAnaquelViewModel VidaAnaquel { get; set; }

        #endregion

        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.apiService = new Services.ApiService();
            this.dataService = new Services.DataService();
            this.Login = new LoginViewModel();
            this.Version = "© Agrolibano, " + Xamarin.Essentials.VersionTracking.CurrentVersion;
        }

        #endregion

        #region Metodos
        public void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            var menus = dataService.Get<MenuLocal>(false);
            //this.menus = menu.ToArray();
            if (!menus.Count.Equals(0))
            {
                for (int i = 0; i < menus.Count; i++)
                {
                    var icoimg = "ic_assignment";
                    if (menus[i].NameMenu.ToString().Length >= 7)
                    {
                        if (menus[i].NameMenu.ToString().Substring(0, 7) != "Reporte")
                        {
                            icoimg = "ic_edit";
                        }
                    }
                    else
                    {
                        icoimg = "ic_edit";
                    }

                    var name0 = menus[i].NameMenu.ToString().Replace(" y ", "");
                    var name1 = name0.ToString().Replace(" de ", "");
                    var name = name1.ToString().Replace(" ", "");

                    this.Menus.Add(new MenuItemViewModel
                    {
                        Ico = icoimg,
                        PageName = name + "Page",
                        Title = menus[i].NameMenu.ToString(),
                    });

                }

            }

            //this.Menus.Add(new MenuItemViewModel
            //{
            //    Ico = "ic_compare_arrows",
            //    PageName = "SincronizaDatosPage",
            //    Title = "Sincronizacion",
            //});
            //Agregamos los menus
            //this.Menus.Add(new MenuItemViewModel
            //{
            //    Ico = "ic_exit_to_app",
            //    PageName = "LoginPage",
            //    Title = "Cerrar Session",
            //});
        }
        private void ExitApp()
        {
            //if (this.PageName == "LoginPage")
            //{
            Settings.IdUser = 0;
            Settings.User = string.Empty;
            this.User = string.Empty;
            this.IdUser = 0;
            this.IdPerfil = 0;
            Application.Current.MainPage = new NavigationPage(new LoginPage());
            //}
        }
        private void IraSync()
        {
            App.Master.IsPresented = false;
            this.Sincroniza = new SincronizaViewModel();
            App.Navigator.PushAsync(new SincronizaDatosPage());
        }
        //ExitCommand
        public ICommand ExitCommand
        {
            get { return new RelayCommand(ExitApp); }
        }
        public ICommand IrCommand
        {
            get { return new RelayCommand(IraSync); }
        }

        #endregion
    }
}
