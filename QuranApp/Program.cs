using System.Diagnostics;

Menu();


static void Menu()
{

    Get(Select());
    Console.WriteLine("\t:)  Enjoy...");
    Console.WriteLine();
    
    Menu();
}

static void OpenFile(string path)
{
    Process.Start("cmd.exe", $"/C {path}");
    Console.WriteLine("\t:)  Enjoy...");
    Show(path);
}

static void Open(string path)
{
    string s = Console.ReadLine();
    if (s == "Y")
    {
        OpenFile(path);
        Console.Write("\n\tEnter the desired surah number:\t");
    }
    Menu();
}

static int Select()
{
mark:
    Console.Write("\n\tEnter the desired surah number:\t");
    
    int n = 0;
    
    try
    {
        n = int.Parse(Console.ReadLine());
        string path = $"{n:D3}.mp3";
        if (n < 1 || n > 114)
        {
            throw new Exception();
        }
        else if (File.Exists(path))
        {
            Console.Write("\tThere is such a surah in your local files, do you want to open it? (Y/N) :");
            Open(path);
            goto mark;
        }
    }
    catch (Exception)
    {
        Console.Write("\tNo such surah was found !\n\tPlease try again :");
        goto mark;
    }
    return n;
}

static void Get(int n)
{
    Console.Write($"\tDownloading start..");
    Downloader file = new Downloader(n);
    file.Download(file.Url);
    Console.WriteLine("\tDownloaded");
    Show(file.Path);
    Console.WriteLine("\tWould you open the file?");
    OpenFile(file.Path);
}

static void Show(string path)
{
    string tab = "\t";

    var infoSurah = TagLib.File.Create(path).Tag;
    var infoFile = new FileInfo(path);
    Console.WriteLine($"\n{tab}Surah: {infoSurah.Title}");
    Console.WriteLine($"{tab}Author: {infoSurah.FirstPerformer}");
    Console.WriteLine($"{tab}Album: {infoSurah.Album}");
    Console.WriteLine($"{tab}Year: {infoSurah.Year}");
    Console.WriteLine();
    Console.WriteLine($"{tab}File size: {infoFile.Length / ((float)(1024 * 1024)):F2} MB");
    Console.WriteLine($"{tab}File Name: {infoFile.Name}");
    Console.WriteLine($"{tab}Full path: {infoFile.FullName}");
}