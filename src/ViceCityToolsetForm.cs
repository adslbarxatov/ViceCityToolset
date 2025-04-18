﻿using System;
using System.IO;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает главную форму приложения
	/// </summary>
	public partial class ViceCityToolsetForm: Form
		{
		// Переменные
		private StartupModes mode;

		/// <summary>
		/// Конструктор. Запускает главную форму
		/// </summary>
		/// <param name="Mode">Режим запуска приложения</param>
		public ViceCityToolsetForm (StartupModes Mode)
			{
			// Инициализация
			InitializeComponent ();
			this.Text = ProgramDescription.AssemblyTitle;
			RDGenerics.LoadWindowDimensions (this);
			mode = Mode;

			LanguageCombo.Items.AddRange (RDLocale.LanguagesNames);
			try
				{
				LanguageCombo.SelectedIndex = (int)RDLocale.CurrentLanguage;
				}
			catch
				{
				LanguageCombo.SelectedIndex = 0;
				}
			}

		private void ViceCityToolsetForm_Load (object sender, EventArgs e)
			{
			// Выбор режима запуска
			switch (mode)
				{
				case StartupModes.CollisionConversion:
					CollisionButton_Click (null, null);
					break;

				case StartupModes.Handling:
					HandlingButton_Click (null, null);
					break;

				case StartupModes.Saves:
					SavesButton_Click (null, null);
					break;

				default:
					return;
				}

			this.Close ();
			}

		// Локализация формы
		private void LanguageCombo_SelectedIndexChanged (object sender, EventArgs e)
			{
			// Сохранение языка
			RDLocale.CurrentLanguage = (RDLanguages)LanguageCombo.SelectedIndex;

			// Локализация
			RDLocale.SetControlsText (this);
			ExitButton.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Exit);
			FBDialog.Description = RDLocale.GetText ("ViceCityToolsetForm_FBDialog");
			AboutTheAppButton.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Control_AppAbout);
			RegisterAssociations.Text = RDLocale.GetText ("RegisterAssociations");
			}

		// Закрытие окна
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void ViceCityToolsetForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			RDGenerics.SaveWindowDimensions (this);
			}

		// Вызов функций
		private void SavesButton_Click (object sender, EventArgs e)
			{
			if (!CheckDirectories ())
				return;

			BExplorerForm bef = new BExplorerForm ();
			bef.Dispose ();
			}

		private void HandlingButton_Click (object sender, EventArgs e)
			{
			if (!CheckDirectories ())
				return;

			HandlingForm hf = new HandlingForm ();
			hf.Dispose ();
			}

		private void CollisionButton_Click (object sender, EventArgs e)
			{
			MakeCSTForm mcstf = new MakeCSTForm ();
			mcstf.Dispose ();
			}

		// Обнаружение директорий GTA Vice city
		private bool CheckDirectories ()
			{
			// Контроль наличия директории с установленной GTA Vice city
			if (string.IsNullOrWhiteSpace (ViceCityToolsetProgram.GTAVCDirectory))
				{
				if (FBDialog.ShowDialog () != DialogResult.OK)
					return false;

				ViceCityToolsetProgram.GTAVCDirectory = FBDialog.SelectedPath;
				}

			if (!Directory.Exists (ViceCityToolsetProgram.GTAVCDirectory))
				return false;

			// Контроль наличия директории с сохранениями
			if (!Directory.Exists (ViceCityToolsetProgram.GTAVCSavesDirectory))
				return false;

			return true;
			}

		// Отображение сведений о программе
		private void AppAboutButton (object sender, EventArgs e)
			{
			RDInterface.ShowAbout (false);
			}

		// Регистрация сопоставлений файлов
		private void RegisterAssociations_Click (object sender, EventArgs e)
			{
			RDGenerics.RegisterFileAssociations (false);
			}
		}
	}
