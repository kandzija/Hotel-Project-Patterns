using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using WindowsFormsHotel.Code;

namespace WindowsFormsHotel.Forms.Guest
{
    public partial class GuestEdit : Form
    {
        public event EventHandler OnSaved;

        #region Properties

        private bool EditMode { get; set; }

        private Guid? Id { get; set; }

        private IBillRepository BillRepository { get; set; }

        private IGuestRepository GuestRepository { get; set; }

        private IRoomRepository RoomRepository { get; set; }

        private IHotelRepository HotelRepository { get; set; }

        #endregion Properties

        public GuestEdit(bool editMode = false, Guid? id = null)
        {
            InitializeComponent();

            BillRepository = new BillRepository();
            GuestRepository = new GuestRepository();
            RoomRepository = new RoomRepository();
            HotelRepository = new HotelRepository();

            EditMode = editMode;
            Id = id;

            lblMessage.Visible = false;

            BindData();
        }

        #region Methods

        private void BindData()
        {
            if (EditMode)
            {
                var entity = GuestRepository.Get(Id.Value);
                txtFirstName.Text = entity.FirstName;
                txtLastName.Text = entity.LastName;
                txtPin.Text = entity.PIN;
                txtAddress.Text = entity.Address;
                txtPhone.Text = entity.Phone;
            }
        }

        #endregion Methods

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtFirstName.Text) &&
                !String.IsNullOrEmpty(txtLastName.Text) &&
                !String.IsNullOrEmpty(txtPin.Text) &&
                !String.IsNullOrEmpty(txtAddress.Text) &&
                !String.IsNullOrEmpty(txtPhone.Text))
            {
                WindowsFormsHotel.Guest entity = null;
                if (EditMode)
                {
                    entity = GuestRepository.Get(Id.Value);
                }
                /**/
                else
                {
                    entity = new WindowsFormsHotel.Guest();

                    var hotel = HotelRepository.Get();
                    if (hotel != null)
                    {
                        entity.HotelId = hotel.Id;
                    }
                }

                entity.FirstName = txtFirstName.Text;
                entity.LastName = txtLastName.Text;
                entity.PIN = txtPin.Text;
                entity.Address = txtAddress.Text;
                entity.Phone = txtPhone.Text;

                try
                {
                    if (EditMode)
                    {
                        GuestRepository.Update(entity);
                    }
                    else
                    {
                        GuestRepository.Create(entity);
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Neuspjesno snimanje";
                }

                //GuestRepository.Create(entity);

                if (OnSaved != null)
                {
                    OnSaved(this, e);
                }

                lblMessage.Visible = false;
                this.Close();
            }
            else
            {
                lblMessage.Visible = true;
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

    



