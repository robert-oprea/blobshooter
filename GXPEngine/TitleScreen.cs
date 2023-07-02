using GXPEngine;


public class TitleScreen : GameObject
{
    Sprite background;
    public TitleScreen()
    {
        background = new Sprite("blob_start.png");
        AddChild(background);
    }
}


