using System.Reflection;

internal class Program
{
    
    
 
    static void Main(string[] args)
    {
        Engine engine = Engine.GetInstance();

        engine.Init();
        engine.LoadScene("level02.map");
        engine.Run();
        engine.Term();

    }
}