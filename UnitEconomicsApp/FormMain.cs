using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UnitEconomicsApp
{
    public partial class FormMain : Form
    {
        private List<MarketingChannel> activeChannels = new List<MarketingChannel>();

        private TextBox txtChannelName;
        private TextBox txtChannelBudget;
        private TextBox txtChannelClicks;
        private NumericUpDown numImpressions;
        private NumericUpDown numBuyers;
        private ComboBox cmbAverageCheck;
        private DataGridView dgvEconomicGrid;
        private Label lblIntegralsSummary;

        public FormMain()
        {
            this.Text = "Панель анализа Юнит-Экономики проекта";
            this.Size = new Size(880, 540);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            InitializeDashboardComponents();
            InjectInitialDemoBusinessData();
            RefreshDataGridAndCalculateTotals();
        }

        private void InjectInitialDemoBusinessData()
        {
            activeChannels.Add(new MarketingChannel("Яндекс.Директ", 40000, 80000, 1600, 45, 2000));
            activeChannels.Add(new MarketingChannel("VK Реклама", 20000, 50000, 1100, 30, 1500));
        }

        private void InitializeDashboardComponents()
        {
            GroupBox gbInput = new GroupBox() { Text = " Ввод параметров рекламного канала воронки продаж ", Location = new Point(15, 15), Size = new Size(830, 95) };
            this.Controls.Add(gbInput);

            Label l1 = new Label() { Text = "Название:", Location = new Point(10, 25), Size = new Size(65, 20) };
            txtChannelName = new TextBox() { Location = new Point(10, 45), Size = new Size(110, 20) };

            Label l2 = new Label() { Text = "Бюджет (руб):", Location = new Point(130, 25), Size = new Size(85, 20) };
            txtChannelBudget = new TextBox() { Location = new Point(130, 45), Size = new Size(85, 20) };

            Label l3 = new Label() { Text = "Клики:", Location = new Point(225, 25), Size = new Size(50, 20) };
            txtChannelClicks = new TextBox() { Location = new Point(225, 45), Size = new Size(65, 20) };

            Label l4 = new Label() { Text = "Показы:", Location = new Point(300, 25), Size = new Size(60, 20) };
            numImpressions = new NumericUpDown() { Location = new Point(300, 45), Size = new Size(75, 20), Maximum = 1000000, Value = 10000 };

            Label l5 = new Label() { Text = "Покупатели:", Location = new Point(385, 25), Size = new Size(80, 20) };
            numBuyers = new NumericUpDown() { Location = new Point(385, 45), Size = new Size(70, 20), Maximum = 100000 };

            Label l6 = new Label() { Text = "Ср. чек (руб):", Location = new Point(465, 25), Size = new Size(80, 20) };
            cmbAverageCheck = new ComboBox() { Location = new Point(465, 45), Size = new Size(85, 20), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbAverageCheck.Items.AddRange(new string[] { "500", "1000", "1500", "2000", "5000" });
            cmbAverageCheck.SelectedIndex = 2;

            gbInput.Controls.AddRange(new Control[] { l1, txtChannelName, l2, txtChannelBudget, l3, txtChannelClicks, l4, numImpressions, l5, numBuyers, l6, cmbAverageCheck });

            Button btnAddChannel = new Button() { Text = "➕ Рассчитать и добавить", Location = new Point(565, 41), Size = new Size(150, 26), BackColor = Color.LightGreen, Font = new Font("Arial", 8, FontStyle.Bold) };
            btnAddChannel.Click += new EventHandler(BtnAddChannel_Click);
            gbInput.Controls.Add(btnAddChannel);

            Button btnReset = new Button() { Text = "🗑 Очистить", Location = new Point(725, 41), Size = new Size(85, 26), BackColor = Color.MistyRose };
            btnReset.Click += (s, e) => { activeChannels.Clear(); RefreshDataGridAndCalculateTotals(); };
            gbInput.Controls.Add(btnReset);

            dgvEconomicGrid = new DataGridView()
            {
                Location = new Point(15, 125),
                Size = new Size(830, 240),
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White
            };
            dgvEconomicGrid.ColumnCount = 8;
            dgvEconomicGrid.Columns[0].Name = "Маркетинговый Канал"; dgvEconomicGrid.Columns[0].Width = 140;
            dgvEconomicGrid.Columns[1].Name = "Бюджет (руб)";
            dgvEconomicGrid.Columns[2].Name = "Показы";
            dgvEconomicGrid.Columns[3].Name = "Клики";
            dgvEconomicGrid.Columns[4].Name = "Покупатели";
            dgvEconomicGrid.Columns[5].Name = "CPC (Клик)";
            dgvEconomicGrid.Columns[6].Name = "CAC (Клиент)";
            dgvEconomicGrid.Columns[7].Name = "Выручка (руб)";
            this.Controls.Add(dgvEconomicGrid);

            GroupBox gbSummary = new GroupBox() { Text = " Интегральные итоги и выводы по экономике проекта ", Location = new Point(15, 380), Size = new Size(830, 95) };
            lblIntegralsSummary = new Label() { Location = new Point(15, 25), Size = new Size(800, 60), Font = new Font("Courier New", 10, FontStyle.Bold) };
            gbSummary.Controls.Add(lblIntegralsSummary);
            this.Controls.Add(gbSummary);
        }

        private void RefreshDataGridAndCalculateTotals()
        {
            dgvEconomicGrid.Rows.Clear();
            double aggregatedBudget = 0;
            double aggregatedRevenue = 0;

            foreach (MarketingChannel channel in activeChannels)
            {
                aggregatedBudget += channel.Budget;
                aggregatedRevenue += channel.CalculateRevenue();

                dgvEconomicGrid.Rows.Add(
                    channel.Name,
                    channel.Budget.ToString("F2"),
                    channel.Impressions,
                    channel.Clicks,
                    channel.Buyers,
                    channel.CalculateCPC().ToString("F2"),
                    channel.CalculateCAC().ToString("F2"),
                    channel.CalculateRevenue().ToString("F2")
                );
            }

            double netProfit = aggregatedRevenue - aggregatedBudget;
            string decisionStrategy = netProfit >= 0
                ? "ПРИБЫЛЬНАЯ МОДЕЛЬ (Допускается масштабирование инвестиций)"
                : "УБЫТОЧНАЯ МОДЕЛЬ! Требуется снизить САС или поднять конверсию воронки";

            lblIntegralsSummary.Text =
                $"Суммарный инвестиционный бюджет: {aggregatedBudget:F2} руб. | Валовый доход: {aggregatedRevenue:F2} руб.\n" +
                $"Чистый финансовый результат (Прибыль): {netProfit:F2} руб.\n" +
                $"СТРАТЕГИЧЕСКИЙ СТАТУС ПРОЕКТА: {decisionStrategy}";
        }

        private void BtnAddChannel_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtChannelName.Text.Trim();
                string budgetStr = txtChannelBudget.Text.Trim();
                string clicksStr = txtChannelClicks.Text.Trim();

                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(budgetStr) || string.IsNullOrEmpty(clicksStr))
                {
                    MessageBox.Show("Предупреждение: Все текстовые поля ввода обязательны для заполнения!", "Валидация полей", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                double budget;
                int clicks;
                int impressions = (int)numImpressions.Value;
                int buyers = (int)numBuyers.Value;
                double avCheck = double.Parse(cmbAverageCheck.Text);

                if (!double.TryParse(budgetStr, out budget) || !int.TryParse(clicksStr, out clicks))
                {
                    MessageBox.Show("Ошибка типа данных! Затраты и клики должны выражаться числовым форматом.", "Ошибка приведения типов", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (budget < 0 || clicks < 0)
                {
                    MessageBox.Show("Ошибка параметров! Экономические переменные не могут принимать отрицательные значения.", "Логическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (clicks > impressions || buyers > clicks)
                {
                    MessageBox.Show("Ошибка воронки: Проверьте логику (Клики не могут быть больше показов, Покупатели не могут быть больше кликов).", "Нарушение воронки", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MarketingChannel newChannel = new MarketingChannel(name, budget, impressions, clicks, buyers, avCheck);
                activeChannels.Add(newChannel);

                RefreshDataGridAndCalculateTotals();

                txtChannelName.Clear();
                txtChannelBudget.Clear();
                txtChannelClicks.Clear();
                numBuyers.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошел системный сбой: " + ex.Message, "Перехват исключений", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
