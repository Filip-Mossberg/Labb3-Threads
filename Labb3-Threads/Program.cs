namespace Labb3_Threads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Car Volvo = new Car(1, "Volvo", 150, "Blue");
            Car Audi = new Car(2, "Audi", 150, "Green");

            Race.Start(Volvo, Audi);
        }
    }
}