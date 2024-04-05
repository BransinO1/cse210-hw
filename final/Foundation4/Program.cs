// Foundation 4: Exercise/Fitness Polymorphism Project

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create activities
        RunningActivity running = new RunningActivity(new DateTime(2022, 11, 3), 30, 3.0);
        CyclingActivity cycling = new CyclingActivity(new DateTime(2022, 11, 3), 30, 10.0);
        SwimmingActivity swimming = new SwimmingActivity(new DateTime(2022, 11, 3), 30, 20);

        // Add activities to list
        List<Activity> activities = new List<Activity>();
        activities.Add(running);
        activities.Add(cycling);
        activities.Add(swimming);

        // Display summary for each activity
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

class Activity
{
    private DateTime date;
    protected int durationMinutes;

    public Activity(DateTime date, int durationMinutes)
    {
        this.date = date;
        this.durationMinutes = durationMinutes;
    }

    public virtual double GetDistance()
    {
        return 0;
    }

    public virtual double GetSpeed()
    {
        return 0;
    }

    public virtual double GetPace()
    {
        return 0;
    }

    public virtual string GetSummary()
    {
        return $"{date.ToShortDateString()} {GetType().Name} ({durationMinutes} min)";
    }
}

class RunningActivity : Activity
{
    private double distance;

    public RunningActivity(DateTime date, int durationMinutes, double distance)
        : base(date, durationMinutes)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return distance / (durationMinutes / 60.0);
    }

    public override double GetPace()
    {
        return durationMinutes / distance;
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $"- Distance: {distance} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.00} min per mile";
    }
}

class CyclingActivity : Activity
{
    private double speed;

    public CyclingActivity(DateTime date, int durationMinutes, double speed)
        : base(date, durationMinutes)
    {
        this.speed = speed;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetDistance()
    {
        return speed * (durationMinutes / 60.0);
    }

    public override double GetPace()
    {
        return 60 / speed;
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $"- Distance: {GetDistance():0.0} miles, Speed: {speed:0.0} mph, Pace: {GetPace():0.00} min per mile";
    }
}

class SwimmingActivity : Activity
{
    private int laps;

    public SwimmingActivity(DateTime date, int durationMinutes, int laps)
        : base(date, durationMinutes)
    {
        this.laps = laps;
    }

   public override double GetDistance()
    {
        return (laps * 50) / 1000 * 0.62; // Distance in miles (converts meters to km and then to miles)
    }

    public override double GetSpeed()
    {
        return GetDistance() / (durationMinutes / 60.0);
    }

    public override double GetPace()
    {
        return 60.0 / GetSpeed(); // Pace in minutes per mile (minutes / distance)
    }

    public override string GetSummary()
    {
        return base.GetSummary() + $"- Distance: {GetDistance():0.0} miles, Speed: {GetSpeed():0.0} mph, Pace: {GetPace():0.00} min per mile";
    }
}