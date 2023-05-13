
namespace Simulation.Actions
{
    abstract class Action
    {
        public abstract bool perform();
    }

    internal class initMapAction : Action
    {
        public override bool perform()
        {
            Map.initMap();
            return true;
        }
    }
    internal class initRenderAction : Action
    {
        public override bool perform()
        {
            Render.initConsole();
            Render.printFrame();
            Render.printComment();
            return true;
        }
    }
    internal class initSimulationAction : Action
    {
        public override bool perform()
        {
            Simulation.resetTurnCounter();
            return true;
        }
    }
}
