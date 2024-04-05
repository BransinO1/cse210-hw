// Foundation 3: Event Planning Inheritance Project

using System;

class Program
{
    static void Main(string[] args)
    {
        // Create events
        LectureEvent lectureEvent = new LectureEvent(
            "Lecture Title",
            "A lecture on a interesting topic",
            new DateTime(2024, 4, 15, 14, 0, 0),
            "123 Main St",
            "Rexburg",
            "ID",
            "USA",
            "John Doe",
            50
        );

        ReceptionEvent receptionEvent = new ReceptionEvent(
            "Reception Title",
            "An elegant reception",
            new DateTime(2024, 5, 20, 18, 0, 0),
            "456 Maple St",
            "Hockeytown",
            "CA",
            "Canada",
            "Ehh@example.com"
        );

        OutdoorEvent outdoorEvent = new OutdoorEvent(
            "Outdoor Event Title",
            "A fun outdoor gathering",
            new DateTime(2024, 6, 25, 12, 0, 0),
            "789 Elm St",
            "Dolphintown",
            "FL",
            "USA",
            "Sunny and warm"
        );

        // Generate marketing messages for each event
        Console.WriteLine("Lecture Event:");
        Console.WriteLine("Standard Details:");
        Console.WriteLine(lectureEvent.GenerateStandardDetails());
        Console.WriteLine("Full Details:");
        Console.WriteLine(lectureEvent.GenerateFullDetails());
        Console.WriteLine("Short Description:");
        Console.WriteLine(lectureEvent.GenerateShortDescription());
        Console.WriteLine();

        Console.WriteLine("Reception Event:");
        Console.WriteLine("Standard Details:");
        Console.WriteLine(receptionEvent.GenerateStandardDetails());
        Console.WriteLine("Full Details:");
        Console.WriteLine(receptionEvent.GenerateFullDetails());
        Console.WriteLine("Short Description:");
        Console.WriteLine(receptionEvent.GenerateShortDescription());
        Console.WriteLine();

        Console.WriteLine("Outdoor Event:");
        Console.WriteLine("Standard Details:");
        Console.WriteLine(outdoorEvent.GenerateStandardDetails());
        Console.WriteLine("Full Details:");
        Console.WriteLine(outdoorEvent.GenerateFullDetails());
        Console.WriteLine("Short Description:");
        Console.WriteLine(outdoorEvent.GenerateShortDescription());
    }
}

class Event
{
    private string title;
    private string description;
    private DateTime dateTime;
    private Address address;

    public Event(string title, string description, DateTime dateTime, string street, string city, string state, string country)
    {
        this.title = title;
        this.description = description;
        this.dateTime = dateTime;
        address = new Address(street, city, state, country);
    }

    public string GenerateStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate & Time: {dateTime}\nAddress: {address.GetFullAddress()}";
    }

    public virtual string GenerateFullDetails()
    {
        return GenerateStandardDetails();
    }

    public virtual string GenerateShortDescription()
    {
        return $"Type: {GetType().Name}\nTitle: {title}\nDate: {dateTime.ToShortDateString()}";
    }
}

class LectureEvent : Event
{
    private string speaker;
    private int capacity;

    public LectureEvent(string title, string description, DateTime dateTime, string street, string city, string state, string country, string speaker, int capacity)
        : base(title, description, dateTime, street, city, state, country)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GenerateFullDetails()
    {
        return base.GenerateStandardDetails() + $"\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }
}

class ReceptionEvent : Event
{
    private string rsvpEmail;

    public ReceptionEvent(string title, string description, DateTime dateTime, string street, string city, string state, string country, string rsvpEmail)
        : base(title, description, dateTime, street, city, state, country)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GenerateFullDetails()
    {
        return base.GenerateStandardDetails() + $"\nType: Reception\nRSVP Email: {rsvpEmail}";
    }
}

class OutdoorEvent : Event
{
    private string weatherForecast;

    public OutdoorEvent(string title, string description, DateTime dateTime, string street, string city, string state, string country, string weatherForecast)
        : base(title, description, dateTime, street, city, state, country)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GenerateFullDetails()
    {
        return base.GenerateStandardDetails() + $"\nType: Outdoor Gathering\nWeather Forecast: {weatherForecast}";
    }
}

class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public string GetFullAddress()
    {
        return $"{street}, {city}, {state}, {country}";
    }
}