using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsHotel.Forms.Hotel;
using WindowsFormsHotel.Code;

namespace WindowsFormsHotel
{
    public partial class HotelDetail : Form
    {
        public HotelDetail()
        {
            InitializeComponent();

            HotelRepository HotelRepository = new HotelRepository();
            Hotel Hotel = HotelRepository.Get();
            lblNameData.Text = Hotel.Name;
            lblAddressData.Text = Hotel.Address;
            lblPhoneData.Text = Hotel.Phone;
        }

        private void BindData()
        {
            HotelRepository HotelRepository = new HotelRepository();
            Hotel Hotel = HotelRepository.Get();
            lblNameData.Text = Hotel.Name;
            lblAddressData.Text = Hotel.Address;
            lblPhoneData.Text = Hotel.Phone;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            HotelEdit edit = new HotelEdit();
            edit.OnSaved += Edit_OnSaved;
            edit.Show();
        }

        private void Edit_OnSaved(object sender, EventArgs e)
        {
            BindData();
        }

        private void lblPhoneData_Click(object sender, EventArgs e)
        {

        }

        private void HotelDetail_Load(object sender, EventArgs e)
        {

        }
    }
}
