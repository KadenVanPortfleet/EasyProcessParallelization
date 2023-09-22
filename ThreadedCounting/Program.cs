// See https://aka.ms/new-console-template for more information
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

class ThreadedCounting
{
    
    public static void Main()
    {
        //List to store scores.
        LinkedList<int> scoreList = new LinkedList<int>();
        
        //User info
        Console.WriteLine("Beginning");

        //Get time before bench.
        DateTime now = DateTime.Now;


        //Threaded Workload
        WaitCallback workItem = (data) => 
        {
            //Start individual counter for every CPU thread. Idea is to us each CPUs own register.
            int core_score = 0;
            while (DateTime.Now < now.AddSeconds(5))
            { 
                core_score++;
            }
            Console.WriteLine("Thread Done!");
            scoreList.AddLast(core_score);
            
        };

        //Produce worker threads. i = Number of threads to produce.
        for (int i = 0; i < 1; i++)
        {
            ThreadPool.QueueUserWorkItem(workItem, scoreList);
        }

        //Sleep for long enough to let all threads complete their work.
        Thread.Sleep(6000);

        //Write scores to console.
        foreach (int score in scoreList) 
        {
            Console.WriteLine($"Score: {score}");
        }

        //Write sum of scores to console.
        Console.WriteLine(scoreList.Sum());
    }
}
