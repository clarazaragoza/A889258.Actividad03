using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A889258.Actividad03
{
    /**************************************************             
    *            Nombre: Clara Zaragoza              *     
    *            Registro: 889258                    *     
    *            Actividad N.03                      *     
    **************************************************/

    class Program : Cuenta
    {
        static void Main(string[] args)
        {
            Cuenta cuenta = new Cuenta();

            SaludarSistema();

            bool menufunciona = true;
            do
            {
                //MENÚ DE OPCIONES: 
                Console.Write("\n" + "***********************************************" + "\n" + "Menú Principal:");
                Console.Write("\n" + "1  - Alta de cuenta");
                Console.Write("\n" + "2  - Modificación de cuenta");
                Console.Write("\n" + "3  - Baja de cuenta");
                Console.Write("\n" + "4  - Buscar cuenta");
                Console.Write("\n" + "0  - Salir del Sistema." + "\n");

                Console.Write("\n" + "Ingrese la opción deseada.");
                Continuar("ingresarla");

                long menuppal = Cuenta.ValidarNumero(0, 4, "Ingrese una opción válida.");

                switch (menuppal)
                {
                    case 1:
                        AltaCuenta("1 - Alta:");
                        break;

                    case 2:
                        ModificarCuenta("2 - Modificar Cuenta:");
                        break;

                    case 3:
                        BajaCuenta("3 - Baja Cuenta:");
                        break;

                    case 4:
                        Buscar("4 - Buscar Cuenta:");
                        break;

                    case 0:
                        SalidaPrograma("0 - Salida del Sistema:");
                        menufunciona = false;
                        break;
                }

            } while (menufunciona);
        }


        private static void Buscar(string msjOpcion)
        {
            //BUSCAR CUENTA:
            Console.WriteLine("\n" + msjOpcion);
            var cuenta = PlanDeCuentas.SeleccionarCuenta();

            if (cuenta == null)
            {
                Continuar("volver al Menú Principal.");
            }
            else
            {
                cuenta.Mostrar();
                Continuar("volver al Menú Principal.");
            }

        }

        private static void AltaCuenta(string msjOpcion)
        {
            //ALTA NUEVA CUENTA:
            Console.WriteLine("\n" + msjOpcion);
            var cuenta = Cuenta.IngresarNueva();
            PlanDeCuentas.Agregar(cuenta);

            Console.Write("\nLa cuenta ha sido ingresada correctamente.");
            Continuar("volver al Menú Principal.");

        }

        private static void BajaCuenta(string msjOpcion)
        {
            //BAJA CUENTA:
            Console.WriteLine("\n" + msjOpcion);

            var cuenta = PlanDeCuentas.SeleccionarCuenta();

            if (cuenta == null)
            {
                Console.WriteLine("\nLa cuenta indicada no ha sido encontrada.");
                Continuar("volver al Menú Principal.");
            }
            else
            {
                cuenta.Mostrar();                                  //mostramos cuenta seleccionada   

                Console.WriteLine($"Se dispone a dar de baja a {cuenta.TituloEntrada}. ¿Está ud. seguro/a? (S/N)");
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.S)
                {
                    PlanDeCuentas.Baja(cuenta);
                    Console.WriteLine($"{cuenta.TituloEntrada}. ha sido dado de baja correctamente.");
                    Continuar("volver al Menú Principal.");

                }
                if (key.Key == ConsoleKey.N)
                {
                    Continuar("volver al Menú Principal.");
                }

                else if (key.Key != ConsoleKey.S && key.Key != ConsoleKey.N)
                {
                    Console.WriteLine("\nERROR: Por favor intente nuevamente ingresando una opción válida.");
                    Continuar("redireccionar al Menú Principal.");
                }

            }
        }

            private static void ModificarCuenta(string msjOpcion)
            {
                //MODIFICAR CUENTA:
                Console.WriteLine("\n" + msjOpcion);
                var cuenta = PlanDeCuentas.SeleccionarCuenta();    //traemos cuenta
                if (cuenta == null)
                {
                    Continuar("volver al Menú Principal.");
                }
                else
                {
                    cuenta.Mostrar();                                  //mostramos cuenta seleccionada   
                    cuenta.Modificar();                                //modificamos cuenta               
                    Console.Write("\nLa cuenta ha sido modificada correctamente.");
                    Continuar("volver al Menú Principal.");
                }

            }

            public static void Continuar(string mensaje)
            {
                Console.WriteLine("\n" + "Presione cualquier tecla para " + mensaje + "...");
                Console.ReadKey();
            }

            public static void SalidaPrograma(string msjOpcion)
            {
                Console.WriteLine("\n" + msjOpcion);
                Console.WriteLine("Muchas gracias por utilizar el Sistema. Hasta la próxima.");
                Continuar("finalizar");
                SaludarSistema();
            }

            public static void SaludarSistema()
            {
                Console.WriteLine("\n" + "\t\t\t***************************************************");
                Console.WriteLine("\t\t\t******Sistema de Gestión: Aplicación Contable******");
                Console.WriteLine("\t\t\t***************************************************");
                Console.ReadKey();
            }


        }
    }
