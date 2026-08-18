int Champernowne()
{
    var highestLength = 1000000;

    // There's definitely a mathematical way to calculate this but we can brute force it.

    // var digits = "";
    // var counter = 1;
    // while (digits.Length <= highestLength)
    // {
    //     digits += $"{counter}";
    //     counter++;
    // }

    // int[] indices = [1, 10, 100, 1000, 10000, 100000, 1000000];
    // int output = 1;
    // foreach (var index in indices)
    // {
    //     output *= (int)char.GetNumericValue(digits[index - 1]);
    // }

    var counter = 0;
    var currentNumber = 0;
    HashSet<int> indices = [1, 10, 100, 1000, 10000, 100000, 1000000];
    List<int> values = [];
    while (counter < highestLength)
    {
        currentNumber++;

        foreach (var c in $"{currentNumber}")
        {
            counter++;
            var converted = (int)char.GetNumericValue(c);
            if (indices.Contains(counter))
            {
                values.Add(converted);
            }
        }
    }

    var output = 1;
    foreach (var value in values)
    {
        output *= value;
    }

    return output;
}

var result = Champernowne();
if (result != 210)
{
    throw new Exception($"{result} is incorrect, expected 210");
}

Console.WriteLine("All tests passed.");
