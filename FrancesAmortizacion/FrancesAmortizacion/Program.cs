using System;
using System.Data;
using DataTablePrettyPrinter;// Install-Package DataTablePrettyPrinter -Version 0.2.0

namespace FrancesAmortizacion
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int saldoPrestado = 0, periodo = 0;
                float cuota = 0, tasa=0, saldoFinal = 0, interes = 0, amortizacion = 0, saldoInicial =0, porcTasa=0, x=0 /*aval=0*/ ;

                Console.WriteLine("Ingrese el calor del monto prestado: ");
                saldoPrestado = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ingrese el interes del prestamo: ");
                tasa = (float)Convert.ToDouble(Console.ReadLine());
                porcTasa = tasa / 100;

                Console.WriteLine("Imgrese el numero de cotas para pagar: ");
                periodo =Convert.ToInt32(Console.ReadLine());


                //(CREACION DE LA TABLA)//
                DataTable table = new DataTable();
                table.TableName = "Amortizacion";

                table.Columns.Add("Cuota - Mes", typeof(int));
                table.Columns.Add("Saldo Inicial", typeof(float));
                table.Columns.Add("Interés", typeof(float));
                table.Columns.Add("Abono a capital (Amortización)", typeof(float));
                table.Columns.Add("Cuota Mensual Fija", typeof(float));
                table.Columns.Add("Aval", typeof(float));
                table.Columns.Add("Saldo Final", typeof(float));


                saldoInicial = saldoPrestado;

                cuota = (float)(saldoPrestado * ( Math.Pow(1 + porcTasa, periodo)* porcTasa) / ( Math.Pow( 1 + porcTasa, periodo) - 1 ));
                x = (float)(saldoPrestado * 0.1);

                for (int i = 1 ; i <= periodo; i++)
                {
                    interes = saldoInicial * porcTasa;
                    amortizacion = (cuota - interes);
                    saldoFinal = saldoInicial - amortizacion;
                    //cuota = cuota + avalito;
                    //avalito = aval /periodo;

                    if (saldoFinal < 0)
                    {
                        saldoFinal = 0;
                    }
                    table.Rows.Add(i, saldoInicial, interes, amortizacion, cuota,/* aval */ saldoFinal);
                    
                    saldoInicial = saldoFinal;

                }
                Console.WriteLine(table.ToPrettyPrintedString());
            }
            catch
            {
                Console.WriteLine("ya la embarraste WEYYY :,c ");
            }
        }
    }
}