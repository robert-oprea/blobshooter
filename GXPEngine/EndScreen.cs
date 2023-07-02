using GXPEngine;


public class EndScreen : GameObject
{
    Sprite background;
    public EndScreen()
    {
        background = new Sprite("blob_end.png");
        AddChild(background);


    }
}


