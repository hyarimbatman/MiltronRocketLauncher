namespace RocketLauncher
{
    public class Wind
    {
        private string direction;
        private float angle;
        private float speed;

        public Wind(string direction, float angle, float speed)
        {
            this.direction = direction;
            this.angle = angle;
            this.speed = speed;
        }

        public string Direction { get => direction; set => direction = value; }
        public float Angle { get => angle; set => angle = value; }
        public float Speed { get => speed; set => speed = value; }
    }
}