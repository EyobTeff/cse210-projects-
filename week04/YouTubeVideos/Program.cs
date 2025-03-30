using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<Video> videos = new List<Video>();

        // Creating Video 1
        Video video1 = new Video("C# Beginner Tutorial", "Code Academy", 600);
        video1.AddComment("Alice", "Great tutorial, very helpful!");
        video1.AddComment("Bob", "I love the examples given.");
        video1.AddComment("Charlie", "Can you make one for advanced topics?");
        videos.Add(video1);

        // Creating Video 2
        Video video2 = new Video("The Future of AI", "Tech Talks", 900);
        video2.AddComment("Dave", "Super insightful discussion.");
        video2.AddComment("Eve", "AI is going to change everything!");
        video2.AddComment("Frank", "Can you cover quantum computing next?");
        videos.Add(video2);

        // Creating Video 3
        Video video3 = new Video("History of Space Exploration", "NASA Learning", 1200);
        video3.AddComment("Grace", "Loved the way this was presented!");
        video3.AddComment("Hank", "More people should watch this.");
        video3.AddComment("Ivy", "Space travel is so fascinating.");
        videos.Add(video3);

        // Displaying all videos
        foreach (Video video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
