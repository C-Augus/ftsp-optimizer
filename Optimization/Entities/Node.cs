namespace Optimizer.Entities
{
    public class Node
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Family Family { get; set; }

        public Node() { }

        public Node(float x, float y)
        {
            X = x;
            Y = y;
        }
    }


}