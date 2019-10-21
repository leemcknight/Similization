using System;
using System.Drawing;
using System.Text;
using Microsoft.DirectX.Direct3D;
using Microsoft.DirectX.Generic;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D.CustomVertex;

namespace LJM.Similization.Client.DirectX.Controls
{
    public static class CustomPainters
    {
        /// <summary>
        /// Creates a <i>VertexBuffer</i> that can used to draw colored rectangles.
        /// </summary>        
        /// <param name="device"></param>
        /// <returns></returns>
        public static VertexBuffer CreateColoredBuffer(Device device)
        {            
            return VertexBuffer.CreateGeneric<TransformedColored>(
                device,
                4,
                Usage.WriteOnly,
                TransformedColored.Format,
                Pool.Managed,
                null);            
        }

        /// <summary>
        /// Creates a <i>VertexBuffer</i> that can be used to draw colored triangles.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static VertexBuffer CreateColoredTriangleBuffer(Device device)
        {
            return VertexBuffer.CreateGeneric<TransformedColored>(
                device,
                3,
                Usage.WriteOnly,
                TransformedColored.Format,
                Pool.Managed,
                null);
        }

        /// <summary>
        /// Creates a <i>VertexBuffer</i> that can be used to render a texture on a rectangle.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static VertexBuffer CreateTexturedBuffer(Device device)
        {           
            return VertexBuffer.CreateGeneric<TransformedTextured>(
                device,
                4,
                Usage.WriteOnly,
                TransformedTextured.Format,
                Pool.Managed,
                null);

        }

        /// <summary>
        /// Paints a triangle onto the screen.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="region"></param>
        /// <param name="buffer"></param>
        /// <param name="fillColor"></param>
        public static void PaintColoredTriangle(Device device, Rectangle rectangle, VertexBuffer buffer, Color fillColor)
        {
            GraphicsBuffer<TransformedColored> data = buffer.Lock<TransformedColored>(0, 3, LockFlags.None);
            buffer.Unlock();
            data[0] = new TransformedColored(rectangle.X, rectangle.Y, .5f, 0, fillColor);
            data[1] = new TransformedColored(rectangle.Right, rectangle.Y, .5f, 0, fillColor);
            data[2] = new TransformedColored((rectangle.X + rectangle.Right) / 2, rectangle.Bottom, .5f, 0, fillColor);
            device.RenderState.ZBufferWriteEnable = false;
            device.RenderState.AlphaBlendEnable = true;
            device.RenderState.CullMode = Cull.None;
            device.VertexFormat = TransformedColored.Format;
            device.SetStreamSource(0, buffer, 0);
            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 1);
        }

        /// <summary>
        /// Paints a the specified rectangle with the colors specified.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="rectangle"></param>
        /// <param name="buffer"></param>
        /// <param name="color1"></param>
        /// <param name="color2"></param>
        /// <param name="gradientDirection"></param>
        public static void PaintColoredRectangle(Device device, Rectangle rectangle, VertexBuffer buffer, Color color1, Color color2, GradientDirection gradientDirection)
        {
            GraphicsBuffer<TransformedColored> data = buffer.Lock<TransformedColored>(0, 4, LockFlags.None);
            int topLeftColor = color1.ToArgb();
            int topRightColor = gradientDirection == GradientDirection.Horizontal ? color2.ToArgb() : color1.ToArgb();
            int bottomLeftColor = gradientDirection == GradientDirection.Horizontal ? color1.ToArgb() : color2.ToArgb();
            int bottomRightColor = color2.ToArgb();
            data[0] = new TransformedColored(rectangle.X, rectangle.Y, .5f, 0, topLeftColor);
            data[1] = new TransformedColored(rectangle.Right, rectangle.Y, .5f, 0, topRightColor);
            data[2] = new TransformedColored(rectangle.X, rectangle.Bottom, .5f, 0, bottomLeftColor);
            data[3] = new TransformedColored(rectangle.Right, rectangle.Bottom, .5f, 0, bottomRightColor);            
            buffer.Unlock();            
            device.RenderState.ZBufferWriteEnable = false;
            device.RenderState.AlphaBlendEnable = false;            
            device.RenderState.CullMode = Cull.None;
            device.VertexFormat = TransformedColored.Format;
            device.SetStreamSource(0, buffer, 0);
            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
        }

        /// <summary>
        /// Paints the specified rectangle with the color specified.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="rectangle"></param>
        /// <param name="buffer"></param>
        /// <param name="color"></param>
        public static void PaintColoredRectangle(Device device, Rectangle rectangle, VertexBuffer buffer, Color color)
        {
            PaintColoredRectangle(device, rectangle, buffer, color, color, GradientDirection.Horizontal);
        }

        /// <summary>
        /// Paints the specified texture on the specified rectangle.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="buffer"></param>
        /// <param name="texture"></param>
        public static void PaintTexturedRectangle(Device device, Rectangle rectangle, VertexBuffer buffer, Texture texture)
        {            
            GraphicsBuffer<TransformedTextured> graphicsBuffer = buffer.Lock<TransformedTextured>(0, 4, LockFlags.None);
            graphicsBuffer[0] = new TransformedTextured(rectangle.X, rectangle.Y, .5f, 0f, 0f, 0f);
            graphicsBuffer[1] = new TransformedTextured(rectangle.Right, rectangle.Y, .5f, 0f, 1f, 0f);
            graphicsBuffer[2] = new TransformedTextured(rectangle.X, rectangle.Bottom, .5f, 0f, 0f, 1f);
            graphicsBuffer[3] = new TransformedTextured(rectangle.Right, rectangle.Bottom, .5f, 0f, 1f, 1f);            
            buffer.Unlock();            
            device.VertexFormat = TransformedTextured.Format;
            device.RenderState.ZBufferWriteEnable = false;                     
            device.RenderState.CullMode = Cull.None;            
            device.SetTexture(0, texture);
            device.SetStreamSource(0, buffer, 0);            
            device.DrawPrimitives(PrimitiveType.TriangleStrip, 0, 2);
        }

        /// <summary>
        /// Draws a rectangle with the specified color.
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="color"></param>
        public static void DrawBoundingRectangle(IDirectXControlHost controlHost, Rectangle rectangle, Color color)
        {            
            Vector2[] verts = new Vector2[] {
                new Vector2(rectangle.X, rectangle.Y),
                new Vector2(rectangle.Right, rectangle.Y),
                new Vector2(rectangle.Right, rectangle.Bottom),
                new Vector2(rectangle.X, rectangle.Bottom),
                new Vector2(rectangle.X, rectangle.Y),
            };
            controlHost.DrawLine(verts, color);
        }

    }
}
