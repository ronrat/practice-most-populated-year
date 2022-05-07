/*
  1900 2 0 2   2
  1901 1 0 1   3
  1910 2 0 2   5
  1911 0 3 -3  2
  1925 0 1 1   3
  1999 1 0 1   4
  2000 0 1 -1  3
  2001 1 0 1   4

*/

var lifeSpans = new LifeSpan[]
{
  new () { BirthYear = 1900, DeathYear = 1910 },
  new () { BirthYear = 1901, DeathYear = 1910 },
  new () { BirthYear = 1900, DeathYear = 1910 },
  new () { BirthYear = 1910, DeathYear = 1925 },
  new () { BirthYear = 1910, DeathYear = 2000 },
  new () { BirthYear = 1999, DeathYear = 2045 },
  new () { BirthYear = 2001, DeathYear = 2002 },
};

var yearWithMostPopulation = GetYearWithHighestPopulation(lifeSpans); // runtime O(L), L = lifespans length

Console.WriteLine($"Year: {yearWithMostPopulation}");

int GetYearWithHighestPopulation(LifeSpan[] lifeSpans)
{
  (int firstBirthYear, int lastBirthYear) = GetFirstAndLastBirthYears(lifeSpans); // O(L)
  int[] populationYearDeltas = new int[lastBirthYear - firstBirthYear + 1];  // O(L)

  UpdateDeltas(populationYearDeltas, firstBirthYear, lifeSpans); // O(L)
  var (mostPopulatedYearIndex, mostPopulatedYearPopulation) = FindMostPopulatedYearIndexAndTotal(populationYearDeltas); // O(L)

  Console.WriteLine($"Most Population: {mostPopulatedYearPopulation}");
  return firstBirthYear + mostPopulatedYearIndex;
}

(int mostPopulatedYearIndex, int mostPopulatedYearPopulation) FindMostPopulatedYearIndexAndTotal(int[] populationYearDeltas)
{
  int mostPopulatedYearIndex = 0;
  int mostPopulatedYearPopulation = 0;
  int currentYearPopulation = 0;

  for (int yearIndex = 0; yearIndex < populationYearDeltas.Length; yearIndex++)
  {
    currentYearPopulation += populationYearDeltas[yearIndex];
    if (currentYearPopulation > mostPopulatedYearPopulation)
    {
      mostPopulatedYearPopulation = currentYearPopulation;
      mostPopulatedYearIndex = yearIndex;
    }
  }

  return (mostPopulatedYearIndex, mostPopulatedYearPopulation);
}

void UpdateDeltas(int[] populationYearDeltas, int firstBirthYear, LifeSpan[] lifeSpans)
{
  foreach (var lifeSpan in lifeSpans)
  {
    populationYearDeltas[lifeSpan.BirthYear - firstBirthYear]++;
    var deathYearIndex = lifeSpan.DeathYear - firstBirthYear + 1;
    if (deathYearIndex < populationYearDeltas.Length) populationYearDeltas[deathYearIndex]--;
  }
}

(int firstBirthYear, int lastBirthYear) GetFirstAndLastBirthYears(LifeSpan[] lifeSpans)
{
  int firstBirthYear = int.MaxValue;
  int lastBirthYear = int.MinValue;

  foreach (var lifeSpan in lifeSpans)
  {
    if (lifeSpan.BirthYear < firstBirthYear) firstBirthYear = lifeSpan.BirthYear;
    if (lifeSpan.DeathYear > lastBirthYear) lastBirthYear = lifeSpan.DeathYear;
  }

  return (firstBirthYear, lastBirthYear);
}

/*
10 30
15 45
5 25
46 70

123
234
345
567
*/
