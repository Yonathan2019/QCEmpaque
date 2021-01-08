namespace QCEmpaque.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using QCEmpaque.Services;
    using System.Windows.Input;
    using Xamarin.Forms;
    using Interfaces;
    using System.Collections.Generic;
    using QCEmpaque.Models.DM;
    using System;
    using QCEmpaque.ModelsLocales;
    using QCEmpaque.Models;
    using QCEmpaque.Helpers;
    using System.Linq;
    using System.Collections.ObjectModel;

    public class VidaAnaquelViewModel : BaseViewModel
    {
        #region Services
        private ApiService apiService;
        private DataService dataService;
        #endregion

        #region Atributos
        public string _codigo;
        public List<Estante> _listEstante;
        public Estante _selectEstante;
        public DateTime _fechaEmpaque = DateTime.Now;
        public DateTime _fechaAlmacenamiento = DateTime.Now;
        public string _guid;
        public bool _isEnable;
        public ObservableCollection<EncabezadoDetalleGenerico> _registros;
        public EncabezadoDetalleGenerico _selectRegistro;
        #endregion

        #region Propiedades
        public string Codigo
        {
            get { return this._codigo; }
            set
            {
                SetValue(ref this._codigo, value);
            }
        }
        public string Guid_
        {
            get { return this._guid; }
            set
            {
                SetValue(ref this._guid, value);
            }
        }
        public bool IsEnabled
        {
            get { return this._isEnable; }
            set
            {
                SetValue(ref this._isEnable, value);
            }
        }
        public DateTime FechaEmpaque
        {
            get { return this._fechaEmpaque; }
            set
            {
                SetValue(ref this._fechaEmpaque, value);
                if (this.FechaEmpaque != null)
                {
                    this.Select();
                }
            }
        }
        public DateTime FechaAlmacenamiento
        {
            get { return this._fechaAlmacenamiento; }
            set
            {
                SetValue(ref this._fechaAlmacenamiento, value);
            }
        }
        public List<Estante> ListEstante
        {
            get { return this._listEstante; }
            set
            {
                SetValue(ref this._listEstante, value);
            }
        }
        public Estante SelectEstante
        {
            get { return this._selectEstante; }
            set
            {
                if (this._selectEstante != value)
                {
                    SetValue(ref this._selectEstante, value);
                }
            }
        }
        public ObservableCollection<EncabezadoDetalleGenerico> Registros
        {
            get { return this._registros; }
            set { SetValue(ref this._registros, value); }
        }
        public EncabezadoDetalleGenerico SelectRegistro
        {
            get { return this._selectRegistro; }
            set
            {
                SetValue(ref this._selectRegistro, value);
            }
        }
        #endregion

        #region Constructor

        public VidaAnaquelViewModel()
        {
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.ListEstante = new List<Estante>();
            this.LlenarStantes();
            this.Registros = new ObservableCollection<EncabezadoDetalleGenerico>();
            Select();
        }

        #endregion

        #region MEtodos

        public async void ScannerCodigo()
        {
            try
            {
                this.IsEnabled = false;
                var scannear = DependencyService.Get<IQrScanningService>();
                var result = await scannear.ScanAsync();

                if (!string.IsNullOrEmpty(result))
                {
                    this.Codigo = string.Empty;
                    this.Codigo = result;               
                }
                this.IsEnabled = true;
            }
            catch (System.Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                                                        "Error",
                                                        ex.Message.ToString(),
                                                        "Aceptar");
                this.IsEnabled = false;
                return;
            }
        }
        public void LlenarStantes()
        {            
            this.ListEstante = Tools.GetInstance.GetEstanteByEmpacadora();
        }
        public async void Add()
        {
            try
            {
                this.IsEnabled = false;

                if (this.FechaEmpaque > DateTime.Now)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Validacion",
                        "La fecha de empaque no puede ser mayor que hoy",
                        "Aceptar");
                    this.IsEnabled = true;
                    return;
                }
                if (this.FechaAlmacenamiento > DateTime.Now)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Validacion",
                        "La fecha de almacenamiento no puede ser mayor que hoy",
                        "Aceptar");
                    this.IsEnabled = true;
                    return;
                }
                if (SelectEstante == null)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Validacion",
                        "Debe seleccionar un estante",
                        "Aceptar");
                    this.IsEnabled = true;
                    return;
                }

                if (string.IsNullOrEmpty(this.Codigo))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Validacion",
                        "Debe ingresar un codigo Bach",
                        "Aceptar");
                    this.IsEnabled = true;
                    return;
                }

                var existe = Tools.GetInstance.GetIndicadorByBach(this.Codigo);

                if (existe == null)
                {
                    Guid_ = Guid.NewGuid().ToString();
                    var user = dataService.First<UserLocal>(false);                    
                    EncabezadoIndicador encabezado = new EncabezadoIndicador()
                    {
                        Guid = Guid_,
                        FK_Proceso = Indicadores.VidaAnaquel,
                        Fecha = this.FechaEmpaque,
                        IdVariedad = -1,
                        IdCiclo = -1,
                        IdUT = -1,
                        IdCaporal = -1,
                        IdJornal1 = -1,
                        IdJornal2 = -1,
                        Observaciones = string.Empty,
                        Usuario = user.NombreUsuario,
                        IdEvaluador = int.Parse(user.CodeEmpleado),
                        FechaSiembra = this.FechaAlmacenamiento,
                        IdValvula = this.SelectEstante.Id,
                        Bach = this.Codigo,
                        FechaHoraCreo = DateTime.Now
                    };

                    var id = this.dataService.Insert(encabezado).GetHashCode();
                    this.SelectEstante = null;
                    this.Codigo = string.Empty;
                    this.IsEnabled = true;
                }
                else
                {
                    var confirm = await Application.Current.MainPage.DisplayAlert(
                                      "Existe", "Esta paleta ya exite, desea Actualizar sus datos?", "Yes", "No");
                    if (confirm)
                    {
                        EncabezadoIndicador encabezado = new EncabezadoIndicador()
                        {
                            Id = existe.Id,
                            Guid = existe.Guid,
                            FK_Proceso = Indicadores.VidaAnaquel,
                            Fecha = this.FechaEmpaque,
                            IdVariedad = -1,
                            IdCiclo = -1,
                            IdUT = -1,
                            IdCaporal = -1,
                            IdJornal1 = -1,
                            IdJornal2 = -1,
                            Observaciones = string.Empty,
                            Usuario = existe.Usuario,
                            IdEvaluador = existe.IdEvaluador,
                            FechaSiembra = this.FechaAlmacenamiento,
                            IdValvula = this.SelectEstante.Id,
                            Bach = existe.Bach,
                            FechaHoraCreo = DateTime.Now
                        };

                        this.dataService.Update(encabezado);
                        this.IsEnabled = true;
                        this.SelectEstante = null;
                        this.Codigo = string.Empty;
                    }
                }

                Select();


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                                                        "Error",
                                                        ex.Message.ToString(),
                                                        "Aceptar");
                this.IsEnabled = true;
                return;
            }
        }
        private async void Select()
        {
            try
            {
                if (FechaAlmacenamiento != null && this.FechaEmpaque != null)
                {                    
                    var data = dataService.Get<EncabezadoIndicador>(false).Where(i => i.FK_Proceso == Indicadores.VidaAnaquel);
                    if (data.Count().Equals(0)) { this.Registros.Clear(); return; }
                    
                    var h = data;
                    var stantes = dataService.Get<Estante>(false);

                    if (FechaAlmacenamiento != null && !h.Count().Equals(0))
                    {
                        var query = from est in stantes.AsEnumerable()
                                    join dataencabezado in h.AsEnumerable()   
                                    on est.Id equals dataencabezado.IdValvula
                                    select new EncabezadoDetalleGenerico
                                    {
                                        Id = dataencabezado == null ? -1 : dataencabezado.Id,
                                        Guid = dataencabezado.Guid,
                                        Variable1 = dataencabezado.Bach,                                        
                                        Variable2 = est.Nombre,
                                        Fecha = dataencabezado.FechaHoraCreo
                                    } into ord
                                    orderby ord.Id descending
                                    select ord;

                        int count = 0;
                        this.Registros.Clear();

                        if (!query.AsEnumerable().Any())
                        {
                            foreach (var item in query.AsEnumerable())
                            {
                                count = count + 1;
                                this.Registros.Add(new EncabezadoDetalleGenerico
                                {
                                    Fila = count,
                                    Id = item.Id,
                                    Guid = item.Guid,
                                    Variable1 = item.Variable1,
                                    Variable2 = item.Variable2,
                                    Fecha = item.Fecha                                    
                                });

                            }
                        }

                        //this.Muestras = Registros.Count;
                        //this.Cumpl = Tools.GetTools.DamePorcentaje((Registros.AsEnumerable().Sum(row => row.Calculo1) / this.Registros.Count), 1);
                    }

                }

                
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        ex.Message.ToString(),
                        "Aceptar");
                
                return;
            }
        }

        #endregion

        #region Commandos
        public ICommand LeerCommand
        {
            get { return new RelayCommand(ScannerCodigo); }
        }

        public ICommand AddCommand
        {
            get { return new RelayCommand(Add); }
        }

        #endregion
    }
}
