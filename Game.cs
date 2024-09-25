using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jalgpall
{
    public class Game
    {
        //домашняя команда
        public Team HomeTeam { get; }
        //уездная команда
        public Team AwayTeam { get; }
        //стадион
        public Stadium Stadium { get; }
        //мяч
        public Ball Ball { get; private set; }
        //создание инры с домашней и уездной команды на стадионе
        public Game(Team homeTeam, Team awayTeam, Stadium stadium)
        {
            HomeTeam = homeTeam;
            homeTeam.Game = this;
            AwayTeam = awayTeam;
            awayTeam.Game = this;
            Stadium = stadium;
        }

        public void Start()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this);
            HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height);
            AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height);
        }
        //получаем позицую
        private (double, double) GetPositionForAwayTeam(double x, double y)
        {
            return (Stadium.Width - x, Stadium.Height - y);
        }
        //получаем позицую в зависемости какая команда
        public (double, double) GetPositionForTeam(Team team, double x, double y)
        {
            return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y);
        }
        //получаем позицую мяча для команд
        public (double, double) GetBallPositionForTeam(Team team)
        {
            return GetPositionForTeam(team, Ball.X, Ball.Y);
        }
        //получаем скорости мяча для команд
        public void SetBallSpeedForTeam(Team team, double vx, double vy)
        {
            if (team == HomeTeam)
            {
                //положительная
                Ball.SetSpeed(vx, vy);
            }
            else
            {
                //отрицательная
                Ball.SetSpeed(-vx, -vy);
            }
        }
        //движение команд и мяча
        public void Move()
        {
            HomeTeam.Move();
            AwayTeam.Move();
            Ball.Move();
        }
    }
}