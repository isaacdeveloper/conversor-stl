using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConversorSTL
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.DragEnter += panel_DragEnter;
            panel1.DragDrop += panel_DragDrop;
            panel1.DragLeave += panel_DragLeave;
            openFileDialog1.Filter = "Archivos 3MF|*.3mf";
        }

        private void panel_DragLeave(object sender, EventArgs e)
        {
            panel1.BackColor = Form1.DefaultBackColor;
        }

        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[]; // get all files droppeds  
            if (files != null && files.Any())
            {
                string _file = files.First(); //select the first one  
                convertirFichero(_file);
                panel1.BackColor = Form1.DefaultBackColor;
            }
        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
            panel1.BackColor = Color.FromArgb(203, 203, 203);
        }

        private void convertirFichero(string _file)
        {
            FileInfo fi = new FileInfo(_file);
            string directorio = Path.Combine(Environment.CurrentDirectory, fi.Name);
            File.Copy(_file, directorio, true);

            var document = new Aspose.ThreeD.Scene(_file);
            // create an instance of StlSaveOptions 
            var options = new Aspose.ThreeD.Formats.StlSaveOptions();
            // save 3MF as a STL 
            string fichero = openFileDialog1.FileNames[0].ToString().Split('.').First();
            string directorioFinal = Path.Combine(Environment.CurrentDirectory, fichero + ".stl");

            saveFileDialog1.FileName = fichero;
            saveFileDialog1.Filter = "Archivos STL|*.stl";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                document.Save(saveFileDialog1.FileName, options);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void elegirArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string _file = openFileDialog1.FileNames[0].ToString();
                convertirFichero(_file);
            }
        }
    }
}
