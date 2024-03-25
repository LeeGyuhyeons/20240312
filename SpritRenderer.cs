using SDL2;

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
        SDL.SDL_GetTicks64();

        if (transform != null)
        {
            Console.SetCursorPosition(transform.x, transform.y);
            Console.Write(Shape);

            Engine myEngine = Engine.GetInstance();

            SDL.SDL_Rect myRect = new SDL.SDL_Rect();
            myRect.x = transform.x * 30;
            myRect.y = transform.y * 30;
            myRect.w = 30;
            myRect.h = 30;

            if(renderOrder == RenderOder.Floor)
            {
                SDL.SDL_SetRenderDrawColor(myEngine.myRenderer, 0, 255, 0, 0);
            }
            else if (renderOrder == RenderOder.Wall)
            {
                SDL.SDL_SetRenderDrawColor(myEngine.myRenderer, 255, 255, 0, 0);
            }
            else if (renderOrder == RenderOder.Player)
            {
                SDL.SDL_SetRenderDrawColor(myEngine.myRenderer, 0, 0, 255, 0);
            }
            else if (renderOrder == RenderOder.Monster)
            {
                SDL.SDL_SetRenderDrawColor(myEngine.myRenderer, 255, 0, 255, 0);
            }
            else if (renderOrder == RenderOder.Goal)
            {
                SDL.SDL_SetRenderDrawColor(myEngine.myRenderer, 255, 255, 255, 0);
            }

            SDL.SDL_RenderFillRect(myEngine.myRenderer, ref myRect);
        }

    }
}
