using GXPEngine;

public class Player : Sprite
{
    public Player() : base("turret.png")
    {

        //sets player sprite center and scale
        SetOrigin(width / 2, height / 2);
        scale = 1.5f;
    }

    void Update()
    {
        //player movement used for debugging
        /*
		if (Input.GetKey(Key.A))
		{
			x -= 1f;
		}
        if (Input.GetKey(Key.D))
        {
            x += 1f;
        }
        if (Input.GetKey(Key.W))
        {
            y -= 1f;
        }
        if (Input.GetKey(Key.S))
        {
            y += 1f;
        }
        */

    }

    //adds event on collision
    void OnCollision(GameObject other)
    {
        if (other is Enemy)
        {
            Enemy enemy = other as Enemy;
            enemy.Respawn(); //respawns enemy on collision
            MyGame myGame = (MyGame)game;
            myGame.HealthPoints--; //subtracts one from the amount of health

        }

    }

}


