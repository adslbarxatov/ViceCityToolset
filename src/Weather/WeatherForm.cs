using System;
using System.Drawing;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает главную форму программы
	/// </summary>
	public partial class WeatherForm: Form
		{
		// Переменные
		private WeatherProvider wp;

		/// <summary>
		/// Конструктор. Запускает главную форму программы
		/// </summary>
		public WeatherForm ()
			{
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle + " – " + RDLocale.GetText (this.Name);

			RDGenerics.LoadWindowDimensions (this);
			this.ShowDialog ();
			}

		private void WeatherForm_Load (object sender, EventArgs e)
			{
			// Попытка загрузки файла данных
			wp = new WeatherProvider ();
			string res = "";

			switch (wp.InitStatus)
				{
				case WeatherProvider.InitStatuses.BrokenDescriptor:
					res = "HandlingForm_BrokenDescriptor";
					break;

				case WeatherProvider.InitStatuses.FileIsEmpty:
					res = "HandlingForm_FileIsEmpty";
					break;

				case WeatherProvider.InitStatuses.FileNotAvailable:
					res = "HandlingForm_FileUnavailable";
					break;

				case WeatherProvider.InitStatuses.FailedToCreateBackup:
					res = "HandlingForm_FailedToCreateBackup";
					break;
				}

			if (!string.IsNullOrEmpty (res))
				{
				RDInterface.MessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
					string.Format (RDLocale.GetText (res), WeatherProvider.ConfigurationFileName));
				this.Close ();
				return;
				}

			// Загружено. Выполнение локализации
			RDLocale.SetControlsText (this);
			BExit.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Exit);
			BSaveCfg.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Save);

			WeatherCombo.Items.Add (RDLocale.GetText ("Weathers_" + ParametersValues.Weathers[1].Name));
			WeatherCombo.Items.Add (RDLocale.GetText ("Weathers_" + ParametersValues.Weathers[2].Name));
			WeatherCombo.Items.Add (RDLocale.GetText ("Weathers_" + ParametersValues.Weathers[3].Name));
			WeatherCombo.Items.Add (RDLocale.GetText ("Weathers_" + ParametersValues.Weathers[5].Name));
			WeatherCombo.Items.Add (RDLocale.GetText ("Weathers_" + ParametersValues.Weathers[0].Name));
			WeatherCombo.Items.Add (RDLocale.GetText ("Weathers_" + ParametersValues.Weathers[4].Name));
			WeatherCombo.SelectedIndex = 4;

			for (int i = 0; i < 24; i++)
				TimeCombo.Items.Add (i.ToString () + ":00");
			TimeCombo.SelectedIndex = 12;
			}

		// Выход
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void WeatherForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			e.Cancel = (RDInterface.LocalizedMessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
				"ChangesSaved", RDLDefaultTexts.Button_Yes, RDLDefaultTexts.Button_No) ==
				RDMessageButtons.ButtonTwo);
			RDGenerics.SaveWindowDimensions (this);
			}

		// Сохранение файла
		private void SaveCfgFile_Click (object sender, EventArgs e)
			{
			// Сохранение
			if (!wp.SaveWeatherData ())
				RDInterface.MessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
					string.Format (RDLocale.GetText ("HandlingForm_SaveFailure"), WeatherProvider.ConfigurationFileName));
			else
				RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText,
					string.Format (RDLocale.GetText ("HandlingForm_SaveSuccess"), WeatherProvider.ConfigurationFileName), 1000);
			}

		// Выбор строки таблицы
		private void WeatherCombo_SelectedIndexChanged (object sender, EventArgs e)
			{
			// Контроль
			if (RowIndex < 0)
				return;
			uint idx = (uint)RowIndex;

			// Загрузка
			loading = true;

			SetButtonColor (StaticAmbience, wp.GetValue (idx, WeatherProvider.ColorValues.StaticAmbience));
			SetButtonColor (DynamicAmbience, wp.GetValue (idx, WeatherProvider.ColorValues.DynamicAmbience));
			SetButtonColor (StaticBlur, wp.GetValue (idx, WeatherProvider.ColorValues.StaticBlur));
			SetButtonColor (DynamicBlur, wp.GetValue (idx, WeatherProvider.ColorValues.DynamicBlur));
			SetButtonColor (DynamicDirect, wp.GetValue (idx, WeatherProvider.ColorValues.DynamicDirect));
			SetButtonColor (Water, wp.GetValue (idx, WeatherProvider.ColorValues.Water));

			SetButtonColor (SkyTop, wp.GetValue (idx, WeatherProvider.ColorValues.SkyTop));
			SetButtonColor (SkyBottom, wp.GetValue (idx, WeatherProvider.ColorValues.SkyBottom));
			SetButtonColor (UpperCloudsTop, wp.GetValue (idx, WeatherProvider.ColorValues.UpperCloudsTop));
			SetButtonColor (UpperCloudsBottom, wp.GetValue (idx, WeatherProvider.ColorValues.UpperCloudsBottom));
			SetButtonColor (SunCore, wp.GetValue (idx, WeatherProvider.ColorValues.SunCore));
			SetButtonColor (SunCorona, wp.GetValue (idx, WeatherProvider.ColorValues.SunCorona));

			SetButtonColor (LowerClouds, wp.GetValue (idx, WeatherProvider.ColorValues.LowerClouds));
			SetButtonColor (BlurTrails, wp.GetValue (idx, WeatherProvider.ColorValues.BlurTrails));

			SunSizeField.Value = (decimal)wp.GetValue (idx, WeatherProvider.FloatValues.SunCoreSize);
			CoronaSizeField.Value = (decimal)wp.GetValue (idx, WeatherProvider.FloatValues.SunCoronaSize);
			CoronaBrightnessField.Value = (decimal)wp.GetValue (idx, WeatherProvider.FloatValues.SunBrightness);
			ShadowField.Value = wp.GetValue (idx, WeatherProvider.IntValues.ShadowIntensity);
			EntityShadingField.Value = wp.GetValue (idx, WeatherProvider.IntValues.LightShading);
			PoleShadingField.Value = wp.GetValue (idx, WeatherProvider.IntValues.PoleShading);
			FarClipField.Value = (decimal)wp.GetValue (idx, WeatherProvider.FloatValues.FarClipping);
			FogStartField.Value = (decimal)wp.GetValue (idx, WeatherProvider.FloatValues.FogStart);
			LightOnGroundField.Value = (decimal)wp.GetValue (idx, WeatherProvider.FloatValues.LightOnGround);
			WaterAlphaField.Value = wp.GetValue (idx, WeatherProvider.IntValues.WaterAlpha);

			loading = false;
			}
		private bool loading = false;

		private static void SetButtonColor (Button B, Color C)
			{
			B.BackColor = C;
			int v = (C.R + C.G + C.B) / 3;
			if (v < 128)
				B.ForeColor = Color.White;
			else
				B.ForeColor = Color.Black;
			}

		// Выбор цвета
		private void StaticAmbience_Click (object sender, EventArgs e)
			{
			// Контроль
			if (RowIndex < 0)
				return;
			uint idx = (uint)RowIndex;
			Button b = (Button)sender;

			// Запрос цвета
			CDialog.Color = b.BackColor;
			if (CDialog.ShowDialog () != DialogResult.OK)
				return;

			// Обработка
			if (b.Name == StaticAmbience.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.StaticAmbience, CDialog.Color);
			else if (b.Name == DynamicAmbience.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.DynamicAmbience, CDialog.Color);
			else if (b.Name == StaticBlur.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.StaticBlur, CDialog.Color);
			else if (b.Name == DynamicBlur.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.DynamicBlur, CDialog.Color);
			else if (b.Name == DynamicDirect.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.DynamicDirect, CDialog.Color);
			else if (b.Name == Water.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.Water, CDialog.Color);

			else if (b.Name == SkyTop.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.SkyTop, CDialog.Color);
			else if (b.Name == SkyBottom.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.SkyBottom, CDialog.Color);
			else if (b.Name == UpperCloudsTop.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.UpperCloudsTop, CDialog.Color);
			else if (b.Name == UpperCloudsBottom.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.UpperCloudsBottom, CDialog.Color);
			else if (b.Name == SunCore.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.SunCore, CDialog.Color);
			else if (b.Name == SunCorona.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.SunCorona, CDialog.Color);

			else if (b.Name == LowerClouds.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.LowerClouds, CDialog.Color);
			else if (b.Name == BlurTrails.Name)
				wp.SetValue (idx, WeatherProvider.ColorValues.BlurTrails, CDialog.Color);

			SetButtonColor (b, CDialog.Color);
			}

		// Изменение числового значения
		private void SunSizeField_ValueChanged (object sender, EventArgs e)
			{
			// Контроль
			if (RowIndex < 0)
				return;
			if (loading)
				return;

			uint idx = (uint)RowIndex;
			NumericUpDown n = (NumericUpDown)sender;

			// Обработка
			if (n.Name == SunSizeField.Name)
				wp.SetValue (idx, WeatherProvider.FloatValues.SunCoreSize, (float)n.Value);
			else if (n.Name == CoronaSizeField.Name)
				wp.SetValue (idx, WeatherProvider.FloatValues.SunCoronaSize, (float)n.Value);
			else if (n.Name == CoronaBrightnessField.Name)
				wp.SetValue (idx, WeatherProvider.FloatValues.SunBrightness, (float)n.Value);

			else if (n.Name == ShadowField.Name)
				wp.SetValue (idx, WeatherProvider.IntValues.ShadowIntensity, (int)n.Value);
			else if (n.Name == EntityShadingField.Name)
				wp.SetValue (idx, WeatherProvider.IntValues.LightShading, (int)n.Value);
			else if (n.Name == PoleShadingField.Name)
				wp.SetValue (idx, WeatherProvider.IntValues.PoleShading, (int)n.Value);

			if (n.Name == FarClipField.Name)
				wp.SetValue (idx, WeatherProvider.FloatValues.FarClipping, (int)n.Value);
			else if (n.Name == FogStartField.Name)
				wp.SetValue (idx, WeatherProvider.FloatValues.FogStart, (int)n.Value);
			else if (n.Name == LightOnGroundField.Name)
				wp.SetValue (idx, WeatherProvider.FloatValues.LightOnGround, (float)n.Value);

			else if (n.Name == WaterAlphaField.Name)
				wp.SetValue (idx, WeatherProvider.IntValues.WaterAlpha, (int)n.Value);
			}

		// Вычисленный индекс строки файла конфигурации для выбранных погоды и времени
		private int RowIndex
			{
			get
				{
				if ((WeatherCombo.SelectedIndex < 0) || (TimeCombo.SelectedIndex < 0))
					return -1;
				return WeatherCombo.SelectedIndex * 24 + TimeCombo.SelectedIndex;
				}
			}
		}
	}
