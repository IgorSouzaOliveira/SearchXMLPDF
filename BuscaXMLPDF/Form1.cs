using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuscaXMLPDF
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtCaminho.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {

                string[] arquivos = Directory.GetFiles((txtCaminho.Text), "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".xml", StringComparison.OrdinalIgnoreCase) || s.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                // Itera sobre os arquivos encontrados
                foreach (string arquivo in arquivos)
                {

                    if (radioXML.Checked)
                    {
                        // Verifica se é um arquivo XML
                        if (arquivo.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] arquivoSearch = txtArquivo.Text.Split(';');

                            foreach (var search in arquivoSearch)
                            {

                                if (Path.GetFileNameWithoutExtension(arquivo).Equals(search.Trim(), StringComparison.OrdinalIgnoreCase))
                                {
                                    string[] conteudoA = { Path.GetFileNameWithoutExtension(arquivo), Path.GetExtension(arquivo), Path.GetDirectoryName(arquivo) };

                                    ListViewItem item = new ListViewItem();
                                    item.Text = conteudoA[0];
                                    item.SubItems.Add(conteudoA[1]);
                                    item.SubItems.Add(conteudoA[2]);
                                    listView.Items.Add(item);
                                    lblTotal.Text = Convert.ToString(listView.Items.Count);
                                    listView.AutoResizeColumns((ColumnHeaderAutoResizeStyle)1);

                                    break;
                                }
                            }
                        }
                    }
                    else if (radioPDF.Checked)
                    {
                        // Verifica se é um arquivo PDF
                        if (arquivo.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                        {
                            string[] arquivoSearch = txtArquivo.Text.Split(';');

                            foreach (var search in arquivoSearch)
                            {

                                if (Path.GetFileNameWithoutExtension(arquivo).Equals(search.Trim(), StringComparison.OrdinalIgnoreCase))
                                {
                                    string[] conteudoA = { Path.GetFileNameWithoutExtension(arquivo), Path.GetExtension(arquivo), Path.GetDirectoryName(arquivo) };

                                    ListViewItem item = new ListViewItem();
                                    item.Text = conteudoA[0];
                                    item.SubItems.Add(conteudoA[1]);
                                    item.SubItems.Add(conteudoA[2]);
                                    listView.Items.Add(item);
                                    lblTotal.Text = Convert.ToString(listView.Items.Count);
                                    listView.AutoResizeColumns((ColumnHeaderAutoResizeStyle)1);

                                    break;
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDestino_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtDestino.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {

            try
            {
                string[] arquivos = Directory.GetFiles(txtCaminho.Text);
                string destino = txtDestino.Text;

                foreach (var arquivo in arquivos)
                {
                    for (int i = 0; i < listView.Items.Count; i++)
                    {
                        var selected = listView.SelectedItems;

                        if (selected.Count >= i)
                        {
                            string nomeArquivo = Path.GetFileName(arquivo);
                            string caminhoDestino = Path.Combine(destino, nomeArquivo);
                            File.Copy(arquivo, caminhoDestino, true);
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
            txtArquivo.Text = string.Empty;
            lblTotal.Text = "0";
        }
    }
}