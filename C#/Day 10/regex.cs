using System;
using System.Text.RegularExpressions;
class Reg
{
    public static void func1()
    {
        string str1 = "abc123";
        string pattern1 = @"\d";
        
        str1="123_123";
        
        
        
        bool result = Regex.IsMatch(str1,pattern1);
        Console.WriteLine(result);

            //      ***\d***        //
        Match m1 = Regex.Match("Amount:5000",@"\d+");
        Console.WriteLine(m1.Value);

        Match m2 = Regex.Match("Amount:5000",@"\d*");
        Console.WriteLine(m2.Value);

        Match m3 = Regex.Match("Amount:5000",@"\d*$");
        Console.WriteLine(m3.Value);

        Match m4 = Regex.Match("10A20B30",@"\d*$");
        Console.WriteLine(m4.Value);

        //      ***\D***        //
        Match m5 = Regex.Match("10A20B30C",@"\D*$");
        Console.WriteLine(m5.Value);

        
                //      ***\w***        //
        Match m6 = Regex.Match("10A20B30",@"\w+");
        Console.WriteLine(m6.Value);

        Match m7 = Regex.Match("10A20B30",@"\w*");
        Console.WriteLine(m7.Value);

        Match m8 = Regex.Match("10A20B30",@"\w");
        Console.WriteLine(m8.Value);

        // MatchCollection m9 = Regex.Matches("10A20B30",@"\w");
        // Console.WriteLine(m9);

        Match m10 = Regex.Match("10A20B30C!@ abc ",@"\w");
        Console.WriteLine(m10.Value);


                //      ***\s***        //
        Match m11 = Regex.Match("10A20B30  ",@"\s");
        Console.WriteLine(m11.Value);

        Match m12 = Regex.Match("10A20B30     ",@"\S+");
        Console.WriteLine(m12.Value);

        Match m13 = Regex.Match("10.txtA20B30  file.txt",@"\.txt+");
        Console.WriteLine(m13.Value);

        Match m14 = Regex.Match("10.txtA20?B30 C:\abc\file.txt?",@"\?+");
        Console.WriteLine(m14.Value);

        Match m15 = Regex.Match("10.txtA20?B30 C:\abc\file.txt?",@"\\");
        Console.WriteLine(m15.Value);

        MatchCollection m16 = Regex.Matches(@"10.txtA20?B30 C:\abc\file.txt?",@"\\");
        Console.WriteLine(m16.Count);

        Match m17 = Regex.Match("Hello0.txtA20?B30 C:\abc\file.txt?Hello",@"^Hello$");
        Console.WriteLine(m17.Value);

        Match m18 = Regex.Match("Hello10.txtA20?B30 C:\abc\file.txt?",@"^Hello");
        Console.WriteLine(m18.Value);

        Match m19 = Regex.Match("Hello10.txtA20?B30 C:\abc\file.txt?hello",@"lo$");
        Console.WriteLine(m19.Value);

        Match m20 = Regex.Match("Hello",@"^Hello$");
        Console.WriteLine(m20.Value);


        Match m21 = Regex.Match("Date:2025-12-30",@"(\d{4})-(\d{2})-(\d{2})");
        Console.WriteLine(m21.Value);

        Match m22 = Regex.Match("Date:2025/12/30",@"(\d{4})-(\d{2})-(\d{2})");
        Console.WriteLine(m22.Groups[1]);

        string str2 = "A,B;C";
        
        string[] sarray = Regex.Split(str2,@"[,;]");

        for(int i=0;i<sarray.Length;i++)
        {
            Console.Write(sarray[i]+" ");
        }
        Console.WriteLine("");


        //string str3 = Amount = 5000;


        string str4 = "23-02-1990";
        string str5 = "1992-08-23";
        string str6 = "1995-98-93,1992-08-23";

        string pattern4 = @"(?<year>\d{4})-(?<month>\d{2})-(?<day>\d{2})";

        Match m23 = Regex.Match(str4,pattern4);
        Console.WriteLine(m23.Groups["year"].Value);

        Match m24 = Regex.Match(str5,pattern4);
        Console.WriteLine(m24.Groups["month"].Value);

        MatchCollection m25 = Regex.Matches(str6,pattern4);
        foreach(Match i in m25)
        {
            Console.Write(i.Groups[1].Value+" ");
            Console.Write(i.Groups["month"].Value+" ");
            Console.Write(i.Groups["day"].Value+" ");
            Console.WriteLine("");
        }
        

        Match m26 = Regex.Match("frapa-e-e",@"(?<grp>a...e)");
        Console.WriteLine(m26.Groups["grp"]);
        Console.WriteLine(m26.Groups[0]);
        Console.WriteLine(m26.Groups[1]);
    }
}