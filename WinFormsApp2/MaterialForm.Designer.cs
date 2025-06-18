namespace WinFormsApp2
{
    partial class MaterialForm
    {
        private System.ComponentModel.IContainer components = null;
        private ComboBox typeCombo;
        private TextBox nameText;
        private TextBox priceText;
        private TextBox stockText;
        private TextBox minQtyText;
        private TextBox packageQtyText;
        private TextBox unitText;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            var nameLabel = new Label();
            var typeLabel = new Label();
            var priceLabel = new Label();
            var stockLabel = new Label();
            var minQtyLabel = new Label();
            var packageQtyLabel = new Label();
            var unitLabel = new Label();
            var saveButton = new Button();
            var backButton = new Button();
            nameText = new TextBox();
            typeCombo = new ComboBox();
            priceText = new TextBox();
            stockText = new TextBox();
            minQtyText = new TextBox();
            packageQtyText = new TextBox();
            unitText = new TextBox();
            SuspendLayout();

            // nameLabel
            nameLabel.Location = new Point(20, 20);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(100, 20);
            nameLabel.Text = "Наименование:";

            // nameText
            nameText.Location = new Point(150, 20);
            nameText.Name = "nameText";
            nameText.Size = new Size(200, 26);

            // typeLabel
            typeLabel.Location = new Point(20, 60);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(100, 20);
            typeLabel.Text = "Тип материала:";

            // typeCombo
            typeCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            typeCombo.Location = new Point(150, 60);
            typeCombo.Name = "typeCombo";
            typeCombo.Size = new Size(200, 28);

            // priceLabel
            priceLabel.Location = new Point(20, 100);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new Size(100, 20);
            priceLabel.Text = "Цена за ед.:";

            // priceText
            priceText.Location = new Point(150, 100);
            priceText.Name = "priceText";
            priceText.Size = new Size(200, 26);

            // stockLabel
            stockLabel.Location = new Point(20, 140);
            stockLabel.Name = "stockLabel";
            stockLabel.Size = new Size(100, 20);
            stockLabel.Text = "На складе:";

            // stockText
            stockText.Location = new Point(150, 140);
            stockText.Name = "stockText";
            stockText.Size = new Size(200, 26);

            // minQtyLabel
            minQtyLabel.Location = new Point(20, 180);
            minQtyLabel.Name = "minQtyLabel";
            minQtyLabel.Size = new Size(100, 20);
            minQtyLabel.Text = "Мин. кол-во:";

            // minQtyText
            minQtyText.Location = new Point(150, 180);
            minQtyText.Name = "minQtyText";
            minQtyText.Size = new Size(200, 26);

            // packageQtyLabel
            packageQtyLabel.Location = new Point(20, 220);
            packageQtyLabel.Name = "packageQtyLabel";
            packageQtyLabel.Size = new Size(100, 20);
            packageQtyLabel.Text = "В упаковке:";

            // packageQtyText
            packageQtyText.Location = new Point(150, 220);
            packageQtyText.Name = "packageQtyText";
            packageQtyText.Size = new Size(200, 26);

            // unitLabel
            unitLabel.Location = new Point(20, 260);
            unitLabel.Name = "unitLabel";
            unitLabel.Size = new Size(100, 20);
            unitLabel.Text = "Ед. изм.:";

            // unitText
            unitText.Location = new Point(150, 260);
            unitText.Name = "unitText";
            unitText.Size = new Size(200, 26);

            // saveButton
            saveButton.BackColor = Color.FromArgb(84, 111, 148);
            saveButton.ForeColor = Color.White;
            saveButton.Location = new Point(100, 300);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(100, 30);
            saveButton.Text = "Сохранить";
            saveButton.Click += SaveButton_Click;

            // backButton
            backButton.BackColor = Color.FromArgb(84, 111, 148);
            backButton.ForeColor = Color.White;
            backButton.Location = new Point(210, 300);
            backButton.Name = "backButton";
            backButton.Size = new Size(100, 30);
            backButton.Text = "Назад";
            backButton.Click += (s, e) => Close();

            // MaterialForm
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(255, 255, 255);
            ClientSize = new Size(400, 500);
            Controls.Add(nameLabel);
            Controls.Add(nameText);
            Controls.Add(typeLabel);
            Controls.Add(typeCombo);
            Controls.Add(priceLabel);
            Controls.Add(priceText);
            Controls.Add(stockLabel);
            Controls.Add(stockText);
            Controls.Add(minQtyLabel);
            Controls.Add(minQtyText);
            Controls.Add(packageQtyLabel);
            Controls.Add(packageQtyText);
            Controls.Add(unitLabel);
            Controls.Add(unitText);
            Controls.Add(saveButton);
            Controls.Add(backButton);
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Name = "MaterialForm";
            Text = materialId.HasValue ? "Редактирование материала" : "Добавление материала";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}