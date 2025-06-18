using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormsApp2
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView materialsGrid;
        private PictureBox logoPictureBox;
        private Button addButton;
        private Button suppliersButton;

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
            materialsGrid = new DataGridView();
            logoPictureBox = new PictureBox();
            addButton = new Button();
            suppliersButton = new Button();
            ((System.ComponentModel.ISupportInitialize)materialsGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            SuspendLayout();

            // materialsGrid
            materialsGrid.Location = new Point(10, 70);
            materialsGrid.Name = "materialsGrid";
            materialsGrid.Size = new Size(760, 400);
            materialsGrid.ReadOnly = true;
            materialsGrid.BackgroundColor = Color.FromArgb(171, 207, 206); // #ABCFCE
            materialsGrid.AllowUserToAddRows = false;
            materialsGrid.DoubleClick += MaterialsGrid_DoubleClick;

            // logoPictureBox
            logoPictureBox.Image = Properties.Resources.logo;
            logoPictureBox.Size = new Size(100, 50);
            logoPictureBox.Location = new Point(10, 10);
            logoPictureBox.Name = "logoPictureBox";
            logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            // addButton
            addButton.Text = "Добавить материал";
            addButton.Location = new Point(10, 480);
            addButton.Name = "addButton";
            addButton.Size = new Size(150, 30);
            addButton.BackColor = Color.FromArgb(84, 111, 148); // #546F94
            addButton.ForeColor = Color.White;
            addButton.Click += AddButton_Click;

            // suppliersButton
            suppliersButton.Text = "Поставщики";
            suppliersButton.Location = new Point(170, 480);
            suppliersButton.Name = "suppliersButton";
            suppliersButton.Size = new Size(150, 30);
            suppliersButton.BackColor = Color.FromArgb(84, 111, 148);
            suppliersButton.ForeColor = Color.White;
            suppliersButton.Click += SuppliersButton_Click;

            // MainForm
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Text = "Учет материалов - Мозаика";
            ClientSize = new Size(800, 600);
            BackColor = Color.FromArgb(255, 255, 255); // #FFFFFF
            Icon = Properties.Resources.app;
            Font = new Font("Comic Sans MS", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Controls.Add(logoPictureBox);
            Controls.Add(materialsGrid);
            Controls.Add(addButton);
            Controls.Add(suppliersButton);
            Name = "MainForm";

            ((System.ComponentModel.ISupportInitialize)materialsGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ResumeLayout(false);
        }
    }
}