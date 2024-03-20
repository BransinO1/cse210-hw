// Week04 - Prepare Assignment for Inheritance Learning

using System;

class Program
{
    static void Main(string[] args)
    {
        WritingAssignment writingAssignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");

        Console.WriteLine(writingAssignment.GetSummary());
        Console.WriteLine(writingAssignment.GetWritingInformation());
    }
}

public class Assignment
{
    protected string studentName;
    private string topic;

    public Assignment(string studentName, string topic)
    {
        this.studentName = studentName;
        this.topic = topic;
    }

    public string GetSummary()
    {
        return $"{studentName} - {topic}";
    }

    public string GetStudentName()
    {
        return studentName;
    }
}