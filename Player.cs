using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//private - закрытая переменная доступная только в этом классе

//namespace - название проэкта 
namespace Jalgpall
{
    //создаем класс с наименованием Player
    public class Player
    {
        //даем каждому player name
        public string Name { get; }

        // даем позицую игроку
        public double X { get; private set; }
        public double Y { get; private set; }

        //указываем шаг для player
        private double _vx, _vy;

        //каждый игрок
        public Team? Team { get; set; } = null;

        //постоянные значения скорости, скорости удара и дистанции

        private const double MaxSpeed = 5;
        private const double MaxKickSpeed = 25;
        private const double BallKickDistance = 10;

        private Random _random = new Random();
        //принемает только имя игрока
        public Player(string name)
        {
            Name = name;
        }
        // принемает позицию, имя и команду
        public Player(string name, double x, double y, Team team)
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }
        //устанавливаем новую позицую(position) для player
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }
        //устанавливаем позицую игрока в какой команде он
        public (double, double) GetAbsolutePosition()
        {
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }
        //получаем дистанцию до меча. Какой игрок ближе бежит к мечу.
        public double GetDistanceToBall()
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        //перемещение игрока к мячу
        public void MoveTowardsBall()
        {
            var ballPosition = Team!.GetBallPosition();
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
            _vx = dx / ratio;
            _vy = dy / ratio;
        }
        //передвижение. передвигается по осям x, y. после удара определяется рандомная сила удара
        public void Move()
        {
            //если шаг его равен 0 то он остается на месте
            if (Team.GetClosestPlayerToBall() != this)
            {
                _vx = 0;
                _vy = 0;
            }

            if (GetDistanceToBall() < BallKickDistance)
            {
                Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(),
                    MaxKickSpeed * (_random.NextDouble() - 0.5)
                    );
            }

            var newX = X + _vx;
            var newY = Y + _vy;
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
            {
                X = newX;
                Y = newY;
            }
            else
            {
                _vx = _vy = 0;
            }
        }
    }
}
