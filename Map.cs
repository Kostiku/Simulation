using System;
using System.Collections.Generic;
using System.Linq;

namespace Simulation
{
    internal class Map
    { // choose static because we have only one map in the game
        static Random rnd = new Random();
        private static int _maxRow, _maxColumn;
        public static int maxRow { get => _maxRow; }
        public static int maxColumn { get => _maxColumn; }
        static Dictionary<Coordinates, Entity> _map = new Dictionary<Coordinates, Entity>();
        internal static void initMap()
        {
            Render.askMapSize(out _maxColumn, out _maxRow);
            _map.Clear();
        }
        public static bool cellIsEmpty(Coordinates cell)
        {
            return !_map.ContainsKey(cell);
        }
        internal static bool cellIsInMap(Coordinates cell) 
        { 
            return cell.row >= 0 && cell.column >= 0 && cell.row < maxRow && cell.column < maxColumn; 
        }
        public static Entity getEntity(Coordinates cell) { return _map[cell]; }
        public static void moveEntity(Coordinates fromCell, Coordinates toCell)
        {
            addEntity(toCell, getEntity(fromCell));
            removeEntity(fromCell);
        }
        public static void removeEntity(Coordinates fromCell)
        {
            _map.Remove(fromCell);
            Render.removeEntity(fromCell);
        }
        public static void addEntity(Coordinates toCell, Entity entity)
        {
            entity.coordinates = toCell;
            _map.Add(toCell, entity);
            Render.printEntity(toCell);
        }
        internal static Coordinates getRandomEmptyCell()
        {
            Coordinates cell;
            do
            {
                cell = new Coordinates(rnd.Next(0, maxRow), rnd.Next(0, maxColumn));
            } while (!Map.cellIsEmpty(cell));
            return cell;
        }
        internal static int cellsCount()
        {
            return maxRow * maxColumn;
        }
        internal static List<Entity> getHerbivoreList()
        {// could not find the way to send type as a parameter
            return _map.Where(item => item.Value is Herbivore).Select(item => item.Value).ToList();
        }
        internal static List<Entity> getPredatorList()
        {// could not find the way to send type as a parameter
            return _map.Where(item => item.Value is Predator).Select(item => item.Value).ToList();
        }

        internal static Entity getClosestGrass(Entity entity)
        {// could not find the way to send type as a parameter
            return _map.Where(item => item.Value is Grass).
                OrderBy(i => distance(i.Key, entity.coordinates)).FirstOrDefault().Value;
        }

        internal static Entity getClosestHerbivore(Entity entity)
        {// could not find the way to send type as a parameter
            return _map.Where(item => item.Value is Herbivore).
                OrderBy(i => distance(i.Key, entity.coordinates)).FirstOrDefault().Value;
        }
        internal static int distance(Coordinates a, Coordinates b)
        {
            return Math.Max(Math.Abs(a.row - b.row),Math.Abs(a.column - b.column));
        }
        internal static List<Coordinates> getNeighbourCells(Coordinates cell, Coordinates preyCell)
        {
            List<Coordinates> shifts = new List<Coordinates> 
                { new Coordinates(1, 0), new Coordinates(1, 1), new Coordinates(0, 1), new Coordinates(-1, 1),
                new Coordinates(-1, 0), new Coordinates(-1, -1), new Coordinates(0, -1), new Coordinates(1, -1)};
            List<Coordinates> result = new List<Coordinates>();
            foreach (var shift in shifts)
            {
                Coordinates shiftCell = cell.shift(shift.row, shift.column);
                if (cellIsInMap(shiftCell) & 
                    (preyCell != null ? cellIsEmpty(shiftCell) || shiftCell.Equals(preyCell) : true)) 
                    result.Add(shiftCell);
            }
            return result;
        }

        internal static List<Coordinates> getAvailableNearCells(Coordinates currentCell, Coordinates preyCell, List<Coordinates> closedCells)
        {
            List<Coordinates> result = new List<Coordinates>();
            foreach (Coordinates cell in getNeighbourCells(currentCell, preyCell))
                if (!closedCells.Contains(cell)) result.Add(cell);
            return result;
        }

        internal static int rocksCount()
        {
            return _map.Where(item => item.Value is Rock).Count(); 
        }

        internal static int treesCount()
        {
            return _map.Where(item => item.Value is Tree).Count();
        }

        internal static int grassCount()
        {
            return _map.Where(item => item.Value is Grass).Count();
        }

        internal static int herbivoresCount()
        {
            return _map.Where(item => item.Value is Herbivore).Count();
        }

        internal static int predatorsCount()
        {
            return _map.Where(item => item.Value is Predator).Count();
        }
    }
}
