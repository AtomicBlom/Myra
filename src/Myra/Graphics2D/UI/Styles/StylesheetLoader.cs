﻿using System;
using System.Collections.Generic;
using Myra.Graphics2D.TextureAtlases;
using Myra.Utility;

#if !XENKO
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
using Xenko.Graphics;
using Xenko.Core.Mathematics;
#endif

namespace Myra.Graphics2D.UI.Styles
{
	internal class StylesheetLoader
	{
		public const string TypeName = "type";
		public const string ColorsName = "colors";
		public const string DrawablesName = "drawables";
		public const string DesktopName = "desktop";
		public const string TextBlockName = "textBlock";
		public const string TextFieldName = "textField";
		public const string ScrollAreaName = "scrollArea";
		public const string ButtonName = "button";
		public const string CheckBoxName = "checkBox";
		public const string RadioButtonName = "radioButton";
		public const string ImageButtonName = "imageButton";
		public const string SpinButtonName = "spinButton";
		public const string TextButtonName = "textButton";
		public const string ComboBoxName = "comboBox";
		public const string ComboBoxItemName = "comboBoxItem";
		public const string ListBoxName = "listBox";
		public const string TabControlName = "tabControl";
		public const string ListBoxItemName = "listBoxItem";
		public const string TabItemName = "tabItem";
		public const string GridName = "grid";
		public const string TreeName = "tree";
		public const string SplitPaneName = "splitPane";
		public const string HorizontalSplitPaneName = "horizontalSplitPane";
		public const string VerticalSplitPaneName = "verticalSplitPane";
		public const string HorizontalMenuName = "horizontalMenu";
		public const string VerticalMenuName = "verticalMenu";
		public const string WindowName = "window";
		public const string DialogName = "dialog";
		public const string LeftName = "left";
		public const string RightName = "right";
		public const string WidthName = "width";
		public const string HeightName = "height";
		public const string TopName = "top";
		public const string BottomName = "bottom";
		public const string BackgroundName = "background";
		public const string OverBackgroundName = "overBackground";
		public const string DisabledBackgroundName = "disabledBackground";
		public const string FocusedBackgroundName = "focusedBackground";
		public const string PressedBackgroundName = "pressedBackground";
		public const string BorderName = "border";
		public const string OverBorderName = "overBorder";
		public const string DisabledBorderName = "disabledBorder";
		public const string FocusedBorderName = "focusedBorder";
		public const string BoundsName = "bounds";
		public const string PaddingName = "padding";
		public const string FontName = "font";
		public const string MessageFontName = "font";
		public const string TextColorName = "textColor";
		public const string DisabledTextColorName = "disabledTextColor";
		public const string FocusedTextColorName = "focusedTextColor";
		public const string OverTextColorName = "overTextColor";
		public const string PressedTextColorName = "pressedTextColor";
		public const string HorizontalScrollName = "horizontalScroll";
		public const string HorizontalScrollKnobName = "horizontalScrollKnob";
		public const string VerticalScrollName = "verticalScroll";
		public const string VerticalScrollKnobName = "verticalScrollKnob";
		public const string SelectionName = "selection";
		public const string SplitHorizontalHandleName = "horizontalHandle";
		public const string SplitHorizontalHandleOverName = "horizontalHandleOver";
		public const string SplitVerticalHandleName = "verticalHandle";
		public const string SplitVerticalHandleOverName = "verticalHandleOver";
		public const string TitleStyleName = "title";
		public const string CloseButtonStyleName = "closeButton";
		public const string UpButtonStyleName = "upButton";
		public const string DownButtonStyleName = "downButton";
		public const string CursorName = "cursor";
		public const string IconWidthName = "iconWidth";
		public const string ShortcutWidthName = "shortcutWidth";
		public const string SpacingName = "spacing";
		public const string HeaderSpacingName = "headerSpacing";
		public const string ButtonSpacingName = "buttonSpacing";
		public const string LabelStyleName = "label";
		public const string ImageStyleName = "image";
		public const string TextFieldStyleName = "textField";
		public const string MenuItemName = "menuItem";
		public const string HandleName = "handle";
		public const string MarkName = "mark";
		public const string ImageName = "image";
		public const string OverImageName = "overImage";
		public const string PressedImageName = "pressedImage";
		public const string SelectionBackgroundName = "selectionBackground";
		public const string SelectionBackgroundWithoutFocusName = "selectionBackgroundWithoutFocus";
		public const string SelectionHoverBackgroundName = "selectionHoverBackground";
		public const string SeparatorName = "separator";
		public const string ThicknessName = "thickness";
		public const string ItemsContainerName = "itemsContainer";
		public const string HorizontalSliderName = "horizontalSlider";
		public const string VerticalSliderName = "verticalSlider";
		public const string KnobName = "knob";
		public const string HorizontalProgressBarName = "horizontalProgressBar";
		public const string VerticalProgressBarName = "verticalProgressBar";
		public const string HorizontalSeparatorName = "horizontalSeparator";
		public const string VerticalSeparatorName = "verticalSeparator";
		public const string FilledName = "filled";
		public const string VariantsName = "variants";
		public const string ContentName = "content";

