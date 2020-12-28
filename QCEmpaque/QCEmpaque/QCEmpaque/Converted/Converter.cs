namespace QCEmpaque.Converted
{
    using Models;
    using ModelsLocales;
    using ViewModels;
    public static class Converter
    {
        public static UserLocal ToUserLocal(Usuario user)
        {
            var main = MainViewModel.GetInstance();

            if (main.IdEmpacadora == 0)
            {
                return new UserLocal
                {
                    IdUsuario = user.IdUsuario,
                    IdPerfil = user.IdPerfil,
                    NombreUsuario = user.NombreUsuario,
                    IdEmpacadora = user.IdEmpacadora,
                    Finca = user.Finca,
                    IdFinca = user.IdEmpacadora,
                    CodeEmpleado = user.CodeEmpleado
                };
            }
            else
            {
                return new UserLocal
                {
                    IdUsuario = user.IdUsuario,
                    IdPerfil = user.IdPerfil,
                    NombreUsuario = user.NombreUsuario,
                    IdFinca = main.IdFinca,
                    IdEmpacadora = main.IdEmpacadora,
                    Finca = user.Finca,
                    CodeEmpleado = user.CodeEmpleado

                };
            }

        }

    }
}
