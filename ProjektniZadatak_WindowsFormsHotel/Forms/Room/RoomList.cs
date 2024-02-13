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

namespace WindowsFormsHotel.Forms.Room
{
    public partial class RoomList : Form
    {
        #region Fields

        private readonly string RoomColumnKey = "Jednokrevetna";

        private readonly string GridEditColumnKey = "btnEdit";
        private readonly string GridDeleteColumnKey = "btnDelete";
        private readonly string GridNumberColumnKey = "Broj sobe";
        private readonly string GridIdColumnKey = "Id";

        #endregion Fields

        #region Properties

        private IRoomRepository RoomRepository { get; set; }

        #endregion Properties

        public RoomList()
        {
            InitializeComponent();

            RoomRepository = new RoomRepository();

            SetGrid();

            BindData();
        }

        #region Methods

        private void SetGrid()
        {
            //TODO postavljanje kolona grida.
            gridData.ColumnCount = 3;
            gridData.AutoGenerateColumns = false;

            gridData.Columns[0].Name = GridIdColumnKey;
            gridData.Columns[0].DataPropertyName = GridIdColumnKey;
            gridData.Columns[0].Visible = false;

            gridData.Columns[1].Name = GridNumberColumnKey;
            gridData.Columns[1].DataPropertyName = "Number";
            gridData.Columns[2].Name = RoomColumnKey;
            gridData.Columns[2].DataPropertyName = "OneBed";

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
            var filter = new RoomFilter();
            filter.Text = txtSearch.Text;

            var data = RoomRepository.Get(filter);
            gridData.DataSource = data;
        }

        #endregion Methods

        private void btnAdd_Click(object sender, EventArgs e)
        {
            RoomEdit form = new RoomEdit();
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

                /*long billNumber = (long)gridData.Rows[e.RowIndex].Cells[GridNumberColumnKey].Value;*/
                Guid id = (Guid)gridData.Rows[e.RowIndex].Cells[GridIdColumnKey].Value;

                if (e.ColumnIndex == editColumnIndex)
                {
                    RoomEdit editForm = new RoomEdit(true, id);
                    editForm.OnSaved += Form_OnSaved;
                    editForm.Show();
                }
                else if (e.ColumnIndex == deleteColumnIndex)
                {
                    MessageBoxButtons amessageBoxButtons = MessageBoxButtons.YesNo;
                    DialogResult result = MessageBox.Show($"Jeste li sigurni da želite obrisati sobu broj: {gridData.Rows[e.RowIndex].Cells[GridNumberColumnKey].Value}", "Brisanje sobe", amessageBoxButtons);

                    if (result == DialogResult.Yes)
                    {
                        RoomRepository.Delete(id);

                        BindData();
                    }
                }
            }
        }

        private void gridData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            WindowsFormsHotel.Room data = gridData.Rows[e.RowIndex].DataBoundItem as WindowsFormsHotel.Room;

            //TODO na ovaj način dolazimo do podataka broja sobe i gostovih imena i prezimena.
            /*gridData.Rows[e.RowIndex].Cells[GuestColumnKey].Value = $"{data.Guest.FirstName} {data.Guest.LastName}";
            gridData.Rows[e.RowIndex].Cells[RoomColumnKey].Value = data.Room.Number;*/
        }
    }
}
