
class Input
{
    public Input()
    {

    }

    ~Input()
    {
    }

    public struct KeyList
    {
        public KeyList(ConsoleKey newButton, ConsoleKey newAliButton)
        {
            button = newButton;
            altButton = newAliButton;
        }
        public ConsoleKey button;
        public ConsoleKey altButton;
    }
    public static void Init()
    {
        InputMapping["Up"] = new KeyList(ConsoleKey.W, ConsoleKey.UpArrow);
        InputMapping["Down"] = new KeyList(ConsoleKey.S, ConsoleKey.DownArrow);
        InputMapping["Left"] = new KeyList(ConsoleKey.A, ConsoleKey.LeftArrow);
        InputMapping["Right"] = new KeyList(ConsoleKey.D, ConsoleKey.RightArrow);
        InputMapping["Quit"] = new KeyList(ConsoleKey.Escape, ConsoleKey.None);

    }
    public static Dictionary<string, KeyList> InputMapping = new Dictionary<string, KeyList>();

    public static ConsoleKeyInfo keyInfo;

    public static bool GetKey(ConsoleKey checkKeyCode)
    {
        return (keyInfo.Key == checkKeyCode);
    }

    public static bool GetButton(string buttonName)
    {
        return (InputMapping[buttonName].button == keyInfo.Key
            || InputMapping[buttonName].altButton == keyInfo.Key);
    }

}

