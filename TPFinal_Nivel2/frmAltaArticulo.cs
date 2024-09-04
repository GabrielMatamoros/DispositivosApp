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
    public partial class frmAltaArticulo : Form
    {
        private Articulo articulo = null;
        public frmAltaArticulo()
        {
            InitializeComponent();
        }
        public frmAltaArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }
        
        private void frmAltaArticulo_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cboMarca.DataSource = marcaNegocio.listar();
                cboMarca.ValueMember = "Id";
                cboMarca.DisplayMember = "Descripcion";

                cboCategoria.DataSource = categoriaNegocio.listar();
                cboCategoria.ValueMember = "Id";
                cboCategoria.DisplayMember = "Descripcion";

                if (articulo != null)
                {
                    txtCodigoArticulo.Text = articulo.codArticulo;
                    txtNombre.Text = articulo.Nombre;
                    txtDescripcion.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    txtImagenUrl.Text = articulo.ImagenUrl;
                    cargarImagen(articulo.ImagenUrl);

                    cboMarca.SelectedValue = articulo.Marca.Id;
                    cboCategoria.SelectedValue = articulo.Categoria.Id;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
       
        private void btnAceptar_Click(object sender, EventArgs e)
        {
          ///  Articulo articulo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                if(articulo == null)
                {
                    articulo = new Articulo();
                }
                articulo.codArticulo = txtCodigoArticulo.Text;
                articulo.Nombre = txtNombre.Text;
                articulo.Descripcion = txtDescripcion.Text;
                articulo.ImagenUrl = txtImagenUrl.Text;
                articulo.Marca = (Marca)cboMarca.SelectedItem;
                articulo.Categoria = (Categoria)cboCategoria.SelectedItem;                 

                if (validarDetalles())
                    return;
                try
                {
                    if (txtPrecio.Text != "")
                        articulo.Precio = decimal.Parse(txtPrecio.Text);

                    if ((soloNumeros(txtPrecio.Text)))
                    {
                        articulo.Precio = decimal.Parse(txtPrecio.Text);
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Debe ingresar un caracter numerico","Numerico",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return ;
                }
                
                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado Exitosamente");
                    Close();
                }                                        
                else
                {       
                negocio.agregar(articulo);
                MessageBox.Show("Agregado Exitosamente");
                Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool validarDetalles()
        {
            if(txtCodigoArticulo.Text == "")
            {
                MessageBox.Show("Debe ingresar un codigo de articulo", "Codigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
                
            }
            if(txtNombre.Text == "")
            {
                MessageBox.Show("Debe ingresar el nombre", "Nombre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if(txtDescripcion.Text == "")
            {
                MessageBox.Show("Debe ingresar la descripcion","Descripcion",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            if (txtPrecio.Text == "")
            {
                MessageBox.Show("Debe ingresar un precio","Precio",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
           
          
            return false;
        }
       
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;
            }
            return true;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        
        private void txtImagenUrl_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtImagenUrl.Text);
        }
       
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulo.Load("https://image.shutterstock.com/image-vector/ui-image-placeholder-wireframes-apps-260nw-1037719204.jpg");
            }
        }

       
    }
}
