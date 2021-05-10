using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A889258.Actividad03
{
    static class PlanDeCuentas
    {
        private static Dictionary<int, Cuenta> entradas;
        private static Dictionary<int, Cuenta> entradasMayor;
        static string nombreArchivo = "PlanDeCuentas.txt";
        static string nombreArchivoMayor = "Mayor.txt";

        public static void Agregar(Cuenta cuenta)
        {
            entradas.Add(cuenta.Codigo, cuenta);
            Grabar();
        }

        public static Cuenta SeleccionarCuenta()
        {
            var busqueda = Cuenta.CrearModeloBusqueda();

            foreach (var cuenta in entradas.Values)
            {
                if (cuenta.CoincideCon(busqueda))
                {
                    return cuenta;
                }
            }

            Console.WriteLine("No se ha encontrado una cuenta que coincida con los criterios indicados.");
            return null;
        }

        public static void Baja(Cuenta cuenta)
        {
            if (ExisteEnTxtMayor(cuenta.Codigo))
            {
                Console.WriteLine("No es posible eliminar la cuenta solicitada debido a que se encuentra en el Mayor.txt");
            }
            else
            {
                entradas.Remove(cuenta.Codigo);
                Grabar();
            }
        }

        public static bool Existe(int codigo)
        {
            return entradas.ContainsKey(codigo);
        }
        public static bool ExisteEnTxtMayor(int codigo)
        {
            return entradasMayor.ContainsKey(codigo);
        }

        static PlanDeCuentas()
        {
            entradas = new Dictionary<int, Cuenta>();
            entradasMayor = new Dictionary<int, Cuenta>();

            if (File.Exists(nombreArchivo))
            {
                using (var reader = new StreamReader(nombreArchivo))
                {
                    while (!reader.EndOfStream)
                    { 
                        var linea = reader.ReadLine();
                        var cuenta = new Cuenta(linea);

                        //if (cuenta.Codigo != -1)
                        //{
                            entradas.Add(cuenta.Codigo, cuenta);
                        //}

                    }
                }
            }
            if (File.Exists(nombreArchivoMayor))
            {
                using (var reader = new StreamReader(nombreArchivoMayor))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea2 = reader.ReadLine();
                        var datos = linea2.Split('|');
                        var cuenta2 = new Cuenta(linea2);

                        if (!int.TryParse(datos[0], out var codigo))
                        {
                            codigo = -1;
                        }
                        if (codigo != -1)
                        {
                            var cuenta = new Cuenta(codigo);
                            entradasMayor.Add(cuenta2.Codigo, cuenta2);
                        }

                    }
                }
            }
        }

        public static void Grabar()
        {
            using (var writer = new StreamWriter(nombreArchivo, append: false))
            {

                foreach (var cuenta in entradas.Values)
                {
                    var linea = cuenta.ObtenerLineaDatos();
                    writer.WriteLine(linea);
                }
            }
        }
    }
}
