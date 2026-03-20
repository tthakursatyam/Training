using System;
using System.Threading;

namespace CallbackDemo
{
    // STEP 1: Define the Delegate
    public delegate void DownloadFinishedHandler(string fileName);

    class FileDownloader
    {
        // STEP 2: Method that accepts the callback
        public void DownloadFile(string name, DownloadFinishedHandler callback)
        {
            Console.WriteLine($"Starting download: {name}...");
            
            // Simulating work
            Thread.Sleep(2000); 
            
            Console.WriteLine($"{name} download complete.");

            // STEP 3: Execute the Callback
            if (callback != null)
            {
                callback(name); 
            }
        }
    }

    // class Program
    // {
    //     // STEP 4: The actual Callback Method
    //     static void DisplayNotification(string file)
    //     {
    //         Console.WriteLine($"NOTIFICATION: You can now open {file}.");
    //     }

    //     static void Main()
    //     {
    //         FileDownloader downloader = new FileDownloader();

    //         // Pass the method 'DisplayNotification' as a callback
    //         downloader.DownloadFile("Presentation.pdf", DisplayNotification);
    //     }
    //}
}