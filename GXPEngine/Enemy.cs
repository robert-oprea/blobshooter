using GXPEngine;

public class Enemy : Sprite
{
    float speedX;
    float speedY;
    float mouseX;
    float mouseY;
    int startingDir;

    float speed = 1;
    Player target;

    public Enemy(Player target, float posX = 0, float posY = 0, float pSpeedY = 1) : base("blob.png")
    {
        SetOrigin(width / 2, height / 2);
        startingDir = Utils.Random(1, 5);

        if (startingDir == 1)
        {
            posX = Utils.Random(this.width, game.width - this.width);
            posY = -1 * Utils.Random(this.height, game.height);
        }
        else if (startingDir == 2)
        {
            posX = Utils.Random(this.width, game.width - this.width);
            posY = Utils.Random(game.height, game.height + this.height);
        }
        else if (startingDir == 3)
        {
            posX = -1 * Utils.Random(this.width, game.width - this.width);
            posY = Utils.Random(this.height, game.height - this.height);
        }
        else if (startingDir == 4)
        {
            posX = Utils.Random(game.width, game.width + this.width);
            posY = Utils.Random(this.height, game.height - this.height);
        }

        speedY = pSpeedY;
        this.target = target;
        SetXY(posX, posY);
    }

    void Update()
    {
        MyGame myGame = (MyGame)game;

        if (myGame.GameStarted == true)
        {
            ChasePlayer();
            updateMousePosition();

            //respawns enemies to posX & posY every time they are clicked
            if (Input.GetMouseButtonDown(0))
            {
                //Console.WriteLine("mouse clicked"); //debug stuff
                if (x - width / 2 < mouseX && x + width / 2 > mouseX)
                {
                    //Console.WriteLine("within x boundary");
                    if (y - height / 2 < mouseY && y + height / 2 > mouseY)
                    {
                        //Console.WriteLine("within y boundary");  //debug stuff
                        Respawn();
                    }
                }
            }
        }

    }

    public void Respawn()
    {
        if (startingDir == 1)
        {
            SetXY(Utils.Random(this.width, game.width - this.width), -1 * Utils.Random(this.height, game.height));
        }
        else if (startingDir == 2)
        {
            SetXY(Utils.Random(this.width, game.width - this.width), Utils.Random(game.height, game.height + this.height));
        }
        else if (startingDir == 3)
        {
            SetXY(-1 * Utils.Random(this.width, game.width - this.width), Utils.Random(this.height, game.height - this.height));
        }
        else if (startingDir == 4)
        {
            SetXY(Utils.Random(game.width, game.width + this.width), Utils.Random(this.height, game.height - this.height));
        }

    }

    void updateMousePosition()
    {
        mouseX = Input.mouseX;
        mouseY = Input.mouseY;
    }

    protected void ChasePlayer()
    {
        //sets damping
        float damping = 0.1f;

        //gets the diff between target pos and current pos
        float xDiff = target.x - x;
        float yDiff = target.y - y;

        //magnitude of the difference vector
        float magnitude = Mathf.Sqrt(xDiff * xDiff + yDiff * yDiff);

        //normalize diff vector
        xDiff = xDiff / magnitude;
        yDiff = yDiff / magnitude;

        //applies damping and multiplies by the speed to get the enemy speed
        //using Time.deltaTime so framerate is not important
        speedX = xDiff * damping * speed * Time.deltaTime;
        speedY = yDiff * damping * speed * Time.deltaTime;

        //moves enemy
        Move(speedX, speedY);
        //Console.WriteLine(speedY); //debug stuff
    }


}


