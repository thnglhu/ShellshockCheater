using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellshockCheater.Game
{
    public class Match
    {
        Vector tank_position;
        public Vector Position { get => tank_position; set => tank_position = value; }
        public float power = 50f;
        public float wind = 0.02f;
        public float gravity = 1.4706f;
        WeaponPredictor predictor;
        public Match()
        {
            tank_position = new Vector(10, 900);
            predictor = new SingleBall();
            // predictor = new HoverBall();
        }
        public List<List<Vector>> GetPaths(float power_scale, float wind_scale, float angle, float time)
        {
            Vector impact_acceleration = new Vector(wind_scale * wind, gravity);
            return predictor.predict(tank_position, power_scale * power, -angle, impact_acceleration, time);
        }
    }
}
