using Microsoft.Xna.Framework;
using NoStackHack.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoStackHack.Rendering.Character
{
    public class VisualComponent
    {

        public StateMachine<GameTime> StateMachine { get; set; }
        public Player Player { get; private set; }

        public PhysicsComponent Physics { get { return Player.PhysicsComponent; } }

        private Box _body;

        private Joint _frontFoot;
        private Apendage _frontLeg;

        private Joint _backFoot;
        private Apendage _backLeg;

        private Apendage _frontArm;

        private double _tickedTime;

        private float _footLength = 15;
        private float _jointLength = 40;

        public VisualComponent(Player player)
        {
            Player = player;
            StateMachine = new StateMachine<GameTime>(new IdleState());
            

            _body = new Box(Physics.Position, new Vector2(40, 75));
            _frontFoot = new Joint();
            _frontFoot.Length = _footLength;
            _frontFoot.Angle = 0;

            _frontLeg = new Apendage();
            _frontLeg.UsePositive = false;
            _frontLeg.JointLength = _jointLength;

            _backFoot = new Joint();
            _backFoot.Length = _footLength;
            _backFoot.Angle = 0;

            _backLeg = new Apendage();
            _backLeg.UsePositive = false;
            _backLeg.JointLength = _jointLength;

            _frontArm = new Apendage();
            _frontArm.JointLength = _footLength;

        }

        public void Update(GameTime time)
        {

            //_tickedTime += time.ElapsedGameTime.TotalMilliseconds;
            _tickedTime += Math.Abs(Physics.Velocity.X );

            if (Math.Abs(Physics.Velocity.X ) < 1f)
            {
                _tickedTime = 0;
            }

            // the y height changes with foot motion, which is x movement. 
            var agg = new Vector2(0, -50);
            var speed = 50;

            var xFlip = Math.Sign(Physics.Velocity.X) == -1 ? Vector2.UnitX * _footLength : Vector2.Zero;
            var xOffset = 5 * Vector2.UnitX;

            agg.Y += Math.Min(15, Math.Abs(Physics.Velocity.X) )
                * 3
                * (float) -Math.Sin(_tickedTime / speed);

            _body.Position = Physics.Position + agg;




            var genFootAgg = new Func<int, Vector2>( sign => {
                var footAgg = new Vector2(0, 0);
                footAgg.Y += Math.Min(15, Math.Max(1, Math.Abs(Physics.Velocity.X)))
                    * 7
                    * (float)Math.Cos( (MathHelper.PiOver2 * sign) + (_tickedTime / speed));
                
                footAgg.Y = -Math.Abs(footAgg.Y);

                footAgg.X += Math.Min(15, Math.Max(1, Math.Abs(Physics.Velocity.X)))
                    * 7
                    * (float)Math.Cos( (MathHelper.PiOver2 * sign) + _tickedTime / speed);

                footAgg.X *= Math.Sign(Physics.Velocity.X);
                return footAgg;
            });

            var frontFootAgg = genFootAgg(1) + xOffset;
            var backFootAgg = genFootAgg(-1) - xOffset;

            _frontFoot.Base = new Vector2(_body.MiddleX, Player.Box.Bottom) + frontFootAgg;
            _backFoot.Base = new Vector2(_body.MiddleX, Player.Box.Bottom) + backFootAgg;

            //if (Physics.Velocity.X < 0)
            //{
            //    _backLeg.UsePositive = false;
            //    _frontLeg.UsePositive = false;
            //} else
            //{
            //    _frontLeg.UsePositive = true;
            //    _backLeg.UsePositive = true;
            //}

            _frontLeg.UsePositive = _backLeg.UsePositive = Physics.Velocity.X < 0;

            _frontLeg.BasePosition = new Vector2(_body.MiddleX, _body.Bottom) + xOffset;
            _frontLeg.TipPosition = _frontFoot.Base + xFlip;

            _backLeg.BasePosition = new Vector2(_body.MiddleX, _body.Bottom) - xOffset;
            _backLeg.TipPosition = _backFoot.Base + xFlip;


            StateMachine.Update(time);
        }

        public void Render(RenderHelper render)
        {
            render.DrawBox(_body, Color.White);


            _frontLeg.Render(render);
            _backLeg.Render(render);
            _frontFoot.Render(render);
            _backFoot.Render(render);
        }

    }
}
