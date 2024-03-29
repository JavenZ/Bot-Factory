using Verse;
using RimWorld;

namespace BotFactory
{
    public class Alert_FullSkillServers : Alert
    {
        public Alert_FullSkillServers()
        {
            defaultLabel = "BF_AlertFullSkillServers".Translate();
            defaultExplanation = "BF_AlertFullSkillServersDesc".Translate();
            defaultPriority = AlertPriority.Medium;
        }


        public override AlertReport GetReport()
        {
            if (!BotFactory_Settings.receiveSkillAlert || Utils.gameComp.GetPointCapacity(ServerType.SkillServer) <= 0)
                return false;

            if (Utils.gameComp.GetPoints(ServerType.SkillServer) >= Utils.gameComp.GetPointCapacity(ServerType.SkillServer) * 0.9f)
            {
                return true;
            }
            return false;
        }
    }
}
