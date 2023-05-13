
namespace Simulation
{
    internal class Coordinates
    { 
        public int row { get; set; }
        public int column { get; set; }
        public Coordinates(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
        public Coordinates shift(int rowShift, int columnShift)
        {
            return new Coordinates(this.row + rowShift, this.column + columnShift);
        }
        public override bool Equals(object obj)
        {
            return obj is Coordinates coordinates &&
                   row == coordinates.row &&
                   column == coordinates.column;
        }

        public override int GetHashCode()
        {
            int hashCode = -1663278630;
            hashCode = hashCode * -1521134295 + row.GetHashCode();
            hashCode = hashCode * -1521134295 + column.GetHashCode();
            return hashCode;
        }
    }
}
