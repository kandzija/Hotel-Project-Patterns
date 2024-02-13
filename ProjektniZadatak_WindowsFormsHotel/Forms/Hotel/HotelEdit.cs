using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsHotel.Code;

namespace WindowsFormsHotel.Forms.Hotel
{
    public partial class HotelEdit : Form
    {

        public event EventHandler OnSaved;

        private IHotelRepository HotelRepository { get; set; }


        public HotelEdit()
        {
            InitializeComponent();

            HotelRepository = new HotelRepository();

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtName.Text) || 
                String.IsNullOrEmpty(txtAddress.Text) ||
                String.IsNullOrEmpty(txtPhone.Text))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Neuspjesno snimanje";
            }
            else
            {
                HotelRepository HotelRepository = new HotelRepository();
                WindowsFormsHotel.Hotel Hotel = HotelRepository.Get();
                Hotel.Name = txtName.Text;
                Hotel.Address = txtAddress.Text;
                Hotel.Phone = txtPhone.Text;

                HotelRepository.Update(Hotel);

                /*if (HotelRepository.Update(Hotel))
                {
                    lblMessage.Text = "Uspjesno snimljeno";
                }
                else
                {
                    lblMessage.Text = "Neuspjesno snimanje";
                }*/

                if (OnSaved != null)
                {
                    OnSaved(this, e);
                }

                this.Close();
            }

            


            /*
             HotelRepository HotelRepository = new HotelRepository();
            WindowsFormsHotel.Hotel Hotel = HotelRepository.Get();
            lblMessage.Visible = false;
            

            if (String.IsNullOrEmpty(txtName.Text) ||
                String.IsNullOrEmpty(txtAddress.Text) ||
                txtPhone != null
                )
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Neuspjesno snimanje";
                
            }
            else
            {
                if (OnSaved != null)
                {
                    Hotel.Name = txtName.Text;
                    Hotel.Address = txtAddress.Text;
                    Hotel.Phone = txtPhone.Text;

                    OnSaved(this, e);
                    lblMessage.Visible = false;
                    this.Close();
                }
                
             */

        }
    }
}

