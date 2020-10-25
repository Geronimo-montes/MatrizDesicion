using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Matriz_decisión
{
    public partial class Form1 : Form
    {                                                 //marca           modelo                  audio           conectividad            diseño          precio  peso          microfono       construccion            almoadilla 
        private static string[] row0 = new string[] { "STEEELSERIES",   "ACTRIC 7 WIRELESS",    "5.1 & 7.1",    "WIRELESS & ALAMBRICA", "EXCELENTE",    "4000", "350 GRAMOS", "EXCELENTE",  "PLASTICO/ALUMINIO",    "TELA",             "3", "01" };
        private static string[] row1 = new string[] { "SENNHEISER",     "GSP 500",              "5.1 & 7.1",    "WIRELESS & ALAMBRICA", "REGULAR",      "5000", "360 GRAMOS", "MUY BUENO",  "PLASTICO/ALUMINIO",    "PIEL SINTETICA",   "8", "02" };
        private static string[] row2 = new string[] { "RAZER",          "VIRTUOSO WIRELESS",    "5.1 & 7.1",    "WIRELESS",             "EXCELENTE",    "5000", "340 GRAMOS", "MUY BUENO",  "ALUMINIO",             "PIEL SINTETICA",   "2", "03" };
        private static string[] row3 = new string[] { "LOGITECH",       "G533",                 "5.1 & 7.1",    "WIRELESS & ALAMBRICA", "BUENO",        "2500", "400 GRAMOS", "EXCELENTE",  "PLASTICO",             "TELA",             "4", "04" };
        private static string[] row4 = new string[] { "HYPER X",        "CLOUD FLIGHT",         "5.1 & 7.1",    "WIRELESS & ALAMBRICA", "MUY BUENO",    "3500", "320 GRAMOS", "BUENO",      "PLASTICO",             "PIEL SINTETICA",   "1", "05" };
        private static string[] row5 = new string[] { "CORSAIR",        "VOID ELITE WIRELESS",  "5.1 & 7.1",    "WIRELESS",             "MUY BUENO",    "3000", "410 GRAMOS", "REGULAR",    "PLASTICO",             "TELA",             "0", "06" };
        private static string[] row6 = new string[] { "BEYERDYNAMIC",   "CUSTOM GAME",          "5.1 & 7.1",    "ALAMBRICA",            "MUY BUENO",    "4800", "250 GRAMOS", "MUY BUENO",  "PLASTICO",             "TELA",             "7", "07" };
        private static string[] row7 = new string[] { "AUDIO-TECHNICA", "AMX-50",               "5.1",          "ALAMBRICA",            "BUENO",        "3500", "280 GRAMOS", "REGULAR",    "PLASTICO/ALUMINIO",    "PIEL SINTETICA",   "9", "08" };
        private static string[] row8 = new string[] { "ASUS",           "ROG DELTA",            "5.1",          "ALAMBRICA",            "MUY BUENO",    "3800", "390 GRAMOS", "MUY BUENO",  "PLASTICO",             "TELA",             "6", "09" };
        private static string[] row9 = new string[] { "ASTRO",          "A30",                  "5.1 & 7.1",    "WIRELESS",             "BUENO",        "4500", "360 GRAMOS", "MUY BUENO",  "PLASTICO/ALUMINIO",    "TELA",             "5", "10" };
        private static object[] rows = new object[10] { row0, row1, row2, row3, row4, row5, row6, row7, row8, row9 };

        //PERMITE MOVER EL FORM ATRAVEZ DEL PANEL SUPERIOR
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*AL correr el programa cargamos los daros en el datagrid*/
            foreach (string[] rowArray in rows)
                dataGridView1.Rows.Add(rowArray);

            //evita ordenar mediante otra columna
            for (int i = 0; i < 10; i++)
                dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

            //resuleve para los valores default
            resolver();
        }

        //se externa en una funcion para ejecutarlo al iniciar el programa
        private void pbBtnIniciar_Click(object sender, EventArgs e)
        {
            if(
                Convert.ToDouble(txtMarca.Text) 
                +Convert.ToDouble(txtModelo.Text)
                +Convert.ToDouble(txtAudio.Text)
                +Convert.ToDouble(txtConectividad.Text)
                +Convert.ToDouble(txtDiseño.Text)
                +Convert.ToDouble(txtPrecio.Text)
                +Convert.ToDouble(txtPeso.Text)
                +Convert.ToDouble(txtCalidad.Text)
                +Convert.ToDouble(txtConstruccion.Text)
                +Convert.ToDouble(txtAlmohadillas.Text) == 100
                )
            {
                resolver();
            }
            else
            {
                MessageBox.Show("La suma no da 100.", "Error Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void resolver()
        {
            //Para resolver ocupamos ordenar como al inicio por el nombre de la marca
            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);

            matrizDesicion matriz = new matrizDesicion();

            //matriz posee valores publicas para establecer los pesos desde fuera de la clase
            matriz.marca        = Convert.ToDouble(txtMarca.Text)       / 100;
            matriz.modelo       = Convert.ToDouble(txtModelo.Text)      / 100;
            matriz.audio        = Convert.ToDouble(txtAudio.Text)       / 100;
            matriz.conextividad = Convert.ToDouble(txtConectividad.Text)/ 100;
            matriz.diseño       = Convert.ToDouble(txtDiseño.Text)      / 100;
            matriz.precio       = Convert.ToDouble(txtPrecio.Text)      / 100;
            matriz.peso         = Convert.ToDouble(txtPeso.Text)        / 100;
            matriz.microfono    = Convert.ToDouble(txtCalidad.Text)     / 100;
            matriz.construcion  = Convert.ToDouble(txtConstruccion.Text)/ 100;
            matriz.almohadillas = Convert.ToDouble(txtAlmohadillas.Text)/ 100;

            //Resultados obtenidos sin ordenar, viene en el orden en que se enviaron los datos
            double[] resultado = new double[10];
            resultado = matriz.evaluarProductos(rows);

            //Los resultados se agregan al dgv
            for (int i = 0; i < 10; i++)
                dataGridView1.Rows[i].Cells[10].Value = resultado[i];

            //Nuevo arreglo para los resultados clasificados segun calificacion y pesos
            DataTable tbres = new DataTable("resultado");
            tbres = matriz.ordenarConRepetidos(dataGridView1);

            //Borramos el dgv y ponemos la datatable como datasource
            dataGridView1.DataSource = null;
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = tbres;
         
           /* 
            //Ocultamos las columnas de los pesos
            dataGridView1.Columns["0"].Visible = false;
            dataGridView1.Columns["1"].Visible = false;
            dataGridView1.Columns["2"].Visible = false;
            dataGridView1.Columns["3"].Visible = false;
            dataGridView1.Columns["4"].Visible = false;
            dataGridView1.Columns["5"].Visible = false;
            dataGridView1.Columns["6"].Visible = false;
            dataGridView1.Columns["7"].Visible = false;
            dataGridView1.Columns["8"].Visible = false;
            dataGridView1.Columns["9"].Visible = false;
            */

            //agregamos el evento de cambio de seleccion de row en el dgv
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);

            #region Identificar repetidos
            //Identificar repetidos
            int con = 0;
            Color[] colores = new Color[10];
            colores[0] = Color.FromArgb(255, 78, 198, 6);
            colores[1] = Color.FromArgb(255, 110, 240, 32);
            colores[2] = Color.FromArgb(255, 138, 241, 77);
            colores[3] = Color.FromArgb(255, 166, 242, 120);
            colores[4] = Color.FromArgb(255, 187, 243, 154);
            colores[5] = Color.FromArgb(255, 255, 185, 185);
            colores[6] = Color.FromArgb(255, 255, 128, 129);
            colores[7] = Color.FromArgb(255, 255, 91, 90);
            colores[8] = Color.FromArgb(255, 255, 52, 53);
            colores[9] = Color.FromArgb(255, 254, 0, 0);

            //Coloreamos el primer renglon dado que los datos ya estan ordenados
            dataGridView1.Rows[0].DefaultCellStyle.BackColor = colores[con];

            for (int i = 0; i < 9; i++)
            {   //Comparamos de dos celdas a la vez, si son iguales se comparan sus pesos
                if(dataGridView1.Rows[i].Cells[10].Value.Equals(dataGridView1.Rows[i + 1].Cells[10].Value))
                {
                    Boolean igual = true;
                    for(int j = 12; j < 21; j++)
                        //Mediante un fir que recorra el datagridview apartir de la posicion de los pesos
                        if (!dataGridView1.Rows[i].Cells[j].Value.Equals(dataGridView1.Rows[i + 1].Cells[j].Value))
                        {   //Si son distintos el contador aumento y se lo colorea el row con indice i + 1
                            con += 1;
                            dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = colores[con];
                            igual = false;
                            break;
                        }

                    if (igual)
                        //AL ser iguales se colorea la celda del mismo color
                        dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = colores[con];
                }
                else
                {   //Al ser directamente distintas coloreamos el row con idice i + 1 con el sig color
                    con += 1;
                    dataGridView1.Rows[i + 1].DefaultCellStyle.BackColor = colores[con];
                }
            }

            //Los colores estan en orden, para simplificar la numeracion de los repetidos
            for(int i = 0; i < 10; i++)
                for(int j = 0; j < 10; j++)
                    if (dataGridView1.Rows[i].DefaultCellStyle.BackColor.Equals(colores[j]))
                        dataGridView1.Rows[i].Cells[11].Value = j + 1;
            #endregion
        }

        #region Funciones
        private void SoloNumDecimal(object sender, KeyPressEventArgs e)
        {
            char cSymbol = (char)46;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != cSymbol)
            {
                e.Handled = true;
            }

            if (e.KeyChar == cSymbol && (sender as TextBox).Text.IndexOf(cSymbol) > -1)
            {
                e.Handled = true;
            }
        }
        private void pbCerrar_Click(object sender, EventArgs e) => Close();
        private void pbMinimizar_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        //eventos
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int control = 10; ;

            if (dataGridView1.CurrentRow != null)
                control = dataGridView1.CurrentRow.Index;

            switch (control)
            {
                case 0:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._0;
                    break;
                case 1:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._1;
                    break;
                case 2:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._2;
                    break;
                case 3:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._3;
                    break;
                case 4:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._4;
                    break;
                case 5:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._5;
                    break;
                case 6:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._6;
                    break;
                case 7:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._7;
                    break;
                case 8:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._8;
                    break;
                case 9:
                    pbImagen.Image = Matriz_decisión.Properties.Resources._9;
                    break;
            }
        }
        #endregion
    }
}