using Simulation.Actions;
using System;
using System.Text;

namespace Simulation
{
    internal class Render
    {
        static int charsInCell = 3;
        static int commentRows = 8;
        static int commentColumns = 40;
        static ConsoleColor normalTextColor = ConsoleColor.Yellow;
        static ConsoleColor highlightTextColor = ConsoleColor.Red;
        public static void askMapSize(out int width, out int height)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();
            Console.CursorVisible = true;
            Console.Write("Введите через пробел размеры карты, ширина высота: ");
            string[] mapSizeAnswer = Console.ReadLine().Split(' ');
            width = int.Parse(mapSizeAnswer[0]); 
            height = int.Parse(mapSizeAnswer[1]);
        }
        public static void initConsole()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = normalTextColor;
            Console.Title = "Simulation";
            Console.SetWindowSize(calculateRenderSize().column, calculateRenderSize().row);
            Console.Clear();
            Console.CursorVisible = false;
        }
        private static Coordinates calculateRenderSize()
        {
            return new Coordinates(Map.maxRow + commentRows, Map.maxColumn * charsInCell + commentColumns);
        }
        public static void printFrame()
        {
            for (int row = 1; row < Map.maxRow + 1; row++)
            {
                Console.SetCursorPosition(0, row);
                Console.Write("│");
                Console.SetCursorPosition((Map.maxColumn + 1) * charsInCell, row);
                Console.Write("│");
            }
            for (int column = 0; column < Map.maxColumn + 1; column++)
            {
                for (int i = 0; i < charsInCell; i++)
                {
                    Console.SetCursorPosition(column * charsInCell + i, 0);
                    Console.Write("─");
                    Console.SetCursorPosition(column * charsInCell + i, Map.maxRow + 1);
                    Console.Write("─");
                }
            }
            Console.SetCursorPosition(0, 0);
            Console.Write("┌");
            Console.SetCursorPosition(0, Map.maxRow + 1);
            Console.Write("└");
            Console.SetCursorPosition((Map.maxColumn + 1) * charsInCell, 0);
            Console.Write("┐");
            Console.SetCursorPosition((Map.maxColumn + 1) * charsInCell, Map.maxRow + 1);
            Console.Write("┘");
        }
        public static void printComment()
        {
            int commentRow = Map.maxRow + 2;
            Console.SetCursorPosition(1, commentRow);
            Console.Write(sprite(new Rock(new Coordinates(1, commentRow))) + " - rock");
            Console.SetCursorPosition(15, commentRow++);
            Console.Write(Program.reStartKeyChar + " - начать симуляцию заново");
            Console.SetCursorPosition(1, commentRow);
            Console.Write(sprite(new Tree(new Coordinates(1, commentRow))) + " - tree");
            Console.SetCursorPosition(15, commentRow++);
            Console.Write(Program.startStopKeyChar + " - остановить/продолжить симуляцию");
            Console.SetCursorPosition(1, commentRow);
            Console.Write(sprite(new Grass(new Coordinates(1, commentRow))) + " - grass");
            Console.SetCursorPosition(15, commentRow++);
            Console.Write(Program.oneTurnKeyChar + " - сделать один шаг симуляции");
            Console.SetCursorPosition(1, commentRow);
            Console.Write(sprite(new Herbivore(new Coordinates(1, commentRow))) + " - herbivore");
            Console.SetCursorPosition(15, commentRow++);
            Console.Write(Program.quitKeyChar + " - завершить выполнение");
            Console.SetCursorPosition(1, commentRow);
            Console.Write(sprite(new Predator(new Coordinates(1, commentRow))) + " - predator");
        }
        public static void moveEntity(Coordinates from, Coordinates to)
        {
            removeEntity(from);
            printEntity(to);
        }
        public static void removeEntity(Coordinates from)
        {
            Console.SetCursorPosition(screenCell(from).column, screenCell(from).row);
            Console.Write(' ');
        }
        public static void printEntity(Coordinates to)
        {
            Entity entity = Map.getEntity(to);
            Console.SetCursorPosition(screenCell(to).column, screenCell(to).row);
            if ((charsInCell > 2) & ((entity is Rock) | (entity is Tree)))
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.Write(sprite(entity));
                Console.SetCursorPosition(screenCell(to).column - charsInCell / 2, screenCell(to).row);
                Console.Write(" ");
                Console.SetCursorPosition(screenCell(to).column + charsInCell / 2, screenCell(to).row);
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            else Console.Write(sprite(entity));
        }
        private static Coordinates screenCell(Coordinates cell)
        {
            return new Coordinates(cell.row + 1, (cell.column + 1) * charsInCell + charsInCell / 2);
        }
        static string sprite(Entity entity)
        {
            string result = " ";
            if (entity is Rock) result = "●";
            if (entity is Tree) result = "ⅎ";
            if (entity is Grass) result = "*";
            if (entity is Herbivore) result = "¤";
            if (entity is Predator) result = "♂";
            return result;
        }
        internal static void printCounter(int turnCounter)
        {
            Console.SetCursorPosition(15, Map.maxRow + commentRows - 1);
            Console.Write($"Счетчик итераций: {turnCounter}");
        }
        internal static void showStopMessage(Actions.Action action)
        {
            clearStopMessage();
            String stopMessage;
            if (action == null) stopMessage = "Симуляция остановлена";
            else
            {
                stopMessage = "Симуляция завершена, закончились ";
                if (action is HerbivoreMoveAction) stopMessage += "травоядные";
                if (action is PredatorMoveAction) stopMessage += "хищники";
            }
            Console.SetCursorPosition(15, Map.maxRow + commentRows - 2);
            Console.ForegroundColor = highlightTextColor;
            Console.Write(stopMessage);
            Console.ForegroundColor = normalTextColor;
        }
        internal static void clearStopMessage() 
        {
            int stopMessageColumn = 15;
            Console.SetCursorPosition(stopMessageColumn, Map.maxRow + commentRows - 2);
            for (int i = stopMessageColumn; i < calculateRenderSize().column; i++) Console.Write(" ");
        }
    }
}
