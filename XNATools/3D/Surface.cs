using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using XNATools.Managers;

namespace XNATools.Space
{
    /// <summary>
    /// Not used for the moment
    /// </summary>
    public class Surface
    {
        /*VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[4];
        static short[] indices = new short[] { 0, 1, 2, 0, 3, 1 };
        public static GraphicsDevice graphicsDevice;

        private bool isTextured = false;

        private string _EffectName;
        /// <summary>
        /// 
        /// </summary>
        public string EffectName
        {
            get { return _EffectName; }
            set { _EffectName = value; isTextured = true; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topLeft"></param>
        /// <param name="bottomRight"></param>
        /// <param name="bottomLeft"></param>
        /// <param name="topRight"></param>
        public Surface(Vector3 topLeft, Vector3 bottomRight, Vector3 bottomLeft, Vector3 topRight)
        {
            vertices[0].Position = topLeft;
            vertices[1].Position = bottomRight;
            vertices[2].Position = bottomLeft;
            vertices[3].Position = topRight;

            vertices[0].TextureCoordinate = Vector2.Zero;
            vertices[1].TextureCoordinate = Vector2.One;
            vertices[2].TextureCoordinate = Vector2.UnitY;
            vertices[3].TextureCoordinate = Vector2.UnitX;

            vertices[0].Color = Color.White;
            vertices[1].Color = Color.White;
            vertices[2].Color = Color.White;
            vertices[3].Color = Color.White;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public void Draw(Matrix view)
        {
            if (!isTextured) return;

            Effect effect = EffectManager.Get(EffectName);

            graphicsDevice.RenderState.CullMode = CullMode.None;

            if (effect is BasicEffect)
            {
                BasicEffect be = effect as BasicEffect;
                be.View = view;
            }
            else
            {
                //ICameraService camera = ServiceHelper.Get<ICameraService>();

                //effect.Parameters["tW"].SetValue(camera.GetWorldMatrix());
                //effect.Parameters["tV"].SetValue(camera.GetViewMatrix());
                //effect.Parameters["tP"].SetValue(camera.GetProjectionMatrix());
                //effect.Parameters["tWVP"].SetValue(camera.GetWorldMatrix() *camera.GetViewMatrix() * camera.GetProjectionMatrix());

                //effect.Parameters["g_txSrcColor"].SetValue(LabyMain.tex);
            }


            effect.Begin();
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Begin();
                graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList,
                    vertices, 0, vertices.Length,
                    indices, 0, indices.Length / 3);
                pass.End();
            }
            effect.End();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="leftX"></param>
        /// <param name="topY"></param>
        /// <param name="rightX"></param>
        /// <param name="bottomY"></param>
        public void SetTextureCoordinates(int leftX, int topY, int rightX, int bottomY)
        {
            vertices[0].TextureCoordinate = new Vector2(leftX, topY);
            vertices[1].TextureCoordinate = new Vector2(rightX, bottomY);
            vertices[2].TextureCoordinate = new Vector2(leftX, bottomY);
            vertices[3].TextureCoordinate = new Vector2(rightX, topY);
        }*/

    }
}
