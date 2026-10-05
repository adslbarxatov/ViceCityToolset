using System;
using System.Drawing;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает главную форму программы
	/// </summary>
	public partial class ColorsForm: Form
		{
		// Переменные
		private CarColors cc;

		private string[][] carIDMapping = [
			[
			"CADDY",
			"COASTG",
			"FAGGIO",
			"HOTRINA",
			"HOTRINB",
			"JETMAX",
			"MESA",
			"PCJ600",
			"PIZZABOY",
			"RCRAIDER",
			"SABRE",
			"SANCHEZ",
			"SENTXS",
			"SKIMMER",
			"VCNMAV",
			"VICECHEE"
			],
			[
			"GOLFCART",
			"COASTGRD",
			"MOPED",
			"HOTRING",
			"HOTRING",
			"CUPBOAT",
			"DESPERAD",
			"BIKE",
			"MOPED",
			"RCCOPTER",
			"SABRE1",
			"DIRTBIKE",
			"MAFIA",
			"SEAPLANE",
			"COASTMAV",
			"CHEETAH"
			]
			];

		/// <summary>
		/// Конструктор. Запускает форму настройки цветовой схемы транспорта
		/// </summary>
		public ColorsForm (CarColors Colors)
			{
			InitializeComponent ();

			// Настройка контролов
			this.Text = ProgramDescription.AssemblyMainName + " – " + RDLocale.GetText (this.Name);
			RDGenerics.LoadWindowDimensions (this);

			RDLocale.SetDefaultControlText (ExitButton, RDLDefaultTexts.Button_Exit);
			RDLocale.SetDefaultControlText (SaveButton, RDLDefaultTexts.Button_Save);
			RDLocale.SetControlText (this.Name, ColorsLabel);
			RDLocale.SetDefaultControlText (AddColorButton, RDLDefaultTexts.Button_Add);
			RDLocale.SetDefaultControlText (UpdateColorButton, RDLDefaultTexts.Button_Update);
			RDLocale.SetDefaultControlText (DeleteColorButton, RDLDefaultTexts.Button_Delete);
			RDLocale.SetControlText (this.Name, CarsLabel);

			cc = Colors;
			for (int i = 0; i < cc.CarsCount; i++)
				{
				string id = cc.GetCarID ((byte)i).ToUpper ();
				string name = HandlingForm.GetVehicheNameByID (id);
				if (name == "—")
					{
					int idx = carIDMapping[0].IndexOf (id);
					if (idx >= 0)
						name = HandlingForm.GetVehicheNameByID (carIDMapping[1][idx]);
					}

				if (CarsCombo.Items.Contains (name))
					{
					if (CarsCombo.Items.Contains (name + " #2"))
						name += " #3";
					else
						name += " #2";
					}
				CarsCombo.Items.Add (name);
				}
			CarsCombo.SelectedIndex = 0;

			RDLocale.SetControlText (this.Name, VariantLabel);
			RDLocale.SetDefaultControlText (AddVariantButton, RDLDefaultTexts.Button_Add);
			RDLocale.SetDefaultControlText (UpdateVariantButton, RDLDefaultTexts.Button_Update);
			RDLocale.SetDefaultControlText (DeleteVariantButton, RDLDefaultTexts.Button_Delete);

			UpdateColorButtons ();

			// Запуск
			this.ShowDialog ();
			}

		// Выход
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void ColorsForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			e.Cancel = (RDInterface.LocalizedMessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
				"ChangesSaved", RDLDefaultTexts.Button_Yes, RDLDefaultTexts.Button_No) ==
				RDMessageButtons.ButtonTwo);
			RDGenerics.SaveWindowDimensions (this);
			}

		// Выбор цвета в таблице
		private void ColorNumberField_ValueChanged (object sender, EventArgs e)
			{
			ColorSampleButton.BackColor = cc.GetColor ((byte)ColorNumberField.Value);
			}

		// Выбор цветового образца
		private void ColorSampleButton_Click (object sender, EventArgs e)
			{
			CLDialog.Color = ColorSampleButton.BackColor;
			if (CLDialog.ShowDialog () != DialogResult.OK)
				return;

			ColorSampleButton.BackColor = CLDialog.Color;
			}

		// Добавление нового цвета
		private void AddColorButton_Click (object sender, EventArgs e)
			{
			if (!cc.AddColor (ColorSampleButton.BackColor))
				return;

			UpdateColorButtons ();
			ColorNumberField.Value = ColorNumberField.Maximum;

			RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText | RDMessageFlags.LockSmallSize,
				string.Format (RDLocale.GetText ("ColorAddedMessage"), (uint)ColorNumberField.Value,
				ColorSampleButton.BackColor.R, ColorSampleButton.BackColor.G, ColorSampleButton.BackColor.B));
			}

		// Обновление состояния кнопок
		private void UpdateColorButtons ()
			{
			ColorNumberField.Maximum = Color1Field.Maximum = Color2Field.Maximum = cc.ColorsCount - 1;
			ColorNumberField_ValueChanged (null, null);

			DeleteColorButton.Enabled = (cc.ColorsCount > CarColors.MinColors);
			AddColorButton.Enabled = (cc.ColorsCount < CarColors.MaxColors);
			}

		// Обновление указанного цвета
		private void UpdateColorButton_Click (object sender, EventArgs e)
			{
			if (!cc.UpdateColor ((byte)ColorNumberField.Value, ColorSampleButton.BackColor))
				return;

			UpdateColorButtons ();

			RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText | RDMessageFlags.LockSmallSize,
				string.Format (RDLocale.GetText ("ColorUpdatedMessage"), (uint)ColorNumberField.Value,
				ColorSampleButton.BackColor.R, ColorSampleButton.BackColor.G, ColorSampleButton.BackColor.B));
			}

		// Удаление указанного цвета
		private void DeleteColorButton_Click (object sender, EventArgs e)
			{
			if (RDInterface.LocalizedMessageBox (RDMessageFlags.Warning | RDMessageFlags.LeftText |
				RDMessageFlags.LockSmallSize, "DeleteColorMessage", RDLDefaultTexts.Button_YesNoFocus,
				RDLDefaultTexts.Button_No) != RDMessageButtons.ButtonOne)
				return;

			if (!cc.DeleteColor ((byte)ColorNumberField.Value))
				return;

			RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText | RDMessageFlags.LockSmallSize,
				string.Format (RDLocale.GetText ("ColorRemovedMessage"), (uint)ColorNumberField.Value));

			UpdateColorButtons ();
			ColorNumberField_ValueChanged (null, null);
			}

		// Сохранение файла
		private void SaveButton_Click (object sender, EventArgs e)
			{
			if (!cc.SaveFile ())
				RDInterface.MessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
					string.Format (RDLocale.GetDefaultText (RDLDefaultTexts.Message_SaveFailure_Fmt),
					"carcols.dat"));
			else
				RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText,
					string.Format (RDLocale.GetDefaultText (RDLDefaultTexts.Message_SaveSuccess_Fmt),
					"carcols.dat"), 1000);
			}

		// Выбор транспорта для настройки
		private void CarsCombo_SelectedIndexChanged (object sender, EventArgs e)
			{
			UpdateVariants ();

			VariantField.Value = 1;
			VariantField_ValueChanged (null, null);
			}
		private byte[] currentColors;

		private void UpdateVariants ()
			{
			currentColors = cc.GetCarColors ((byte)CarsCombo.SelectedIndex);
			VariantField.Maximum = currentColors.Length / 2;

			AddVariantButton.Enabled = (currentColors.Length < 2 * CarColors.MaxColorsPerCar);
			DeleteVariantButton.Enabled = (currentColors.Length > 2);
			}

		// Выбор цвета транспорта для настройки
		private void VariantField_ValueChanged (object sender, EventArgs e)
			{
			int idx = 2 * ((int)VariantField.Value - 1);

			Color1Field.Value = currentColors[idx];
			Color1Field_ValueChanged (null, null);

			Color2Field.Value = currentColors[idx + 1];
			Color2Field_ValueChanged (null, null);
			}

		// Изменение вариантов цветов транспорта
		private void Color1Field_ValueChanged (object sender, EventArgs e)
			{
			Color1Label.BackColor = cc.GetColor ((byte)Color1Field.Value);
			if (Color1Label.BackColor.R + Color1Label.BackColor.G + Color1Label.BackColor.B > 128 * 3)
				Color1Label.ForeColor = Color.Black;
			else
				Color1Label.ForeColor = Color.White;
			}

		private void Color2Field_ValueChanged (object sender, EventArgs e)
			{
			Color2Label.BackColor = cc.GetColor ((byte)Color2Field.Value);
			if (Color2Label.BackColor.R + Color2Label.BackColor.G + Color2Label.BackColor.B > 128 * 3)
				Color2Label.ForeColor = Color.Black;
			else
				Color2Label.ForeColor = Color.White;
			}

		// Добавление цветового варианта
		private void AddVariantButton_Click (object sender, EventArgs e)
			{
			if (!cc.AddCarColorVariant ((byte)CarsCombo.SelectedIndex, (byte)Color1Field.Value,
				(byte)Color2Field.Value))
				return;

			UpdateVariants ();
			VariantField.Value = VariantField.Maximum;

			RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText | RDMessageFlags.LockSmallSize,
				string.Format (RDLocale.GetText ("VariantAddedMessage"), (uint)VariantField.Value,
				CarsCombo.Text, (uint)Color1Field.Value, (uint)Color2Field.Value));
			}

		// Обновление цветового варианта
		private void UpdateVariantButton_Click (object sender, EventArgs e)
			{
			if (!cc.UpdateCarColorVariant ((byte)CarsCombo.SelectedIndex, (byte)(VariantField.Value - 1),
				(byte)Color1Field.Value, (byte)Color2Field.Value))
				return;

			UpdateVariants ();
			
			RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText | RDMessageFlags.LockSmallSize,
				string.Format (RDLocale.GetText ("VariantUpdatedMessage"), (uint)VariantField.Value,
				CarsCombo.Text, (uint)Color1Field.Value, (uint)Color2Field.Value));
			}

		// Удаление цветового варианта
		private void DeleteVariantButton_Click (object sender, EventArgs e)
			{
			if (!cc.DeleteCarColorVariant ((byte)CarsCombo.SelectedIndex, (byte)(VariantField.Value - 1)))
				return;

			RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText | RDMessageFlags.LockSmallSize,
				string.Format (RDLocale.GetText ("VariantRemovedMessage"), (uint)VariantField.Value, CarsCombo.Text));

			UpdateVariants ();
			VariantField_ValueChanged (null, null);
			}
		}
	}
