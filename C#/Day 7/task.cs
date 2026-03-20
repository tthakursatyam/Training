class task2
{
public static string GenerateKey(string input)
{
    if (string.IsNullOrEmpty(input) || input.Length < 6)
        return string.Empty;

    foreach (char c in input)
    {
        if (!char.IsLetter(c))
            return string.Empty;
    }

    input = input.ToLower();

    List<char> chars = new List<char>();
    foreach (char c in input)
    {
        if ((int)c % 2 != 0)
            chars.Add(c);
    }

    chars.Reverse();

    for (int i = 0; i < chars.Count; i++)
    {
        if (i % 2 == 0)
            chars[i] = char.ToUpper(chars[i]);
    }

    return new string(chars.ToArray());
}


}