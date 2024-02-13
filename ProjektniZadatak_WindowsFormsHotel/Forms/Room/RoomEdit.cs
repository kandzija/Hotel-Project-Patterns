using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsHotel.Code;

namespace WindowsFormsHotel.Forms.Room
{
    public partial class RoomEdit : Form
    {

        public event EventHandler OnSaved;

        private bool EditMode { get; set; }

        private Guid? Id { get; set; }

        private IBillRepository BillRepository { get; set; }

        private IGuestRepository GuestRepository { get; set; }

        private IRoomRepository RoomRepository { get; set; }

        private IHotelRepository HotelRepository { get; set; }

        public RoomEdit(bool editMode = false, Guid? id = null)
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
                var entity = RoomRepository.Get(Id.Value);
                if(entity.OneBed == true)
                {
                    cbRoom.Checked = true;
                }
                else
                {
                    cbRoom.Checked = false;
                }
                txtNumber.Text = Convert.ToString(entity.Number);
            }
        }


        #endregion Methods

        #region UI Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtNumber.Text) &&
                (Int32.Parse(txtNumber.Text)) >0 )
            {
                WindowsFormsHotel.Room entity = null;
                if (EditMode)
                {
                    //TODO edit mode - moramo dohvatiti postojeći entitet
                    entity = RoomRepository.Get(Id.Value);
                }
                else
                {
                    //TODO create mode - moramo kreirati entitet
                    entity = new WindowsFormsHotel.Room();

                    var hotel = HotelRepository.Get();
                    if (hotel != null)
                    {
                        entity.HotelId = hotel.Id;
                    }
                }

                //entity.Number = Int32.Parse(txtNumber.Text);

                entity.Number = Convert.ToInt32(txtNumber.Text);

                if(cbRoom.Checked == true)
                {
                    entity.OneBed = true;
                }
                else
                { 
                    entity.OneBed = false;
                }
                //entity.OneBed = cmbRoom.SelectedItem;

                try
                {
                    if (EditMode)
                    {
                        RoomRepository.Update(entity);
                    }
                    else
                    {
                        RoomRepository.Create(entity);
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Dogodila se greška prilikom snimanja. Pokušajte ponovno kasnije.";
                }

                //TODO pozivamo event/događaj koji javi BillList formi da se odradilo snimanje i da se mogu ponovno učitati podatci u grid zbog izmjena na računu na ovoj formi.
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

        #endregion UI Events
    }
}
