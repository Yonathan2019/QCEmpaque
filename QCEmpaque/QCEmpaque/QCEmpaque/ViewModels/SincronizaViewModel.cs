namespace QCEmpaque.ViewModels
{
    using Acr.UserDialogs;
    using GalaSoft.MvvmLight.Command;
    using QCEmpaque.Helpers;
    using QCEmpaque.Models.DM;
    using QCEmpaque.ModelsLocales;
    using QCEmpaque.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class SincronizaViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Constructor

        public SincronizaViewModel()
        {
            apiService = new ApiService();
            dataService = new DataService();
            IsRunning = false;
            Procesos = new ObservableCollection<ProcesoEstado>();
        }

        #endregion

        #region Atributos        

        public bool isEnable;
        public bool isRunning;
        public bool isRefreshin;
        public ObservableCollection<ProcesoEstado> _procesos;

        #endregion

        #region Propiedades
        public bool IsEnable
        {
            get { return this.isEnable; }
            set { SetValue(ref this.isEnable, value); }
        }
        public bool IsRunning
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }
        public ObservableCollection<ProcesoEstado> Procesos
        {
            get { return this._procesos; }
            set { SetValue(ref this._procesos, value); }
        }
        public bool IsRefreshin
        {
            get { return this.isRefreshin; }
            set { SetValue(ref this.isRefreshin, value); }
        }

        #endregion

        #region Metodo para descarga de datos
        private async void GetEstante()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Descargando");
                this.IsEnable = false;
                var connection = await apiService.CheckConnection();
                if (!connection.IsSuccess)
                {
                    this.IsEnable = true;
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Error Get Estante" + connection.Message,
                        "Aceptar");
                    this.IsRunning = false;
                    UserDialogs.Instance.HideLoading();
                    return;
                }

                var controller = "api/dm/GetEstantesEmpacadora";
                var response = await this.apiService.MyGetList<Estante>(UrlConexion.UrlConx, controller);
                List<Estante> empleados = (List<Estante>)response.Result;

                if (empleados.Count.Equals(0))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        "Problemas para Descargar datos de Estantes",
                        "Aceptar");
                    this.IsEnable = true;
                    UserDialogs.Instance.HideLoading();
                    return;
                }
                //Delete and update tabla local
                this.dataService.DeleteAllTable<Estante>();
                this.dataService.SaveBulk(empleados);
                this.IsEnable = true;
                UserDialogs.Instance.HideLoading();

                this.AddProceso("Descarga de Estantes", "Finalizado");
                //this.Proceso5 = " Finalizada";
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Problemas para Descargar datos de Estantes",
                        ex.Message.ToString(),
                        "Aceptar");
                this.isRunning = false;
                this.IsEnable = true;
                UserDialogs.Instance.HideLoading();
                return;
            }

        }

        #endregion

        #region Metodo
        private void AddProceso(string proceso, string pstado)
        {
            this.Procesos.Add(new ProcesoEstado
            {
                Proceso = proceso,
                Estado = pstado
            });
        }

        /// <summary>
        /// Llama a todos los eventos para la descarga de los datos maestros
        /// </summary>
        private async void DescargarDatos()
        {
            try
            {
                this.Procesos.Clear();
                this.GetEstante();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error de Descargar datos",
                        ex.Message.ToString(),
                        "Aceptar");
                return;
            }

        }

        /// <summary>
        /// Publica todos los datos en la db productiva
        /// </summary>
        private void PublicaInformacion()
        {
            this.Procesos.Clear();            
        }
        #endregion

        #region Comandos
        public ICommand DescargarCommand
        {
            get { return new RelayCommand(DescargarDatos); }
        }
        public ICommand PublicaCommand
        {
            get { return new RelayCommand(PublicaInformacion); }
        }
        #endregion  
    }
}
