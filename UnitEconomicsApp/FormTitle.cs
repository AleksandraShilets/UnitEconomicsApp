using System;
using System.Drawing;
using System.Windows.Forms;

namespace UnitEconomicsApp
{
    public partial class FormTitle : Form
    {
        private string courseHeader = "КУРСОВАЯ РАБОТА ПО ДИСЦИПЛИНЕ\n\"ОБЪЕКТНО-ОРИЕНТИРОВАННОЕ ПРОГРАММИРОВАНИЕ\"";
        private string studentMeta = "студента I курса группы ИВТ-25\nШилец Александры Владиславовны";
        private string topicMeta = "на тему:\nИнформационная система для расчета юнит-экономики проекта";
        private string teachersMeta = "Руководители: ст. преп. Беднякова Т.М.,\nст. преп. Арефьева А.В.";
        private string currentYear = "2026 г.";

        public FormTitle()
        {
            InitializeComponent();

            this.Text = "Титульный лист проекта";
            this.Size = new Size(620, 460);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(250, 250, 250);

            InitializeTitleComponents();
        }

        private void InitializeTitleComponents()
        {
            Label lblHeader = new Label() { Text = courseHeader, Font = new Font("Arial", 9, FontStyle.Bold), TextAlign = ContentAlignment.TopCenter, Size = new Size(580, 40), Location = new Point(12, 20) };
            this.Controls.Add(lblHeader);

            Label lblTopic = new Label() { Text = topicMeta, Font = new Font("Times New Roman", 15, FontStyle.Bold | FontStyle.Italic), ForeColor = Color.DarkSlateBlue, TextAlign = ContentAlignment.MiddleCenter, Size = new Size(580, 65), Location = new Point(12, 110) };
            this.Controls.Add(lblTopic);

            Label lblStudent = new Label() { Text = studentMeta, Font = new Font("Arial", 11), Size = new Size(400, 50), Location = new Point(45, 210) };
            this.Controls.Add(lblStudent);

            Label lblTeachers = new Label() { Text = teachersMeta, Font = new Font("Arial", 11), TextAlign = ContentAlignment.TopRight, Size = new Size(350, 50), Location = new Point(230, 280) };
            this.Controls.Add(lblTeachers);

            Label lblYear = new Label() { Text = currentYear, Font = new Font("Arial", 10), TextAlign = ContentAlignment.BottomCenter, Size = new Size(100, 25), Location = new Point(260, 390) };
            this.Controls.Add(lblYear);

            Button btnEnter = new Button() { Text = "Запустить расчетную панель", Font = new Font("Arial", 10, FontStyle.Bold), Size = new Size(250, 42), Location = new Point(185, 340), BackColor = Color.LightSkyBlue };
            btnEnter.Click += new EventHandler(BtnEnter_Click);
            this.Controls.Add(btnEnter);
        }

        private void BtnEnter_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMain mainDashboard = new FormMain();
            mainDashboard.ShowDialog();
            this.Close();
        }

        private void FormTitle_Load(object sender, EventArgs e)
        {
        }
    }
}
