namespace AutonomousRobot.AI
{
    enum RobotAction
    {
        Stop,SlowDown,Reroute,Continue
    }
    class SensorReading
    {
        public int SensorID;
        public string Type;
        public double Value;
        public DateTime Timestamp;
        public double Confidence;
    }
    class DecisionEngine
    {
        public static List<SensorReading> GetRecentReadings(List<SensorReading> sensorHistory,DateTime fromTime)
        {
            var result = sensorHistory.Where(s=>s.Timestamp>=fromTime).ToList();
            return result;
        }

        public static bool IsBatteryCritical(List<SensorReading> readings)
        {
            var result = readings.Where(s=>s.Type=="Battery" && s.Value>20);
            if(result.Count()==0) return false;
            return true;
        }

        public static double GetNearestObstacleDistance(List<SensorReading> readings)
        {
            var result = readings.Where(s=>s.Type=="Distance").Min(s=>s.Value);
            return result;
        }
        public static bool IsTemperatureSafe(List<SensorReading> readings)
        {
            var result = readings.Where(s=>s.Type=="Temperature").All(s=>s.Value<90);
            return result;
        }
        public static double GetAverageVibration(List<SensorReading> readings)
        {
             var result = readings.Where(s=>s.Type=="Vibration").Average(s=>s.Value);
             return result;
        }
        public static Dictionary<string,double> CalculateSensorHealth(List<SensorReading> sensorHistory)
        {
            Dictionary<string,double> dict = new Dictionary<string,double>();
            
            dict = sensorHistory.GroupBy(s=>s.Type).ToDictionary(s=>s.Key,s=>s.Average(s=>s.Confidence));
            return dict;
        }
        public static List<string> DetectFaultySensors(List<SensorReading> sensorHistory)
        {
            var result = sensorHistory.Where(s=>s.Confidence<0.4).GroupBy(s=>s.Type).Where(s=>s.Count()>=2).Select(s=>s.Key).ToList();
            return result;
        }
    }

    class Program
    {
        public static void main()
        {
            
            List<SensorReading> sensor = new List<SensorReading>{
            new SensorReading { SensorID = 1, Type = "Distance", Value = 1, Timestamp = DateTime.Now, Confidence = 0.99 },
            new SensorReading { SensorID = 2, Type = "Battery", Value = 18, Timestamp = DateTime.Now, Confidence = 0.43 },
            new SensorReading { SensorID = 3, Type = "Temperature", Value = 40, Timestamp = DateTime.Now, Confidence = 0.83 },
            new SensorReading { SensorID = 4, Type = "Vibration", Value = 2, Timestamp = DateTime.Now, Confidence = 0.76 },
            new SensorReading { SensorID = 5, Type = "Battery", Value = 20, Timestamp = DateTime.Now, Confidence = 0.97 },
            new SensorReading { SensorID = 6, Type = "Distance", Value = 0.4, Timestamp = DateTime.Now, Confidence = 0.68 },
            new SensorReading { SensorID = 11, Type = "Distance", Value = 1, Timestamp = DateTime.Now, Confidence = 0.99 },
            new SensorReading { SensorID = 12, Type = "Battery", Value = 18, Timestamp = DateTime.Now, Confidence = 0.43 },
            new SensorReading { SensorID = 13, Type = "Temperature", Value = 40, Timestamp = DateTime.Now, Confidence = 0.83 },
            new SensorReading { SensorID = 14, Type = "Vibration", Value = 2, Timestamp = DateTime.Now, Confidence = 0.76 },
            new SensorReading { SensorID = 15, Type = "Battery", Value = 20, Timestamp = DateTime.Now, Confidence = 0.97 },
            new SensorReading { SensorID = 16, Type = "Distance", Value = 0.4, Timestamp = DateTime.Now, Confidence = 0.68 }
        };

        DecisionEngine obj = new DecisionEngine();

        //// TASK 1
        Console.WriteLine("\n");
        DateTime fromTime = DateTime.Now.AddSeconds(-10);
        List<SensorReading> listResult = DecisionEngine.GetRecentReadings(sensor, fromTime);
        foreach (var i in listResult)
        {
            Console.WriteLine(i.SensorID + " " + i.Type + " " + i.Value);
        }



        //// TASK 2
        Console.WriteLine("\n");
        bool Task_2_Result = DecisionEngine.IsBatteryCritical(sensor);
        Console.WriteLine("Answer for Task 2 (IsBatteryCritical) is: "+Task_2_Result);


        //// TASk 3
        Console.WriteLine("\n");
        double Task_3_Result = DecisionEngine.GetNearestObstacleDistance(sensor);
        Console.WriteLine("Answer for Task 3 (GetNearestObstacleDistance) is: "+Task_3_Result);


        //// TASk 4
        Console.WriteLine("\n");
        bool Task_4_Result = DecisionEngine.IsTemperatureSafe(sensor);
        Console.WriteLine("Answer for Task 4 (IsTemperatureSafe) is: "+Task_4_Result);


        //// TASk 5
        Console.WriteLine("\n");
        double Task_5_Result = DecisionEngine.GetAverageVibration(sensor);
        Console.WriteLine("Answer for Task 5 (GetAverageVibration) is: "+Task_5_Result);


        //// TASk 6
        Console.WriteLine("\n");
        Dictionary<string, double> Task_6_Result = DecisionEngine.CalculateSensorHealth(sensor);
        Console.WriteLine("Answer for Task 6 (CalculateSensorHealth) is: "+string.Join(" ",Task_6_Result));


        //// TASk 7
        Console.WriteLine("\n");
        List<string> Task_7_Result = DecisionEngine.DetectFaultySensors(sensor);
        Console.WriteLine("Answer for Task 7 (DetectFaultySensors) is:KO "+string.Join(" ",Task_7_Result));


        }
    }
}