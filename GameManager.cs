
class GameManager : Component
{
    public bool isGameOver;
    public bool isNextStage;

    public GameManager()
    {
        isGameOver = false;
        isNextStage = false;
    }

    public override void Update()
    {
        if (isGameOver)
        {
            Engine.GetInstance().Stop();
            Console.Clear();
            Console.WriteLine("GameOver");
        }

        if (isNextStage)
        {
            Console.Clear();
            Console.WriteLine("GameClear");
            Console.ReadKey();

            Engine.GetInstance().NextLoadScene("Level02.map");

            //Engine.GetInstance().Term();
            //Engine.GetInstance().LoadScene("Level02.map");
        }
    }
}
