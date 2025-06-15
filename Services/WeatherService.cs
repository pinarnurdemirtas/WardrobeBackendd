namespace WardrobeBackendd.Services;

public class WeatherService
{
    public string GetSeason(double temperature)
    {
        if (temperature < 10) return "Kış";
        if (temperature < 15) return "Sonbahar";
        if (temperature < 20) return "İlkbahar";
        return "Yaz";
    }
}