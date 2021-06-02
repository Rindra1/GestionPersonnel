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

namespace TestTanyah
{
    public partial class Form1 : Form
    {
        private Personne personne;
        private Personne detailPersonne;
        private List<Personne> listePersonne;

        /********************************************************************************
         **************Récuperer la liste des personnes**********************************
         ********************************************************************************/
        private List<Personne> Listepers()
        {
            return personne.ListePersonne();
        }

        public Form1()
        {
            InitializeComponent();
            //MessageBox.Show(Directory.GetCurrentDirectory());
            personne = new Personne();
            listePersonne = Listepers();
            foreach(var listePers in listePersonne)
            {
                dgvListePersonne.Rows.Add(listePers.nom, listePers.prenom);
            }
            btModifier.Enabled = false;
            btSupprimer.Enabled = false;
            btEnregistrer.Enabled = false;

        }

        private void dgvListePersonne_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nom = dgvListePersonne.Rows[e.RowIndex].Cells[0].Value.ToString();
            string prenom = dgvListePersonne.Rows[e.RowIndex].Cells[1].Value.ToString();
            detailPersonne = listePersonne.First(x => x.nom.ToUpper().Trim().Equals(nom.ToUpper().Trim()) && x.prenom.ToUpper().Trim().Equals(prenom.ToUpper().Trim()));
            txtId.Text = detailPersonne.id.ToString();
            txtNom.Text = detailPersonne.nom;
            txtPrenom.Text = detailPersonne.prenom;
            txtTelephone.Text = detailPersonne.telephone;
            txtAdresse.Text = detailPersonne.adresse;
            txtDatedenaissance.Text = detailPersonne.date_de_naissance.ToShortDateString();
            btModifier.Enabled = true;
            btSupprimer.Enabled = true;
            btEnregistrer.Enabled = false;
        }

        /************************************************************************
         ************************Enregistrer une personne************************
         ************************************************************************/
        private void btEnregistrer_Click(object sender, EventArgs e)
        {
            detailPersonne = new Personne();
            detailPersonne.nom = txtNom.Text;
            detailPersonne.prenom = txtPrenom.Text;
            detailPersonne.telephone = txtTelephone.Text;
            detailPersonne.date_de_naissance = Convert.ToDateTime(txtDatedenaissance.Text);
            detailPersonne.adresse = txtAdresse.Text;
            detailPersonne = personne.AjouterPersonne(detailPersonne);
            Raz();
            raffraichirDatagrid();
            btAjouter.Enabled = true;
            btModifier.Enabled = false;
            btSupprimer.Enabled = false;
            btEnregistrer.Enabled = false;

        }

        /**************************************************************************
         *******************************Supprimer une personne*********************
         **************************************************************************/
        private void btSupprimer_Click(object sender, EventArgs e)
        {
            personne.SupprimerPersonne(detailPersonne);
            listePersonne.Remove(detailPersonne);
            Raz();
            raffraichirDatagrid();
            btAjouter.Enabled = true;
            btModifier.Enabled = false;
            btSupprimer.Enabled = false;
            btEnregistrer.Enabled = false;
        }

        private void btModifier_Click(object sender, EventArgs e)
        {
            Personne detailPers = new Personne();
            detailPers.id = Convert.ToInt32(txtId.Text);
            detailPers.nom = txtNom.Text;
            detailPers.prenom = txtPrenom.Text;
            detailPers.telephone = txtTelephone.Text;
            detailPers.date_de_naissance = Convert.ToDateTime(txtDatedenaissance.Text);
            detailPers.adresse = txtAdresse.Text;
            personne.ModifierPersonne(detailPers);
            listePersonne.First(x => x.id == Convert.ToInt32(txtId.Text)).adresse = txtAdresse.Text;
            listePersonne.First(x => x.id == Convert.ToInt32(txtId.Text)).nom = txtNom.Text;
            listePersonne.First(x => x.id == Convert.ToInt32(txtId.Text)).prenom = txtPrenom.Text;
            listePersonne.First(x => x.id == Convert.ToInt32(txtId.Text)).telephone = txtTelephone.Text;
            listePersonne.First(x => x.id == Convert.ToInt32(txtId.Text)).date_de_naissance = Convert.ToDateTime(txtDatedenaissance.Text);
            Raz();
            raffraichirDatagrid();
            btAjouter.Enabled = true;
            btModifier.Enabled = false;
            btSupprimer.Enabled = false;
            btEnregistrer.Enabled = false;
        }

        /********************************************************************
         ****************************Vider les textbox***********************
         ********************************************************************/
        private void Raz()
        {
            txtId.Text = "";
            txtNom.Text = "";
            txtPrenom.Text = "";
            txtTelephone.Text = "";
            txtAdresse.Text = "";
            txtDatedenaissance.Text = "";
            listePersonne = Listepers();
        }

        /************************************************************************
         **********************Raffraichir le datagrid***************************
         ************************************************************************/
        private void raffraichirDatagrid()
        {
            if (dgvListePersonne.Rows.Count > 0)
                dgvListePersonne.Rows.Clear();
            foreach (var listePers in listePersonne)
            {
                dgvListePersonne.Rows.Add(listePers.nom, listePers.prenom);
            }
        }


        /**************************************************************************************************************
         ***********************Crétion d'une nouvelle ligne dans le datagrid pour ajouter une nouvelle personne*******
         **************************************************************************************************************/
        private void btAjouter_Click(object sender, EventArgs e)
        {
            dgvListePersonne.Rows.Add("", "");
            Raz();
            btAjouter.Enabled = false;
            btModifier.Enabled = false;
            btSupprimer.Enabled = false;
            btEnregistrer.Enabled = false;

        }

        /*******************************************************************************
         *********************selection d'une personne pour le supprimer ou la modifier 
         ********************* ou aussi pour ajouter une nouvelle personne**************/

        private void dgvListePersonne_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string nom = dgvListePersonne.Rows[e.RowIndex].Cells[0].Value.ToString();
            string prenom = dgvListePersonne.Rows[e.RowIndex].Cells[1].Value.ToString();

            if (nom.Trim()=="" && prenom.Trim() == "")
            {
                Raz();
                btEnregistrer.Enabled = true;
                btModifier.Enabled = false;
                btSupprimer.Enabled = false;
            }
            else
            {
                detailPersonne = listePersonne.First(x => x.nom.ToUpper().Trim().Equals(nom.ToUpper().Trim()) && x.prenom.ToUpper().Trim().Equals(prenom.ToUpper().Trim()));
                txtId.Text = detailPersonne.id.ToString();
                txtNom.Text = detailPersonne.nom;
                txtPrenom.Text = detailPersonne.prenom;
                txtTelephone.Text = detailPersonne.telephone;
                txtAdresse.Text = detailPersonne.adresse;
                txtDatedenaissance.Text = detailPersonne.date_de_naissance.ToShortDateString();
                btModifier.Enabled = true;
                btSupprimer.Enabled = true;
                btEnregistrer.Enabled = false;
            }
        }
    }
}
