
using SDL2;

class PlayerController : Component
{
    public PlayerController()
    {

    }
    ~PlayerController()
    {

    }
    public override void Update()
    {
        if (transform == null)
        {
            return;
        }
        int oldX = transform.x;
        int oldY = transform.y;
        //Collider2D collider = gameObject.GetComponent<Collider2D>();
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_a))
        {
            transform.Translate(-1, 0);

            //if (collider != null)
            //{
            //    if (oldX - 1 != collider.transform.x)
            //    {
            //        transform.Translate(-1, 0);
            //    }

            //}

        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_d))
        {
            transform.Translate(1, 0);
        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_w))
        {
            transform.Translate(0, -1);
        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_s))
        {
            transform.Translate(0, 1);
        }
        if (Input.GetKey(SDL.SDL_Keycode.SDLK_ESCAPE))
        {
            //singleton pattern
            Engine.GetInstance().Stop();
        }

        transform.x = Math.Clamp(transform.x, 0, 80);
        transform.y = Math.Clamp(transform.y, 0, 80);

        //find new x, new y 해당 게임오브젝트 탐색
        //찾은 게임 오브젝트에서 Collider2D 그리고 충돌 체크
        foreach (GameObject findGameObject in Engine.GetInstance().gameObjects)
        {
            //Collider2D 컴포넌트를 가지는 모든 게임 오브젝트를 찾는다.
            //자기 자신은 제외
            //Player와 충돌 하는 체크
            if (findGameObject == gameObject)
            {
                continue;
            }

            Collider2D? findComponent = findGameObject.GetComponent<Collider2D>();
            if (findComponent != null)
            {
                if (findComponent.Check(gameObject) && findComponent.isTrigger == false)
                {
                    //충돌
                    transform.x = oldX;
                    transform.y = oldY;
              
                    break;
                }
                if (findComponent.Check(gameObject) && findComponent.isTrigger == true)
                {
                    OnTrigger(findGameObject);
                }
            }
        }
    }
    public void OnTrigger(GameObject other)
    {
        //겹쳤을 때 처리 할 로직
        if(other.name == "Monster")
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Engine.GetInstance().Find("GameManager").GetComponent<GameManager>().isGameOver = true;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        else if (other.name == "Goal")
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Engine.GetInstance().Find("GameManager").GetComponent<GameManager>().isNextStage = true;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        }
    }
}
