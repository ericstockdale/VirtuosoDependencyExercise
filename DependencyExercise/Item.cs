namespace DependencyExercise
{
    public class Item
    {
        public Item(string name, int level)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }

            if (level < 0)
            {
                throw new ArgumentException("Level must be greater than zero.");
            }

            Name = name;
            Level = level;
        }

        public string Name { get; }
        public int Level { get; set; }
    }
}