namespace EPSA.Objects.Models
{
    public class HistoricalConsumption
    {
        public string Line { get; set; }
        public DateTime Date { get; set; }
        public float Residential_Consumption { get; set; }
        public float Commercial_Consumption { get; set; }
        public float Industrial_Consumption { get; set; }

        public float Residential_Losses { get; set; }
        public float Commercial_Losses { get; set; }
        public float Industrial_Losses { get; set; }

        public float Residential_CostToConsumption { get; set; }
        public float Commercial_CostToConsumption { get; set; }
        public float Industrial_CostToConsumption { get; set; }

    }
}
