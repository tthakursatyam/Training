using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

class IList
{
    public static void main()
    {
        List<int> listNumbers = new List<int> { 10, 20, 30 };

        Collection<int> collectionNumbers = new Collection<int> { 100, 200, 300 };

        ObservableCollection<int> observableNumbers = new ObservableCollection<int> { 1000, 2000, 3000 };

        Console.WriteLine("Calling method that accepts List:");
        PrintList(listNumbers);

        // This line will NOT work because the method expects List
        // PrintList(collectionNumbers);  

        Console.WriteLine();
        Console.WriteLine("Calling method that accepts IList:");

        PrintIList(listNumbers);
        PrintIList(collectionNumbers);
        PrintIList(observableNumbers);
    }

    static void PrintList(List<int> numbers)
    {
        foreach (var n in numbers)
        {
            Console.WriteLine(n);
        }
    }

    static void PrintIList(IList<int> numbers)
    {
        foreach (var n in numbers)
        {
            Console.WriteLine(n);
        }
    }
}