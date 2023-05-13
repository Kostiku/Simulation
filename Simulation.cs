using System;
using System.Collections.Generic;
using System.Threading;
using Simulation.Actions;

namespace Simulation
{
    internal class Simulation
    {
        private static int turnCounter;
        public static bool stopFlag = true;
        public static void initSimulation()
        {
            List<Actions.Action> initActions = new List<Actions.Action> 
                { new initMapAction(), 
                new initRenderAction(),
                new initSimulationAction(),
                };
            foreach (var action in initActions) { action.perform(); }
            List<Actions.Action> spawnActions = new List<Actions.Action>
                { 
                new RockGenerateAction(7),
                new TreeGenerateAction(7),
                new GrassGenerateAction(7),
                new HerbivoreGenerateAction(5),
                new PredatorGenerateAction(3)
                };
            foreach (var action in spawnActions) { action.perform(); }

        }
        public static void startSimulation()
        {
            stopFlag = false;
            Render.clearStopMessage();
            while (!Program.commandKeyIsPressed()) 
            {
                Actions.Action action = nextTurn();
                if (action != null) raiseStop(action);
                Thread.Sleep(100);
            }
        }
        private static void raiseStop(Actions.Action action)
        { 
            Program.pressedKey = new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false);
            Render.showStopMessage(action);
        }
        public static void stopSimulation(Actions.Action action) 
        { 
            stopFlag = true; 
        }
        public static Actions.Action nextTurn() 
        {
            List<Actions.Action> turnActions = new List<Actions.Action>
                { 
                new HerbivoreMoveAction(),
                new PredatorMoveAction(),
                new GrassGenerateAction(7, 2),
                new HerbivoreGenerateAction(5, 3),
                new PredatorGenerateAction(3, 2),
                };
            foreach (var action in turnActions) 
            { 
                if (!action.perform()) if (action is HerbivoreMoveAction || action is PredatorMoveAction) return action; 
            }
            turnCounter++;
            Render.printCounter(turnCounter);
            return null;
        }
        internal static void resetTurnCounter()
        {
            turnCounter = 0;
        }
    }
}
