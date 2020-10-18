using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using JerarquiaEmpresa.Models;

namespace JerarquiaEmpresa
{
    public partial class _Default : Page
    {
        Models.BDJerarquiaDeEmpresaEntities BD = new BDJerarquiaDeEmpresaEntities();

        void ListarEmpresas(){

            tblListaEmpresa.DataSource = BD.tblEmpresa.ToList();
            tblListaEmpresa.DataBind();
            
        }

        void ListarDepartamento()
        {

            tblListaDepartamento.DataSource = BD.TblDepartamento.ToList();
            tblListaDepartamento.DataBind();

        }

        void ListarSubDepartamento()
        {

            btlListaSubDepartamento.DataSource = BD.tblSubDepartamento.ToList();
            btlListaSubDepartamento.DataBind();

        }

        void BorrarCamposEmpresas()
        {
            txtnombreEmpresa.Text = "";
            TxtNitEmpresa.Text = "";
            TxtTelEmpresa.Text = "";
            TxtDireccion.Text = "";
        }

        void BorrarCamposDepartamento()
        {
            TxtDepartamento.Text = "";
            TxtNomGerenDepart.Text = "";
            TxtTelGerenteDepartamento.Text = "";
            TxtFechaNaciGerenteDpto.Text = "";
            TxtFechaIniGerenciaDpto.Text = "";
        }

        void BorrarCamposSubDepartamento()
        {
            txtNomSubDepartamento.Text = "";
            txtNomGerentSubDepartamento.Text = "";
            txtTelGerentSubDepartamento.Text = "";
            txtFechaNaciGerentSubDpto.Text = "";
            txtFechaIniGerenSubDpto.Text = "";
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            ListarEmpresas();
        }


        protected void Onclick_RegistrarEmpresa(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Registro", "<script>alert('La informacion de la empresa a sido registrada correctamente');</script>");

            string nombre = txtnombreEmpresa.Text;
            string Nit = TxtNitEmpresa.Text;
            int Telefono = Convert.ToInt32(TxtTelEmpresa.Text);
            string Direccion = TxtDireccion.Text;

            BD.RegistrarEmpresa(Nit, nombre, Telefono, Direccion);

            ListarEmpresas();

            BorrarCamposEmpresas();
            
        }

        protected void tblListaEmpresa_SelectedIndexChanged4(object sender, EventArgs e)
        {
            tblListaDepartamento.DataSource = null;
            tblListaDepartamento.DataBind();
            btlListaSubDepartamento.DataSource = null;
            btlListaSubDepartamento.DataBind();

            int Nit = Convert.ToInt32(tblListaEmpresa.SelectedRow.Cells[1].Text);
            //procedimiento que me muestre losDepartamentos ligados a esta empresa
            var queryDepartamento = from depart in BD.TblDepartamento
                                       where depart.NitEmpresa == Nit
                                       select depart;

            tblListaDepartamento.DataSource = queryDepartamento.ToList();
            tblListaDepartamento.DataBind();



        }

        protected void tblListaEmpresa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            int Nit = Convert.ToInt32(((GridView)sender).Rows[e.RowIndex].Cells[1].Text);
            BD.EliminarEmpresa(Nit);
            ListarEmpresas();
        }

        protected void Onclick_RegistrarDepartamento2(object sender, EventArgs e)
        {

            Page.ClientScript.RegisterStartupScript(this.GetType(), "Registro", "<script>alert('La informacion del departamento  a sido Registrada correctamente');</script>");


            string NomDepartamento = TxtDepartamento.Text;
            string NomGerentDepar = TxtNomGerenDepart.Text;
            int Telefono = Convert.ToInt32(TxtTelGerenteDepartamento.Text);
            DateTime FechaNaciGerenteDpto = Convert.ToDateTime(TxtFechaNaciGerenteDpto.Text);
            DateTime FechaIniGerenciaDpto = Convert.ToDateTime(TxtFechaIniGerenciaDpto.Text);
            //int ComboListaEmpresas = Convert.ToInt32(ComboboxListaEmpresas.SelectedValue);
            int Nit = Convert.ToInt32(tblListaEmpresa.SelectedRow.Cells[1].Text);

            BD.RegistrarDepartamento(NomDepartamento, NomGerentDepar, Telefono, FechaNaciGerenteDpto, FechaIniGerenciaDpto, Nit);

            var queryDepartamento = from depart in BD.TblDepartamento
                                            where depart.NitEmpresa == Nit
                                            select depart;

            tblListaDepartamento.DataSource = queryDepartamento.ToList();
            tblListaDepartamento.DataBind();

            BorrarCamposDepartamento();

        }

        protected void tblListaDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            btlListaSubDepartamento.DataSource = null;
            btlListaSubDepartamento.DataBind();

            //procedimiento que me muestre los SubDepartamentos ligados a este Departamento
            int Departamento = Convert.ToInt32(tblListaDepartamento.SelectedRow.Cells[1].Text);

            var querySubDepartamento = from Subdepart in BD.tblSubDepartamento
                                       where Subdepart.IdDepartamento == Departamento
                                       select Subdepart;

            btlListaSubDepartamento.DataSource = querySubDepartamento.ToList();
            btlListaSubDepartamento.DataBind();
        }

        protected void tblListaDepartamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int IdDepartamento = Convert.ToInt32(((GridView)sender).Rows[e.RowIndex].Cells[1].Text);                                       
            BD.EliminarDepartamento(IdDepartamento);
            var queryDepartamentoEliminar = from depart in BD.TblDepartamento
                                       where depart.IdDepartamento == IdDepartamento
                                        select depart;

            tblListaDepartamento.DataSource = queryDepartamentoEliminar.ToList();
            tblListaDepartamento.DataBind();
        }

        protected void Onclick_RegistrarSubDepartamento(object sender, EventArgs e)
        {

            string NomSubDepartamento = txtNomSubDepartamento.Text;
            string NomGerentSubDepar = txtNomGerentSubDepartamento.Text;
            int TelefonoGerenteSubDpto = Convert.ToInt32(txtTelGerentSubDepartamento.Text);
            DateTime FechaNaciGerenteSubDpto = Convert.ToDateTime(txtFechaNaciGerentSubDpto.Text);
            DateTime FechaIniGerenciaSubDpto = Convert.ToDateTime(txtFechaIniGerenSubDpto.Text);
            //int ComboListaEmpresas = Convert.ToInt32(ComboboxListaEmpresas.SelectedValue);
            int Departamento = Convert.ToInt32(tblListaDepartamento.SelectedRow.Cells[1].Text);

            BD.RegistrarSubDepartamento(NomSubDepartamento, NomGerentSubDepar, TelefonoGerenteSubDpto, FechaNaciGerenteSubDpto, FechaIniGerenciaSubDpto, Departamento);

            ListarSubDepartamento();

            BorrarCamposSubDepartamento();

        }

        protected void tblListaSubDepartamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            int IdSubDepartamento = Convert.ToInt32(((GridView)sender).Rows[e.RowIndex].Cells[1].Text);
            BD.EliminarSubDepartamento(IdSubDepartamento);
            ListarSubDepartamento();
        }

    }
}