		private readonly Dictionary<string, Color> _colors = new Dictionary<string, Color>();
		private readonly Dictionary<string, Tuple<string, Color>> _drawables = new Dictionary<string, Tuple<string, Color>>();
		private readonly Dictionary<string, object> _root;
		private readonly Func<string, SpriteFont> _fontLoader;
		private readonly Func<string, TextureRegion> _textureLoader;

		public StylesheetLoader(Dictionary<string, object> root,
			Func<string, TextureRegion> textureLoader,
			Func<string, SpriteFont> fontLoader)
		{
			if (root == null)
			{
				throw new ArgumentNullException("root");
			}

			if (textureLoader == null)
			{
				throw new ArgumentNullException("textureLoader");
			}

			if (fontLoader == null)
			{
				throw new ArgumentNullException("fontLoader");
			}

			_root = root;
			_fontLoader = fontLoader;
			_textureLoader = textureLoader;
		}

		private IRenderable GetDrawable(string id)
		{
			Tuple<string, Color> IRenderableFromTable;
			if (_drawables.TryGetValue(id, out IRenderableFromTable))
			{
				return new ColoredRegion(_textureLoader(IRenderableFromTable.Item1), IRenderableFromTable.Item2);
			}

			return _textureLoader(id);
		}

		private void ParseDrawables(Dictionary<string, object> drawables)
		{
			_drawables.Clear();

			foreach (var props in drawables)
			{
				// Parse it
				var drawable = (Dictionary<string, object>)props.Value;

				var name = drawable["name"].ToString();
				var color = drawable["color"].ToString();

				var renderable = new Tuple<string, Color>(name, GetColor(color));
				_drawables.Add(props.Key, renderable);
			}
		}

		private SpriteFont GetFont(string id)
		{
			if (_fontLoader == null || string.IsNullOrEmpty(id))
			{
				return null;
			}

			return _fontLoader(id);
		}

		private void ParseColors(Dictionary<string, object> colors)
		{
			_colors.Clear();

			foreach (var props in colors)
			{
				// Parse it
				var stringValue = props.Value.ToString();
				var parsedColor = stringValue.FromName();
				if (parsedColor == null)
				{
					throw new Exception(string.Format("Could not parse color '{0}'", stringValue));
				}

				_colors.Add(props.Key, parsedColor.Value);
			}
		}

		private Color GetColor(string color)
		{
			Color result;
			if (_colors.TryGetValue(color, out result))
			{
				return result;
			}

			var fromName = color.FromName();
			if (fromName != null)
			{
				return fromName.Value;
			}

			// Not found
			throw new Exception(string.Format("Unknown color '{0}'", color));
		}

