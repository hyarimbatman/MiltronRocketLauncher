namespace RocketLauncher
{
    public class Payload
    {
        private string description;
        private int weight;

        public Payload(string description, int weight)
        {
            this.description = description;
            this.weight = weight;
        }

        public string Description { get => description; set => description = value; }
        public int Weight { get => weight; set => weight = value; }
    }
}