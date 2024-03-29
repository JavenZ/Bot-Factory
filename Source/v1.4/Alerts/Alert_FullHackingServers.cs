using Verse;
using RimWorld;

namespace BotFactory
{
    public class Alert_FullHackingServers : Alert
    {
        public Alert_FullHackingServers()
        {
            defaultLabel = "BF_AlertFullHackingServers".Translate();
            defaultExplanation = "BF_AlertFullHackingServersDesc".Translate();
            defaultPriority = AlertPriority.Medium;
        }


        public override AlertReport GetReport()
        {
            if (!BotFactory_Settings.playerCanHack || !BotFactory_Settings.receiveHackingAlert || Utils.gameComp.GetPointCapacity(ServerType.HackingServer) <= 0)
                return false;

            // Only display the hacking alert if it is near capacity and the hacking penalty is not so bad they can't afford an operation even with used capacity.
            float points = Utils.gameComp.GetPoints(ServerType.HackingServer);
            if (points >= Utils.gameComp.GetPointCapacity(ServerType.HackingServer) * 0.9f && points >= Utils.gameComp.hackCostTimePenalty + 400)
            {
                return true;
            }
            return false;
        }
    }
}
