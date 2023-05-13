using System;
using System.Threading;

namespace Simulation
{
    internal class Program
    {
        public static char reStartKeyChar = '1';
        public static char startStopKeyChar = '2';
        public static char oneTurnKeyChar = '3';
        public static char quitKeyChar = '4';
        public static ConsoleKeyInfo dummyKey = new ConsoleKeyInfo('0', ConsoleKey.D0, false, false, false);
        public static ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false);

        static void Main(string[] args)
        {
            while (true)
            {
                checkPressedKey();
                Thread.Sleep(100);
            }
        }
        public static void checkPressedKey()
        {
            if (Console.KeyAvailable) pressedKey = Console.ReadKey(true);
            if (pressedKey == dummyKey) return;
            if (pressedKey.KeyChar == reStartKeyChar)
            {
                Simulation.initSimulation();
                Simulation.startSimulation();
                return;
            }
            if (pressedKey.KeyChar == startStopKeyChar)
            {
                if (Simulation.stopFlag) { Simulation.startSimulation(); return; }
                else Simulation.stopSimulation(null);
            }
            if (pressedKey.KeyChar == oneTurnKeyChar)
            {
                if (!Simulation.stopFlag) Simulation.stopSimulation(null);
                Simulation.nextTurn();
            }
            if (pressedKey.KeyChar == quitKeyChar)
            {
                Environment.Exit(0);
            }
            pressedKey = dummyKey;
        }
        internal static bool commandKeyIsPressed()
        {
            if (Console.KeyAvailable)
            {
                pressedKey = Console.ReadKey(true);
                if (pressedKey.KeyChar == reStartKeyChar | pressedKey.KeyChar == startStopKeyChar
                    | pressedKey.KeyChar == oneTurnKeyChar | pressedKey.KeyChar == quitKeyChar)
                    return true;
            }
            return false;
        }
    }
}
