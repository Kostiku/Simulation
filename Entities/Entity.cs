
namespace Simulation
{
    internal abstract class Entity
    {
        public Coordinates coordinates { get; set; }
        protected Entity(Coordinates coordinates) { this.coordinates = coordinates; }
    }
}
