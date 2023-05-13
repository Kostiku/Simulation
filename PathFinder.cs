using DotNetty.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
    internal class PathFinder
    { // chosen static because we do not need multiply PathFinder objects

        internal static Coordinates findNextCellToMove(Coordinates hunterCell, Coordinates preyCell, int hunterVelocity)
        { // returns one cell because we find path every move and we need a cell to move

            IComparer<Coordinates> comparer = new CoordComparer(preyCell);
            Queue<Coordinates> path = new Queue<Coordinates>();
            PriorityQueue<Coordinates> openCells = new PriorityQueue<Coordinates>(comparer);
            List<Coordinates> closedCells = new List<Coordinates>();
            openCells.Enqueue(hunterCell);
            while (openCells.Count() > 0)
            {
                Coordinates currentCell = openCells.Dequeue();
                if (currentCell.Equals(preyCell))
                {
                    for (int i = 0; i < Math.Min(hunterVelocity, path.Count); i++) path.Dequeue();
                    return path.Dequeue();
                }
                closedCells.Add(currentCell);
                List<Coordinates> availableNearCells = Map.getAvailableNearCells(currentCell,preyCell, closedCells);
                foreach (Coordinates availableNearCell in availableNearCells)
                {
                    if (!openCells.Contains(availableNearCell))
                    {
                        openCells.Enqueue(availableNearCell);
                        if (!path.Contains(currentCell)) path.Enqueue(currentCell);
                     }
                }
            }
            return null;
        }
    }
}