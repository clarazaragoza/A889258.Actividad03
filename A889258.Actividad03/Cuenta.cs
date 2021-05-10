using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A889258.Actividad03
{
    public class Cuenta
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }

        public string TituloEntrada
        {
            get
            {
                return $"{Tipo} | {Nombre} | {Codigo}";
            }
        }

        public Cuenta() { }

        public Cuenta(string linea)
        {
            //FORMATO: CodigoCuenta, NombreCuenta, TipoC
            var datos = linea.Split('|');
            Codigo = int.Parse(datos[0]);
            Nombre = datos[1];
            Tipo = datos[2];
        }

        public Cuenta(int codigo)
        {
            Codigo = codigo;
        }


        public string ObtenerLineaDatos()
        {
            return $"{Codigo}|{Nombre}|{Tipo}";
        }


        public static Cuenta IngresarNueva()
        {
            var cuenta = new Cuenta();
            Console.WriteLine("Nueva cuenta");

            do
            {
                int codigocuenta = IngresoCodigo("Ingrese un código de cuenta");

                if (PlanDeCuentas.Existe(codigocuenta))
                {
                    Console.WriteLine("El número de cuenta ingresado ya existe en el Plan de cuentas.");
                    continue;
                }

                cuenta.Codigo = codigocuenta;

            } while (cuenta.Codigo == 0);

            cuenta.Tipo = ValidarTipoCuenta("Ingrese un tipo de cuenta. (A/P/PN)");

            cuenta.Nombre = IngresoNombre("Ingrese el Nombre de la Cuenta");

            return cuenta;

        }

        public void Modificar()
        {
            Console.WriteLine($"Nombre Cuenta: {Nombre} - S para modificar / cualquier tecla para seguir.");
            var tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.S)
            {
                this.Nombre = IngresoNombre("Ingrese el nuevo nombre de cuenta.");
            }

            Console.WriteLine($"Tipo de Cuenta: {Tipo} - S para modificar / cualquier tecla para seguir.");
            tecla = Console.ReadKey(true);
            if (tecla.Key == ConsoleKey.S)
            {
                this.Tipo = ValidarTipoCuenta("Ingrese el nuevo tipo de cuenta.");
            }
            PlanDeCuentas.Grabar();
        }

        public void Mostrar()
        {
            Console.WriteLine("\n" + $"Nombre de Cuenta: {Nombre}");
            Console.WriteLine($"Tipo: {Tipo}");
            Console.WriteLine($"Código: {Codigo}" + "\n");
        }

        // VALIDO CODIGO INGRESADO PARA EL MODELO DE BUSQUEDA
        private static int IngresoCodigo(string titulo, bool obligatorio = true)
        {
            if (!obligatorio)
            {
                titulo += " o presione [0] para continuar";
            }
            else
            {
                titulo += ": ";
            }

            do
            {
                var codigocuenta = ValidarNumero(0, 50, titulo);

                if (!obligatorio && codigocuenta == 0)
                {
                    return 0;
                }

                return codigocuenta;

            } while (true);

        }

        // VALIDO NOMBRE INGRESADO PARA EL MODELO DE BUSQUEDA
        private static string IngresoNombre(string titulo, bool obligatorio = true)
        {
            string ingreso;

            if (!obligatorio)
            {
                titulo += " o presione [Enter] para continuar";
            }
            else
            {
                titulo += ": ";
            }

            do
            {
                Console.WriteLine(titulo);
                ingreso = Console.ReadLine();

                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return null;
                }
                if (obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                if (ingreso.Any(Char.IsDigit))
                {
                    Console.WriteLine("El valor no debe contener números.");
                    continue;
                }
                break;
            } while (true);
            return ingreso;

        }

        // VALIDO TIPO INGRESADO PARA EL MODELO DE BUSQUEDA
        private static string IngresoTipo(string titulo, bool obligatorio = true)
        {
            string ingreso;

            if (!obligatorio)
            {
                titulo += " o presione [Enter] para continuar";
            }
            else
            {
                titulo += ": ";
            }

            do
            {
                Console.WriteLine(titulo);
                ingreso = Console.ReadLine();

                if (!obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    return null;
                }

                if (obligatorio && string.IsNullOrWhiteSpace(ingreso))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                if (ingreso.Any(Char.IsDigit))
                {
                    Console.WriteLine("El valor ingresado no debe contener números.");
                    continue;
                }
                if (ingreso != "P" && ingreso != "A" && ingreso != "PN")
                {
                    Console.WriteLine("Recuerda ingresar una opción de tipo de cuenta válida. (A/P/PN)");
                    continue;
                }
                break;
            } while (true);
            return ingreso;
        }


        public static Cuenta CrearModeloBusqueda()
        {
            var modelo = new Cuenta();
            bool ok = false;

            do
            {
                modelo.Codigo = IngresoCodigo("Ingrese el Código de Cuenta", obligatorio: false);

                modelo.Nombre = IngresoNombre("Ingrese el Nombre de la Cuenta", obligatorio: false);

                modelo.Tipo = IngresoTipo("Ingrese el Tipo de Cuenta (A/P/PN)", obligatorio: false);

                if (modelo.Codigo == 0 && modelo.Nombre == "" && modelo.Tipo == "")
                {
                    ok = false;
                    return null;
                }
                else
                {
                    ok = true;
                    return modelo;
                }

            } while (!ok);
        }


        public bool CoincideCon(Cuenta modelo)
        {
            if (modelo.Codigo != 0 && Codigo != modelo.Codigo)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(modelo.Nombre) && !Nombre.Equals(modelo.Nombre, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(modelo.Tipo) && !Tipo.Equals(modelo.Tipo, StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }

            return true;
        }


        //VALIDA NÚMEROS INGRESADOS: 
        public static int ValidarNumero(long min, long max, string msj)
        {
            int resultado;
            do
            {
                Console.WriteLine("\n" + msj);
                Console.WriteLine("Recuerde que debe ser un número entre " + min + " y " + max + ".");

                if (!int.TryParse(Console.ReadLine(), out resultado))
                {
                    Console.WriteLine("Ingrese un número, por favor.");
                    resultado = -1;
                }

            } while (resultado < min || resultado > max);

            return resultado;
        }

        //VALIDA TEXTOS INGRESADOS: 
        public static string ValidarTexto(string msj)
        {
            string resultado;
            do
            {
                Console.WriteLine("\n" + msj);
                resultado = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(resultado))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                if (resultado.Any(Char.IsDigit))
                {
                    Console.WriteLine("El valor ingresado no debe contener números.");
                    continue;
                }
                break;

            } while (resultado == "");

            return resultado;
        }

        public static string ValidarTipoCuenta(string msj)
        {
            string resultado;
            do
            {
                Console.WriteLine("\n" + msj);
                resultado = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(resultado))
                {
                    Console.WriteLine("Debe ingresar un valor.");
                    continue;
                }
                if (resultado.Any(Char.IsDigit))
                {
                    Console.WriteLine("El valor ingresado no debe contener números.");
                    continue;
                }
                if (resultado != "P" && resultado != "A" && resultado != "PN")
                {
                    Console.WriteLine("Recuerda ingresar una opción de tipo de cuenta válida. (A/P/PN)");
                    continue;
                }
                break;
            } while (true);
            return resultado;
        }

    }

}