		private void LoadWidgetStyleFromSource(Dictionary<string, object> source, WidgetStyle result)
		{
			int i;
			if (source.GetStyle(WidthName, out i))
			{
				result.Width = i;
			}

			if (source.GetStyle(HeightName, out i))
			{
				result.Height = i;
			}

			string name;
			if (source.GetStyle(BackgroundName, out name))
			{
				result.Background = GetDrawable(name);
			}

			if (source.GetStyle(OverBackgroundName, out name))
			{
				result.OverBackground = GetDrawable(name);
			}

			if (source.GetStyle(DisabledBackgroundName, out name))
			{
				result.DisabledBackground = GetDrawable(name);
			}

			if (source.GetStyle(FocusedBackgroundName, out name))
			{
				result.FocusedBackground = GetDrawable(name);
			}

			if (source.GetStyle(BorderName, out name))
			{
				result.Border = GetDrawable(name);
			}

			if (source.GetStyle(OverBorderName, out name))
			{
				result.OverBorder = GetDrawable(name);
			}

			if (source.GetStyle(DisabledBorderName, out name))
			{
				result.DisabledBorder = GetDrawable(name);
			}

			if (source.GetStyle(FocusedBorderName, out name))
			{
				result.FocusedBorder = GetDrawable(name);
			}

			Dictionary<string, object> padding;
			if (source.GetStyle(PaddingName, out padding))
			{
				int value;
				if (padding.GetStyle(LeftName, out value))
				{
					result.PaddingLeft = value;
				}

				if (padding.GetStyle(RightName, out value))
				{
					result.PaddingRight = value;
				}

				if (padding.GetStyle(TopName, out value))
				{
					result.PaddingTop = value;
				}

				if (padding.GetStyle(BottomName, out value))
				{
					result.PaddingBottom = value;
				}
			}
		}

		private void LoadTextBlockStyleFromSource(Dictionary<string, object> source, TextBlockStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			string name;
			if (source.GetStyle(FontName, out name))
			{
				result.Font = GetFont(name);
			}

			if (source.GetStyle(TextColorName, out name))
			{
				result.TextColor = GetColor(name);
			}

			if (source.GetStyle(DisabledTextColorName, out name))
			{
				result.DisabledTextColor = GetColor(name);
			}

			if (source.GetStyle(OverTextColorName, out name))
			{
				result.OverTextColor = GetColor(name);
			}

			if (source.GetStyle(PressedTextColorName, out name))
			{
				result.PressedTextColor = GetColor(name);
			}
		}

		private void LoadTextFieldStyleFromSource(Dictionary<string, object> source, TextFieldStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			string name;
			if (source.GetStyle(TextColorName, out name))
			{
				result.TextColor = GetColor(name);
			}

			if (source.GetStyle(DisabledTextColorName, out name))
			{
				result.DisabledTextColor = GetColor(name);
			}

			if (source.GetStyle(FocusedTextColorName, out name))
			{
				result.FocusedTextColor = GetColor(name);
			}

			if (source.GetStyle(FontName, out name))
			{
				result.Font = GetFont(name);
			}

			if (source.GetStyle(MessageFontName, out name))
			{
				result.MessageFont = GetFont(name);
			}

			if (source.GetStyle(CursorName, out name))
			{
				result.Cursor = GetDrawable(name);
			}

			if (source.GetStyle(SelectionName, out name))
			{
				result.Selection = GetDrawable(name);
			}
		}

		private void LoadScrollAreaStyleFromSource(Dictionary<string, object> source, ScrollPaneStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			string TextureRegionName;
			if (source.GetStyle(HorizontalScrollName, out TextureRegionName))
			{
				result.HorizontalScrollBackground = GetDrawable(TextureRegionName);
			}

			if (source.GetStyle(HorizontalScrollKnobName, out TextureRegionName))
			{
				result.HorizontalScrollKnob = GetDrawable(TextureRegionName);
			}

			if (source.GetStyle(VerticalScrollName, out TextureRegionName))
			{
				result.VerticalScrollBackground = GetDrawable(TextureRegionName);
			}

			if (source.GetStyle(VerticalScrollKnobName, out TextureRegionName))
			{
				result.VerticalScrollKnob = GetDrawable(TextureRegionName);
			}
		}

		private void LoadImageStyleFromSource(Dictionary<string, object> source, ImageStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			string name;
			if (source.GetStyle(ImageName, out name))
			{
				result.Image = GetDrawable(name);
			}

			if (source.GetStyle(OverImageName, out name))
			{
				result.OverImage = GetDrawable(name);
			}
		}

		private void LoadPressableImageStyleFromSource(Dictionary<string, object> source, PressableImageStyle result)
		{
			LoadImageStyleFromSource(source, result);

			string name;
			if (source.GetStyle(PressedImageName, out name))
			{
				result.PressedImage = GetDrawable(name);
			}
		}

		private void LoadButtonStyleFromSource(Dictionary<string, object> source, ButtonStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			string name;
			if (source.GetStyle(PressedBackgroundName, out name))
			{
				result.PressedBackground = GetDrawable(name);
			}
		}

