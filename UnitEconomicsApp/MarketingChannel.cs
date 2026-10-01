using System;

namespace UnitEconomicsApp
{
    /// <summary>
    /// Класс, описывающий отдельный маркетинговый канал привлечения клиентов.
    /// </summary>
    public class MarketingChannel
    {
        public string Name { get; set; }          // Название рекламного канала
        public double Budget { get; set; }        // Инвестиционный бюджет канала, руб
        public int Impressions { get; set; }      // Количество показов объявлений
        public int Clicks { get; set; }           // Количество переходов (кликов)
        public int Buyers { get; set; }           // Количество реальных покупателей
        public double AverageCheck { get; set; }   // Средний чек одного покупателя, руб

        public MarketingChannel(string name, double budget, int impressions, int clicks, int buyers, double avCheck)
        {
            Name = name;
            Budget = budget;
            Impressions = impressions;
            Clicks = clicks;
            Buyers = buyers;
            AverageCheck = avCheck;
        }

        public double CalculateCPC()
        {
            if (Clicks == 0) return 0.0;
            return Math.Round(Budget / Clicks, 2);
        }

        public double CalculateCAC()
        {
            if (Buyers == 0) return 0.0;
            return Math.Round(Budget / Buyers, 2);
        }

        public double CalculateRevenue()
        {
            return Math.Round(Buyers * AverageCheck, 2);
        }

        public double CalculateROMI(ref string strategyStatus)
        {
            double revenue = CalculateRevenue();
            if (Budget == 0)
            {
                strategyStatus = "Бюджет не инвестирован";
                return 0.0;
            }

            double romi = ((revenue - Budget) / Budget) * 100;
            romi = Math.Round(romi, 2);

            if (romi >= 0)
            {
                strategyStatus = "ПРИБЫЛЬНАЯ МОДЕЛЬ";
            }
            else
            {
                strategyStatus = "УБЫТОЧНАЯ МОДЕЛЬ";
            }

            return romi;
        }
    }
}
