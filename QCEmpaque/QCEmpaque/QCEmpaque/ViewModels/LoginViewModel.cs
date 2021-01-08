namespace QCEmpaque.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Helpers;
    using Models;
    using Models.DM;
    using ModelsLocales;
    using QCEmpaque.Converted;
    using Services;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Views;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        #region Services

        private ApiService apiService;
        private DataService dataService;
        #endregion
        #region Atributos que se usaran en el formulairio o variables
        //Los atributos se usaran con minusculas

        private string user;
        private string password;
        private bool isEnable;
        private bool isRunning;
        private bool isRemembered;
        private Usuario propertiesuser;
        private List<MenuLocal> menus;
        public List<Empacadora> _empacadoraList;
        public Empacadora _selectEmpacadora;
        #endregion

        #region Propiedades que le pasaran a las atributos o varibles
        //Las propiedad se usaran con letras mayusculas

        public string User //capturamos valor a usuario
        {
            get { return this.user; }
            set { SetValue(ref user, value); }
        }
        public string Password
        {
            get { return this.password; }
            set { SetValue(ref password, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref isRunning, value); }
        }
        public bool IsRemembered
        {
            get { return this.isRemembered; }
            set { SetValue(ref isRemembered, value); }
        }
        public bool IsEnable
        {
            get { return this.isEnable; }
            set { SetValue(ref isEnable, value); }
        }
        public Empacadora SelectedEmpacadora
        {
            get { return this._selectEmpacadora; }
            set
            {
                if (_selectEmpacadora != value)
                {
                    SetValue(ref this._selectEmpacadora, value);
                }
            }
        }
        public List<Empacadora> ListaEmpacadora
        {
            get { return this._empacadoraList; }
            set { SetValue(ref _empacadoraList, value); }
        }

        #endregion

        #region Contrustor de Modelo
        public LoginViewModel() //Ejecuta Load en el LoginPage
        {
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.ListaEmpacadora = new List<Empacadora>();
            this.IsRemembered = true;
            this.IsEnable = true;
            this.LlenarEmpacadoras();
        }
        #endregion

        #region Comandas o ejecutar eventos de los controles
        public ICommand LoginCommand
        {
            get { return new RelayCommand(Login); }
        }

        #endregion

        #region Metodo o fuciones que se ejecutaran

        private async void Login() //Desclaramos un metodo Asincrono
        {
            if (string.IsNullOrEmpty(this.User))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresar el Usuario",
                    "Aceptar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingresar la contraseña",
                    "Aceptar");
                return;
            }

            if (this.SelectedEmpacadora == null && this.SelectedEmpacadora.Id > -1)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Seleccione una Empacadora",
                    "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnable = false;

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                return;
            }
            
            var urlbase = "api/User/GetUsuario";
            var login = new Login()
            {
                user = User,
                pass = Encryptor.EncriptarPass(Password),
                modulo = "12"
            };

            var token = await this.apiService.Login<Usuario>("http://65.52.32.252/sinaapi/", urlbase, login);
            this.propertiesuser = (Usuario)token.Result;

            if (propertiesuser.IdUsuario.Equals(0))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Usuario o contraseña incorrectos",
                    "Aceptar");

                this.IsRunning = false;
                this.IsEnable = true;
                this.Password = string.Empty;
                return;
            }

            //Guardamos contrasemas datos credenciales para usar el guardar mis credenciasles
            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.User = propertiesuser.NombreUsuario;
            mainViewModel.UserName = propertiesuser.NombreUsuario;
            mainViewModel.IdUser = propertiesuser.IdUsuario;
            mainViewModel.IdPerfil = propertiesuser.IdPerfil;
            mainViewModel.IdEmpacadora = SelectedEmpacadora.Id;

            var userlocal = Converter.ToUserLocal(propertiesuser);
            mainViewModel.UserLocal = userlocal;
            var urlbasemenu = "api/User/GetMenuPerfil/?FK_Perfil=" + propertiesuser.IdPerfil + "&FK_Modulo=12";
            var result = await this.apiService.MyGetList<MenuLocal>("http://65.52.32.252/sinaapi/", urlbasemenu);
            this.menus = (List<MenuLocal>)result.Result;

            if (IsRemembered)
            {
                //Verificamos que el usuarios haya aceptado registrarse con guardar contrasenas y usuario
                Settings.User = Encryptor.Encriptar(propertiesuser.NombreUsuario, "mko10ag0rti2o0ax");
                Settings.IdUser = propertiesuser.IdUsuario;
                Settings.IdPerfil = propertiesuser.IdPerfil;
                Settings.UserName = propertiesuser.NombreUsuario;
                Settings.IdEmpacadora = SelectedEmpacadora.Id;
                this.dataService.DeleteAllAndInsert(userlocal);
                
            }

            this.dataService.DeleteAllTable<MenuLocal>();
            this.dataService.SaveBulk(this.menus);
            mainViewModel.LoadMenu();

            this.IsRunning = false;
            this.IsEnable = true;

            this.User = string.Empty;
            this.Password = string.Empty;


            MainViewModel.GetInstance().Home = new HomeViewModel(); //Instacia del Singleton --- Garantizamos para un solo uso de MainViewModel
            //await Application.Current.MainPage.Navigation.PushAsync(new NavierasPage()); //Redireccionamos a la homepage
            App.Current.MainPage = new MasterPage();
        }
        private async void LlenarEmpacadoras()
        {
            try
            {
                this.IsRunning = true;
                var connection = await apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
                    this.IsRunning = false;
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "No tiene acceso a internet, verfica tu coneccion",
                        "Aceptar");

                    var empa = new Empacadora()
                    {
                        Id = -1,
                        Nombre = "No hay Empacadora"
                    };
                    this.ListaEmpacadora.Add(empa);
                    this.IsRunning = false;
                    return;
                }

                var urlbase = "api/dm/GetEmpacadoras";
                var token = await this.apiService.MyGetList<Empacadora>(UrlConexion.UrlConx, urlbase);
                this.ListaEmpacadora = (List<Empacadora>)token.Result;
                this.IsRunning = false;
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        ex.Message.ToString(),
                        "Aceptar"); ;
            }

        }

        #endregion
    }
}
