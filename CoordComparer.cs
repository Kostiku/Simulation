using System.Collections.Generic;

namespace Simulation
{
    internal class CoordComparer : IComparer<Coordinates>
    {
        private Coordinates preyCell;
        public CoordComparer(Coordinates preyCell) 
        { 
            this.preyCell = preyCell;
        }
        public int Compare(Coordinates o1, Coordinates o2)
        {
            int o1Length = Map.distance(preyCell, o1);
            int o2Length = Map.distance(preyCell, o2);
            return o1Length.CompareTo(o2Length);
        }
    }
}
