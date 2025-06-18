namespace WinFormsApp2
{
    partial class SuppliersForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView suppliersGrid;
        private Button backButton;

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
            suppliersGrid = new DataGridView();
            backButton = new Button();
            ((System.ComponentModel.ISupportInitialize)suppliersGrid).BeginInit();
            SuspendLayout();

            // suppliersGrid
            suppliersGrid.Location = new Point(10, 10);
            suppliersGrid.Name = "suppliersGrid";
            suppliersGrid.Size = new Size(560, 300);
            suppliersGrid.ReadOnly = true;
            suppliersGrid.BackgroundColor = Color.FromArgb(171, 207, 206); // #ABCFCE
            suppliersGrid.AllowUserToAddRows = false;

            // backButton
            backButton.Text = "Назад";
            backButton.Location = new Point(460, 320);
            backButton.Name = "backButton";
            backButton.Size = new Size(100, 30);
            backButton.BackColor = Color.FromArgb(84, 111, 148); // #546F94
            backButton.ForeColor = Color.White;
            backButton.Click += (s, e) => Close();

            // SuppliersForm
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Text = "Поставщики материала";
            ClientSize = new Size(600, 400);
            BackColor = Color.FromArgb(255, 255, 255); // #FFFFFF
            Controls.Add(suppliersGrid);
            Controls.Add(backButton);
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Name = "SuppliersForm";

            ((System.ComponentModel.ISupportInitialize)suppliersGrid).EndInit();
            ResumeLayout(false);
        }
    }
}