		private void LoadImageTextButtonStyleFromSource(Dictionary<string, object> source, ImageTextButtonStyle result)
		{
			LoadButtonStyleFromSource(source, result);

			Dictionary<string, object> style;
			if (source.GetStyle(LabelStyleName, out style))
			{
				result.LabelStyle = new TextBlockStyle();
				LoadTextBlockStyleFromSource(style, result.LabelStyle);
			}

			if (source.GetStyle(ImageStyleName, out style))
			{
				result.ImageStyle = new PressableImageStyle();
				LoadPressableImageStyleFromSource(style, result.ImageStyle);
			}

			int spacing;
			if (source.GetStyle(SpacingName, out spacing))
			{
				result.ImageTextSpacing = spacing;
			}
		}

		private void LoadTextButtonStyleFromSource(Dictionary<string, object> source, TextButtonStyle result)
		{
			LoadButtonStyleFromSource(source, result);

			Dictionary<string, object> labelStyle;
			if (source.GetStyle(LabelStyleName, out labelStyle))
			{
				result.LabelStyle = new TextBlockStyle();
				LoadTextBlockStyleFromSource(labelStyle, result.LabelStyle);
			}
		}

		private void LoadImageButtonStyleFromSource(Dictionary<string, object> source, ImageButtonStyle result)
		{
			LoadButtonStyleFromSource(source, result);

			Dictionary<string, object> style;
			if (source.GetStyle(ImageStyleName, out style))
			{
				result.ImageStyle = new PressableImageStyle();
				LoadPressableImageStyleFromSource(style, result.ImageStyle);
			}
		}

		private void LoadSpinButtonStyleFromSource(Dictionary<string, object> source, SpinButtonStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			Dictionary<string, object> style;
			if (source.GetStyle(TextFieldStyleName, out style))
			{
				result.TextFieldStyle = new TextFieldStyle();
				LoadTextFieldStyleFromSource(style, result.TextFieldStyle);
			}

			if (source.GetStyle(UpButtonStyleName, out style))
			{
				result.UpButtonStyle = new ImageButtonStyle();
				LoadImageButtonStyleFromSource(style, result.UpButtonStyle);
			}

			if (source.GetStyle(DownButtonStyleName, out style))
			{
				result.DownButtonStyle = new ImageButtonStyle();
				LoadImageButtonStyleFromSource(style, result.DownButtonStyle);
			}
		}

		private void LoadSliderStyleFromSource(Dictionary<string, object> source, SliderStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			Dictionary<string, object> knobStyle;
			if (source.GetStyle(KnobName, out knobStyle))
			{
				result.KnobStyle = new ImageButtonStyle();
				LoadImageButtonStyleFromSource(knobStyle, result.KnobStyle);
			}
		}

		private void LoadProgressBarStyleFromSource(Dictionary<string, object> source, ProgressBarStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			string style;
			if (source.GetStyle(FilledName, out style))
			{
				result.Filled = GetDrawable(style);
			}
		}

		private void LoadComboBoxStyleFromSource(Dictionary<string, object> source, ComboBoxStyle result)
		{
			LoadImageTextButtonStyleFromSource(source, result);

			Dictionary<string, object> subStyle;
			if (source.GetStyle(ItemsContainerName, out subStyle))
			{
				result.ItemsContainerStyle = new WidgetStyle();
				LoadWidgetStyleFromSource(subStyle, result.ItemsContainerStyle);
			}

			if (source.GetStyle(ComboBoxItemName, out subStyle))
			{
				result.ListItemStyle = new ImageTextButtonStyle();
				LoadImageTextButtonStyleFromSource(subStyle, result.ListItemStyle);
			}
		}

		private void LoadListBoxStyleFromSource(Dictionary<string, object> source, ListBoxStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			Dictionary<string, object> subStyle;
			if (source.GetStyle(ListBoxItemName, out subStyle))
			{
				result.ListItemStyle = new ImageTextButtonStyle();
				LoadImageTextButtonStyleFromSource(subStyle, result.ListItemStyle);
			}

			if (source.GetStyle(SeparatorName, out subStyle))
			{
				result.SeparatorStyle = new SeparatorStyle();
				LoadSeparatorStyleFromSource(subStyle, result.SeparatorStyle);
			}
		}

