class Component
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Component()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {

    }
    
    ~Component()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    //내가 어디 속해 있는지 확인 하는 용도
    public GameObject gameObject;

    //내가 속해 게임오브젝트의 이동을 처리하기 위해
    public Transform transform;
}

