using System;
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
			if (!RDLocale.IsXPUNClassAcceptable)
				return;

			// Проверка запуска единственной копии
			if (!RDGenerics.IsAppInstanceUnique (true))
				return;

			// Проверка наличия компонентов программы
			if (!RDGenerics.CheckLibraries (ProgramDescription.AssemblyLibName, true))
				return;

			// Отображение справки и запроса на принятие Политики
			if (!RDGenerics.AcceptEULA ())
				return;
			/*if (!RDGenerics.ShowAbout (true))
				ProgramDescription.RegisterAppExtensions ();*/
			RDGenerics.ShowAbout (true);

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
				return RDGenerics.GetSettings (gtavcDirectoryPar, "");
				}
			set
				{
				RDGenerics.SetSettings (gtavcDirectoryPar, value);
				}
			}
		private const string gtavcDirectoryPar = "GTAVCDirectory";

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
