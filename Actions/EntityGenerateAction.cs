
namespace Simulation.Actions
{
    internal abstract class EntityGenerateAction : Action
    {
        public int spawnMaximum; 
        public int spawnMinimum; 
        public int entityCount;
        public override bool perform()
        {
            if (entityCount > spawnMinimum) {  return false; }
            while (entityCount < spawnMaximum * Map.cellsCount() / 100)
            {
                Coordinates cell = Map.getRandomEmptyCell();
                Map.addEntity(cell, createEntity(cell));
                entityCount++;
            }
            return true;
        }
        public abstract Entity createEntity(Coordinates cell);
    }
    internal class RockGenerateAction : EntityGenerateAction
    {
        public RockGenerateAction(int spawnMaximum = 5, int spawnMinimum = 0)
        {
            base.spawnMinimum = spawnMinimum;
            base.spawnMaximum = spawnMaximum;
            base.entityCount = Map.rocksCount();
        }
        public override Entity createEntity(Coordinates cell)
        { 
            return new Rock(cell);
        }
    }
    internal class TreeGenerateAction : EntityGenerateAction
    {
        public TreeGenerateAction(int spawnMaximum = 5, int spawnMinimum = 0)
        {
            base.spawnMinimum = spawnMinimum;
            base.spawnMaximum = spawnMaximum;
            base.entityCount = Map.treesCount();
        }
        public override Entity createEntity(Coordinates cell)
        {
            return new Tree(cell);
        }
    }
    internal class GrassGenerateAction : EntityGenerateAction
    {
        public GrassGenerateAction(int spawnMaximum = 8, int spawnMinimum = 2)
        {
            base.spawnMinimum = spawnMinimum;
            base.spawnMaximum = spawnMaximum;
            base.entityCount = Map.grassCount();
        }
        public override Entity createEntity(Coordinates coordinates)
        {
            return new Grass(coordinates);
        }
    }
    internal class HerbivoreGenerateAction : EntityGenerateAction
    {
        public HerbivoreGenerateAction(int spawnMaximum = 8, int spawnMinimum = 3)
        {
            base.spawnMinimum = spawnMinimum;
            base.spawnMaximum = spawnMaximum;
            base.entityCount = Map.herbivoresCount();
        }
        public override Entity createEntity(Coordinates coordinates)
        {
            return new Herbivore(coordinates);
        }
    }
    internal class PredatorGenerateAction : EntityGenerateAction
    {
        public PredatorGenerateAction(int spawnMaximum = 3, int spawnMinimum = 1)
        {
            base.spawnMinimum = spawnMinimum;
            base.spawnMaximum = spawnMaximum;
            base.entityCount = Map.predatorsCount();
        }
        public override Entity createEntity(Coordinates coordinates)
        {
            return new Predator(coordinates);
        }
    }
}
