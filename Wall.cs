
internal class Wall : GameObject
{

    public Wall(int newX, int newY)
    {
        shape = '*';

        x = newX;
        y = newY;
    }
    ~Wall()
    {

    }

    public override void Start()
    {

    }
    public override void Update()
    {

    }

    public override void Render()
    {
        base.Render();

    }
}
