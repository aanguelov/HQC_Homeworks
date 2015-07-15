namespace MineSweeper
{
    public class Player
    {
        public Player(string name, int points)
        {
            this.Name = name;
            this.Points = points;
        }

        public Player()
        {
        }

        public string Name { get; set; }

        public int Points { get; set; }
    }
}
