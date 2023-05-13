
namespace Simulation
{
    internal abstract class Creature : Entity
    {
        public int velocity { get; }
        public int health { get; set; }
        public int healthMoveChange { get; set; }
        protected Creature(Coordinates coordinates,int velocity, int health, int healthMoveChange) : base(coordinates)
        {
            this.velocity = velocity;
            this.health = health;
            this.healthMoveChange = healthMoveChange;
        }
        public void makeMove()
        {
            Entity prey = this.getClosestPrey();
            if (prey == null) return;
            if (this.isNearTo(prey)) this.attack(prey);
            else
            {
                this.health += this.healthMoveChange;
                if (this.health > 0)
                    Map.moveEntity(this.coordinates, PathFinder.findNextCellToMove(this.coordinates, prey.coordinates, this.velocity));
                else
                    Map.removeEntity(this.coordinates);
            }
        }
        public abstract Entity getClosestPrey();
        private bool isNearTo(Entity prey)
        {
            return Map.getNeighbourCells(this.coordinates, null).Contains(prey.coordinates);
        }
        public abstract void attack(Entity prey);
    }
}
