using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class SuppliersForm : Form
    {
        private readonly string connectionString;
        private readonly int materialId;

        public SuppliersForm(string connString, int matId)
        {
            connectionString = connString;
            materialId = matId;
            InitializeComponent();
            SetupFormStyle();
            LoadSuppliers();
        }

        private void SetupFormStyle()
        {
            foreach (Control control in Controls)
            {
                control.Font = new Font("Comic Sans MS", 10);
            }
        }

        private void LoadSuppliers()
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                var query = @"
                    SELECT s.supplier_name, s.rating, s.start_date
                    FROM supplier s
                    JOIN material_supplier ms ON s.supplier_id = ms.supplier_id
                    WHERE ms.material_id = @materialId";
                using var adapter = new MySqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@materialId", materialId);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                suppliersGrid.DataSource = dataTable;

                suppliersGrid.Columns["supplier_name"].HeaderText = "Наименование поставщика";
                suppliersGrid.Columns["rating"].HeaderText = "Рейтинг";
                suppliersGrid.Columns["start_date"].HeaderText = "Дата начала работы";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки поставщиков: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
