using RimWorld;

namespace BotFactory
{
    [DefOf]
    public static class BF_QuestScriptDefOf
    {
        static BF_QuestScriptDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BF_QuestScriptDefOf));
        }

        [MayRequireRoyalty]
        public static QuestScriptDef ProblemCauser;
    }
}
