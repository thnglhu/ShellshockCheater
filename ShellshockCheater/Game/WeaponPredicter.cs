using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShellshockCheater.Game
{
    public abstract class WeaponPredictor
    {
        protected float interval = 0.5f;
        public static Vector CalculateInstance(
            Vector start_position,
            Vector init_vector,
            Vector impact_acceleration,
            float time
            )
        {
            return start_position + time * init_vector + 0.5f * impact_acceleration * time * time;
        }
        public static float Convert(float degree)
        {
            return (float)(Math.PI / 180) * degree;
        }
        public abstract List<List<Vector>> predict(
            Vector start_position,
            float init_velocity,
            float angle,
            Vector impact_acceleration,
            float time
            );
    }
    public class SingleBall : WeaponPredictor
    {
        public override List<List<Vector>> predict(
            Vector start_position,
            float init_velocity,
            float angle,
            Vector impact_acceleration,
            float time
            )
        {
            List<Vector> vectors = new List<Vector>();
            Vector init_vector = (new Vector(init_velocity, 0)) ^ angle;
            for (float instant = 0f; instant < time; instant += interval)
            {
                vectors.Add(CalculateInstance(start_position, init_vector, impact_acceleration, instant));
            }
            List<List<Vector>> result = new List<List<Vector>>();
            result.Add(vectors);
            return result;
        }
    }
    public class TripleBall : SingleBall
    {
        static readonly float delta = 5.5f;
        public override List<List<Vector>> predict(
            Vector start_position,
            float init_velocity,
            float angle,
            Vector impact_acceleration,
            float time
            )
        {
            List<List<Vector>> result = new List<List<Vector>>();
            result.AddRange(base.predict(
                start_position,
                init_velocity,
                angle,
                impact_acceleration,
                time)
            );
            result.AddRange(base.predict(
                start_position,
                init_velocity,
                angle + Convert(delta),
                impact_acceleration,
                time)
            );
            result.AddRange(base.predict(
                start_position,
                init_velocity,
                angle - Convert(delta),
                impact_acceleration,
                time)
            );
            return result;
        }
    }
    //public class HoverBall : WeaponPredictor
    //{
    //    static float hover_time = 5f;
    //    private static Vector Hover(Vector start_position, float init_velocity, float impact, float time)
    //    {
    //        Vector result = new Vector();
    //        result.x = start_position.x + init_velocity * time + 0.5f * impact * time * time;
    //        return result;
    //    }
    //    public override List<List<Vector>> predict(
    //        Vector start_position,
    //        float init_velocity,
    //        float angle,
    //        Vector impact_acceleration,
    //        float time
    //        )
    //    {
    //        Vector init_vector = (new Vector(init_velocity, 0)) ^ angle;
    //        Console.WriteLine($"{time}, "
    //            + $"\n{-init_vector.y / impact_acceleration.y}");
    //        List<Vector> vectors = new List<Vector>();
    //        float instance = 0f;
    //        float peek_time = -init_vector.y / impact_acceleration.y;
    //        for (; instance < peek_time; instance += interval)
    //        {
    //            vectors.Add(CalculateInstance(start_position, init_vector, impact_acceleration, instance));
    //        }
    //        Vector peek = vectors[vectors.Count - 1];
    //        float init_peek_velocity = impact_acceleration.x * peek_time + init_vector.x;
    //        for (float hover = 0f; hover < hover_time; hover += interval)
    //        {
    //            vectors.Add(Hover(peek, ))
    //        }
    //        List<List<Vector>> result = new List<List<Vector>>();
    //        result.Add(vectors);
    //        return result;
    //    }
    //}
}
