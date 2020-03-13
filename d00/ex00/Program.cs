using System;
using StereoKit;
using StereoKit.Framework;
using StereoKitUtilities;

namespace ex00
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!StereoKitApp.Initialize("ex00", Runtime.MixedReality))
                Environment.Exit(1);

            Model balloon = Model.FromMesh(
                Mesh.GenerateSphere(1, 1),
                Default.Material);

            balloon.AddSubset(Mesh.GenerateCylinder(0.05f, 2, new Vec3(0, 1, 0)), Default.Material, Matrix.T(0, -1, 0));

            /*Model clone = Clone.Duplicate(ref balloon);*/

         /*   Grabbable grabBalloon = new Grabbable(ref clone);*/

       /*     grabBalloon.SetOnGrabStart(() => {
                grabBalloon._color = Color.Black;
            });

            grabBalloon.SetOnGrabEnd(() => {
                grabBalloon._color = Color.White;
            });*/

            float score = 0;
            float scale = 0.25f;
            float air = 1f;
            Hand hand = Input.Hand(Handed.Right);
            while (StereoKitApp.Step(() =>
            {
                if (scale > 0 && scale <= 0.5f)
                {
                    if (air > 0 && balloon.Bounds.Contains(hand.palm.position))
                    {
                        scale += 0.01f;
                        air -= 0.05f;
                    }
                    scale -= Time.Elapsedf / 10;
                    balloon.Draw(Matrix.TS(Vec3.Zero, scale));
                   /* clone.Draw(Matrix.TS(new Vec3(0.5f, 0, 0), scale));*/
                    if (air < 1)
                        air += 0.01f;
                }
                else
                {
                    if (score == 0)
                        score = Time.Totalf;
                    GameOver();
                }
               /* grabBalloon.Update();*/
            })) ;
            StereoKitApp.Shutdown();

            void GameOver()
            {
                Text.Add(String.Format("Game Over!\nScore: {0}", score), Matrix.T(new Vec3(0, 0, 0)));
            }
        }
    }
}
