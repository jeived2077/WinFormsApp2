using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp2
{
    public partial class MainForm : Form
    {
        private readonly string connectionString = "Server=127.0.0.1;Database=mosaic_materials;Uid=root;Pwd=root;Port=3307;";

        public MainForm()
        {
            InitializeComponent();
            SetupFormStyle();
            LoadMaterials();
        }

        private void SetupFormStyle()
        {
            foreach (Control control in Controls)
            {
                control.Font = new Font("Comic Sans MS", 10);
            }
        }

        private void LoadMaterials()
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                var query = @"
                    SELECT m.material_id, m.material_name, mt.type_name, m.unit_price, 
                           m.stock_quantity, m.min_quantity, m.package_quantity, m.unit_measure,
                           CASE 
                               WHEN m.stock_quantity < m.min_quantity 
                               THEN ROUND(
                                   CEILING((m.min_quantity - m.stock_quantity) / m.package_quantity) * m.package_quantity * m.unit_price, 
                                   2
                               )
                               ELSE 0 
                           END AS min_purchase_cost
                    FROM material m
                    JOIN material_type mt ON m.material_type_id = mt.material_type_id";
                using var adapter = new MySqlDataAdapter(query, connection);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                materialsGrid.DataSource = dataTable;

                materialsGrid.Columns["material_id"].Visible = false;
                materialsGrid.Columns["material_name"].HeaderText = "Наименование";
                materialsGrid.Columns["type_name"].HeaderText = "Тип";
                materialsGrid.Columns["unit_price"].HeaderText = "Цена за ед.";
                materialsGrid.Columns["stock_quantity"].HeaderText = "На складе";
                materialsGrid.Columns["min_quantity"].HeaderText = "Мин. кол-во";
                materialsGrid.Columns["package_quantity"].HeaderText = "В упаковке";
                materialsGrid.Columns["unit_measure"].HeaderText = "Ед. изм.";
                materialsGrid.Columns["min_purchase_cost"].HeaderText = "Стоимость мин. партии";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            var materialForm = new MaterialForm(connectionString, null);
            materialForm.ShowDialog();
            LoadMaterials();
        }

        private void MaterialsGrid_DoubleClick(object sender, EventArgs e)
        {
            if (materialsGrid.SelectedRows.Count > 0)
            {
                int materialId = Convert.ToInt32(materialsGrid.SelectedRows[0].Cells["material_id"].Value);
                var materialForm = new MaterialForm(connectionString, materialId);
                materialForm.ShowDialog();
                LoadMaterials();
            }
        }

        private void SuppliersButton_Click(object sender, EventArgs e)
        {
            if (materialsGrid.SelectedRows.Count > 0)
            {
                int materialId = Convert.ToInt32(materialsGrid.SelectedRows[0].Cells["material_id"].Value);
                var suppliersForm = new SuppliersForm(connectionString, materialId);
                suppliersForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите материал для просмотра поставщиков.", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public int CalculateProductionQuantity(int productTypeId, int materialTypeId, int materialQuantity,
            double param1, double param2)
        {
            if (materialQuantity <= 0 || param1 <= 0 || param2 <= 0)
                return -1;

            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                var productQuery = "SELECT product_coefficient FROM product_type WHERE product_type_id = @id";
                using var cmd = new MySqlCommand(productQuery, connection);
                cmd.Parameters.AddWithValue("@id", productTypeId);
                var productCoefficient = cmd.ExecuteScalar();
                if (productCoefficient == null)
                    return -1;
                double coefficient = Convert.ToDouble(productCoefficient);

                var materialQuery = "SELECT loss_percentage FROM material_type WHERE material_type_id = @id";
                cmd.CommandText = materialQuery;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", materialTypeId);
                var lossPercentage = cmd.ExecuteScalar();
                if (lossPercentage == null)
                    return -1;
                double loss = Convert.ToDouble(lossPercentage);

                double materialPerUnit = param1 * param2 * coefficient;
                materialPerUnit /= 1 - loss;
                int units = (int)Math.Floor(materialQuantity / materialPerUnit);
                return units;
            }
            catch
            {
                return -1;
            }
        }
    }
}