using GXPEngine;
using GXPEngine.HUD;
using System;
using System.Collections.Generic;



public class MyGame : Game
{
    public List<Enemy> enemyList; //creates a list w enemies
    float enemySpeed = 1; //enemy speed
    Player player;
    TitleScreen titleScreen;
    public bool GameStarted = false;
    int score = 10;
    public int HealthPoints = 3; //health points
    EasyDraw hudScore; //shows score on screen
    EasyDraw healthPointsValue; //shows health on screen
    bool showEndScreen = false;
    Sprite gBackground;
    EndScreen endScreen;
    bool spawnEnemies = false;
    EasyDraw deathScore;
    int enemyAmount = 10;

    Sound startSound = new Sound("start_game.wav", false, false);
    Sound deathSound = new Sound("end_game.wav", false, false);

    public MyGame() : base(800, 600, false)
    {
        gBackground = new Sprite("game_bg.png", false, false);
        AddChild(gBackground);
        //creates a list to easily change the amount of enemies
        enemyList = new List<Enemy>();

        player = new Player();
        player.SetXY(width / 2, height / 2);
        AddChild(player);

        SpawnEnemies();

        //adds crosshair
        Crosshair aim = new Crosshair();
        AddChild(aim);

        //adds the titlescreen
        titleScreen = new TitleScreen();
        AddChild(titleScreen);

        //adds score to the screen
        hudScore = new EasyDraw(250, 60, false);
        hudScore.TextAlign(CenterMode.Min, CenterMode.Min);
        hudScore.SetXY(5, 45);
        AddChild(hudScore);

        //adds health amount to the screen
        healthPointsValue = new EasyDraw(250, 60, false);
        healthPointsValue.TextAlign(CenterMode.Min, CenterMode.Min);
        healthPointsValue.SetXY(5, 0);
        AddChild(healthPointsValue);
    }


    void Update()
    {
        //destroys title screen when space is pressed
        if (Input.GetKey(Key.SPACE) && showEndScreen == false)
        {
            titleScreen.Destroy();
            GameStarted = true;
        }

        //shows and updates score & health every frame
        if (GameStarted == true)
        {
            score += 1;
            hudScore.Clear(0, 0, 0, 0); //clears text that was displayed on previous frames
            healthPointsValue.Clear(0, 0, 0, 0);
            hudScore.Text("SCORE" + Environment.NewLine + score / 10);
            healthPointsValue.Text("HEALTH" + Environment.NewLine + HealthPoints);
        }

        if (spawnEnemies)
        {
            SpawnEnemies();
        }

        //shows end screen when left with no health
        if (HealthPoints <= 0 && showEndScreen == false)
        {
            endScreen = new EndScreen();
            AddChild(endScreen);
            GameStarted = false;
            showEndScreen = true;
            deathScore = new EasyDraw(250, 60, false);
            deathScore.TextAlign(CenterMode.Min, CenterMode.Min);
            deathScore.SetXY(300, 500);
            endScreen.AddChild(deathScore);
            deathScore.Text("YOUR SCORE: " + score / 10);
            deathSound.Play();
        }

        if (Input.GetKey(Key.SPACE) && showEndScreen == true)
        {
            foreach (Enemy enemyyy in enemyList)
            {
                endScreen.Destroy();
                spawnEnemies = true;
                showEndScreen = false;
                HealthPoints = 3;
                score = 10;
                GameStarted = true;
                startSound.Play();
            }
        }
    }

    void SpawnEnemies()
    {
        //reverse for loop to remove all enemies
        for (int j = enemyList.Count - 1; j >= 0; j--)
        {
            Enemy currentEnemy = enemyList[j];
            RemoveChild(currentEnemy);
            enemyList.Remove(currentEnemy);
            currentEnemy.Destroy();
        }

        //for an i amount of enemies
        for (int i = 0; i < enemyAmount; i++)
        {
            //sets target, coords and speed
            Enemy enemy = new Enemy(player, 0, 0, enemySpeed);
            enemyList.Add(enemy);
        }

        //adds every enemy from the list in the game
        foreach (Enemy enemyy in enemyList)
        {
            AddChild(enemyy);
        }

        spawnEnemies = false;
    }

    static void Main()
    {
        new MyGame().Start();
    }
}