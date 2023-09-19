using System;
using System.Xml.Linq;

namespace CommonLib.Entities
{
    public class Node : IEquatable<Node>
    {
        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public Family Family { get; set; }

        public Node(int id, float x, float y)
        {
            Id = id;
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Node);
        }

        public bool Equals(Node other)
        {
            if (other == null)
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}