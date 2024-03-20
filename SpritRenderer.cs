public enum RenderOder
{
    None = 0,
    Floor = 100,
    Wall = 200,
    Goal = 300,
    Player = 400,
    Monster = 500,
}

class SpritRenderer : Renderer
{
    public SpritRenderer()
    {
        renderOrder = RenderOder.None;
    }

    public char Shape;
    public RenderOder renderOrder;

    public override void Render()
    {
        if (transform != null)
        {
            Console.SetCursorPosition(transform.x, transform.y);
            Console.Write(Shape);

        }
    }
}