		private void LoadTabControlStyleFromSource(Dictionary<string, object> source, TabControlStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			Dictionary<string, object> subStyle;
			if (source.GetStyle(TabItemName, out subStyle))
			{
				result.TabItemStyle = new ImageTextButtonStyle();
				LoadImageTextButtonStyleFromSource(subStyle, result.TabItemStyle);
			}

			if (source.GetStyle(ContentName, out subStyle))
			{
				result.ContentStyle = new WidgetStyle();
				LoadWidgetStyleFromSource(subStyle, result.ContentStyle);
			}

			int spacing;
			if (source.GetStyle(HeaderSpacingName, out spacing))
			{
				result.HeaderSpacing = spacing;
			}

			if (source.GetStyle(ButtonSpacingName, out spacing))
			{
				result.ButtonSpacing = spacing;
			}
		}

		private void LoadMenuItemStyleFromSource(Dictionary<string, object> source, MenuItemStyle result)
		{
			LoadImageTextButtonStyleFromSource(source, result);

			int value;
			if (source.GetStyle(IconWidthName, out value))
			{
				result.IconWidth = value;
			}

			if (source.GetStyle(ShortcutWidthName, out value))
			{
				result.ShortcutWidth = value;
			}
		}

		private void LoadSeparatorStyleFromSource(Dictionary<string, object> source, SeparatorStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			string name;
			if (source.GetStyle(ImageName, out name))
			{
				result.Image = GetDrawable(name);
			}

			if (source.GetStyle(ThicknessName, out name))
			{
				result.Thickness = int.Parse(name);
			}
		}

		private void LoadSplitPaneStyleFromSource(Dictionary<string, object> source, SplitPaneStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			Dictionary<string, object> handle;
			if (source.GetStyle(HandleName, out handle))
			{
				result.HandleStyle = new ButtonStyle();
				LoadButtonStyleFromSource(handle, result.HandleStyle);
			}
		}

		private void LoadMenuStyleFromSource(Dictionary<string, object> source, MenuStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			Dictionary<string, object> menuItem;
			if (source.GetStyle(MenuItemName, out menuItem))
			{
				result.MenuItemStyle = new MenuItemStyle();
				LoadMenuItemStyleFromSource(menuItem, result.MenuItemStyle);
			}

			if (source.GetStyle(SeparatorName, out menuItem))
			{
				result.SeparatorStyle = new SeparatorStyle();
				LoadSeparatorStyleFromSource(menuItem, result.SeparatorStyle);
			}
		}

		private void LoadTreeStyleFromSource(Dictionary<string, object> source, TreeStyle result)
		{
			string name;
			if (source.GetStyle(SelectionBackgroundName, out name))
			{
				result.SelectionBackground = GetDrawable(name);
			}

			if (source.GetStyle(SelectionHoverBackgroundName, out name))
			{
				result.SelectionHoverBackground = GetDrawable(name);
			}

			Dictionary<string, object> obj;
			if (source.GetStyle(MarkName, out obj))
			{
				result.MarkStyle = new ImageButtonStyle();
				LoadImageButtonStyleFromSource(obj, result.MarkStyle);

				if (obj.GetStyle(LabelStyleName, out obj))
				{
					result.LabelStyle = new TextBlockStyle();
					LoadTextBlockStyleFromSource(obj, result.LabelStyle);
				}
			}
		}

		private void LoadWindowStyleFromSource(Dictionary<string, object> source, WindowStyle result)
		{
			LoadWidgetStyleFromSource(source, result);

			Dictionary<string, object> obj;
			if (source.GetStyle(TitleStyleName, out obj))
			{
				result.TitleStyle = new TextBlockStyle();
				LoadTextBlockStyleFromSource(obj, result.TitleStyle);
			}

			if (source.GetStyle(CloseButtonStyleName, out obj))
			{
				result.CloseButtonStyle = new ImageButtonStyle();
				LoadImageButtonStyleFromSource(obj, result.CloseButtonStyle);
			}
		}

		private void LoadDialogStyleFromSource(Dictionary<string, object> source, DialogStyle result)
		{
			LoadWindowStyleFromSource(source, result);
		}

