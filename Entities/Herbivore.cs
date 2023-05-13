
namespace Simulation
{
    internal class Herbivore : Creature
    {
        public Herbivore(Coordinates coordinates, int velocity = 1, int health = 10, int healthMoveChange = -3) : 
            base(coordinates, velocity, health, healthMoveChange)
        { }
        public override Entity getClosestPrey()
        {
            return Map.getClosestGrass(this);
        }
        public override void attack(Entity prey)
        {
            this.health += ((Grass)prey).foodValue;
            Map.removeEntity(prey.coordinates);
            Map.moveEntity(this.coordinates, prey.coordinates);
        }
    }
}
