using System;
using System.Windows.Forms;
using System.Data;


namespace Matriz_decisión
{
    class matrizDesicion
    {
        //Vectores de valores de evaluacion
        private string[,] audioArray = new string[2, 2] { { "5.1", "8" }, { "5.1 & 7.1", "10" } };
        private string[,] conectividadArray = new string[3, 2] { { "ALAMBRICA", "6" }, { "WIRELESS", "8" }, { "WIRELESS & ALAMBRICA", "10" } };
        private string[,] diseñoArray = new string[4, 2] { { "REGULAR", "4" }, { "BUENO", "6" }, { "MUY BUENO", "8" }, { "EXCELENTE", "10" } };
        private double[,] precioArray = new double[7, 3] { { 5000, 5499, 4 }, { 4500, 4999, 5 }, { 4000, 4499, 6 }, { 3500, 3999, 7 }, { 3000, 3499, 8 }, { 2500, 2999, 9 }, { 2000, 2499, 10 } };
        private double[,] pesoArray = new double[6, 3] { { 410, 450, 5 }, { 380, 409, 6 }, { 340, 379, 7 }, { 310, 339, 8 }, { 280, 309, 9 }, { 250, 279, 10 } };
        private string[,] microfonoArray = new string[4, 2] { { "REGULAR", "4" }, { "BUENO", "6" }, { "MUY BUENO", "8" }, { "EXCELENTE", "10" } };
        private string[,] construccionArray = new string[3, 2] { { "PLASTICO", "6" }, { "ALUMINIO", "8" }, { "PLASTICO/ALUMINIO", "10" } };
        private string[,] almohadillasArray = new string[2, 2] { { "TELA", "8" }, { "PIEL SINTETICA", "10" } };
        //Pesos asignados por el usuario (porcentajes)
        public double marca;
        public double modelo;
        public double audio;
        public double conextividad;
        public double diseño;
        public double precio;
        public double peso;
        public double microfono;
        public double construcion;
        public double almohadillas;
        //variables de uso para retornar valores y realizar calculos
        private double[] res;
        private double[,] pesos;
        private int conRowActual = 0, conColumnaActual = 0;

        public matrizDesicion() {  }

