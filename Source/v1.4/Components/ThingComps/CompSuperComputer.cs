using System.Collections.Generic;
using System.Text;
using Verse;

namespace BotFactory
{
    public class CompSuperComputer : ThingComp
    {
        public CompProperties_SuperComputer Props
        {
            get
            {
                return (CompProperties_SuperComputer)props;
            }
        }

        public override void PostSpawnSetup(bool respawningAfterLoad)
        {
            base.PostSpawnSetup(respawningAfterLoad);
            building = (Building)parent;

            // The server lists need to know how much storage and point generation exists for each server mode. This adds it to all three types.
            if (!respawningAfterLoad)
                Utils.gameComp.AddServer(building);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            // Servers in hacking mode allow access to the hacking menu for deploying a hack. Supercomputers always enable hacking mode, so they always have the gizmo to open the menu.
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
        }

        public override string CompInspectStringExtra()
        {
            StringBuilder ret = new StringBuilder();

            ret.AppendLine("BF_SkillServersSynthesis".Translate(Utils.gameComp.GetPoints(ServerType.SkillServer), Utils.gameComp.GetPointCapacity(ServerType.SkillServer)))
               .AppendLine("BF_SecurityServersSynthesis".Translate(Utils.gameComp.GetPoints(ServerType.SecurityServer), Utils.gameComp.GetPointCapacity(ServerType.SecurityServer)))
               .AppendLine("BF_HackingServersSynthesis".Translate(Utils.gameComp.GetPoints(ServerType.HackingServer), Utils.gameComp.GetPointCapacity(ServerType.HackingServer)))
               .AppendLine("BF_SkillProducedPoints".Translate(Props.passivePointGeneration))
               .AppendLine("BF_SecurityProducedPoints".Translate(Props.passivePointGeneration))
               .AppendLine("BF_HackingProducedPoints".Translate(Props.passivePointGeneration))
               .AppendLine("BF_SkillSlotsAdded".Translate(Props.pointStorage))
               .AppendLine("BF_SecuritySlotsAdded".Translate(Props.pointStorage))
               .Append("BF_HackingSlotsAdded".Translate(Props.pointStorage));
            return ret.Append(base.CompInspectStringExtra()).ToString();
        }

        public override void PostDeSpawn(Map map)
        {
            base.PostDeSpawn(map);

            // The server lists need to know how much storage exists for each server mode. This removes it from all three types.
            Utils.gameComp.RemoveServer(building);
        }

        public override void Notify_MapRemoved()
        {
            base.Notify_MapRemoved();

            // Things that have their map removed are not despawned but outright lost, but it should still be removed from the server types.
            Utils.gameComp.RemoveServer(building);
        }

        private Building building = null;
    }
}
