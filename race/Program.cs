using System;
using System.Collections.Generic;

namespace race
{
    abstract class obstacle
    {
        protected int posX;
        protected int posY;
        public int PosX { set { posX = value; } get { return posX; } }
        public int PosY { set { posY = value; } get { return posY; } }
        ~obstacle() { }
        public abstract void show();

    }
    abstract class car
    {
        protected int posX;
        protected int posY;
        public int PosX { set { posX = value; } get { return posX; } }
        public int PosY { set { posY = value; } get { return posY; } }
        public abstract void show();
        public abstract void move(int pos_x, int pos_y);

    }
    class road
    {
        Random rnd = new Random();
        FirstCar Car = new FirstCar(10,10);
        List<FirstObstacle> obstacles = new List<FirstObstacle>();
        public void createObstacle()
        {
            obstacles.Add(new FirstObstacle(0, rnd.Next(1,20)));
        }
        public void show()
        {
            Car.show();
            foreach (var item in obstacles)
            {
                item.show();
            }
        }
        public bool checkCollision()
        {
            foreach (var item in obstacles)
            {
                if(Car.PosX<item.PosX+4&& Car.PosX > item.PosX - 4 && Car.PosY < item.PosY + 4 && Car.PosY > item.PosY - 4)
                {
                    return true;
                }
            }return false;
        }
        public void move(int x)
        {
            int counter = 0;
            while(counter<obstacles.Count)
            {
                obstacles[counter].PosX++;
                if (obstacles[counter].PosX > 15)
                {
                    obstacles.RemoveAt(counter);
                    if (obstacles.Count == 0) { break; }
                }
                else
                    counter++;
            }
            Car.move(0,x);
            if (obstacles.Count < 3)
                createObstacle();
        }
    }
    class controller
    {
        road Road;

        public controller()
        {
            Road = new road();
        }
        public void getKey() 
        {   
            Console.Clear();
            Road.show();
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.D:
                    Road.move(1);
                    break;
                case ConsoleKey.A:
                    Road.move(-1);
                    break;
                default:
                    break;
            }
            if (Road.checkCollision())
            {
                Console.Clear();
                Console.WriteLine("loser");
                Console.ReadKey();
            }
            else
                getKey();
        }
        public void stop() { }

    }
    class FirstCar : car
    {
        public override void show()
        {
            int counter = 0;
            Console.SetCursorPosition(posY, posX);
            Console.WriteLine("@__@");counter++;
            Console.SetCursorPosition(posY, posX+counter);
            Console.WriteLine("|()|");counter++;
            Console.SetCursorPosition(posY, posX+counter);
            Console.WriteLine("@--@");counter++;
            Console.SetCursorPosition(posY, posX+counter);

        }
        public override void move(int pos_x, int pos_y)
        {
            posX += pos_x;
            posY += pos_y;
        }
        public FirstCar(int pos_x, int pos_y)
        {
            posX = pos_x;
            posY = pos_y;
        }
    }
    class FirstObstacle : obstacle
    {
        public override void show()
        {

            int counter = 0;
            Console.SetCursorPosition(posY, posX);
            Console.WriteLine(" @@ "); counter++;
            Console.SetCursorPosition(posY, posX + counter);
            Console.WriteLine("@@@@"); counter++;
            Console.SetCursorPosition(posY, posX + counter);
            Console.WriteLine(" @@ "); counter++;
            Console.SetCursorPosition(posY, posX + counter);
        }
        public FirstObstacle(int pos_x, int pos_y)
        {
            posX = pos_x;
            posY = pos_y;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            controller Game = new controller();
            Game.getKey();
        }
    }
}