        public double[] evaluarProductos(object[] productos)
        {
            //array a retronar con las sumas de los pesos
            res = new double[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            //Array que almacena los calculos por categoria
            pesos = new double[10, 10];
            //se inicializa pesos con valores en 0
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                    pesos[i, j] = 0;

            foreach(string[] rowArray in productos)
            {
                foreach(string caracteristica in rowArray)
                {
                    //Dependiendo de la variable se realiza el calculo correspondiente
                    switch (conColumnaActual)
                    {
                        case 0:
                            break;
                        case 1:
                            break;
                        case 2: //audio
                            for(int i = 0; i < 2; i++)
                            {
                                if(caracteristica == audioArray[i, 0])
                                {
                                    res[conRowActual] += Convert.ToDouble(audioArray[i, 1]) * audio;    //ocumulamos el resultado en el array res
                                    pesos[conRowActual, conColumnaActual] = Convert.ToDouble(audioArray[i, 1]) * audio; //guardamos el calculo por individual
                                    break;
                                }
                            }
                            break;
                        case 3: // conectividad
                            for (int i = 0; i < 3; i++)
                            {
                                if (caracteristica == conectividadArray[i, 0])
                                {
                                    res[conRowActual] += Convert.ToDouble(conectividadArray[i, 1]) * conextividad;
                                    pesos[conRowActual, conColumnaActual] = Convert.ToDouble(conectividadArray[i, 1]) * conextividad;
                                    break;
                                }
                            }
                            break;
                        case 4: // diseño
                            for (int i = 0; i < 4; i++)
                            {
                                if (caracteristica == diseñoArray[i, 0])
                                {
                                    res[conRowActual] += Convert.ToDouble(diseñoArray[i, 1]) * diseño;
                                    pesos[conRowActual, conColumnaActual] = Convert.ToDouble(diseñoArray[i, 1]) * diseño;
                                    break;
                                }
                            }
                            break;
                        case 5: //precio
                            for (int i = 0; i < 7; i++)
                            {
                                if (Convert.ToDouble(caracteristica) >= precioArray[i, 0] & Convert.ToDouble(caracteristica) <= precioArray[i, 1])
                                {
                                    res[conRowActual] += precioArray[i, 2] * precio;
                                    pesos[conRowActual, conColumnaActual] = precioArray[i, 2] * precio;
                                    break;
                                }
                            }
                            break;
                        case 6: //peso
                            for (int i = 0; i < 6; i++)
                            {
                                if (Convert.ToDouble(caracteristica.Substring(0,3)) >= pesoArray[i, 0] & Convert.ToDouble(caracteristica.Substring(0, 3)) <= pesoArray[i, 1])
                                {
                                    res[conRowActual] += pesoArray[i, 2] * peso;
                                    pesos[conRowActual, conColumnaActual] = pesoArray[i, 2] * peso;
                                    break;
                                }
                            }
                            break;
                        case 7: //caidad MICROFONO
                            for (int i = 0; i < 4; i++)
                            {
                                if (caracteristica == microfonoArray[i, 0])
                                {
                                    res[conRowActual] += Convert.ToDouble(microfonoArray[i, 1]) * microfono;
                                    pesos[conRowActual, conColumnaActual] = Convert.ToDouble(microfonoArray[i, 1]) * microfono;
                                    break;
                                }
                            }
                            break;
                        case 8: //construccion
                            for (int i = 0; i < 3; i++)
                            {
                                if (caracteristica == construccionArray[i, 0])
                                {
                                    res[conRowActual] += Convert.ToDouble(construccionArray[i, 1]) * construcion;
                                    pesos[conRowActual, conColumnaActual] = Convert.ToDouble(construccionArray[i, 1]) * construcion;
                                    break;
                                }
                            }
                            break;
                        case 9: //almohadillas
                            for (int i = 0; i < 3; i++)
                            {
                                if (caracteristica == almohadillasArray[i, 0])
                                {
                                    res[conRowActual] += Convert.ToDouble(almohadillasArray[i, 1]) * almohadillas;
                                    pesos[conRowActual, conColumnaActual] = Convert.ToDouble(almohadillasArray[i, 1]) * almohadillas;
                                    break;
                                }
                            }
                            break;
                    }

                    conColumnaActual++;
                }

                conColumnaActual = 0;
                conRowActual++;
            }

            return res;
        }

        /*Esta funcion sirve pero da resultados inesperados*/
        public DataTable ordenarConRepetidos(DataGridView dgv)
        {
            //Primero creamos dos array con los pesos de cada categoria y en el otro array sus vaores de posicion
            double[] pesosPorcentajes = new double[10] {marca, modelo, audio, conextividad, diseño, precio, peso, microfono, construcion, almohadillas};

            //Ordenamos mediante burbuja anmbos arrays
            double d;
            string s;

            for (int a = 0; a <= pesosPorcentajes.Length - 2; a++)
            {
                for (int b = 0; b <= pesosPorcentajes.Length - 2; b++)
                {
                    if (pesosPorcentajes[b] < pesosPorcentajes[b + 1])
                    {
                        d = pesosPorcentajes[b + 1];
                        pesosPorcentajes[b + 1] = pesosPorcentajes[b];
                        pesosPorcentajes[b] = d;

                        for (int i = 0; i < 10; i++)
                        {
                            d = pesos[i, b + 1];
                            pesos[i, b + 1] = pesos[i, b];
                            pesos[i, b] = d;
                        }
                    }
                }
            }

            DataTable tbPesos = new DataTable("pesos");

            /*Coluumnas del data table*/

            //Columnas a mostrar
            tbPesos.Columns.Add("Marca");
            tbPesos.Columns.Add("Modelo");
            tbPesos.Columns.Add("Audio");
            tbPesos.Columns.Add("Conectividad");
            tbPesos.Columns.Add("Diseño");
            tbPesos.Columns.Add("Precio");
            tbPesos.Columns.Add("Peso");
            tbPesos.Columns.Add("Calidad");
            tbPesos.Columns.Add("Construcion");
            tbPesos.Columns.Add("Almohadillas");
            tbPesos.Columns.Add("Calificacion");
            tbPesos.Columns["Calificacion"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("Posicion");
            //columnas a ocultar
            tbPesos.Columns.Add("0");
            tbPesos.Columns["0"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("1");
            tbPesos.Columns["1"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("2");
            tbPesos.Columns["2"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("3");
            tbPesos.Columns["3"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("4");
            tbPesos.Columns["4"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("5");
            tbPesos.Columns["5"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("6");
            tbPesos.Columns["6"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("7");
            tbPesos.Columns["7"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("8");
            tbPesos.Columns["8"].DataType = System.Type.GetType("System.Double");
            tbPesos.Columns.Add("9");
            tbPesos.Columns["9"].DataType = System.Type.GetType("System.Double");

            //se agregan los valores row por row
            for (int i = 0; i < 10; i++)
            {
                tbPesos.Rows.Add(
                        dgv.Rows[i].Cells[0].Value,
                        dgv.Rows[i].Cells[1].Value,
                        dgv.Rows[i].Cells[2].Value,
                        dgv.Rows[i].Cells[3].Value,
                        dgv.Rows[i].Cells[4].Value,
                        dgv.Rows[i].Cells[5].Value,
                        dgv.Rows[i].Cells[6].Value,
                        dgv.Rows[i].Cells[7].Value,
                        dgv.Rows[i].Cells[8].Value,
                        dgv.Rows[i].Cells[9].Value,
                        Convert.ToDouble( dgv.Rows[i].Cells[10].Value),
                        dgv.Rows[i].Cells[11].Value,

                        pesos[i, 0],
                        pesos[i, 1],
                        pesos[i, 2],
                        pesos[i, 3],
                        pesos[i, 4],
                        pesos[i, 5],
                        pesos[i, 6],
                        pesos[i, 7],
                        pesos[i, 8],
                        pesos[i, 9]
                );
            }

            //se ordena la tabla
            tbPesos.DefaultView.Sort = "Calificacion DESC, 0 DESC, 1 DESC, 2 DESC, 3 DESC, 4 DESC, 5 DESC, 6 DESC, 7 DESC, 8 DESC, 9 DESC";
            //se retorna
            return tbPesos;
        }
    }
}
