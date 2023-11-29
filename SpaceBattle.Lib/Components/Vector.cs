namespace SpaceBattle.Lib
{
    public class Vector
    {
        private readonly int[] _array;
        private readonly int _size;

        public Vector(params int[] array)
        {
            _size = array.Length;
            _array = new int[_size];

            for (var i = 0; i < _size; i++)
            {
                _array[i] = array[i];
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vector vector)
            {
                return this == vector;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _array.GetHashCode();
        }

        public int this[int index]
        {
            get => _array[index];

            set => _array[index] = value;
        }

        public int Size()
        {
            return _size;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            if (a.Size() != b.Size())
            {
                throw new ArgumentException();
            }
            else
            {
                var new_array = new int[a.Size()];

                for (var i = 0; i < a.Size(); i++)
                {
                    new_array[i] = a[i] + b[i];
                }

                return new Vector(new_array);
            }
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            if (v1.Size() != v2.Size())
            {
                throw new ArgumentException();
            }

            for (var i = 0; i < v1.Size(); i++)
            {
                if (v1._array[i] != v2._array[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !(v1 == v2);
        }
    }
}
