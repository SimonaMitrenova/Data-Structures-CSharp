namespace BiDictionary
{
    public class BiDictionaryMain
    {
        public static void Main(string[] args)
        {
            var distances = new BiDictionary<string, string, int>();

            distances.Add("Sofia", "Varna", 443);
            distances.Add("Sofia", "Varna", 468);
            distances.Add("Sofia", "Varna", 490);
            distances.Add("Sofia", "Plovdiv", 145);
            distances.Add("Sofia", "Bourgas", 383);
            distances.Add("Plovdiv", "Bourgas", 253);
            distances.Add("Plovdiv", "Bourgas", 292);

            var distancesFromSofia = distances.FindByFirstKey("Sofia"); // [443, 468, 490, 145, 383]
            var distancesToBourgas = distances.FindBySecondKey("Bourgas"); // [383, 253, 292]
            var distancesPlovdivBourgas = distances.Find("Plovdiv", "Bourgas"); // [253, 292]
            var distancesRousseVarna = distances.Find("Rousse", "Varna"); // []
            var distancesSofiaVarna = distances.Find("Sofia", "Varna"); // [443, 468, 490]
            bool succeeded = distances.Remove("Sofia", "Varna"); // true
            var distancesFromSofiaAgain = distances.FindByFirstKey("Sofia"); // [145, 383]
            var distancesToVarna = distances.FindBySecondKey("Varna"); // []
            var distancesSofiaVarnaAgain = distances.Find("Sofia", "Varna"); // []
        }
    }
}
