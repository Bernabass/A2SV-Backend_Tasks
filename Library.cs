using System;
using System.Collections.Generic;

class Library
{
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Book> Books { get; set; }
    public List<MediaItem> MediaItems { get; set; }

    public Library(string name, string address)
    {
        Name = name;
        Address = address;
        Books = new List<Book>();
        MediaItems = new List<MediaItem>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        Books.Remove(book);
    }

    public void AddMediaItem(MediaItem item)
    {
        MediaItems.Add(item);
    }

    public void RemoveMediaItem(MediaItem item)
    {
        MediaItems.Remove(item);
    }

    public void PrintCatalog()
    {
        Console.WriteLine($"Library Name: {Name}");
        Console.WriteLine($"Address: {Address}");
        Console.WriteLine("\nBooks in the Library:");
        
        foreach (var book in Books)
        {
            Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN}, Year: {book.PublicationYear})");
        }

        Console.WriteLine("\nMedia Items in the Library:");
        foreach (var item in MediaItems)
        {
            Console.WriteLine($"{item.Title} ({item.MediaType}, Duration: {item.Duration} minutes)");
        }
    }
}

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public int PublicationYear { get; set; }

    public Book(string title, string author, string isbn, int publicationYear)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        PublicationYear = publicationYear;
    }
}

class MediaItem
{
    public string Title { get; set; }
    public string MediaType { get; set; }
    public int Duration { get; set; }

    public MediaItem(string title, string mediaType, int duration)
    {
        Title = title;
        MediaType = mediaType;
        Duration = duration;
    }
}

class Program
{
    static void Main()
    {
        var library = new Library("My Library", "123 mexico");

        var book1 = new Book("fikir eske mekabir", "Haddis Alemayehu", "123456789", 1992);
        var book2 = new Book("Baytewaru", "Albert Camu", "987654321", 1934);
        library.AddBook(book1);
        library.AddBook(book2);

        var mediaItem1 = new MediaItem("Inception", "DVD", 120);
        var mediaItem2 = new MediaItem("Shojo ji", "CD", 60);
        library.AddMediaItem(mediaItem1);
        library.AddMediaItem(mediaItem2);

        library.PrintCatalog();
    }
}
