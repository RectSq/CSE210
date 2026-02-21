namespace ScriptureApp
{
    public class WeeklyPlan : StudyPlan
    {
        public override string PlanName => "Weekly Theme";
        public override string GetPlanGoal() => "Draw seven scriptures on Sunday to study throughout the week.";
    }
}