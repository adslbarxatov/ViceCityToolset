using System;
using System.Runtime.InteropServices;

namespace RD_AAOW
	{
	/// <summary>
	/// Возможные параметры оружия
	/// </summary>
	public enum WeaponsParCodes
		{
		/// <summary>
		/// Тип оружия
		/// </summary>
		WeaponType = 0,

		/// <summary>
		/// Количество патронов
		/// </summary>
		WeaponAmmo = 1
		}

	/// <summary>
	/// Возможные параметры гаража
	/// </summary>
	public enum GaragesParCodes
		{
		/// <summary>
		/// Модель авто
		/// </summary>
		CarModel = 0,

		/// <summary>
		/// Флаги защиты
		/// </summary>
		Immunity = 1,

		/// <summary>
		/// Первый цвет
		/// </summary>
		PrimaryColor = 2,

		/// <summary>
		/// Второй цвет
		/// </summary>
		SecondaryColor = 3,

		/// <summary>
		/// Радиостанция
		/// </summary>
		RadioStation = 4,

		/// <summary>
		/// Тип минирования
		/// </summary>
		BombType = 5
		}

	/// <summary>
	/// Возможные параметры банд
	/// </summary>
	public enum GangsParCodes
		{
		/// <summary>
		/// Модел авто
		/// </summary>
		CarModel = 0,

		/// <summary>
		/// Первый скин
		/// </summary>
		PrimaryPedModel = 1,

		/// <summary>
		/// Второй скин
		/// </summary>
		SecondaryPedModel = 2,

		/// <summary>
		/// Первое оружие
		/// </summary>
		PrimaryWeapon = 3,

		/// <summary>
		/// Второе оружие
		/// </summary>
		SecondaryWeapon = 4
		}

	/// <summary>
	/// Возможные параметры собираемых объектов
	/// </summary>
	public enum PickupsParCodes
		{
		/// <summary>
		/// Модель объекта
		/// </summary>
		ObjectModel = 0,

		/// <summary>
		/// Координата X
		/// </summary>
		ObjectX = 1,

		/// <summary>
		/// Координата Y
		/// </summary>
		ObjectY = 2,

		/// <summary>
		/// Координата Z
		/// </summary>
		ObjectZ = 3,

		/// <summary>
		/// Тип объекта
		/// </summary>
		ObjectType = 4,

		/// <summary>
		/// Накопитель объекта
		/// </summary>
		ObjectAsset = 5,        // Устанавливать можно только это значение

		/// <summary>
		/// Флаг состояния "объект собран"
		/// </summary>
		HasBeenPickedUp = 6
		}

	/// <summary>
	/// Возможные параметры парковок
	/// </summary>
	public enum GeneratorsParCodes
		{
		/// <summary>
		/// Модель авто
		/// </summary>
		CarModel = 0,

		/// <summary>
		/// Координата X
		/// </summary>
		CarX = 1,

		/// <summary>
		/// Координата Y
		/// </summary>
		CarY = 2,

		/// <summary>
		/// Координата Z
		/// </summary>
		CarZ = 3,

		/// <summary>
		/// Угол поворота
		/// </summary>
		CarAngle = 4,

		/// <summary>
		/// Разрешение генерации авто
		/// </summary>
		AllowSpawn = 5,

		/// <summary>
		/// Первый цвет
		/// </summary>
		PrimaryColor = 6,

		/// <summary>
		/// Второй цвет
		/// </summary>
		SecondaryColor = 7,

		/// <summary>
		/// Вероятность срабатывания сигнализации
		/// </summary>
		AlarmProbability = 8,

		/// <summary>
		/// Вероятность блокировки
		/// </summary>
		LockProbability = 9,

		/// <summary>
		/// Флаг обязательной генерации
		/// </summary>
		ForceSpawn = 10
		}

	/// <summary>
	/// Коды операций для следующих трёх функций
	/// </summary>
	public enum OpCodes
		{
		/// <summary>
		/// Год
		/// </summary>
		SaveYear = 0,

		/// <summary>
		/// Месяц
		/// </summary>
		SaveMonth = 1,

		/// <summary>
		/// День
		/// </summary>
		SaveDay = 2,

		/// <summary>
		/// Час
		/// </summary>
		SaveHour = 3,

		/// <summary>
		/// Минута
		/// </summary>
		SaveMinute = 4,

		/// <summary>
		/// Секунда
		/// </summary>
		SaveSecond = 5,

		/// <summary>
		/// Длина минуты в игре
		/// </summary>
		InGameMinuteLength = 6,

		/// <summary>
		/// Час внутри игры
		/// </summary>
		InGameHour = 7,

		/// <summary>
		/// Минута внутри игры
		/// </summary>
		InGameMinute = 8,

		/// <summary>
		/// Скорость времени в игре
		/// </summary>
		GameSpeed = 9,

		/// <summary>
		/// Текущая погода
		/// </summary>
		CurrentWeather = 10,

		/// <summary>
		/// Позиция камеры наблюдения
		/// </summary>
		CarOverview = 11,

		/// <summary>
		/// Состояние радиоволны таксистов
		/// </summary>
		CabsRadio = 100,

		/// <summary>
		/// Базовый код для настроек оружия.
		/// Этот и следующие (количество определено далее) коды отвечают за оружие
		/// </summary>
		PlayerWeapons_Base = 200,

		/// <summary>
		/// Текущая броня
		/// </summary>
		CurrentArmor = 210,

		/// <summary>
		/// Интерес полиции
		/// </summary>
		MaxPoliceStars = 211,

		/// <summary>
		/// Костюм игрока
		/// </summary>
		PlayerSuit = 212,

		/// <summary>
		/// Базовый код для настроек гаражей
		/// </summary>
		Garages_Base = 300,

		/// <summary>
		/// Базовый код для настроек банд
		/// </summary>
		Gangs_Base = 400,

		/// <summary>
		/// Текущие наличные
		/// </summary>
		CurrentMoney = 500,

		/// <summary>
		/// Флаг бесконечного бега
		/// </summary>
		InfiniteRun = 501,

		/// <summary>
		/// Флаг быстрой перезарядки оружия
		/// </summary>
		FastReload = 502,

		/// <summary>
		/// Флаг несгораемости
		/// </summary>
		Fireproof = 503,

		/// <summary>
		/// Максимальное здоровье
		/// </summary>
		MaxHealth = 504,

		/// <summary>
		/// Максимальная броня
		/// </summary>
		MaxArmor = 505,

		/// <summary>
		/// Флаг бесконечных патронов
		/// </summary>
		InfiniteAmmo = 506,

		/// <summary>
		/// Базовый код для собираемых объектов
		/// </summary>
		Pickups_Base = 1000,

		/// <summary>
		/// Количество доступных парковок
		/// </summary>
		ActiveGenerators = 2000,

		/// <summary>
		/// Базовый код для настроек парковок
		/// </summary>
		Generators_Base = 2001
		}

	/// <summary>
	/// Варианты загружаемых файлов
	/// </summary>
	public enum LoadableParameters
		{
		/// <summary>
		/// Параметры гаражей
		/// </summary>
		Garages = 1,

		/// <summary>
		/// Статистика
		/// </summary>
		Stats = 2,

		/// <summary>
		/// Параметры парковок
		/// </summary>
		Generators = 3
		}

	/// <summary>
	/// Варианты загружаемых файлов
	/// </summary>
	public enum SaveableParameters
		{
		/// <summary>
		/// Файл сохранения
		/// </summary>
		SaveFile = 0,

		/// <summary>
		/// Параметры гаражей
		/// </summary>
		Garages = 1,

		/// <summary>
		/// Статистика
		/// </summary>
		Stats = 2,

		/// <summary>
		/// Параметры парковок
		/// </summary>
		Generators = 3
		}

	/// <summary>
	/// Класс обеспечивает доступ к методам библиотеки BExplorerLib
	/// </summary>
	public static class BExplorerLib
		{
		#region Импортированные функции

		/// <summary>
		/// Метод формирует сообщение ошибки по её коду
		/// </summary>
		/// <param name="ErrorCode">Код ошибки</param>
		/// <returns>Возвращает сообщение об ошибке</returns>
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern IntPtr SaveData_ErrorPromptEx (Int16 ErrorCode);

		/// <summary>
		/// Метод формирует сообщение ошибки по её коду
		/// </summary>
		/// <param name="ErrorCode">Код ошибки</param>
		/// <returns>Возвращает сообщение об ошибке</returns>
		public static string SaveData_ErrorPrompt (Int16 ErrorCode)
			{
			return Marshal.PtrToStringAnsi (SaveData_ErrorPromptEx ((Int16)ErrorCode));
			}

		/// <summary>
		/// Метод получает краткую информацию о файле сохранения
		/// </summary>
		/// <returns>Возвращает строку информации</returns>
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern IntPtr SaveData_GetSaveInfoEx ();

		/// <summary>
		/// Метод получает краткую информацию о файле сохранения
		/// </summary>
		/// <returns>Возвращает строку информации</returns>
		public static string SaveData_SaveInfo
			{
			get
				{
				return Marshal.PtrToStringAnsi (SaveData_GetSaveInfoEx ());
				}
			}

		/// <summary>
		/// Метод получает ToDo-статус сохранения
		/// </summary>
		/// <returns>Возвращает строку статуса</returns>
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern IntPtr SaveData_GetToDoStatusEx ();

		/// <summary>
		/// Метод получает ToDo-статус сохранения
		/// </summary>
		/// <returns>Возвращает строку статуса</returns>
		public static string SaveData_ToDoStatus
			{
			get
				{
				return Marshal.PtrToStringAnsi (SaveData_GetToDoStatusEx ());
				}
			}

		/// <summary>
		/// Метод возвращает версию библиотеки. Позволяет избегать рассогласования
		/// протоколов взаимодействия библиотеки и приложения
		/// </summary>
		/// <returns>Версия библиотеки в строковом представлении</returns>
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern IntPtr SaveData_GetLibVersionEx ();

		/// <summary>
		/// Метод выполняет загрузку файла сохранения из указанного расположения. Начинает
		/// сеанс взаимодействия со структурой данных сохранения
		/// </summary>
		/// <param name="FilePath">Путь к загружаемому файлу</param>
		/// <returns>Возвращает код ошибки или 0 в случае успеха</returns>
		[DllImport (ProgramDescription.AssemblyLibName)]
		public static extern Int16 SaveData_LoadEx (string FilePath);

		/// <summary>
		/// Командный интерпретатор. Возвращает сообщение с результатом выполнения команды или сообщение об ошибке
		/// </summary>
		/// <param name="Mode">Режим интерпретации</param>
		/// <param name="OpCode">Код операции</param>
		/// <param name="ParCode">Код параметра</param>
		/// <param name="Value">Новое значение параметра</param>
		/// <returns>Возвращает сообщение с результатом выполнения команды или сообщение об ошибке</returns>
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern IntPtr SaveData_CommandInterpreterEx (UInt16 Mode, UInt16 OpCode, UInt16 ParCode,
			string Value);

		#endregion

		#region Оболочка

		/// <summary>
		/// Функция применяет рекомендуемые исправления в файле сохранения
		/// </summary>
		/// <returns>Возвращает сообщение о результате выполнения</returns>
		public static string SaveData_FixFile ()
			{
			string res = Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (5, 0, 0, "0"));
			res += ("\r\n" + Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (5, 1, 0, "0")));
			return res;
			}

		/// <summary>
		/// Количество доступных слотов оружия
		/// </summary>
		public const uint WeaponsCount = 10;

		/// <summary>
		/// Количество доступных гаражей
		/// </summary>
		public const uint GaragesCount = 21;

		/// <summary>
		/// Количество доступных банд
		/// </summary>
		public const uint GangsCount = 8;

		/// <summary>
		/// Максимальное количество собираемых объектов
		/// </summary>
		public const uint PickupsCount = 336;

		/// <summary>
		/// Количество доступных парковок
		/// </summary>
		public const uint GeneratorsCount = 185;

		/// <summary>
		/// Функция получает значение указанного параметра
		/// </summary>
		/// <param name="OpCode">Код параметра</param>
		/// <param name="ParCode">Код субпараметра</param>
		/// <returns>Возвращает сообщение с результатом выполнения команды или сообщение об ошибке</returns>
		public static string SaveData_GetParameterValue (UInt16 OpCode, UInt16 ParCode)
			{
			return Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (0, OpCode, ParCode, "0"));
			}

		/// <summary>
		/// Функция устанавливает значение указанного параметра
		/// </summary>
		/// <param name="OpCode">Код параметра</param>
		/// <param name="ParCode">Код субпараметра</param>
		/// <param name="NewValue">Новое значение параметра</param>
		/// <returns>Возвращает сообщение с результатом выполнения команды или сообщение об ошибке</returns>
		public static string SaveData_SetParameterValue (UInt16 OpCode, UInt16 ParCode, string NewValue)
			{
			// Код ошибки "значение вне диапазона"
			if (NewValue == null)
				return Marshal.PtrToStringAnsi (SaveData_ErrorPromptEx (-1002));

			return Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (1, OpCode, ParCode,
				NewValue.Replace (',', '.')));
			}

		/// <summary>
		/// Функция получает допустимый диапазон значений указанного параметра
		/// </summary>
		/// <param name="OpCode">Код параметра</param>
		/// <param name="ParCode">Код субпараметра</param>
		/// <param name="Max">Указывает, что следует вернуть максимум вместо минимума</param>
		/// <returns>Возвращает запрошенное значение</returns>
		public static float SaveData_GetParameterLimit (UInt16 OpCode, UInt16 ParCode, bool Max)
			{
			// Извлечение значений границ
			string v = Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (4, OpCode, ParCode, ""));
			if (!IsResultSuccessful (v))
				return 0.0f;

			char[] splitters = new char[] { ';' };
			string[] values = v.Split (splitters, StringSplitOptions.RemoveEmptyEntries);
			if (values.Length != 2)
				return 0.0f;

			float min = 0.0f, max = 0.0f;
			try
				{
				min = float.Parse (values[0]);
				max = float.Parse (values[1]);
				}
			catch
				{
				}

			// Возврат
			if (Max)
				return max;
			else
				return min;
			}

		/// <summary>
		/// Функция загружает указанный файл параметров в файл сохранения
		/// </summary>
		/// <param name="ParametersType">Тип файла параметров</param>
		/// <param name="FileName">Имя файла параметров</param>
		/// <returns>Возвращает сообщение с результатом выполнения команды или сообщение об ошибке</returns>
		public static string SaveData_LoadParametersFile (LoadableParameters ParametersType, string FileName)
			{
			if (FileName == null)
				return Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (2, (UInt16)ParametersType, 0, "<"));

			return Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (2, (UInt16)ParametersType, 0, FileName));
			}

		/// <summary>
		/// Функция сохраняет указанный файл параметров в файл сохранения или сам файл сохранения
		/// </summary>
		/// <param name="ParametersType">Тип файла параметров</param>
		/// <param name="FileName">Имя файла параметров</param>
		/// <returns>Возвращает сообщение с результатом выполнения команды или сообщение об ошибке</returns>
		public static string SaveData_SaveParametersFile (SaveableParameters ParametersType, string FileName)
			{
			if (FileName == null)
				return Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (3, (UInt16)ParametersType, 0, "<"));

			return Marshal.PtrToStringAnsi (SaveData_CommandInterpreterEx (3, (UInt16)ParametersType, 0, FileName));
			}

		/// <summary>
		/// Метод выполняет проверку методов на доступность и корректность взаимодействия с программой
		/// </summary>
		/// <returns>Возвращает номер метода, в котором произошёл сбой, или 0 в случае успеха. 
		/// -1 означает несовпадение версий приложения и библиотеки</returns>
		public static int Check ()
			{
			try
				{
				if (Marshal.PtrToStringAnsi (SaveData_GetLibVersionEx ()) != ProgramDescription.AssemblyLibVersion)
					return -1;
				}
			catch
				{
				return 1;
				}

			return 0;
			}

		/// <summary>
		/// Метод проверяет успешность выполнения команды интерпретатором
		/// </summary>
		/// <param name="InterpreterReturnedResult">Ответ командного интерпретатора</param>
		/// <returns>Возвращает true, если команда выполнена успешно</returns>
		public static bool IsResultSuccessful (string InterpreterReturnedResult)
			{
			return (InterpreterReturnedResult.IndexOf ("\x13") == -1);
			}

		#endregion
		}
	}
