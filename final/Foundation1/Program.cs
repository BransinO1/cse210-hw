// Foundation 1: Youtube Abstraction Project

using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store videos
        List<Video> videos = new List<Video>();

        // Create 3-4 videos and add them to the list
        videos.Add(new Video("Video 1", "Author 1", 120));
        videos.Add(new Video("Video 2", "Author 2", 180));
        videos.Add(new Video("Video 3", "Author 3", 150));

        // Add comments to each video
        videos[0].AddComment("Commenter 1", "First comment.");
        videos[0].AddComment("Commenter 2", "Great video!");
        videos[1].AddComment("Commenter 3", "Nice content!");
        videos[1].AddComment("Commenter 1", "Keep it up!");
        videos[2].AddComment("Commenter 2", "Interesting topic.");
        videos[2].AddComment("Commenter 3", "I learned a lot.");

        // Display information about each video
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.Length} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (var comment in video.Comments)
            {
                Console.WriteLine($" - {comment.Name}: {comment.Text}");
            }
            Console.WriteLine();
        }
    }
}

class Video
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Length { get; private set; }
    public List<Comment> Comments { get; private set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(string name, string text)
    {
        Comments.Add(new Comment(name, text));
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string Name { get; private set; }
    public string Text { get; private set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
