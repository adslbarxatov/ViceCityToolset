using System;
using System.IO;
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
	/// Результаты работы интерпретаора команд
	/// </summary>
	public enum ResultCodes
		{
		/// <summary>
		/// Корректировка выполнена успешно
		/// </summary>
		FileFixed = 0,

		/// <summary>
		/// Общий положительный результат выполнения
		/// </summary>
		OK = 0,

		/// <summary>
		/// Файл успешно загружен
		/// </summary>
		LoadSuccess = 1,

		/// <summary>
		/// Файл успешно сохранён
		/// </summary>
		SaveSuccess = 2,

		/// <summary>
		/// Указанное значение находится вне допустимого диапазона
		/// </summary>
		ValueOutOfRange = -1002,

		/// <summary>
		/// Недопустимый код режима
		/// </summary>
		ModeIsIncorrect = -1003,

		/// <summary>
		/// Недопустимый код операции для данного режима
		/// </summary>
		OpCodeIsIncorrect = -1004,

		/// <summary>
		/// Недопустимый код параметра для данных режима и операции
		/// </summary>
		ParCodeIsIncorrect = -1005,

		/// <summary>
		/// Указанный файл статистики недоступен
		/// </summary>
		StatsFileNotFound = -1101,

		/// <summary>
		/// Указанный файл повреждён или не является файлом статистики
		/// </summary>
		StatsFileIsIncorrect = -1102,

		/// <summary>
		/// Не удаётся записать файл статистики
		/// </summary>
		CannotCreateStatsFile = -1103,

		/// <summary>
		/// Указанный файл параметров парковок недоступен
		/// </summary>
		CGFileNotFound = -1104,

		/// <summary>
		/// Указанный файл повреждён или не является файлом параметров парковок
		/// </summary>
		CGFileIsIncorrect = -1105,

		/// <summary>
		/// Не удаётся записать файл параметров парковок
		/// </summary>
		CannotCreateCGFile = -1106,

		/// <summary>
		/// Указанный файл параметров гаражей недоступен
		/// </summary>
		GaragesFileNotFound = -1107,

		/// <summary>
		/// Указанный файл повреждён или не является файлом параметров гаражей
		/// </summary>
		GaragesFileIsIncorrect = -1108,

		/// <summary>
		/// Не удаётся записать файл параметров гаражей
		/// </summary>
		CannotCreateGaragesFile = -1109,

		/// <summary>
		/// Не удаётся создать указанный файл. Возможно, выбранное расположение недоступно для записи
		/// </summary>
		CannotCreateFile = -2001,

		/// <summary>
		/// Файл сохранения не был загружен (структура пуста)
		/// </summary>
		FileNotLoaded = -1001,

		/// <summary>
		/// Указанный файл отсутствует или недоступен
		/// </summary>
		FileNotFound = -1,

		/// <summary>
		/// Не удаётся выделить память для хранения данных программы
		/// </summary>
		MemoryAllocationFailure = -2,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// общих параметров сохранения
		/// </summary>
		ErrorLoadDP = -101,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока общих параметров массива переменных
		/// </summary>
		ErrorLoadSB = -102,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// массива переменных
		/// </summary>
		ErrorLoadSBA = -103,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// продолжения блока переменных
		/// </summary>
		ErrorLoadSBB = -104,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// общих параметров массива скриптов
		/// </summary>
		ErrorLoadSC = -105,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// массива скриптов
		/// </summary>
		ErrorLoadSS = -106,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// общих параметров массива игроков
		/// </summary>
		ErrorLoadPPL = -107,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// массива игроков
		/// </summary>
		ErrorLoadPPS = -135,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока описателей гаражей
		/// </summary>
		ErrorLoadGR = -108,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// общих параметров массива транспортных средств
		/// </summary>
		ErrorLoadVS = -110,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// массива транспортных средств
		/// </summary>
		ErrorLoadVH = -136,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока стримминга
		/// </summary>
		ErrorLoadSR = -132,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока описателей точек создания такси
		/// </summary>
		ErrorLoadTS = -109,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// общих параметров массива трасс
		/// </summary>
		ErrorLoadPH = -113,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// массива трасс
		/// </summary>
		ErrorLoadPHD = -114,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// общих параметров массива скрипт-контролируемых объектов
		/// </summary>
		ErrorLoadOP = -111,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// массива скрипт-контролируемых объектов
		/// </summary>
		ErrorLoadOS = -112,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока портовых кранов
		/// </summary>
		ErrorLoadCR = -115,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока собираемых объектов
		/// </summary>
		ErrorLoadPU = -116,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока телефонов-автоматов
		/// </summary>
		ErrorLoadPI = -117,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока зон респауна
		/// </summary>
		ErrorLoadRL = -118,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока радарных указателей
		/// </summary>
		ErrorLoadRB = -119,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока зон карты
		/// </summary>
		ErrorLoadZB = -120,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока банд
		/// </summary>
		ErrorLoadGD = -121,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока парковок
		/// </summary>
		ErrorLoadCG = -122,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// общих параметров массива эффектов анимации
		/// </summary>
		ErrorLoadPR = -123,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// массива эффектов анимации
		/// </summary>
		ErrorLoadPRD = -124,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// общих параметров массива аудиоскриптов
		/// </summary>
		ErrorLoadAU = -125,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// массива аудиоскриптов
		/// </summary>
		ErrorLoadAS = -126,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока параметров пути перемещения спецобъектов
		/// </summary>
		ErrorLoadSP = -127,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока информации о текущем игроке
		/// </summary>
		ErrorLoadPL = -129,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока статистики игры
		/// </summary>
		ErrorLoadST = -130,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока триггер-зон
		/// </summary>
		ErrorLoadTZ = -131,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока типов персонажей
		/// </summary>
		ErrorLoadPT = -133,

		/// <summary>
		/// Неопознанная ошибка в процессе обработки файла сохранения
		/// блока контрольной суммы или её сравнении
		/// </summary>
		ErrorLoadCS = -134,
		}

	/// <summary>
	/// Класс обеспечивает доступ к методам библиотеки BExplorerLib
	/// </summary>
	public static class BExplorerLib
		{
		#region Импортированные функции

		/// <summary>
		/// Метод получает последнее текстовое сообщение от интерпретатора
		/// </summary>
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern IntPtr SaveData_GetLastMessageEx ();

		/// <summary>
		/// Возвращает последнее сообщение от интерпретатора
		/// </summary>
		public static string SaveData_LastMessage
			{
			get
				{
				return Marshal.PtrToStringAnsi (SaveData_GetLastMessageEx ());
				}
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
		/// Метод выполняет загрузку файла сохранения из указанного расположения. Начинает
		/// сеанс взаимодействия со структурой данных сохранения
		/// </summary>
		/// <param name="FilePath">Путь к загружаемому файлу</param>
		/// <returns>Возвращает код ошибки или 0 в случае успеха</returns>
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern Int16 SaveData_LoadEx (string FilePath);

		/// <summary>
		/// Метод выполняет загрузку файла сохранения из указанного расположения и начинает
		/// сеанс взаимодействия со структурой данных сохранения
		/// </summary>
		/// <param name="FilePath">Путь к загружаемому файлу</param>
		/// <returns>Возвращает результат выполнения операции</returns>
		public static ResultCodes SaveData_Load (string FilePath)
			{
			return (ResultCodes)SaveData_LoadEx (FilePath);
			}

		/// <summary>
		/// Командный интерпретатор. Возвращает сообщение с результатом выполнения команды или сообщение об ошибке
		/// </summary>
		/// <param name="Mode">Режим интерпретации</param>
		/// <param name="OpCode">Код операции</param>
		/// <param name="ParCode">Код параметра</param>
		/// <param name="Value">Новое значение параметра</param>
		/// <returns>Возвращает код результата выполнения команды</returns>
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern Int16 SaveData_CommandInterpreterEx (UInt16 Mode, UInt16 OpCode, UInt16 ParCode,
			string Value);

		private static ResultCodes SaveData_CommandInterpreter (uint Mode, uint OpCode, uint ParCode,
			string Value)
			{
			return (ResultCodes)SaveData_CommandInterpreterEx ((UInt16)Mode, (UInt16)OpCode, (UInt16)ParCode, Value);
			}

		#endregion

		#region Оболочка

		/// <summary>
		/// Функция применяет рекомендуемые исправления в файле сохранения
		/// </summary>
		/// <returns>Возвращает сообщение о результате выполнения</returns>
		public static ResultCodes SaveData_FixFile4 ()
			{
			ResultCodes res = SaveData_CommandInterpreter (5, 0, 0, "0");
			if (res != ResultCodes.FileFixed)
				return res;

			res = SaveData_CommandInterpreter (5, 1, 0, "0");
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
		/// <returns>Возвращает результат выполнения команды или код ошибки с префиксом \x13</returns>
		public static string SaveData_GetParameterValue (OpCodes OpCode, uint ParCode)
			{
			ResultCodes res = SaveData_CommandInterpreter (0, (UInt16)OpCode, (UInt16)ParCode, "0");
			if (res != ResultCodes.OK)
				return "\x13" + ((int)res).ToString ();

			return SaveData_LastMessage;
			}

		/// <summary>
		/// Функция устанавливает значение указанного параметра
		/// </summary>
		/// <param name="OpCode">Код параметра</param>
		/// <param name="ParCode">Код субпараметра</param>
		/// <param name="NewValue">Новое значение параметра</param>
		/// <returns>Возвращает сообщение с результатом выполнения команды или сообщение об ошибке</returns>
		public static ResultCodes SaveData_SetParameterValue (OpCodes OpCode, uint ParCode, string NewValue)
			{
			// Код ошибки "значение вне диапазона"
			if (string.IsNullOrWhiteSpace (NewValue))
				return ResultCodes.ValueOutOfRange;

			return SaveData_CommandInterpreter (1, (UInt16)OpCode, (UInt16)ParCode, NewValue.Replace (',', '.'));
			}

		/// <summary>
		/// Функция получает допустимый диапазон значений указанного параметра
		/// </summary>
		/// <param name="OpCode">Код параметра</param>
		/// <param name="ParCode">Код субпараметра</param>
		/// <param name="Max">Указывает, что следует вернуть максимум вместо минимума</param>
		/// <returns>Возвращает запрошенное значение</returns>
		public static float SaveData_GetParameterLimit (OpCodes OpCode, UInt16 ParCode, bool Max)
			{
			// Извлечение значений границ
			ResultCodes res = SaveData_CommandInterpreter (4, (UInt16)OpCode, ParCode, "");
			if (res != ResultCodes.OK)
				return 0.0f;

			string[] values = SaveData_LastMessage.Split (splitters, StringSplitOptions.RemoveEmptyEntries);
			if (values.Length != 2)
				return 0.0f;

			float min = 0.0f, max = 0.0f;
			try
				{
				min = float.Parse (values[0]);
				max = float.Parse (values[1]);
				}
			catch { }

			// Возврат
			if (Max)
				return max;
			else
				return min;
			}
		private static char[] splitters = [';'];

		/// <summary>
		/// Функция загружает указанный файл параметров в файл сохранения
		/// </summary>
		/// <param name="ParametersType">Тип файла параметров</param>
		/// <param name="FileName">Имя файла параметров</param>
		/// <returns>Возвращает сообщение с результатом выполнения команды или сообщение об ошибке</returns>
		public static ResultCodes SaveData_LoadParametersFile (LoadableParameters ParametersType, string FileName)
			{
			if (string.IsNullOrWhiteSpace (FileName))
				SaveData_CommandInterpreter (2, (UInt16)ParametersType, 0, "<");

			return SaveData_CommandInterpreter (2, (UInt16)ParametersType, 0, FileName);
			}

		/// <summary>
		/// Функция сохраняет указанный файл параметров в файл сохранения или сам файл сохранения
		/// </summary>
		/// <param name="ParametersType">Тип файла параметров</param>
		/// <param name="FileName">Имя файла параметров</param>
		/// <returns>Возвращает сообщение с результатом выполнения команды или сообщение об ошибке</returns>
		public static ResultCodes SaveData_SaveParametersFile (SaveableParameters ParametersType, string FileName)
			{
			if (string.IsNullOrWhiteSpace (FileName))
				return SaveData_CommandInterpreter (3, (UInt16)ParametersType, 0, "<");

			return SaveData_CommandInterpreter (3, (UInt16)ParametersType, 0, FileName);
			}

		/// <summary>
		/// Метод выполняет сведение статистики с комментариями в конечном текстовом файле
		/// </summary>
		/// <param name="FileName">Имя исходного файла статистики</param>
		public static ResultCodes SaveData_MergeStats (string FileName)
			{
			// Формирование путей
			string tempFile = FileName + "tmp";
			string textFile = FileName + ".txt";

			// Попытка инициализации
			/*FileStream FI = null;*/
			FileStream FI;
			try
				{
				FI = new FileStream (tempFile, FileMode.Open);
				}
			catch
				{
				return ResultCodes.CannotCreateStatsFile;
				}
			StreamReader SR = new StreamReader (FI, RDGenerics.GetEncoding (RDEncodings.UTF8));

			/*FileStream FO = null;*/
			FileStream FO;
			try
				{
				FO = new FileStream (textFile, FileMode.Create);
				}
			catch
				{
				SR.Close ();
				FI.Close ();

				return ResultCodes.CannotCreateStatsFile;
				}
			StreamWriter SW = new StreamWriter (FO, RDGenerics.GetEncoding (RDEncodings.UTF8));

			// Сборка комментариев
			string comments;
			if (RDLocale.IsCurrentLanguageRuRu)
				comments = RDGenerics.GetEncoding (RDEncodings.UTF8).GetString (ViceCityToolsetResources.StatsText_ru_ru);
			else
				comments = RDGenerics.GetEncoding (RDEncodings.UTF8).GetString (ViceCityToolsetResources.StatsText_en_us);
			StringReader LR = new StringReader (comments);

			// Склеивание
			while (!SR.EndOfStream)
				{
				string s = LR.ReadLine () + SR.ReadLine ();
				SW.WriteLine (s);
				}

			// Завершено
			SR.Close ();
			FI.Close ();
			SW.Close ();
			FO.Close ();
			LR.Close ();

			// Удаление вспомогательных файлов
			try
				{
				File.Delete (tempFile);
				}
			catch { }

			// Успешно
			return ResultCodes.SaveSuccess;
			}

		/*/// <summary>
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
			}*/

		#endregion
		}
	}
