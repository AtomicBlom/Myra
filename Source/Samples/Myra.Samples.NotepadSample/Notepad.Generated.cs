/* Generated by Myra UI Editor at 10/2/2017 12:50:44 AM */
using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;

namespace Myra.Samples.NotepadSample
{
	partial class Notepad
	{
		private void BuildUI()
		{
			menuItemNew = new MenuItem();
			menuItemNew.Id = "menuItemNew";
			menuItemNew.Text = "New";
			menuItemNew.Color = null;

			menuItemOpen = new MenuItem();
			menuItemOpen.Id = "menuItemOpen";
			menuItemOpen.Text = "Open...";
			menuItemOpen.Color = null;

			menuItemSave = new MenuItem();
			menuItemSave.Id = "menuItemSave";
			menuItemSave.Text = "Save";
			menuItemSave.Color = null;

			menuItemSaveAs = new MenuItem();
			menuItemSaveAs.Id = "menuItemSaveAs";
			menuItemSaveAs.Text = "Save As...";
			menuItemSaveAs.Color = null;

			var menuSeparator1 = new MenuSeparator();
			menuSeparator1.Id = null;

			menuItemQuit = new MenuItem();
			menuItemQuit.Id = "menuItemQuit";
			menuItemQuit.Text = "Quit";
			menuItemQuit.Color = null;

			menuItemFile = new MenuItem();
			menuItemFile.Id = "menuItemFile";
			menuItemFile.Text = "File";
			menuItemFile.Color = null;
			menuItemFile.Items.Add(menuItemNew);
			menuItemFile.Items.Add(menuItemOpen);
			menuItemFile.Items.Add(menuItemSave);
			menuItemFile.Items.Add(menuItemSaveAs);
			menuItemFile.Items.Add(menuSeparator1);
			menuItemFile.Items.Add(menuItemQuit);

			menuItemAbout = new MenuItem();
			menuItemAbout.Id = "menuItemAbout";
			menuItemAbout.Text = "About";
			menuItemAbout.Color = null;

			menuItemHelp = new MenuItem();
			menuItemHelp.Id = "menuItemHelp";
			menuItemHelp.Text = "Help";
			menuItemHelp.Color = null;
			menuItemHelp.Items.Add(menuItemAbout);

			mainMenu = new HorizontalMenu();
			mainMenu.DrawLines = false;
			mainMenu.DrawLinesColor = Color.White;
			mainMenu.Enabled = true;
			mainMenu.Id = "mainMenu";
			mainMenu.XHint = 0;
			mainMenu.YHint = 0;
			mainMenu.WidthHint = null;
			mainMenu.HeightHint = null;
			mainMenu.PaddingLeft = 0;
			mainMenu.PaddingRight = 0;
			mainMenu.PaddingTop = 0;
			mainMenu.PaddingBottom = 0;
			mainMenu.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			mainMenu.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			mainMenu.GridPositionX = 0;
			mainMenu.GridPositionY = 0;
			mainMenu.GridSpanX = 1;
			mainMenu.GridSpanY = 1;
			mainMenu.Visible = true;
			mainMenu.ClipToBounds = false;
			mainMenu.CanFocus = false;
			mainMenu.Items.Add(menuItemFile);
			mainMenu.Items.Add(menuItemHelp);

			textArea = new TextField();
			textArea.VerticalSpacing = 0;
			textArea.Text = "";
			textArea.Wrap = true;
			textArea.TextColor = Color.White;
			textArea.DisabledTextColor = new Color
			{
				B = 170,
				G = 170,
				R = 170,
				A = 255,
				PackedValue = 4289374890,
			};
			textArea.FocusedTextColor = Color.TransparentBlack;
			textArea.MessageTextColor = new Color
			{
				B = 170,
				G = 170,
				R = 170,
				A = 255,
				PackedValue = 4289374890,
			};
			textArea.BlinkIntervalInMs = 450;
			textArea.Id = "textArea";
			textArea.XHint = 0;
			textArea.YHint = 0;
			textArea.WidthHint = null;
			textArea.HeightHint = null;
			textArea.PaddingLeft = 0;
			textArea.PaddingRight = 0;
			textArea.PaddingTop = 0;
			textArea.PaddingBottom = 0;
			textArea.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			textArea.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			textArea.GridPositionX = 0;
			textArea.GridPositionY = 0;
			textArea.GridSpanX = 1;
			textArea.GridSpanY = 1;
			textArea.Enabled = true;
			textArea.Visible = true;
			textArea.ClipToBounds = false;
			textArea.CanFocus = true;

			var scrollPane1 = new ScrollPane();
			scrollPane1.Enabled = true;
			scrollPane1.Id = null;
			scrollPane1.XHint = 0;
			scrollPane1.YHint = 0;
			scrollPane1.WidthHint = null;
			scrollPane1.HeightHint = null;
			scrollPane1.PaddingLeft = 0;
			scrollPane1.PaddingRight = 0;
			scrollPane1.PaddingTop = 0;
			scrollPane1.PaddingBottom = 0;
			scrollPane1.HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			scrollPane1.VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			scrollPane1.GridPositionX = 0;
			scrollPane1.GridPositionY = 1;
			scrollPane1.GridSpanX = 1;
			scrollPane1.GridSpanY = 1;
			scrollPane1.Visible = true;
			scrollPane1.ClipToBounds = true;
			scrollPane1.CanFocus = true;
			scrollPane1.Child = textArea;

			
			DrawLines = false;
			DrawLinesColor = Color.White;
			ColumnSpacing = 0;
			RowSpacing = 0;
			RowsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.Grid.ProportionType.Auto,
				Value = 1,
			});
			RowsProportions.Add(new Proportion
			{
				Type = Myra.Graphics2D.UI.Grid.ProportionType.Fill,
				Value = 1,
			});
			TotalRowsPart = null;
			TotalColumnsPart = null;
			Enabled = true;
			Id = "Root";
			XHint = 0;
			YHint = 0;
			WidthHint = null;
			HeightHint = null;
			PaddingLeft = 0;
			PaddingRight = 0;
			PaddingTop = 0;
			PaddingBottom = 0;
			HorizontalAlignment = Myra.Graphics2D.UI.HorizontalAlignment.Stretch;
			VerticalAlignment = Myra.Graphics2D.UI.VerticalAlignment.Stretch;
			GridPositionX = 0;
			GridPositionY = 0;
			GridSpanX = 1;
			GridSpanY = 1;
			Visible = true;
			ClipToBounds = false;
			CanFocus = false;
			Widgets.Add(mainMenu);
			Widgets.Add(scrollPane1);
		}

		
		public MenuItem menuItemNew;
		public MenuItem menuItemOpen;
		public MenuItem menuItemSave;
		public MenuItem menuItemSaveAs;
		public MenuItem menuItemQuit;
		public MenuItem menuItemFile;
		public MenuItem menuItemAbout;
		public MenuItem menuItemHelp;
		public HorizontalMenu mainMenu;
		public TextField textArea;
	}
}