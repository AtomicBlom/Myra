﻿using System.IO;

#if !XENKO
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
using Xenko.Core.Mathematics;
using Xenko.Graphics;
using Texture2D = Xenko.Graphics.Texture;
#endif

namespace Myra.Utility
{
	internal static class CrossEngineStuff
	{
		public static Texture2D LoadTexture2D(Stream stream)
		{
#if !XENKO
			return Texture2D.FromStream(MyraEnvironment.GraphicsDevice, stream);
#else
			return Texture2D.Load(MyraEnvironment.GraphicsDevice, stream);
#endif
		}

		public static Texture2D CreateTexture2D(int width, int height)
		{
#if !XENKO
			return new Texture2D(MyraEnvironment.GraphicsDevice, width, height, false, SurfaceFormat.Color);
#else
			return Texture2D.New2D(MyraEnvironment.GraphicsDevice, width, height, false, PixelFormat.R8G8B8A8_UNorm, TextureFlags.ShaderResource);
#endif
		}

		public static Color[] GetData(Texture2D texture)
		{
#if !XENKO
			var result = new Color[texture.Width * texture.Height];
			texture.GetData(result);
			return result;
#else
			var commandList = MyraEnvironment.Game.GraphicsContext.CommandList;

			return texture.GetData<Color>(commandList);
#endif
		}

		public static void SetData(Texture2D texture, Color[] data)
		{
#if !XENKO
			texture.SetData(data);
#else
			var commandList = MyraEnvironment.Game.GraphicsContext.CommandList;

			texture.SetData<Color>(commandList, data);
#endif
		}

		public static int LineSpacing(SpriteFont font)
		{
#if !XENKO
			return font.LineSpacing;
#else
			return (int)font.Size;
#endif
		}

		public static Rectangle GetScissor()
		{
#if !XENKO
			var rect = MyraEnvironment.GraphicsDevice.ScissorRectangle;

			rect.X -= MyraEnvironment.GraphicsDevice.Viewport.X;
			rect.Y -= MyraEnvironment.GraphicsDevice.Viewport.Y;

			return rect;
#else
			return MyraEnvironment.Game.GraphicsContext.CommandList.Scissor;
#endif
		}

		public static void SetScissor(Rectangle rect)
		{
#if !XENKO
			rect.X += MyraEnvironment.GraphicsDevice.Viewport.X;
			rect.Y += MyraEnvironment.GraphicsDevice.Viewport.Y;
			MyraEnvironment.GraphicsDevice.ScissorRectangle = rect;
#else
			MyraEnvironment.Game.GraphicsContext.CommandList.SetScissorRectangle(rect);
#endif
		}
	}
}
