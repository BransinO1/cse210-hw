using System;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._jobTitle = "Software Developer";
        job1._company = "Micron";
        job1._startYear = 2020;
        job1._endYear = 2022;

        Job job2 = new Job();
        job2._jobTitle = "Head Software Engineer";
        job2._company = "Simplot";
        job2._startYear = 2022;
        job2._endYear = 2023;

        Resume myResume = new Resume();
        myResume._name = "Corbin Meacham";

        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        myResume.Display();
    }
}