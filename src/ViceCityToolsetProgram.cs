using System;
using System.IO;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает точку входа приложения
	/// </summary>
	public static class ViceCityToolsetProgram
		{
		/// <summary>
		/// Главная точка входа для приложения
		/// </summary>
		[STAThread]
		public static void Main (string[] args)
			{
			// Инициализация
			Application.EnableVisualStyles ();
			Application.SetCompatibleTextRenderingDefault (false);

			// Язык интерфейса и контроль XPUN
			if (!Localization.IsXPUNClassAcceptable)
				return;

			// Проверка запуска единственной копии
			if (!RDGenerics.IsAppInstanceUnique (true))
				return;

			// Проверка наличия компонентов программы
			if (!File.Exists (RDGenerics.AppStartupPath + ProgramDescription.AssemblyLibName))
				{
				if (RDGenerics.MessageBox (RDMessageTypes.Question_Left,
					string.Format (Localization.GetText ("ComponentMissing"), ProgramDescription.AssemblyLibName),
					Localization.GetDefaultText (LzDefaultTextValues.Button_Yes),
					Localization.GetDefaultText (LzDefaultTextValues.Button_No)) ==
					RDMessageButtons.ButtonOne)
					{
					AboutForm af = new AboutForm (null);
					}

				return;
				}

			// Отображение справки и запроса на принятие Политики
			if (!RDGenerics.AcceptEULA ())
				return;
			if (!RDGenerics.ShowAbout (true))
				ProgramDescription.RegisterAppExtensions ();

			// Запуск
			StartupModes mode = StartupModes.None;
			if (args.Length > 0)
				{
				string s = args[0].ToLower ();
				switch (s)
					{
					case "-b":
						mode = StartupModes.Saves;
						break;

					case "-h":
						mode = StartupModes.Handling;
						break;

					case "-c":
						mode = StartupModes.CollisionConversion;
						break;
					}
				}

			Application.Run (new ViceCityToolsetForm (mode));
			}

		/// <summary>
		/// Возвращает или задаёт путь к директории с установленной GTA Vice city
		/// </summary>
		public static string GTAVCDirectory
			{
			get
				{
				if (string.IsNullOrWhiteSpace (gtavcDirectory))
					gtavcDirectory = RDGenerics.GetAppSettingsValue ("GTAVCDirectory");

				return gtavcDirectory;
				}
			set
				{
				gtavcDirectory = value;
				RDGenerics.SetAppSettingsValue ("GTAVCDirectory", gtavcDirectory);
				}
			}
		private static string gtavcDirectory = "";

		/// <summary>
		/// Возвращает путь к файлам сохранений
		/// </summary>
		public static string GTAVCSavesDirectory
			{
			get
				{
				return gtavcSavesDirectory;
				}
			}
		private static string gtavcSavesDirectory = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) +
			"\\GTA Vice City User Files";
		}

	/// <summary>
	/// Режимы запуска приложения
	/// </summary>
	public enum StartupModes
		{
		/// <summary>
		/// Обычный режим (ручной выбор)
		/// </summary>
		None,

		/// <summary>
		/// Файлы сохранений
		/// </summary>
		Saves,

		/// <summary>
		/// Файл конфигурации транспортных средств
		/// </summary>
		Handling,

		/// <summary>
		/// Преобразователь скриптов коллизий
		/// </summary>
		CollisionConversion
		}
	}
