using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;

namespace TPFinal_Nivel2
{
    public partial class frmVerDetalles : Form
    {
        private Articulo articulo;
        public frmVerDetalles(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmVerDetalles_Load(object sender, EventArgs e)
        {
            txtCodArticulo.Text = articulo.codArticulo.ToString();
            txtNombre.Text = articulo.Nombre.ToString();
            txtDescripcion.Text = articulo.Descripcion.ToString();
            txtMarca.Text = articulo.Marca.ToString();
            txtCategoria.Text = articulo.Categoria.ToString();
            txtImagenUIrl.Text = articulo.ImagenUrl.ToString();
            txtPrecio.Text = articulo.Precio.ToString();

            cargarImagen(articulo.ImagenUrl); //
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticuloDetalle.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticuloDetalle.Load("https://image.shutterstock.com/image-vector/ui-image-placeholder-wireframes-apps-260nw-1037719204.jpg");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
