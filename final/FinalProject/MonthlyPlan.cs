namespace ScriptureApp
{
    public class MonthlyPlan : StudyPlan
    {
        public override string PlanName => "Monthly Marathon";
        public override string GetPlanGoal() => "High rotation selection to read the entire file over 30 days.";
    }
}