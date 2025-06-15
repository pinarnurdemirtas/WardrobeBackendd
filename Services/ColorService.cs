namespace WardrobeBackendd.Services
{
    public class ColorService
    {
        private readonly Dictionary<string, List<string>> compatibleColors = new()
        {
            { "Siyah", new List<string> { "Beyaz", "Kırmızı", "Gri" } },
            { "Beyaz", new List<string> { "Siyah", "Mavi", "Krem" } },
            { "Mavi", new List<string> { "Beyaz", "Gri", "Krem" } },
            { "Kırmızı", new List<string> { "Siyah", "Beyaz" } },
            { "Gri", new List<string> { "Siyah", "Beyaz" } },
           
        };

        public bool AreColorsCompatible(string color1, string color2)
        {
            return compatibleColors.TryGetValue(color1, out var list) && list.Contains(color2);
        }
    }
}