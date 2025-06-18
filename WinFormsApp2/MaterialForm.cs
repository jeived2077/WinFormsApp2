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
    public partial class MaterialForm : Form
    {
        private readonly string connectionString;
        private readonly int? materialId;

        public MaterialForm(string connString, int? id)
        {
            connectionString = connString;
            materialId = id;
            InitializeComponent();
            SetupFormStyle();
            LoadMaterialTypes();
            if (materialId.HasValue)
                LoadMaterialData();
        }

        private void SetupFormStyle()
        {
            foreach (Control control in Controls)
            {
                control.Font = new Font("Comic Sans MS", 10);
            }
        }

        private void LoadMaterialTypes()
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                var query = "SELECT material_type_id, type_name FROM material_type";
                using var cmd = new MySqlCommand(query, connection);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    typeCombo.Items.Add(new ComboItem(reader.GetString("type_name"), reader.GetInt32("material_type_id")));
                }
                if (typeCombo.Items.Count > 0)
                    typeCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки типов: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMaterialData()
        {
            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                var query = "SELECT * FROM material WHERE material_id = @id";
                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", materialId);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    nameText.Text = reader.GetString("material_name");
                    priceText.Text = reader.GetDecimal("unit_price").ToString("F2");
                    stockText.Text = reader.GetDecimal("stock_quantity").ToString("F2");
                    minQtyText.Text = reader.GetDecimal("min_quantity").ToString("F2");
                    packageQtyText.Text = reader.GetInt32("package_quantity").ToString();
                    unitText.Text = reader.GetString("unit_measure");

                    int typeId = reader.GetInt32("material_type_id");
                    foreach (ComboItem item in typeCombo.Items)
                    {
                        if (item.Value == typeId)
                        {
                            typeCombo.SelectedItem = item;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки материала: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                using var connection = new MySqlConnection(connectionString);
                connection.Open();
                string query = materialId.HasValue ?
                    "UPDATE material SET material_name = @name, material_type_id = @typeId, unit_price = @price, " +
                    "stock_quantity = @stock, min_quantity = @minQty, package_quantity = @package, unit_measure = @unit " +
                    "WHERE material_id = @id" :
                    "INSERT INTO material (material_name, material_type_id, unit_price, stock_quantity, min_quantity, " +
                    "package_quantity, unit_measure) VALUES (@name, @typeId, @price, @stock, @minQty, @package, @unit)";

                using var cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", nameText.Text);
                cmd.Parameters.AddWithValue("@typeId", ((ComboItem)typeCombo.SelectedItem).Value);
                cmd.Parameters.AddWithValue("@price", decimal.Parse(priceText.Text));
                cmd.Parameters.AddWithValue("@stock", decimal.Parse(stockText.Text));
                cmd.Parameters.AddWithValue("@minQty", decimal.Parse(minQtyText.Text));
                cmd.Parameters.AddWithValue("@package", int.Parse(packageQtyText.Text));
                cmd.Parameters.AddWithValue("@unit", unitText.Text);
                if (materialId.HasValue)
                    cmd.Parameters.AddWithValue("@id", materialId);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Материал успешно сохранен.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(nameText.Text))
            {
                MessageBox.Show("Введите наименование материала.", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (typeCombo.SelectedItem == null)
            {
                MessageBox.Show("Выберите тип материала.", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!decimal.TryParse(priceText.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Цена должна быть неотрицательным числом с двумя знаками после запятой.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!decimal.TryParse(stockText.Text, out decimal stock) || stock < 0)
            {
                MessageBox.Show("Количество на складе не может быть отрицательным.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!decimal.TryParse(minQtyText.Text, out decimal minQty) || minQty < 0)
            {
                MessageBox.Show("Минимальное количество не может быть отрицательным.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!int.TryParse(packageQtyText.Text, out int packageQty) || packageQty <= 0)
            {
                MessageBox.Show("Количество в упаковке должно быть положительным целым числом.",
                    "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(unitText.Text))
            {
                MessageBox.Show("Введите единицу измерения.", "Ошибка ввода",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
    }
    public class ComboItem
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public ComboItem(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString() => Text;
    }
}
