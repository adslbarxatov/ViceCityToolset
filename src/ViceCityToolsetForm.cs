using System;
using System.Diagnostics;
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
		private CarColors cc;

		/// <summary>
		/// Конструктор. Запускает главную форму
		/// </summary>
		/// <param name="Mode">Режим запуска приложения</param>
		public ViceCityToolsetForm (StartupModes Mode)
			{
			// Инициализация
			InitializeComponent ();
			this.Text = RDGenerics.DefaultAssemblyVisibleName;
			RDGenerics.LoadWindowDimensions (this);
			mode = Mode;

			LocalizeForm_Click (null, null);
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

				case StartupModes.Weather:
					WeatherButton_Click (null, null);
					break;

				case StartupModes.Archive:
					ArchiveButton_Click (null, null);
					break;

				case StartupModes.Colors:
					ColorsButton_Click (null, null);
					break;

				default:
					return;
				}

			this.Close ();
			}

		// Локализация формы
		private void LocalizeForm_Click (object sender, EventArgs e)
			{
			// Выбор языка
			if ((sender != null) && !RDInterface.MessageBox ())
				return;

			// Локализация
			RDLocale.SetControlText (this.Name, ArchiveButton);
			RDLocale.SetControlText (this.Name, CollisionButton);
			RDLocale.SetControlText (this.Name, HandlingButton);
			RDLocale.SetControlText (this.Name, RunGTAVC);
			RDLocale.SetControlText (this.Name, SavesButton);
			RDLocale.SetControlText (this.Name, WeatherButton);
			RDLocale.SetDefaultControlText (ExitButton, RDLDefaultTexts.Button_Exit);
			/*FBDialog. Description = RDLocale.GetText ("ViceCityToolsetForm_FBDialog");*/
			RDLocale.SetControlText (this.Name, ColorsButton);

			RDLocale.SetDefaultControlText (AboutTheAppButton, RDLDefaultTexts.Control_AppAbout);
			RDLocale.SetControlText (RegisterAssociations);
			RDLocale.SetDefaultControlText (BLanguage, RDLDefaultTexts.Control_InterfaceLanguage);
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

			_ = new BExplorerForm (cc);
			}

		private void HandlingButton_Click (object sender, EventArgs e)
			{
			if (!CheckDirectories ())
				return;

			_ = new HandlingForm ();
			}

		private void CollisionButton_Click (object sender, EventArgs e)
			{
			_ = new MakeCSTForm ();
			}

		private void WeatherButton_Click (object sender, EventArgs e)
			{
			if (!CheckDirectories ())
				return;

			_ = new WeatherForm ();
			}

		private void ArchiveButton_Click (object sender, EventArgs e)
			{
			if (!CheckDirectories ())
				return;

			_ = new ArchiveForm ();
			}

		private void RunGTAVCButton_Click (object sender, EventArgs e)
			{
			if (!CheckDirectories ())
				return;

			try
				{
				ProcessStartInfo psi = new ProcessStartInfo (ViceCityToolsetProgram.GTAVCDirectory + "\\GTA-VC.exe");
				psi.UseShellExecute = true;
				psi.Verb = "open";
				psi.WorkingDirectory = ViceCityToolsetProgram.GTAVCDirectory;	// Почему-то критично

				Process.Start (psi);
				}
			catch { }
			}

		private void ColorsButton_Click (object sender, EventArgs e)
			{
			if (!CheckDirectories ())
				return;

			_ = new ColorsForm (cc);
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

			// Проверка наличия файла цветовой схемы
			if (cc == null)
				{
				int error;
				cc = new CarColors (out error);
				if (error != 0)
					{
					this.Close ();
					return false;
					}
				}

			// Успешно
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
