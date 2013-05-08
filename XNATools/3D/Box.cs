using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNATools.Enums;
using XNATools.Services.Interfaces;
using XNATools.Services;
using XNATools.Managers;

namespace XNATools.Space
{   
    /// <summary>
    /// Not used for the moment
    /// </summary>
    public class Box //: DrawableGameComponent
    {
        /*Surface[] sides = new Surface[6];
        BoundingBox box;

        VertexDeclaration vertexDeclaration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <param name="min">Coin Front-Bottom-Left de la Box</param>
        /// <param name="max">Coin Back-Top-Right de la Box</param>
        public Box(Game game, Vector3 min, Vector3 max)
            : base(game)
        {
            box = new BoundingBox(min, max);

            if (Surface.graphicsDevice == null)
                Surface.graphicsDevice = game.GraphicsDevice;

            // compute vertex positions
            float left = min.X;
            float bottom = min.Y;
            float front = min.Z;

            float right = max.X;
            float top = max.Y;
            float back = max.Z;

            // define positions into vectors
            Vector3 FrontTopLeft = new Vector3(left, top, front);
            Vector3 FrontTopRight = new Vector3(right, top, front);
            Vector3 FrontBottomLeft = new Vector3(left, bottom, front);
            Vector3 FrontBottomRight = new Vector3(right, bottom, front);
            Vector3 BackTopLeft = new Vector3(left, top, back);
            Vector3 BackTopRight = new Vector3(right, top, back);
            Vector3 BackBottomLeft = new Vector3(left, bottom, back);
            Vector3 BackBottomRight = new Vector3(right, bottom, back);

            // create sides
            sides[(int)BoxSideName.Front] = new Surface(FrontTopLeft, FrontBottomRight, FrontBottomLeft, FrontTopRight);
            sides[(int)BoxSideName.Back] = new Surface(BackTopRight, BackBottomLeft, BackBottomRight, BackTopLeft);
            sides[(int)BoxSideName.Left] = new Surface(BackTopLeft, FrontBottomLeft, BackBottomLeft, FrontTopLeft);
            sides[(int)BoxSideName.Right] = new Surface(FrontTopRight, BackBottomRight, FrontBottomRight, BackTopRight);
            sides[(int)BoxSideName.Top] = new Surface(BackTopLeft, FrontTopRight, FrontTopLeft, BackTopRight);
            sides[(int)BoxSideName.Bottom] = new Surface(FrontBottomLeft, BackBottomRight, FrontBottomRight, BackBottomLeft);

            vertexDeclaration = new VertexDeclaration(game.GraphicsDevice, VertexPositionColorTexture.VertexElements);
        }


        public override void Draw(GameTime gameTime)
        {
            Game.GraphicsDevice.VertexDeclaration = vertexDeclaration;
            ICameraService camera = ServiceManager.Get<ICameraService>();

            for (int i = 0; i < sides.Length; i++)
                sides[i].Draw(camera.View);

            base.Draw(gameTime);
        }

        public void ApplyEffectToSide(BoxSideName side, string effectName)
        {
            sides[(int)side].EffectName = effectName;
        }*/

    }
}
