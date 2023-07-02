namespace GXPEngine.HUD

{
    public class Crosshair : Sprite
    {

        public Crosshair() : base("aim.png")
        {
            SetOrigin(width / 2, height / 2);
            scale = 1;
        }
        void Update()
        {
            //sets crosshair positions the same as the cursor
            x = Input.mouseX;
            y = Input.mouseY;

        }
    }
}