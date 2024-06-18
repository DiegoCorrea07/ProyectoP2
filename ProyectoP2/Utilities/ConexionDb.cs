using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoP2.Utilities
{
    public static class ConexionDb
    {
        public static string ObtenerConnectionString()
        {
            return "Server=DIEGOC\\SQLEXPRESS02;Database=VentasData;User ID=diego;Password=@DiegoC2024.;MultipleActiveResultSets=true;Encrypt=true;TrustServerCertificate=true;";
        }
    }
}
