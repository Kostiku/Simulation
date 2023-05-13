
namespace Simulation
{
    internal class Predator : Creature
    {
        private int attackForce;
        public Predator(Coordinates coordinates, int velocity = 1, int health = 10, int attackForce = 5, int healthMoveChange = -4) : 
            base(coordinates, velocity, health, healthMoveChange)
        {
            this.attackForce = attackForce;
        }
        public override Entity getClosestPrey()
        {
            return Map.getClosestHerbivore(this);
        }
        public override void attack(Entity prey)
        {
            if (((Creature)prey).health > attackForce)
            {
                this.health += attackForce;
                ((Creature)prey).health -= attackForce;
            }
            else
            {
                this.health += ((Creature)prey).health;
                Map.removeEntity(prey.coordinates);
                Map.moveEntity(this.coordinates, prey.coordinates);
            }
        }
    }
}
