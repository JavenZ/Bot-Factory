using Verse;

namespace BotFactory
{
    public class CompProperties_SkyMindTower : CompProperties
    {
        public CompProperties_SkyMindTower()
        {
            compClass = typeof(CompSkyMindTower);
        }

        public int SkyMindSlotsProvided;
    }
}
