namespace ScriptureApp
{
    public class DailyPlan : StudyPlan
    {
        public override string PlanName => "Daily Bread";
        public override string GetPlanGoal() => "Draw one scripture daily for deep meditation.";
    }
}