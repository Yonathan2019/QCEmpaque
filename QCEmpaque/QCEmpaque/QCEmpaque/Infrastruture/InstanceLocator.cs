namespace QCEmpaque.Infrastruture
{
    using ViewModels;
    public class InstanceLocator
    {
        #region Propiedades

        public MainViewModel Main { get; set; }

        #endregion

        #region Constructor

        public InstanceLocator()
        {
            Main = new MainViewModel();
        }

        #endregion
    }
}
