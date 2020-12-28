using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QCEmpaque.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();

            //Creamos estas propiedades para navegar
            App.Navigator = Navigator;

            //creamos esta propiedad en el App.cs para poder ocultar el menu
            App.Master = this;
        }
    }
}