		private void FillStyles<T>(string key,
			Dictionary<string, T> stylesDict,
			Action<Dictionary<string, object>, T> fillAction) where T : WidgetStyle, new()
		{
			Dictionary<string, object> source;
			if (!_root.GetStyle(key, out source) || source == null)
			{
				return;
			}

			stylesDict[Stylesheet.DefaultStyleName] = new T();
			fillAction(source, stylesDict[Stylesheet.DefaultStyleName]);

			Dictionary<string, object> styles;
			if (!source.GetStyle(VariantsName, out styles) || styles == null)
			{
				return;
			}

			foreach (var pair in styles)
			{
				var variant = (T)stylesDict[Stylesheet.DefaultStyleName].Clone();
				fillAction((Dictionary<string, object>)pair.Value, variant);
				stylesDict[pair.Key] = variant;
			}
		}

		private DesktopStyle LoadDesktopStyleFromSource()
		{
			var result = new DesktopStyle();

			Dictionary<string, object> source;
			if (!_root.GetStyle(DesktopName, out source) || source == null)
			{
				return result;
			}

			string name;
			if (source.GetStyle(BackgroundName, out name))
			{
				result.Background = GetDrawable(name);
			}

			return result;
		}

		public Stylesheet Load()
		{
			var result = new Stylesheet();

			Dictionary<string, object> colors;
			if (_root.GetStyle(ColorsName, out colors))
			{
				ParseColors(colors);
			}

			Dictionary<string, object> drawables;
			if (_root.GetStyle(DrawablesName, out drawables))
			{
				ParseDrawables(drawables);
			}

			result.DesktopStyle = LoadDesktopStyleFromSource();

			FillStyles(TextBlockName, result.TextBlockStyles, LoadTextBlockStyleFromSource);
			FillStyles(TextFieldName, result.TextFieldStyles, LoadTextFieldStyleFromSource);
			FillStyles(ScrollAreaName, result.ScrollPaneStyles, LoadScrollAreaStyleFromSource);
			FillStyles(ButtonName, result.ButtonStyles, LoadButtonStyleFromSource);
			FillStyles(CheckBoxName, result.CheckBoxStyles, LoadImageTextButtonStyleFromSource);
			FillStyles(RadioButtonName, result.RadioButtonStyles, LoadImageTextButtonStyleFromSource);
			FillStyles(SpinButtonName, result.SpinButtonStyles, LoadSpinButtonStyleFromSource);
			FillStyles(HorizontalSliderName, result.HorizontalSliderStyles, LoadSliderStyleFromSource);
			FillStyles(VerticalSliderName, result.VerticalSliderStyles, LoadSliderStyleFromSource);
			FillStyles(HorizontalProgressBarName, result.HorizontalProgressBarStyles, LoadProgressBarStyleFromSource);
			FillStyles(VerticalProgressBarName, result.VerticalProgressBarStyles, LoadProgressBarStyleFromSource);
			FillStyles(HorizontalSeparatorName, result.HorizontalSeparatorStyles, LoadSeparatorStyleFromSource);
			FillStyles(VerticalSeparatorName, result.VerticalSeparatorStyles, LoadSeparatorStyleFromSource);
			FillStyles(ComboBoxName, result.ComboBoxStyles, LoadComboBoxStyleFromSource);
			FillStyles(ListBoxName, result.ListBoxStyles, LoadListBoxStyleFromSource);
			FillStyles(TabControlName, result.TabControlStyles, LoadTabControlStyleFromSource);
			FillStyles(TreeName, result.TreeStyles, LoadTreeStyleFromSource);
			FillStyles(HorizontalSplitPaneName, result.HorizontalSplitPaneStyles, LoadSplitPaneStyleFromSource);
			FillStyles(VerticalSplitPaneName, result.VerticalSplitPaneStyles, LoadSplitPaneStyleFromSource);
			FillStyles(HorizontalMenuName, result.HorizontalMenuStyles, LoadMenuStyleFromSource);
			FillStyles(VerticalMenuName, result.VerticalMenuStyles, LoadMenuStyleFromSource);
			FillStyles(WindowName, result.WindowStyles, LoadWindowStyleFromSource);
			FillStyles(DialogName, result.DialogStyles, LoadDialogStyleFromSource);

			return result;
		}
	}
}