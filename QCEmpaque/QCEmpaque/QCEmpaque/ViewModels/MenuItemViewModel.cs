namespace QCEmpaque.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Views;
    using System.Windows.Input;

    public class MenuItemViewModel
    {
        public string Ico { get; set; }
        public string Title { get; set; }
        public string PageName { get; set; }

        #region Comandos

        public ICommand NavigateCommand
        {
            get
            {
                return new RelayCommand(Navigate);
            }
        }
        #endregion

        #region Metodos
        private void Navigate()
        {
            //Agregmos esto para esconder lo page actual
            App.Master.IsPresented = false;

            //if (this.PageName == "SincronizaDatosPage")
            //{
            //    MainViewModel.GetInstance().Sincronizacion = new SincronizacionDatosViewModel();
            //    App.Navigator.PushAsync(new SincronizaDatosPage());
            //}

            if (this.PageName.ToLower() == string.Format("VidaAnaquelPage").ToLower())
            {
                MainViewModel.GetInstance().VidaAnaquel = new VidaAnaquelViewModel();
                App.Navigator.PushAsync(new VidaAnaquelPage());
            }            
        }

        #endregion
    }
}
