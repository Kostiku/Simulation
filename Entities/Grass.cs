
namespace Simulation
{
    internal class Grass : Entity
    {
        public int foodValue { get; set; }
        public Grass(Coordinates coordinates) : base(coordinates)
        { 
            foodValue = 10;
        }
    }
}
