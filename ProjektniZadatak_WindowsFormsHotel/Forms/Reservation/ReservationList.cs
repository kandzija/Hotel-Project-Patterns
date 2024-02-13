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

namespace WindowsFormsHotel.Forms.Reservation
{
    public partial class ReservationList : Form
    {

        #region Fields

        private readonly string GuestColumnKey = "Gost";
        private readonly string RoomColumnKey = "Soba";
        private readonly string DateArrivalColumnKey = "Datum dolaska";
        private readonly string DateDepartureColumnKey = "Datum odlaska";

        private readonly string GridEditColumnKey = "btnEdit";
        private readonly string GridDeleteColumnKey = "btnDelete";
        private readonly string GridIdColumnKey = "Id";

        #endregion Fields

        #region Properties

        private IReservationRepository ReservationRepository { get; set; }

        #endregion Properties

        public ReservationList()
        {
            InitializeComponent();

            ReservationRepository = new ReservationRepository();

            SetGrid();

            BindData();
        }

        #region Methods

        private void SetGrid()
        {
            //TODO postavljanje kolona grida.
            gridData.ColumnCount = 5;
            gridData.AutoGenerateColumns = false;

            gridData.Columns[0].Name = GridIdColumnKey;
            gridData.Columns[0].DataPropertyName = GridIdColumnKey;
            gridData.Columns[0].Visible = false;

            gridData.Columns[1].Name = GuestColumnKey;
            gridData.Columns[1].DataPropertyName = "Guest.FirstName";
            gridData.Columns[2].Name = RoomColumnKey;
            gridData.Columns[2].DataPropertyName = "Room.Number";
            gridData.Columns[3].Name = DateArrivalColumnKey;
            gridData.Columns[3].DataPropertyName = "DateArrival";
            gridData.Columns[4].Name = DateDepartureColumnKey;
            gridData.Columns[4].DataPropertyName = "DateDeparture";


            //TODO dodavanje akcija u grid
            DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
            gridData.Columns.Add(btnEdit);
            btnEdit.HeaderText = String.Empty;
            btnEdit.Text = "Izmijeni";
            btnEdit.Name = GridEditColumnKey;
            btnEdit.UseColumnTextForButtonValue = true;

            DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
            gridData.Columns.Add(btnDelete);
            btnDelete.HeaderText = String.Empty;
            btnDelete.Text = "Obriši";
            btnDelete.Name = GridDeleteColumnKey;
            btnDelete.UseColumnTextForButtonValue = true;
        }

        private void BindData()
        {
            //TODO metoda koju pozivamo kad je potrebno prikazati podatke
            var filter = new ReservationFilter();
            filter.Text = txtSearch.Text;

            var data = ReservationRepository.Get(filter);
            gridData.DataSource = data;
        }

        #endregion Methods

        #region UI Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ReservationEdit form = new ReservationEdit();
            form.OnSaved += Form_OnSaved;
            form.Show();
        }

        private void Form_OnSaved(object sender, EventArgs e)
        {
            BindData();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtSearch.Text = String.Empty;

            BindData();
        }

        private void gridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int editColumnIndex = gridData.Columns[GridEditColumnKey].Index;
                int deleteColumnIndex = gridData.Columns[GridDeleteColumnKey].Index;
                int numberColumnIndex = gridData.Columns[GridDeleteColumnKey].Index;

                //long rezerv = (long)gridData.Rows[e.RowIndex].Cells[GuestColumnKey].Value;
                Guid id = (Guid)gridData.Rows[e.RowIndex].Cells[GridIdColumnKey].Value;

                if (e.ColumnIndex == editColumnIndex)
                {
                    ReservationEdit editForm = new ReservationEdit(true, id);
                    editForm.OnSaved += Form_OnSaved;
                    editForm.Show();
                }
                else if (e.ColumnIndex == deleteColumnIndex)
                {
                    MessageBoxButtons amessageBoxButtons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show($"Jeste li sigurni da želite obrisati rezervaciju gosta: {gridData.Rows[e.RowIndex].Cells[GuestColumnKey].Value}", "Brisanje rezervacije", amessageBoxButtons);

                    if (result == DialogResult.Yes)
                    {
                        ReservationRepository.Delete(id);

                        BindData();
                    }
                }
            }
        }


        private void gridData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            WindowsFormsHotel.Reservation data = gridData.Rows[e.RowIndex].DataBoundItem as WindowsFormsHotel.Reservation;

            //TODO na ovaj način dolazimo do podataka broja sobe i gostovih imena i prezimena.
            gridData.Rows[e.RowIndex].Cells[GuestColumnKey].Value = $"{data.Guest.FirstName} {data.Guest.LastName}";
            gridData.Rows[e.RowIndex].Cells[RoomColumnKey].Value = data.Room.Number;
        }

        #endregion UI Events
    }
}
