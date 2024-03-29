using Verse;
using RimWorld;
using System.Collections.Generic;

namespace BotFactory
{
    public class CompInsightBench : ThingComp
    {
        public ServerType ServerType
        {
            get
            {
                return serverMode;
            }
        }

        public override void PostExposeData()
        {
            base.PostExposeData();

            Scribe_Values.Look(ref serverMode, "BF_serverMode", ServerType.SkillServer);
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            building = (Building)parent;
            networkConnection = parent.GetComp<CompSkyMind>();
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (!parent.GetComp<CompPowerTrader>().PowerOn || networkConnection?.connected == false)
                yield break;

            // Generate button to switch server mode based on which servermode the server is currently in.
            switch (serverMode)
            {
                case ServerType.None:
                    yield break;
                case ServerType.SkillServer:
                    yield return new Command_Action
                    { // In Skill Mode, can switch to Security
                        icon = Tex.SkillIcon,
                        defaultLabel = "BF_SkillMode".Translate(),
                        defaultDesc = "BF_SkillModeDesc".Translate(),
                        action = delegate ()
                        {
                            serverMode = ServerType.SecurityServer;
                        }
                    };
                    break;
                case ServerType.SecurityServer:
                    yield return new Command_Action
                    { // In Security Mode, can switch to Hacking
                        icon = Tex.SecurityIcon,
                        defaultLabel = "BF_SecurityMode".Translate(),
                        defaultDesc = "BF_SecurityModeDesc".Translate(),
                        action = delegate ()
                        {
                            serverMode = ServerType.HackingServer;
                        }
                    };
                    break;
                case ServerType.HackingServer:
                    yield return new Command_Action
                    { // In Hacking Mode, can switch to Skill
                        icon = Tex.HackingIcon,
                        defaultLabel = "BF_HackingMode".Translate(),
                        defaultDesc = "BF_HackingModeDesc".Translate(),
                        action = delegate ()
                        {
                            serverMode = ServerType.SkillServer;
                        }
                    };

                    // Servers in hacking mode allow access to the hacking menu for deploying a hack.
                    if (BotFactory_Settings.playerCanHack)
                    {
                        yield return new Command_Action
                        {
                            icon = Tex.HackingWindowIcon,
                            defaultLabel = "BF_HackingWindow".Translate(),
                            defaultDesc = "BF_HackingWindowDesc".Translate(),
                            action = delegate ()
                            {
                                Find.WindowStack.Add(new Dialog_HackingWindow());
                            }
                        };
                    }
                    break;
                default:
                    yield return new Command_Action
                    { // In an illegal Mode, can switch to Skill
                        icon = Tex.SkillIcon,
                        defaultLabel = "BF_SwitchToSkillMode".Translate(),
                        defaultDesc = "BF_SwitchToSkillModeDesc".Translate(),
                        action = delegate ()
                        {
                            serverMode = ServerType.SkillServer;
                        }
                    };
                    break;
            }
        }

        public override void PostDeSpawn(Map map)
        {
            base.PostDeSpawn(map);

            // No building can be connected to the network when despawned.
            if (networkConnection?.connected == true)
            {
                Utils.gameComp.DisconnectFromSkyMind(building);
            }
        }

        private CompSkyMind networkConnection;
        private Building building;
        private ServerType serverMode = ServerType.SkillServer;
    }
}