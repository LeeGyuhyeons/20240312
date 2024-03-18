class Transform : Component
{
    //public override void Start()
    //{
        
    //}

    //public override void Update()
    //{
        
    //}

    public Transform()
    {
        x = 0; 
        y = 0;
    }

    public int x;
    public int y;

    public void Translate(int xOffset, int yOffset) 
    {
        x += xOffset;
        y += yOffset;
    }
}

