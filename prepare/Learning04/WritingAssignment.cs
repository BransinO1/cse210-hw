// WritingAssignment.cs file   W04 Prep

using System;

public class WritingAssignment : Assignment
{
    private string essayTitle;

    public WritingAssignment(string studentName, string topic, string essayTitle)
        : base(studentName, topic)
    {
        this.essayTitle = essayTitle;
    }

    public string GetWritingInformation()
    {
        string studentName = GetStudentName();
        return $"{essayTitle} by {studentName}";
    }
}
