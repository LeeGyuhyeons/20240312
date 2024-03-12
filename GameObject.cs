class GameObject
{
    public int x;
    public int y;

    public GameObject()
    {
        x = 0;
        y = 0;
    }

    ~GameObject()
    {

    }

    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }

    public virtual void Render()
    {

    }

    public char shape;
}
