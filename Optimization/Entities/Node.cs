namespace Optimizer.Entities
{
    public class Node
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public Family Family { get; set; }

        public Node() { }

        public Node(int id, float x, float y)
        {
            Id = Id;
            X = x;
            Y = y;
        }
    }


}