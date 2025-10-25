using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс обеспечивает доступ и управление настройками погоды
	/// </summary>
	public class WeatherProvider
		{
		// Переменные и константы
		private char[] splitters = ['\t', ' '];

		// Цвет освещения статичных объектов (в режиме наложения)
		private List<Color> staticAmbience = [];
		// Цвет освещения подвижных объектов (в режиме наложения)
		private List<Color> dynamicAmbience = [];
		// Цвет размытия освещения статичных объектов (эффект неочевиден)
		private List<Color> staticBlur = [];
		// Цвет размытия освещения подвижных объектов (эффект неочевиден)
		private List<Color> dynamicBlur = [];
		// Цвет направленного освещения подвижных объектов (луч со стороны Солнца, в режиме наложения)
		private List<Color> dynamicDirect = [];
		// Цвет верхней части небесной сферы (в режиме замены)
		private List<Color> skyTop = [];
		// Цвет нижней части небесной сферы (в режиме замены)
		private List<Color> skyBottom = [];
		// Цвет середины Солнца (в режиме замены)
		private List<Color> sunCore = [];
		// Цвет короны Солнца (в режиме замены)
		private List<Color> sunCorona = [];
		// Размер Солнца (точно)
		private List<float> sunCoreSize = [];
		// Размер короны Солнца (точно)
		private List<float> sunCoronaSize = [];
		// Яркость короны Солнца (1 – отсутствует, 0 – полная яркость)
		private List<float> sunBrightness = [];
		// Интенсивность теней (точно)
		private List<int> shadowIntensity = [];
		// Параметр оттенения людей и транспорта (коэффициент внутренних алгоритмов)
		private List<int> lightShading = [];
		// Параметр оттенения объектов с флагом 32768 (коэффициент внутренних алгоритмов)
		private List<int> poleShading = [];
		// Дальность видимости объектов (точно)
		private List<float> farClipping = [];
		// Дальность границы тумана (точно)
		private List<float> fogStart = [];
		// Количество света на поверхностях (эффект неочевиден)
		private List<float> lightOnGround = [];
		// Цвет нижних облаков (в режиме наложения)
		private List<Color> lowerClouds = [];
		// Цвет верхней части верхних облаков (в режиме замены)
		private List<Color> upperCloudsTop = [];
		// Цвет нижней части верхних облаков (в режиме замены)
		private List<Color> upperCloudsBottom = [];
		// Цвет общего размытия / следов (эффект неочевиден)
		private List<Color> blurTrails = [];
		// Цвет воды (в режиме наложения)
		private List<Color> water = [];
		// Альфа-канал воды (прозрачность текстуры)
		private List<byte> waterAlpha = [];

		private const uint rowLength = 52;

		/// <summary>
		/// Возвращает имя файла кофигурации, с которым работает данный провайдер
		/// </summary>
		public const string ConfigurationFileName = "timecyc.dat";

		private string weatherFile = ViceCityToolsetProgram.GTAVCDirectory + "\\data\\" + ConfigurationFileName;
		private string weatherBackup = ViceCityToolsetProgram.GTAVCDirectory + "\\data\\timecyc.vctbak";

		/// <summary>
		/// Символ-признак комментария
		/// </summary>
		public const string CommentSymbol = "//";

		/// <summary>
		/// Строка-признак комментария
		/// </summary>
		public const string CommentPrefix = CommentSymbol + " ";

		/// <summary>
		/// Возвращает статус инициализации класса
		/// </summary>
		public InitStatuses InitStatus
			{
			get
				{
				return initStatus;
				}
			}
		private InitStatuses initStatus = InitStatuses.FailedToCreateBackup;

		/// <summary>
		/// Возможные статусы инициализации класса
		/// </summary>
		public enum InitStatuses
			{
			/// <summary>
			/// Данные успешно загружены
			/// </summary>
			Ok,

			/// <summary>
			/// Файл не найден или недоступен
			/// </summary>
			FileNotAvailable,

			/// <summary>
			/// При чтении файла обнаружено некорректное описание дескриптора
			/// </summary>
			BrokenDescriptor,

			/// <summary>
			/// В файле отсутствуют основные дескрипторы
			/// </summary>
			FileIsEmpty,

			/// <summary>
			/// Не удаётся создать резервную копию
			/// </summary>
			FailedToCreateBackup,
			}

		/// <summary>
		/// Конструктор. Загружает данные из файла timecyc.dat и формирует соответствующие дескрипторы
		/// </summary>
		public WeatherProvider ()
			{
			// Резервное копирование
			if (!File.Exists (weatherBackup))
				{
				try
					{
					File.Copy (weatherFile, weatherBackup, false);
					}
				catch
					{
					return;
					}
				}

			// Попытка открытия файла
			FileStream FS;
			try
				{
				FS = new FileStream (weatherFile, FileMode.Open);
				}
			catch
				{
				initStatus = InitStatuses.FileNotAvailable;
				return;
				}
			StreamReader SR = new StreamReader (FS, RDGenerics.GetEncoding (RDEncodings.CP1251));

			// Чтение файла
			var fmt = RDLocale.GetCulture (RDLanguages.en_us);

			while (!SR.EndOfStream)
				{
				// Чтение и разделение
				string s = SR.ReadLine ();
				if (s.StartsWith ('/'))
					continue;

				string[] values = s.Split (splitters, StringSplitOptions.RemoveEmptyEntries);
				if (values.Length != rowLength)
					{
					initStatus = InitStatuses.BrokenDescriptor;
					return;
					}

				// Сборка составляющих
				try
					{
					staticAmbience.Add (Color.FromArgb (byte.Parse (values[0]), byte.Parse (values[1]), byte.Parse (values[2])));
					dynamicAmbience.Add (Color.FromArgb (byte.Parse (values[3]), byte.Parse (values[4]), byte.Parse (values[5])));
					staticBlur.Add (Color.FromArgb (byte.Parse (values[6]), byte.Parse (values[7]), byte.Parse (values[8])));
					dynamicBlur.Add (Color.FromArgb (byte.Parse (values[9]), byte.Parse (values[10]), byte.Parse (values[11])));
					dynamicDirect.Add (Color.FromArgb (byte.Parse (values[12]), byte.Parse (values[13]), byte.Parse (values[14])));

					skyTop.Add (Color.FromArgb (byte.Parse (values[15]), byte.Parse (values[16]), byte.Parse (values[17])));
					skyBottom.Add (Color.FromArgb (byte.Parse (values[18]), byte.Parse (values[19]), byte.Parse (values[20])));
					sunCore.Add (Color.FromArgb (byte.Parse (values[21]), byte.Parse (values[22]), byte.Parse (values[23])));
					sunCorona.Add (Color.FromArgb (byte.Parse (values[24]), byte.Parse (values[25]), byte.Parse (values[26])));

					sunCoreSize.Add (float.Parse (values[27], fmt));
					sunCoronaSize.Add (float.Parse (values[28], fmt));
					sunBrightness.Add (float.Parse (values[29], fmt));
					shadowIntensity.Add (int.Parse (values[30]));
					lightShading.Add (int.Parse (values[31]));
					poleShading.Add (int.Parse (values[32]));
					farClipping.Add (float.Parse (values[33], fmt));
					fogStart.Add (float.Parse (values[34], fmt));
					lightOnGround.Add (float.Parse (values[35], fmt));

					lowerClouds.Add (Color.FromArgb (byte.Parse (values[36]), byte.Parse (values[37]), byte.Parse (values[38])));
					upperCloudsTop.Add (Color.FromArgb (byte.Parse (values[39]), byte.Parse (values[40]), byte.Parse (values[41])));
					upperCloudsBottom.Add (Color.FromArgb (byte.Parse (values[42]), byte.Parse (values[43]), byte.Parse (values[44])));
					blurTrails.Add (Color.FromArgb (byte.Parse (values[45]), byte.Parse (values[46]), byte.Parse (values[47])));

					water.Add (Color.FromArgb (byte.Parse (values[48]), byte.Parse (values[49]), byte.Parse (values[50])));
					waterAlpha.Add (byte.Parse (values[51]));
					}
				catch
					{
					initStatus = InitStatuses.BrokenDescriptor;
					return;
					}
				}

			// Контроль
			if (water.Count != 7 * 24)
				{
				initStatus = InitStatuses.BrokenDescriptor;
				return;
				}

			// Завершено
			SR.Close ();
			FS.Close ();
			initStatus = InitStatuses.Ok;
			}

		/// <summary>
		/// Доступные значения типа Color
		/// </summary>
		public enum ColorValues
			{
			/// <summary>
			/// Цвет освещения статичных объектов
			/// </summary>
			StaticAmbience,

			/// <summary>
			/// Цвет освещения подвижных объектов
			/// </summary>
			DynamicAmbience,

			/// <summary>
			/// Цвет размытия освещения статичных объектов
			/// </summary>
			StaticBlur,

			/// <summary>
			/// Цвет размытия освещения подвижных объектов
			/// </summary>
			DynamicBlur,

			/// <summary>
			/// Цвет прямого освещения подвижных объектов
			/// </summary>
			DynamicDirect,

			/// <summary>
			/// Цвет верхней части небесной сферы
			/// </summary>
			SkyTop,

			/// <summary>
			/// Цвет нижней части небесной сферы
			/// </summary>
			SkyBottom,

			/// <summary>
			/// Цвет середины Солнца
			/// </summary>
			SunCore,

			/// <summary>
			/// Цвет короны Солнца
			/// </summary>
			SunCorona,

			/// <summary>
			/// Цвет нижних облаков
			/// </summary>
			LowerClouds,

			/// <summary>
			/// Цвет верхней части верхних облаков
			/// </summary>
			UpperCloudsTop,

			/// <summary>
			/// Цвет нижней части верхних облаков
			/// </summary>
			UpperCloudsBottom,

			/// <summary>
			/// Цвет общего размытия / следов
			/// </summary>
			BlurTrails,

			/// <summary>
			/// Цвет воды
			/// </summary>
			Water
			}

		/// <summary>
		/// Доступные значения типа Float
		/// </summary>
		public enum FloatValues
			{
			/// <summary>
			/// Размер Солнца
			/// </summary>
			SunCoreSize,

			/// <summary>
			/// Размер короны Солнца
			/// </summary>
			SunCoronaSize,

			/// <summary>
			/// Яркость короны Солнца
			/// </summary>
			SunBrightness,

			/// <summary>
			/// Дальность видимости объектов
			/// </summary>
			FarClipping,

			/// <summary>
			/// Дальность границы тумана
			/// </summary>
			FogStart,

			/// <summary>
			/// Количество света на поверхностях
			/// </summary>
			LightOnGround
			}

		/// <summary>
		/// Доступные значения типа Int
		/// </summary>
		public enum IntValues
			{
			/// <summary>
			/// Интенсивность теней
			/// </summary>
			ShadowIntensity,

			/// <summary>
			/// Параметр оттенения людей и транспорта
			/// </summary>
			LightShading,

			/// <summary>
			/// Параметр оттенения объектов с флагом 32768
			/// </summary>
			PoleShading,

			/// <summary>
			/// Альфа-канал воды (0 – 255)
			/// </summary>
			WaterAlpha
			}

		/// <summary>
		/// Метод возвращает значение типа Int для указанного номера строки конфигурации
		/// </summary>
		/// <param name="ValueIndex">Номер строки конфигурации</param>
		/// <param name="ValueType">Тип значения</param>
		/// <returns>Возвращает значение или Int.MinValue, если номер строки указан некорректно</returns>
		public int GetValue (uint ValueIndex, IntValues ValueType)
			{
			if (ValueIndex >= water.Count)
				return int.MinValue;
			int idx = (int)ValueIndex;

			switch (ValueType)
				{
				case IntValues.ShadowIntensity:
					return shadowIntensity[idx];

				case IntValues.LightShading:
					return lightShading[idx];

				case IntValues.PoleShading:
					return poleShading[idx];

				case IntValues.WaterAlpha:
					return waterAlpha[idx];
				}

			return int.MinValue;
			}

		/// <summary>
		/// Метод возвращает значение типа Float для указанного номера строки конфигурации
		/// </summary>
		/// <param name="ValueIndex">Номер строки конфигурации</param>
		/// <param name="ValueType">Тип значения</param>
		/// <returns>Возвращает значение или Float.MinValue, если номер строки указан некорректно</returns>
		public float GetValue (uint ValueIndex, FloatValues ValueType)
			{
			if (ValueIndex >= water.Count)
				return float.MinValue;
			int idx = (int)ValueIndex;

			switch (ValueType)
				{
				case FloatValues.FarClipping:
					return farClipping[idx];

				case FloatValues.FogStart:
					return fogStart[idx];

				case FloatValues.LightOnGround:
					return lightOnGround[idx];

				case FloatValues.SunBrightness:
					return sunBrightness[idx];

				case FloatValues.SunCoreSize:
					return sunCoreSize[idx];

				case FloatValues.SunCoronaSize:
					return sunCoronaSize[idx];
				}

			return float.MinValue;
			}

		/// <summary>
		/// Метод возвращает значение типа Color для указанного номера строки конфигурации
		/// </summary>
		/// <param name="ValueIndex">Номер строки конфигурации</param>
		/// <param name="ValueType">Тип значения</param>
		/// <returns>Возвращает значение или Color.Magenta, если номер строки указан некорректно</returns>
		public Color GetValue (uint ValueIndex, ColorValues ValueType)
			{
			if (ValueIndex >= water.Count)
				return Color.Magenta;
			int idx = (int)ValueIndex;

			switch (ValueType)
				{
				case ColorValues.BlurTrails:
					return blurTrails[idx];

				case ColorValues.DynamicAmbience:
					return dynamicAmbience[idx];

				case ColorValues.DynamicBlur:
					return dynamicBlur[idx];

				case ColorValues.DynamicDirect:
					return dynamicDirect[idx];

				case ColorValues.LowerClouds:
					return lowerClouds[idx];

				case ColorValues.SkyBottom:
					return skyBottom[idx];

				case ColorValues.SkyTop:
					return skyTop[idx];

				case ColorValues.StaticAmbience:
					return staticAmbience[idx];

				case ColorValues.StaticBlur:
					return staticBlur[idx];

				case ColorValues.SunCore:
					return sunCore[idx];

				case ColorValues.SunCorona:
					return sunCorona[idx];

				case ColorValues.UpperCloudsBottom:
					return upperCloudsBottom[idx];

				case ColorValues.UpperCloudsTop:
					return upperCloudsTop[idx];

				case ColorValues.Water:
					return water[idx];
				}

			return Color.Magenta;
			}

		/// <summary>
		/// Метод задаёт значение типа Int для указанного номера строки конфигурации
		/// </summary>
		/// <param name="ValueIndex">Номер строки конфигурации</param>
		/// <param name="ValueType">Тип значения</param>
		/// <param name="Value">Новое значение параметра</param>
		public void SetValue (uint ValueIndex, IntValues ValueType, int Value)
			{
			if (ValueIndex >= water.Count)
				return;
			int idx = (int)ValueIndex;

			switch (ValueType)
				{
				case IntValues.LightShading:
					lightShading[idx] = Value;
					break;

				case IntValues.PoleShading:
					poleShading[idx] = Value;
					break;

				case IntValues.ShadowIntensity:
					shadowIntensity[idx] = Value;
					break;

				case IntValues.WaterAlpha:
					waterAlpha[idx] = (byte)Value;
					break;
				}
			}

		/// <summary>
		/// Метод задаёт значение типа Float для указанного номера строки конфигурации
		/// </summary>
		/// <param name="ValueIndex">Номер строки конфигурации</param>
		/// <param name="ValueType">Тип значения</param>
		/// <param name="Value">Новое значение параметра</param>
		public void SetValue (uint ValueIndex, FloatValues ValueType, float Value)
			{
			if (ValueIndex >= water.Count)
				return;
			int idx = (int)ValueIndex;

			switch (ValueType)
				{
				case FloatValues.FarClipping:
					farClipping[idx] = Value;
					break;

				case FloatValues.FogStart:
					fogStart[idx] = Value;
					break;

				case FloatValues.LightOnGround:
					lightOnGround[idx] = Value;
					break;

				case FloatValues.SunBrightness:
					sunBrightness[idx] = Value;
					break;

				case FloatValues.SunCoreSize:
					sunCoreSize[idx] = Value;
					break;

				case FloatValues.SunCoronaSize:
					sunCoronaSize[idx] = Value;
					break;
				}
			}

		/// <summary>
		/// Метод задаёт значение типа Color для указанного номера строки конфигурации
		/// </summary>
		/// <param name="ValueIndex">Номер строки конфигурации</param>
		/// <param name="ValueType">Тип значения</param>
		/// <param name="Value">Новое значение параметра</param>
		public void SetValue (uint ValueIndex, ColorValues ValueType, Color Value)
			{
			if (ValueIndex >= water.Count)
				return;
			int idx = (int)ValueIndex;

			switch (ValueType)
				{
				case ColorValues.BlurTrails:
					blurTrails[idx] = Value;
					break;

				case ColorValues.DynamicAmbience:
					dynamicAmbience[idx] = Value;
					break;

				case ColorValues.DynamicBlur:
					dynamicBlur[idx] = Value;
					break;

				case ColorValues.DynamicDirect:
					dynamicDirect[idx] = Value;
					break;

				case ColorValues.LowerClouds:
					lowerClouds[idx] = Value;
					break;

				case ColorValues.SkyBottom:
					skyBottom[idx] = Value;
					break;

				case ColorValues.SkyTop:
					skyTop[idx] = Value;
					break;

				case ColorValues.StaticAmbience:
					staticAmbience[idx] = Value;
					break;

				case ColorValues.StaticBlur:
					staticBlur[idx] = Value;
					break;

				case ColorValues.SunCore:
					sunCore[idx] = Value;
					break;

				case ColorValues.SunCorona:
					sunCorona[idx] = Value;
					break;

				case ColorValues.UpperCloudsBottom:
					upperCloudsBottom[idx] = Value;
					break;

				case ColorValues.UpperCloudsTop:
					upperCloudsTop[idx] = Value;
					break;

				case ColorValues.Water:
					water[idx] = Value;
					break;
				}
			}

		/// <summary>
		/// Метод сохраняет данные в файл timecyc.dat
		/// </summary>
		/// <returns>Возвращает true в случае успеха</returns>
		public bool SaveWeatherData ()
			{
			// Контроль инициализации
			if (initStatus != InitStatuses.Ok)
				return false;

			// Попытка открытия файла
			FileStream FS;
			try
				{
				FS = new FileStream (weatherFile, FileMode.Create);
				}
			catch
				{
				return false;
				}
			StreamWriter SW = new StreamWriter (FS, RDGenerics.GetEncoding (RDEncodings.CP1251));

			// Запись
			// Заголовок файла и основные дескрипторы
			SW.WriteLine (CommentPrefix + "TIMECYC.DAT for GTA Vice City");
			SW.WriteLine (CommentPrefix + "Updated by " + ProgramDescription.AssemblyTitle + ", " +
				DateTime.Now.ToString ("dd.MM.yyyy; HH.mm"));

			// Запись по типам погоды
			string tab = splitters[0].ToString ();
			var fmt = RDLocale.GetCulture (RDLanguages.en_us);

			for (int w = 0; w < 7; w++)
				{
				SW.WriteLine (CommentSymbol);
				if (w < 6)
					SW.WriteLine (CommentPrefix + "WEATHER " + (w + 1).ToString ());
				else
					SW.WriteLine (CommentPrefix + "EXTRA INTERIORS COLORS");

				// Запись по часам
				for (int h = 0; h < 24; h++)
					{
					if (w < 6)
						SW.WriteLine (CommentPrefix + h.ToString () + ":00");
					else
						SW.WriteLine (CommentPrefix + "INTERIOR " + (h + 1).ToString ());

					// Сборка строки параметров
					int idx = w * 24 + h;

					string line = BuildColorString (staticAmbience[idx]) + tab;
					line += BuildColorString (dynamicAmbience[idx]) + tab;
					line += BuildColorString (staticBlur[idx]) + tab;
					line += BuildColorString (dynamicBlur[idx]) + tab;
					line += BuildColorString (dynamicDirect[idx]) + tab;
					line += BuildColorString (skyTop[idx]) + tab;
					line += BuildColorString (skyBottom[idx]) + tab;
					line += BuildColorString (sunCore[idx]) + tab;
					line += BuildColorString (sunCorona[idx]) + tab;

					line += sunCoreSize[idx].ToString (fmt) + tab;
					line += sunCoronaSize[idx].ToString (fmt) + tab;
					line += sunBrightness[idx].ToString (fmt) + tab;
					line += shadowIntensity[idx].ToString () + tab;
					line += lightShading[idx].ToString () + tab;
					line += poleShading[idx].ToString () + tab;
					line += farClipping[idx].ToString (fmt) + tab;
					line += fogStart[idx].ToString (fmt) + tab;
					line += lightOnGround[idx].ToString (fmt) + tab;

					line += BuildColorString (lowerClouds[idx]) + tab;
					line += BuildColorString (upperCloudsTop[idx]) + tab;
					line += BuildColorString (upperCloudsBottom[idx]) + tab;
					line += BuildColorString (blurTrails[idx]) + tab;

					line += BuildColorString (water[idx]) + " ";
					line += waterAlpha[idx].ToString ();

					SW.WriteLine (line);
					}
				}

			// Завершено
			SW.Close ();
			FS.Close ();
			return true;
			}

		private static string BuildColorString (Color C)
			{
			return C.R.ToString () + " " + C.G.ToString () + " " + C.B.ToString ();
			}
		}
	}
