using Verse;
using RimWorld;
using System.Collections.Generic;

namespace BotFactory
{
    public class ChoiceLetter_PersonalityShiftRequest : ChoiceLetter
    {
        public Pawn subject;

        public override bool CanDismissWithRightClick => false;

        public override IEnumerable<DiaOption> Choices
        {
            get
            {
                if (ArchivedOnly)
                {
                    yield return Option_Close;
                    yield break;
                }
                DiaOption diaOption = new DiaOption("AcceptButton".Translate());
                DiaOption optionReject = new DiaOption("RejectLetter".Translate());
                diaOption.action = delegate
                {
                    ChoiceLetter_PersonalityShift choiceLetter = (ChoiceLetter_PersonalityShift)LetterMaker.MakeLetter(BF_LetterDefOf.BF_PersonalityShiftLetter);
                    choiceLetter.ConfigureChoiceLetter(subject, 3, 3, true, true);
                    choiceLetter.Label = "BF_PersonalityShiftRequest".Translate(subject);
                    choiceLetter.StartTimeout(2500);
                    Find.LetterStack.ReceiveLetter(choiceLetter);
                    Find.LetterStack.RemoveLetter(this);
                    subject.needs.mood?.thoughts?.memories?.TryGainMemoryFast(BF_ThoughtDefOf.BF_PersonalityShiftAllowed);
                };
                diaOption.resolveTree = true;
                optionReject.action = delegate
                {
                    Find.LetterStack.RemoveLetter(this);
                    subject.needs.mood?.thoughts?.memories?.TryGainMemoryFast(BF_ThoughtDefOf.BF_PersonalityShiftDenied);
                };
                optionReject.resolveTree = true;
                yield return diaOption;
                yield return optionReject;
                if (lookTargets.IsValid())
                {
                    yield return Option_JumpToLocationAndPostpone;
                }
                yield return Option_Postpone;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(ref subject, "BF_shiftSubject");
        }
    }
}