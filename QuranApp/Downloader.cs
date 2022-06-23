public class Downloader
{
    private readonly string _baseUrl = "https://server7.mp3quran.net/s_gmd";

    public string Path { get; set; }

    public string Url { get; set; }
    public Downloader(int number)
    {
        Number = number;
        Path = $"{Number:D3}.mp3";
        Url = String.Format($"{_baseUrl}/{Path}");
    }

    private int Number { get; set; }

    public void Download(string url)
    {
        var client = new HttpClient();
        var bytes = client.GetByteArrayAsync(Url).Result;
        File.WriteAllBytesAsync(Path, bytes);
    }
}
