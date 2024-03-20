class GameObject
{
    public Transform transform;

    public List<Component> components;
    
    public string name;

    public GameObject()
    {
        name = "";
        transform = new Transform();
        components = new List<Component>();

        components.Add(transform);
    }

    public T? GetComponent<T>() where T : Component
    {
        int findIndex = -1;
        for (int i = 0; i < components.Count; ++i)
        {
            if(components[i] is T)
            {
                findIndex = i;
                break;
            }
        }
        if (findIndex > 0)
        {
            return (T)components[findIndex];
        }
        else
        {
            return null;
        }
    }

    public T AddComponent<T>() where T : Component, new()
    {
        T newT = new T();
        newT.gameObject = this;
        newT.transform = transform;
        components.Add(newT);

        return newT;
    }
    
    //public void RemoveComponent<T>() where T : Component
    //{
    //    for (int i = 0; i < components.Count; ++i)
    //    {
    //        if (components[i] is T)
    //        {
    //            findIndex = i;
    //            break;
    //        }
    //    }
    //}
}

