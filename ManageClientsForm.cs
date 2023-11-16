using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Room_System
{
    public partial class ManageClientsForm : Form
    {
        Client client = new Client();
        public ManageClientsForm()
        {
            InitializeComponent();
        }

        private void btnClearFields_Click(object sender, EventArgs e)
        {
            tbID.Text = "";
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbPhone.Text = "";
            tbType.Text = "";
            tbAddress.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            String fname = tbFirstName.Text;
            String lname = tbLastName.Text;
            String phone = tbPhone.Text;
            String type = tbType.Text;
            String address = tbAddress.Text;

            if(fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals(""))
            {
                MessageBox.Show("Required fields - Firstname/Lastname/Phone", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean IsInsertedClient = client.InsertClient( fname, lname, phone, type, address);

                if (IsInsertedClient)
                {
                    dgvClients.DataSource = client.GetAllClients();
                    MessageBox.Show("Client inserted successfully!", "Client Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Client not inserted!", "Client Add", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void ManageClientsForm_Load(object sender, EventArgs e)
        {
            dgvClients.DataSource = client.GetAllClients();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id;
            String fname = tbFirstName.Text;
            String lname = tbLastName.Text;
            String phone = tbPhone.Text;
            String type = tbType.Text;
            String address = tbAddress.Text;

            try
            {
                id = Convert.ToInt32(tbID.Text);

                if (fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals(""))
                {
                    MessageBox.Show("Required fields - Firstname/Lastname/Phone", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean IsUpdatedClient = client.EditClient(id, fname, lname, phone,type, address);

                    if (IsUpdatedClient)
                    {
                        dgvClients.DataSource = client.GetAllClients();
                        MessageBox.Show("Client updated successfully!", "Client Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Client not updated!", "Client Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

         
        }


        private void dgvClients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tbID.Text = dgvClients.CurrentRow.Cells[0].Value.ToString();
            tbFirstName.Text = dgvClients.CurrentRow.Cells[1].Value.ToString();
            tbLastName.Text = dgvClients.CurrentRow.Cells[2].Value.ToString();
            tbPhone.Text = dgvClients.CurrentRow.Cells[3].Value.ToString();
            tbType.Text = dgvClients.CurrentRow.Cells[4].Value.ToString();
            tbAddress.Text = dgvClients.CurrentRow.Cells[5].Value.ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(tbID.Text);

                if (client.RemoveClient(id)) 
                {
                    dgvClients.DataSource = client.GetAllClients();
                    MessageBox.Show("Client deleted successfully!", "Client Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClearFields.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Client not deleted!", "Client Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            dgvClients.DataSource = client.GetClientsByMultipleFields(tbSearch.Text);
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
