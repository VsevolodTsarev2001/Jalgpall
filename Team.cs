using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall
{
    public class Team
        //отребуты
    {
        public List<Player> Players { get; } = new List<Player>();
        //имя команды
        public string Name { get; private set; }
        //путь к классу game
        public Game Game { get; set; }
        //присваеваем название команды
        public Team(string name)
        {
            Name = name;
        }
        //функция для запуска игры. 
        public void StartGame(int width, int height)
        {
            //рандомные player
            Random rnd = new Random();
            foreach (var player in Players)
            {
                //рандомно устанавлмаем позицую игрокам
                player.SetPosition(
                    rnd.NextDouble() * width,
                    rnd.NextDouble() * height
                    );
            }
        }
        // Функция на добовление нового player. Если команда создана то добовляются в player
        public void AddPlayer(Player player)
        {
            if (player.Team != null) return;
            Players.Add(player);
            player.Team = this;
        }
        // Функция для установление позиции мяча и её возращаем 
        public (double, double) GetBallPosition()
        {
            return Game.GetBallPositionForTeam(this);
        }
        //функция для устоновления скорости мяча
        public void SetBallSpeed(double vx, double vy)
        {
            Game.SetBallSpeedForTeam(this, vx, vy);
        }
        //находит ближайшего игрока к мячу
        public Player GetClosestPlayerToBall()
        {
            Player closestPlayer = Players[0];
            double bestDistance = Double.MaxValue;
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall();
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }

            return closestPlayer;
        }

        public void Move()
        {
            GetClosestPlayerToBall().MoveTowardsBall();
            Players.ForEach(player => player.Move());
        }
    }
}
