using System;
using System.Windows.Forms;
using WindowsFormsHotel.Code;

namespace WindowsFormsHotel.Forms.Guest
{
    public partial class GuestList : Form
    {

        #region Fields

        private readonly string GuestColumnKey = "Gost";

        private readonly string GridEditColumnKey = "btnEdit";
        private readonly string GridDeleteColumnKey = "btnDelete";
        private readonly string GridIdColumnKey = "Id";
        private readonly string GridFNameColumnKey = "Ime";
        private readonly string GridLNameColumnKey = "Prezime";

        #endregion Fields

        private IGuestRepository GuestRepository { get; set; }

        public GuestList()
        {
            InitializeComponent();

            GuestRepository = new GuestRepository();

            SetGrid();

            BindData();
        }

        #region Methods

        private void SetGrid()
        {
            //TODO postavljanje kolona grida.
            gridData.ColumnCount = 6;
            gridData.AutoGenerateColumns = false;

            gridData.Columns[0].Name = GridIdColumnKey;
            gridData.Columns[0].DataPropertyName = GridIdColumnKey;
            gridData.Columns[0].Visible = false;

            gridData.Columns[1].Name = GridFNameColumnKey;
            gridData.Columns[1].DataPropertyName = "FirstName";
            gridData.Columns[2].Name = GridLNameColumnKey;
            gridData.Columns[2].DataPropertyName = "LastName";
            gridData.Columns[3].Name = "PIN";
            gridData.Columns[3].DataPropertyName = "PIN";
            gridData.Columns[4].Name = "Adresa";
            gridData.Columns[4].DataPropertyName = "Address";
            gridData.Columns[5].Name = "Broj mobitela";
            gridData.Columns[5].DataPropertyName = "Phone";

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
            var filter = new GuestFilter();
            filter.Text = txtSearch.Text;

            var data = GuestRepository.Get(filter);
            gridData.DataSource = data;
        }

        #endregion Methods

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GuestEdit form = new GuestEdit();
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

        private void gridData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                int editColumnIndex = gridData.Columns[GridEditColumnKey].Index;
                int deleteColumnIndex = gridData.Columns[GridDeleteColumnKey].Index;

                /*long guestSelection = (long)gridData.Rows[e.RowIndex].Cells[GridFNameColumnKey].Value + (long)gridData.Rows[e.RowIndex].Cells[GridLNameColumnKey].Value;*/
                Guid id = (Guid)gridData.Rows[e.RowIndex].Cells[GridIdColumnKey].Value;

            if (e.ColumnIndex == editColumnIndex)
            {
                GuestEdit editForm = new GuestEdit(true, id);
                editForm.OnSaved += Form_OnSaved;
                editForm.Show();
            }
            else if (e.ColumnIndex == deleteColumnIndex)
            {
                MessageBoxButtons amessageBoxButtons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show($"Jeste li sigurni da želite obrisati gosta: {gridData.Rows[e.RowIndex].Cells[GridFNameColumnKey].Value}", "Brisanje gosta", amessageBoxButtons);

                if (result == DialogResult.Yes)
                {
                    GuestRepository.Delete(id);

                    BindData();
                }
            }
            }
        

        /*private void gridData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            WindowsFormsHotel.Bill data = gridData.Rows[e.RowIndex].DataBoundItem as WindowsFormsHotel.Bill;

            //TODO na ovaj način dolazimo do podataka broja sobe i gostovih imena i prezimena.
            gridData.Rows[e.RowIndex].Cells[GuestColumnKey].Value = $"{data.Guest.FirstName} {data.Guest.LastName}";
        }*/

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
