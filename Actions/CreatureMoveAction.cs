using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Simulation.Actions
{
    internal abstract class CreatureMoveAction : Action
    {
        protected abstract List<Entity> creatureList { get; set; }
        public override bool perform() 
        {   
            if (creatureList.Count() == 0) return false;
            foreach (Creature creature in creatureList)
            {
                creature.makeMove();
                Thread.Sleep(200);
            }
            return true;
        }
    }
    internal class HerbivoreMoveAction : CreatureMoveAction
    {
        public HerbivoreMoveAction()
        {
            creatureList = Map.getHerbivoreList();
        }
        protected override List<Entity> creatureList { get; set; }
    }
    internal class PredatorMoveAction : CreatureMoveAction
    {
        public PredatorMoveAction()
        {
            creatureList = Map.getPredatorList();
        }
        protected override List<Entity> creatureList { get; set; }
    }
}
