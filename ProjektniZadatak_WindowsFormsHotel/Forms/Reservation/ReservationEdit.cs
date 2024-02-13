using System;
using System.Globalization;
using System.Windows.Forms;
using WindowsFormsHotel.Code;

namespace WindowsFormsHotel.Forms.Reservation
{
    public partial class ReservationEdit : Form
    {

        public event EventHandler OnSaved;

        private bool EditMode { get; set; }

        private Guid? Id { get; set; }

        private IBillRepository BillRepository { get; set; }

        private IGuestRepository GuestRepository { get; set; }

        private IRoomRepository RoomRepository { get; set; }

        private IHotelRepository HotelRepository { get; set; }

        private IReservationRepository ReservationRepository { get; set; }

        public ReservationEdit(bool editMode = false, Guid? id = null)
        {
            InitializeComponent();

            BillRepository = new BillRepository();
            GuestRepository = new GuestRepository();
            RoomRepository = new RoomRepository();
            HotelRepository = new HotelRepository();
            ReservationRepository = new ReservationRepository();

            EditMode = editMode;
            Id = id;

            lblMessage.Visible = false;

            BindData();
        }

        #region Methods

        private void BindData()
        {
            var guests = GuestRepository.Get();
            cmbGuest.DisplayMember = nameof(WindowsFormsHotel.Guest.LastName);
            cmbGuest.ValueMember = nameof(WindowsFormsHotel.Guest.Id);
            cmbGuest.DataSource = guests;

            var rooms = RoomRepository.Get();
            cmbRoom.DisplayMember = nameof(WindowsFormsHotel.Room.Number);
            cmbRoom.ValueMember = nameof(WindowsFormsHotel.Room.Id);
            cmbRoom.DataSource = rooms;

            if (EditMode)
            {
                var entity = ReservationRepository.Get(Id.Value);
                cmbGuest.SelectedIndex = cmbGuest.FindString(entity.Guest.LastName.ToString());
                cmbRoom.SelectedIndex = cmbRoom.FindString(entity.Room.Number.ToString());
                dateArrival.Value = entity.DateArrival;
                dateDeparture.Value = entity.DateDeparture;
            }
        }

        #endregion Methods

        #region UI Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            //TODO provjerava se jesu li svi elementi unešeni
            if (cmbGuest.SelectedValue != null &&
                cmbRoom.SelectedValue != null &&
                DateTime.Compare(dateArrival.Value, dateDeparture.Value) <0
                )
            {
                WindowsFormsHotel.Reservation entity = null;
                if (EditMode)
                {
                    //TODO edit mode - moramo dohvatiti postojeći entitet
                    entity = ReservationRepository.Get(Id.Value);
                }
                else
                {
                    //TODO create mode - moramo kreirati entitet
                    entity = new WindowsFormsHotel.Reservation();

                    var hotel = HotelRepository.Get();
                    if (hotel != null)
                    {
                        entity.HotelId = hotel.Id;
                    }
                }

                entity.GuestId = (Guid)cmbGuest.SelectedValue;
                entity.RoomId = (Guid)cmbRoom.SelectedValue;
                entity.DateArrival = dateArrival.Value;
                entity.DateDeparture = dateDeparture.Value;

                try
                {
                    if (EditMode)
                    {
                        ReservationRepository.Update(entity);
                    }
                    else
                    {
                        ReservationRepository.Create(entity);
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
