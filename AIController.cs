
class AIController : Component
{
    public AIController()
    {

    }

    ~AIController()
    {

    }
    public override void Update()
    {
        Random random = new Random();
        int oldX = transform.x;
        int oldY = transform.y;

        int NextDirection = random.Next(0, 4);
        if (NextDirection == 0)
        {
            transform.Translate(-1, 0);

     
        }
        if (NextDirection == 1)
        {
            transform.Translate(1, 0);
        }
        if (NextDirection == 2)
        {
            transform.Translate(0, -1);
        }
        if (NextDirection == 3)
        {
            transform.Translate(0, 1);
        }
      

        transform.x = Math.Clamp(transform.x, 0, 80);
        transform.y = Math.Clamp(transform.y, 0, 80);

        
        foreach (GameObject findGameObject in Engine.GetInstance().gameObjects)
        {
           
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
            }
        }
    }
}
