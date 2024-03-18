
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
        if (Input.GetButton("Left"))
        {
            transform.Translate(-1, 0);
        }
        if (Input.GetButton("Right"))
        {
            transform.Translate(1, 0);
        }
        if (Input.GetButton("Up"))
        {
            transform.Translate(0, -1);
        }
        if (Input.GetButton("Down"))
        {
            transform.Translate(0, 1);
        }
        if (Input.GetButton("Quit"))
        {
            //singleton pattern
            Engine.GetInstance().Stop();
        }

        transform.x = Math.Clamp(transform.x, 0, 80);
        transform.y = Math.Clamp(transform.y, 0, 80);
    }
}
