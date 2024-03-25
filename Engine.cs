using SDL2;

class Engine
{
    protected Engine()
    {
        gameObjects = new List<GameObject>();
        isRunning = true;
    }

    ~Engine()
    {

    }
    private static Engine? instance;
    public static Engine GetInstance()
    {
        if (instance == null)
        {
            instance = new Engine();
        }
        return instance;
        //return instance ?? (instance = new Engine());
    }

    public List<GameObject> gameObjects;
    public bool isRunning;

    public bool isNextLoading = false;
    public string nextSceneName = string.Empty;

    public IntPtr myWindow;
    public IntPtr myRenderer;
    public SDL.SDL_Event myEvent;

    public ulong deltaTime;
    protected ulong lastTime;

    public void NextLoadScene(string _nextSceneName)
    {
        isNextLoading = true;
        nextSceneName = _nextSceneName;
    }

    public void Init()
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING) < 0) // 전부 초기화
        {
            Console.WriteLine("Init fail");
            return;
        }

        myWindow = SDL.SDL_CreateWindow("2D Engine", 100, 100, 640, 480, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN); // "타이틀", 위치, 위치, 크기, 크기, 화면에 그리게함

        myRenderer = SDL.SDL_CreateRenderer(myWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE); // 화면에 그릴 붓 만들기? 그래픽 카드 기능들 PRESENTVSYNC 모니터 주파수 맞추기

        Input.Init();

    }

    public void Stop()
    {
        isRunning = false;
    }
    public void LoadScene(string sceneName)
    {
#if DEBUG
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        string Dir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        string[] map = File.ReadAllLines(Dir + "/data/" + sceneName);
#else
        string Dir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
        string[] map = File.ReadAllLines(Dir + "/data/" + sceneName);
#endif
        //string Dir = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        //file로 읽어온다.
        //string[] map = File.ReadAllLines("../../../data/" + sceneName);

        GameObject newGameObject;
        for (int y = 0; y < map.Length; ++y)
        {
            for (int x = 0; x < map.Length; ++x)
            {
                if (map[y][x] == '*')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Wall";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpritRenderer renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = '*';
                    renderer.renderOrder = RenderOder.Wall;
                    Collider2D collider2D = newGameObject.AddComponent<Collider2D>();

                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = ' ';
                    renderer.renderOrder = RenderOder.Floor;

                }
                else if (map[y][x] == ' ')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpritRenderer renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = ' ';
                    renderer.renderOrder = RenderOder.Floor;
                }
                else if (map[y][x] == 'P')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Player";
                    newGameObject.transform.x = 1;
                    newGameObject.transform.y = 1;
                    SpritRenderer renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = 'P';
                    renderer.renderOrder = RenderOder.Player;
                    newGameObject.AddComponent<PlayerController>();
                    Collider2D collider2D = newGameObject.AddComponent<Collider2D>();
                    collider2D.isTrigger = true;


                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = ' ';
                    renderer.renderOrder = RenderOder.Floor;


                }
                else if (map[y][x] == 'M')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Monster";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpritRenderer renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = 'M';
                    renderer.renderOrder = RenderOder.Monster;
                    Collider2D collider2D = newGameObject.AddComponent<Collider2D>();
                    collider2D.isTrigger = true;
                    newGameObject.AddComponent<AIController>();


                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = ' ';
                    renderer.renderOrder = RenderOder.Floor;



                }
                else if (map[y][x] == 'G')
                {
                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Goal";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    SpritRenderer renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = 'G';
                    renderer.renderOrder = RenderOder.Goal;
                    Collider2D collider2D = newGameObject.AddComponent<Collider2D>();
                    collider2D.isTrigger = true;


                    newGameObject = Instantiate<GameObject>();
                    newGameObject.name = "Floor";
                    newGameObject.transform.x = x;
                    newGameObject.transform.y = y;
                    renderer = newGameObject.AddComponent<SpritRenderer>();
                    renderer.Shape = ' ';
                    renderer.renderOrder = RenderOder.Floor;


                }
            }
        }
        newGameObject = Instantiate<GameObject>();
        newGameObject.name = "GameManager";
        newGameObject.AddComponent<GameManager>();

        RenderSort();
    }

    public void Run()
    {
        while (isRunning)
        {
            ProcessInput();
            Update();
            Render();
            if (isNextLoading)
            {
                gameObjects.Clear();
                LoadScene(nextSceneName);
                isNextLoading = false;
                nextSceneName = string.Empty;
            }
        }//frame
    }
    public void Term()
    {
        gameObjects.Clear();

        SDL.SDL_DestroyRenderer(myRenderer);
        SDL.SDL_DestroyWindow(myRenderer);
        SDL.SDL_Quit();
    }

    public T Instantiate<T>() where T : GameObject, new()
    {
        T newObject = new T();
        gameObjects.Add(newObject);


        return newObject;
    }

    //public GameObject Instantiate(GameObject newGameObject)
    //{
    //    gameObjects.Add(newGameObject);
    //    return newGameObject;
    //}

    public void RenderSort()
    {

        for (int i = 0; i < gameObjects.Count; i++)
        {
            for (int j = i + 1; j < gameObjects.Count; j++)
            {
                SpritRenderer? prevRender = gameObjects[i].GetComponent<SpritRenderer>();
                SpritRenderer? nextRender = gameObjects[j].GetComponent<SpritRenderer>();

                if (prevRender != null && nextRender != null)
                {
                    if ((int)prevRender.renderOrder > (int)nextRender.renderOrder)
                    {
                        GameObject temp = gameObjects[i];
                        gameObjects[i] = gameObjects[j];
                        gameObjects[j] = temp;
                    }

                }

            }
        }
    }
    protected void ProcessInput()
    {
        SDL.SDL_PollEvent(out myEvent);
        //Input.keyInfo = Console.ReadKey();
    }
    protected void Update()
    {
        deltaTime = SDL.SDL_GetTicks64() - lastTime;
        foreach (GameObject gameObject in gameObjects)
        {
            foreach (Component component in gameObject.components)
            {
                component.Update();
            }
        }
        lastTime = SDL.SDL_GetTicks64();
    }
    protected void Render()
    {
        //for (int i = 0; i < gameObjects.Count; i++)
        //{
        //    gameObjects[i].Render();
        //}
        //Console.Clear();
        foreach (GameObject gameObject in gameObjects)
        {
            Renderer? renderer = gameObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.Render();
            }
        }

        SDL.SDL_RenderPresent(Engine.GetInstance().myRenderer);
    }

    public GameObject Find(string name)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            if (gameObject.name == name)
            {
                return gameObject;
            }
        }
#pragma warning disable CS8603 // Possible null reference return.
        return null;
#pragma warning restore CS8603 // Possible null reference return.
    }
}