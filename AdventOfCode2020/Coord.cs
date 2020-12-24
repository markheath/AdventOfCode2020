using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public struct Coord : IEnumerable<int>
    {
        private readonly int x;
        private readonly int y;
        private readonly int z;
        
        public int this[int index]
        {
            get { return index == 0 ? x : index == 1 ? y : z;  }
        }

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
            z = 0;
        }
        public Coord(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static implicit operator (int, int)(Coord c) => (c.x,c.y);
        public static implicit operator Coord((int, int) c) => new Coord(c.Item1, c.Item2);
        public static implicit operator (int, int, int)(Coord c) => (c.x, c.y, c.z);
        public static implicit operator Coord((int, int, int) c) => new Coord(c.Item1, c.Item2, c.Item3);

        public void Deconstruct(out int x, out int y)
        {
            x = this.x;
            y = this.y;
        }

        public void Deconstruct(out int x, out int y, out int z)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }

        public static Coord operator +(Coord a, Coord b)
        {
            return new Coord(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public override bool Equals(object other) =>
            other is Coord c
                && c.x == x
                && c.y == y
                && c.z == z;

        public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        public override string ToString() => $"({x},{y},{z})";

        public IEnumerator<int> GetEnumerator()
        {
            yield return x;
            yield return y;
            yield return z;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
