using System;

public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, string points) : base(name, description, points)
    {
    }

    public override void RecordEvent()
    {
        // Eternal goals are never "done", so we do nothing here specifically.
        // The points are handled in the Manager.
    }

    public override bool IsComplete()
    {
        return false; // Never complete
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{_shortName}|{_description}|{_points}";
    }
}