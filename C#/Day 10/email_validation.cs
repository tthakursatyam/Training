using System;
using System.Text.RegularExpressions;
class Task1
{
    public static void func1()
    {
        List<string> Emails = new List<string>
        {
            "john.doe@gmail.com",
            "alice_123@yahoo.in",
            "mark.smith@company.com",
            "support-abc@banking.co.in",
            "user.nametag@domain.org",
            "john.doe@gmail",          // Missing domain extension
            "alice@@yahoo.com",        // Double @
            "mark.smith@.com",         // Domain missing name
            "support@banking..com",    // Double dot in domain
            "user name@gmail.com",     // Space not allowed
            "@domain.com",             // Missing username
            "admin@domain",            // No top-level domain
            "info@domain,com",         // Comma instead of dot
            "finance#dept@corp.com",   // Invalid character #
            "plainaddress"             // Missing @ and domain

        };
        string pattern = @"^[\w.-]+@[\w-]+(\.[\w]{2,})+$";
        foreach(string input in Emails)
        {
            if (Regex.IsMatch(input, pattern))
            {
                Console.WriteLine(input+" :Email validation succesfull!");
            }
            else
            {
                Console.WriteLine(input+" :Email validation failed!");
            }
        }
    }
}