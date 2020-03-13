using System;
using StereoKit;

namespace ex01
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!StereoKitApp.Initialize("ex01", Runtime.MixedReality))
                Environment.Exit(1);

            Solid solid = new Solid(Vec3.Zero, Quat.Identity);

            Model cube = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One, 0.2f),
                Default.Material);

            Plane floor = new Plane(Vec3.Up, 10);

            solid.AddBox(Vec3.One);

            while (StereoKitApp.Step(() =>
            {
                Log.Info(String.Format("normal: {0}", floor.normal));
                cube.SetTransform(0, solid.GetPose().ToMatrix());
                cube.Draw(Matrix.TS(Vec3.Zero, 0.1f));
            })) ;

            StereoKitApp.Shutdown();
        }
    }
}
