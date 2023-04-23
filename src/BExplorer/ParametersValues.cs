using System.Collections.Generic;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает отдельное значение параметра, объединяя его код и название
	/// </summary>
	public class ParameterValue
		{
		// Переменные
		private int valueID;
		private string valueName;

		/// <summary>
		/// Конструктор. Создаёт экземпляр значения параметра
		/// </summary>
		public ParameterValue (int ValueID, string ValueName)
			{
			valueID = ValueID;
			valueName = ValueName;
			}

		/// <summary>
		/// Возвращает код значения
		/// </summary>
		public int ID
			{
			get
				{
				return valueID;
				}
			}

		/// <summary>
		/// Возвращает название значения
		/// </summary>
		public string Name
			{
			get
				{
				return valueName;
				}
			}
		}

	/// <summary>
	/// Класс описывает отдельное значение параметра, объединяя его кодовое имя и название
	/// </summary>
	public class ParameterStringValue
		{
		// Переменные
		private string valueCodeName;
		private string valueName;

		/// <summary>
		/// Конструктор. Создаёт экземпляр значения параметра
		/// </summary>
		public ParameterStringValue (string ValueCodeName, string ValueName)
			{
			valueCodeName = ValueCodeName;
			valueName = ValueName;
			}

		/// <summary>
		/// Возвращает кодовое имя значения
		/// </summary>
		public string CodeName
			{
			get
				{
				return valueCodeName;
				}
			}

		/// <summary>
		/// Возвращает название значения
		/// </summary>
		public string Name
			{
			get
				{
				return valueName;
				}
			}
		}

	/// <summary>
	/// Класс предоставляет доступ к спискам значений параметров
	/// </summary>
	public static class ParametersValues
		{
		#region Виды погоды
		private static ParameterValue[] weathers = {
			new ParameterValue (4, "Sunny"),
			new ParameterValue (0, "Cloudy"),
			new ParameterValue (1, "Gray"),
			new ParameterValue (2, "Rainy"),
			new ParameterValue (5, "Windy"),
			new ParameterValue (3, "Foggy"),
			new ParameterValue (-1, "Random")};

		/// <summary>
		/// Возвращает список видов погоды
		/// </summary>
		public static ParameterValue[] Weathers
			{
			get
				{
				return weathers;
				}
			}
		#endregion

		#region Позиции камеры обзора в транспорте
		private static ParameterValue[] cameraPositions = {
			new ParameterValue (0, "Front"),
			new ParameterValue (1, "MinDistance"),
			new ParameterValue (2, "MidDistance"),
			new ParameterValue (3, "MaxDistance"),
			new ParameterValue (4, "Spectate"),
			new ParameterValue (5, "Auto")};

		/// <summary>
		/// Возвращает список возможных положений камеры обзора относительно
		/// транспортного средства
		/// </summary>
		public static ParameterValue[] CameraPositions
			{
			get
				{
				return cameraPositions;
				}
			}
		#endregion

		#region Интерьеры
		private static ParameterValue[] interiors = {
			new ParameterValue (4, "north mall point"),
			new ParameterValue (13, "printworks exterior"),
			new ParameterValue (18, "printworks interior"),
			new ParameterValue (11, "апартаменты 3C"),
			new ParameterValue (3, "банк"),
			new ParameterValue (2, "вилла Версетти"),
			new ParameterValue (12, "дом тётушки Полет"),
			new ParameterValue (6, "кабинет адвоката"),
			new ParameterValue (7, "кафе Robina"),
			new ParameterValue (17, "клуб Malibu"),
			new ParameterValue (11, "клуб байкеров"),
			new ParameterValue (8, "концертный зал"),
			new ParameterValue (1, "отель Ocean view"),
			new ParameterValue (14, "стадион (dirtring)"),
			new ParameterValue (15, "стадион (bloodring)"),
			new ParameterValue (16, "стадион (hotring)"),
			new ParameterValue (5, "стрипклуб"),
			new ParameterValue (9, "студия звукозаписи"),
			new ParameterValue (10, "тир"),
			new ParameterValue (0, "улица (основной)"),
			new ParameterValue (12, "штаб-квартира полиции")};

		/// <summary>
		/// Возвращает список интерьеров
		/// </summary>
		public static ParameterValue[] Interiors
			{
			get
				{
				return interiors;
				}
			}
		#endregion

		#region Костюмы
		private static ParameterStringValue[] suits = {
			new ParameterStringValue ("player", "Default"),
			new ParameterStringValue ("player4", "Golf"),
			new ParameterStringValue ("player7", "Job"),
			new ParameterStringValue ("player8", "Jeans"),
			new ParameterStringValue ("play11", "RedSport"),
			new ParameterStringValue ("player5", "Cuban"),
			new ParameterStringValue ("player2", "Soierie"),
			new ParameterStringValue ("player9", "MrVercetti"),
			new ParameterStringValue ("player6", "Police"),
			new ParameterStringValue ("player3", "Worker"),
			new ParameterStringValue ("igalscb", "Shrub"),
			new ParameterStringValue ("igjezz", "Jezz"),
			new ParameterStringValue ("igdick", "Dick"),
			new ParameterStringValue ("igmike", "Cam"),
			new ParameterStringValue ("igken", "Ken"),
			new ParameterStringValue ("igcandy", "Candy"),
			new ParameterStringValue ("igbuddy", "Lance"),
			new ParameterStringValue ("igmerc", "Mercedes"),
			new ParameterStringValue ("igcolon", "Coronel"),
			new ParameterStringValue ("igpercy", "Percy"),
			new ParameterStringValue ("igdiaz", "Diaz"),
			new ParameterStringValue ("igsonny", "Sonny"),
			new ParameterStringValue ("igphil", "Phil"),
			new ParameterStringValue ("ighlary", "Hilary"),
			new ParameterStringValue ("play12", "Frankie"),
			new ParameterStringValue ("play10", "BlackSport")};

		/// <summary>
		/// Возвращает список костюмов
		/// </summary>
		public static ParameterStringValue[] Suits
			{
			get
				{
				return suits;
				}
			}
		#endregion

		#region Виды оружия
		private static ParameterValue[] weapons = {
			new ParameterValue (26, "M4"),
			new ParameterValue (32, "M60"),
			new ParameterValue (24, "MAC"),
			new ParameterValue (25, "MP5"),
			new ParameterValue (18, "Python357"),
			new ParameterValue (20, "SPAS12"),
			new ParameterValue (22, "TEC9"),
			new ParameterValue (6, "Baseball"),
			new ParameterValue (11, "Chainsaw"),
			new ParameterValue (28, "Sniper"),
			new ParameterValue (12, "Grenade"),
			new ParameterValue (4, "NightStick"),
			new ParameterValue (10, "Katana"),
			new ParameterValue (3, "Golf"),
			new ParameterValue (17, "Colt45"),
			new ParameterValue (1, "BrassKnuckle"),
			new ParameterValue (27, "Ruger"),
			new ParameterValue (0, "Fist"),
			new ParameterValue (29, "LaserScope"),
			new ParameterValue (9, "Machete"),
			new ParameterValue (15, "Molotov"),
			new ParameterValue (7, "Hammer"),
			new ParameterValue (5, "Knife"),
			new ParameterValue (21, "Stubby"),
			new ParameterValue (31, "Flamethrower"),
			new ParameterValue (2, "ScrewDriver"),
			new ParameterValue (33, "Minigun"),
			new ParameterValue (30, "Rocket"),
			new ParameterValue (19, "Shotgun"),
			new ParameterValue (14, "TearGas"),
			new ParameterValue (8, "Cleaver"),
			new ParameterValue (23, "UZI"),
			new ParameterValue (13, "RadioGrenade"),
			new ParameterValue (36, "Camera")};

		/// <summary>
		/// Возвращает список видов оружия
		/// </summary>
		public static ParameterValue[] Weapons
			{
			get
				{
				return weapons;
				}
			}
		#endregion

		#region Виды транспорта
		private static ParameterValue[] cars = {
			new ParameterValue (175, "Admiral"),
			new ParameterValue (146, "Ambulance"),
			new ParameterValue (166, "Angel"),
			new ParameterValue (215, "Baggage"),
			new ParameterValue (159, "Banshee"),
			new ParameterValue (163, "Barracks"),
			new ParameterValue (229, "Benson"),
			new ParameterValue (154, "BF injection"),
			new ParameterValue (226, "Blista compact"),
			new ParameterValue (234, "Bloodring A"),
			new ParameterValue (235, "Bloodring B"),
			new ParameterValue (152, "Bobcat"),
			new ParameterValue (228, "Boxville"),
			new ParameterValue (212, "Burrito"),
			new ParameterValue (161, "Bus"),
			new ParameterValue (168, "Cabbie"),
			new ParameterValue (187, "Caddy"),
			new ParameterValue (145, "Cheetah"),
			new ParameterValue (167, "Coach"),
			new ParameterValue (202, "Coast guard"),
			new ParameterValue (210, "Comet"),
			new ParameterValue (164, "Cuban hermes"),
			new ParameterValue (211, "Deluxo"),
			new ParameterValue (203, "Dinghy"),
			new ParameterValue (157, "Enforcer"),
			new ParameterValue (149, "Esperanto"),
			new ParameterValue (192, "Faggio"),
			new ParameterValue (220, "FBI rancher"),
			new ParameterValue (147, "FBI washington"),
			new ParameterValue (137, "Firetruck"),
			new ParameterValue (185, "Flatbed"),
			new ParameterValue (193, "Freeway"),
			new ParameterValue (179, "Gang burrito"),
			new ParameterValue (196, "Glendale"),
			new ParameterValue (222, "Greenwood"),
			new ParameterValue (204, "Hermes"),
			new ParameterValue (224, "Hotring"),
			new ParameterValue (232, "Hotring A"),
			new ParameterValue (233, "Hotring B"),
			new ParameterValue (155, "Hunter"),
			new ParameterValue (131, "Idaho"),
			new ParameterValue (141, "Infernus"),
			new ParameterValue (223, "Jetmax"),
			new ParameterValue (216, "Kauffman cab"),
			new ParameterValue (130, "Landstalker"),
			new ParameterValue (133, "Line runner"),
			new ParameterValue (201, "Lovefist"),
			new ParameterValue (140, "Manana"),
			new ParameterValue (214, "Marquis"),
			new ParameterValue (217, "Maverick"),
			new ParameterValue (230, "Mesa grande"),
			new ParameterValue (148, "Moonbeam"),
			new ParameterValue (153, "Mr Whoopie"),
			new ParameterValue (144, "Mule"),
			new ParameterValue (197, "Oceanic"),
			new ParameterValue (173, "Packer"),
			new ParameterValue (200, "Patriot"),
			new ParameterValue (191, "PCJ 600"),
			new ParameterValue (134, "Perennial"),
			new ParameterValue (207, "Pheonix"),
			new ParameterValue (178, "Pizzaboy"),
			new ParameterValue (156, "Police"),
			new ParameterValue (227, "Police maverick"),
			new ParameterValue (143, "Pony"),
			new ParameterValue (160, "Predator"),
			new ParameterValue (219, "Rancher"),
			new ParameterValue (183, "Reefer"),
			new ParameterValue (209, "Regina"),
			new ParameterValue (162, "Rhino"),
			new ParameterValue (136, "Rio"),
			new ParameterValue (172, "Romero"),
			new ParameterValue (170, "Rumpo"),
			new ParameterValue (205, "Sabre"),
			new ParameterValue (206, "Sabre turbo"),
			new ParameterValue (198, "Sanchez"),
			new ParameterValue (225, "Sandking"),
			new ParameterValue (177, "Sea sparrow"),
			new ParameterValue (158, "Securicar"),
			new ParameterValue (135, "Sentinel"),
			new ParameterValue (174, "Sentinel XS"),
			new ParameterValue (190, "Skimmer"),
			new ParameterValue (213, "Spand express"),
			new ParameterValue (199, "Sparrow"),
			new ParameterValue (182, "Speeder"),
			new ParameterValue (176, "Squalo"),
			new ParameterValue (169, "Stallion"),
			new ParameterValue (132, "Stinger"),
			new ParameterValue (139, "Stretch"),
			new ParameterValue (150, "Taxi"),
			new ParameterValue (189, "Topfun"),
			new ParameterValue (138, "Trash master"),
			new ParameterValue (184, "Tropic"),
			new ParameterValue (218, "VCN maverick"),
			new ParameterValue (236, "Vice cheetah"),
			new ParameterValue (221, "Virgo"),
			new ParameterValue (142, "Voodoo"),
			new ParameterValue (208, "Walton"),
			new ParameterValue (151, "Washington"),
			new ParameterValue (186, "Yankee"),
			new ParameterValue (188, "Zebra")};

		/// <summary>
		/// Возвращает список видов транспорта для гаражей
		/// </summary>
		public static ParameterValue[] CarsForGarages
			{
			get
				{
				List<ParameterValue> cfg = new List<ParameterValue> (cars);
				cfg.Insert (0, new ParameterValue (0, Localization.GetText ("Cars_No")));
				return cfg.ToArray ();
				}
			}

		/// <summary>
		/// Возвращает список видов транспорта для банд
		/// </summary>
		public static ParameterValue[] CarsForGangs
			{
			get
				{
				List<ParameterValue> cfg = new List<ParameterValue> (cars);
				cfg.Insert (0, new ParameterValue (-1, Localization.GetText ("Cars_No")));
				return cfg.ToArray ();
				}
			}

		/// <summary>
		/// Возвращает список видов транспорта для парковок
		/// </summary>
		public static ParameterValue[] CarsForCG
			{
			get
				{
				List<ParameterValue> cfg = new List<ParameterValue> (cars);
				cfg.Insert (0, new ParameterValue (-1, Localization.GetText ("Cars_Random")));
				cfg.Insert (0, new ParameterValue (0, Localization.GetText ("Cars_No")));
				return cfg.ToArray ();
				}
			}
		#endregion

		#region Радиостанции
		private static ParameterValue[] radios = {
			new ParameterValue (0, "Wild"),
			new ParameterValue (1, "Flash FM"),
			new ParameterValue (2, "KChat"),
			new ParameterValue (3, "Fever 105 FM"),
			new ParameterValue (4, "VCPR"),
			new ParameterValue (5, "VRock"),
			new ParameterValue (6, "Espantoso"),
			new ParameterValue (7, "Emotion 98.3 FM"),
			new ParameterValue (8, "Wave 103 FM"),
			new ParameterValue (9, "(MP3)"),
			new ParameterValue (10, "(off)"),
			new ParameterValue (23, "(police)")};

		/// <summary>
		/// Возвращает список радиостанций
		/// </summary>
		public static ParameterValue[] Radios
			{
			get
				{
				return radios;
				}
			}
		#endregion

		#region Бомбы
		private static ParameterValue[] bombs = {
			new ParameterValue (0, "No"),
			new ParameterValue (1, "Timer"),
			new ParameterValue (2, "Acceleration"),
			// new ParameterValue (3, "Радиобомба"),				// Работает только при минировании в доках
			// new ParameterValue (4, "Активная часовая"),			// Не работает
			new ParameterValue (5, "AccelerationActive") };

		/// <summary>
		/// Возвращает список вариантов минирования авто
		/// </summary>
		public static ParameterValue[] Bombs
			{
			get
				{
				return bombs;
				}
			}
		#endregion
		}
	}
