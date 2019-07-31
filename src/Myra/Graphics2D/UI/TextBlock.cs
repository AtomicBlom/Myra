using System.ComponentModel;
using System.Linq;
using Myra.Attributes;
using Myra.Graphics2D.Text;
using Myra.Graphics2D.UI.Styles;
using Myra.Utility;
using System.Xml.Serialization;

#if !XENKO
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
using Xenko.Core.Mathematics;
using Xenko.Graphics;
#endif

namespace Myra.Graphics2D.UI
{
	public class TextBlock : Widget
	{
		private readonly FormattedText _formattedText = new FormattedText();
		private bool _wrap = false;

		[EditCategory("Appearance")]
		[DefaultValue(0)]
		public int VerticalSpacing
		{
			get
			{
				return _formattedText.VerticalSpacing;
			}
			set
			{
				_formattedText.VerticalSpacing = value;
				InvalidateMeasure();
			}
		}

		[EditCategory("Appearance")]
		public string Text
		{
			get
			{
				return _formattedText.Text;
			}
			set
			{
				_formattedText.Text = value;
				InvalidateMeasure();
			}
		}

		[HiddenInEditor]
		[XmlIgnore]
		[EditCategory("Appearance")]
		public SpriteFont Font
		{
			get
			{
				return _formattedText.Font;
			}
			set
			{
				_formattedText.Font = value;
				InvalidateMeasure();
			}
		}

		[EditCategory("Appearance")]
		[DefaultValue(false)]
		public bool Wrap
		{
			get
			{
				return _wrap;
			}

			set
			{
				if (value == _wrap)
				{
					return;
				}

				_wrap = value;
				InvalidateMeasure();
			}
		}

		[EditCategory("Appearance")]
		public Color TextColor
		{
			get; set;
		}

		[EditCategory("Appearance")]
		public Color? DisabledTextColor
		{
			get; set;
		}

		[HiddenInEditor]
		[XmlIgnore]
		public Color? OverTextColor
		{
			get; set;
		}

		[HiddenInEditor]
		[XmlIgnore]
		public Color? PressedTextColor
		{
			get; set;
		}

		[HiddenInEditor]
		[XmlIgnore]
		public bool IsPressed
		{
			get; set;
		}

		public TextBlock(TextBlockStyle style)
		{
			if (style != null)
			{
				ApplyTextBlockStyle(style);
			}
		}

		public TextBlock(Stylesheet stylesheet, string style) : this(stylesheet.TextBlockStyles[style])
		{
		}

		public TextBlock(Stylesheet stylesheet) : this(stylesheet.TextBlockStyle)
		{
		}

		public TextBlock(string style) : this(Stylesheet.Current, style)
		{
		}

		public TextBlock() : this(Stylesheet.Current)
		{
		}

		public override void InternalRender(RenderContext context)
		{
			if (_formattedText.Font == null)
			{
				return;
			}

			var bounds = ActualBounds;

			var color = TextColor;
			if (!Enabled && DisabledTextColor != null)
			{
				color = DisabledTextColor.Value;
			}
			else if (IsPressed && PressedTextColor != null)
			{
				color = PressedTextColor.Value;
			}
			else if (IsMouseOver && OverTextColor != null)
			{
				color = OverTextColor.Value;
			}

			_formattedText.Draw(context.Batch, bounds.Location, context.View, color, context.Opacity);
		}

		protected override Point InternalMeasure(Point availableSize)
		{
			if (Font == null)
			{
				return Point.Zero;
			}

			var width = availableSize.X;
			if (Width != null && Width.Value < width)
			{
				width = Width.Value;
			}

			var result = Point.Zero;
			if (Font != null)
			{
				result = _formattedText.Measure(_wrap ? width : default(int?));
			}

			if (result.Y < CrossEngineStuff.LineSpacing(Font))
			{
				result.Y = CrossEngineStuff.LineSpacing(Font);
			}

			return result;
		}

		public override void Arrange()
		{
			base.Arrange();

			_formattedText.Width = _wrap ? Bounds.Width : default(int?);
		}

		public void ApplyTextBlockStyle(TextBlockStyle style)
		{
			ApplyWidgetStyle(style);

			TextColor = style.TextColor;
			DisabledTextColor = style.DisabledTextColor;
			OverTextColor = style.OverTextColor;
			PressedTextColor = style.PressedTextColor;
			Font = style.Font;
		}

		protected override void SetStyleByName(Stylesheet stylesheet, string name)
		{
			ApplyTextBlockStyle(stylesheet.TextBlockStyles[name]);
		}

		internal override string[] GetStyleNames(Stylesheet stylesheet)
		{
			return stylesheet.TextBlockStyles.Keys.ToArray();
		}
	}
}