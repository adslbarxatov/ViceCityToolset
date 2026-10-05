using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс обеспечивает доступ к списку цветов транспортных средств
	/// </summary>
	public class CarColors
		{
		// Переменные
		private string colorsFile = ViceCityToolsetProgram.GTAVCDirectory + "\\data\\carcols.dat";
		private string colorsFileBackup = ViceCityToolsetProgram.GTAVCDirectory + "\\data\\carcols.vctbak";

		private const string colorsSection = "col";
		private const string sectionEnd = "end";
		private const string carsSection = "car";
		private const string commentPrefix = "# ";

		/// <summary>
		/// Возвращает максимальное количество цветов в таблице
		/// </summary>
		public const byte MaxColors = 95;

		/// <summary>
		/// Возвращает нижнее ограничение на количество цветов в таблице
		/// </summary>
		public const byte MinColors = 10;

		/// <summary>
		/// Возвращает максимальное количество вариантов цветов для одного
		/// вида транспорта
		/// </summary>
		public const byte MaxColorsPerCar = 8;

		/// <summary>
		/// Метод возвращает цвет по его номеру в таблице
		/// </summary>
		/// <param name="ColorNumber">Номер цвета в таблице</param>
		/// <returns>Возвращает чёрный цвет, если номер указан неправильно</returns>
		public Color GetColor (byte ColorNumber)
			{
			if (ColorNumber >= colors.Count)
				return Color.Black;

			return colors[ColorNumber];
			}

		/// <summary>
		/// Возвращает количество цветов в таблице
		/// </summary>
		public byte ColorsCount
			{
			get
				{
				return (byte)colors.Count;
				}
			}

		// Список сопоставлений
		private List<Color> colors = [];
		private List<string> carAliases = [];
		private List<List<byte>> carColors = [];

		/// <summary>
		/// Конструктор. Загружает список цветов из расположения приложения
		/// </summary>
		/// <param name="Error">Возвращает код ошибки или 0 в случае успеха</param>
		public CarColors (out int Error)
			{
			// Попытка открытия файла
			FileStream FS;
			try
				{
				FS = new FileStream (colorsFile, FileMode.Open);
				}
			catch
				{
				// Сброс заданной директории
				RDInterface.MessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
					string.Format (RDLocale.GetText ("CarColorsFileUnavailable"), colorsFile));
				ViceCityToolsetProgram.GTAVCDirectory = "";

				Error = -1;
				return;
				}
			StreamReader SR = new StreamReader (FS, RDGenerics.GetEncoding (RDEncodings.CP1251));

			// Загрузка цветовой схемы
			string line;
			char[] splitters = [',', '\t', ' '];

			do
				{
				line = SR.ReadLine ();
				} while (line != colorsSection);

			while ((line = SR.ReadLine ()) != sectionEnd)
				{
				string[] rgb = line.Split (splitters, StringSplitOptions.RemoveEmptyEntries);
				if (rgb.Length < 3)
					continue;

				byte r, g, b;
				try
					{
					r = byte.Parse (rgb[0]);
					g = byte.Parse (rgb[1]);
					b = byte.Parse (rgb[2]);
					}
				catch
					{
					continue;
					}

				colors.Add (Color.FromArgb (r, g, b));

				// Ограничение gtavc.exe
				if (colors.Count >= MaxColors)
					break;
				}

			// Загрузка цветов автомобилей
			do
				{
				line = SR.ReadLine ();
				} while (line != carsSection);

			while ((line = SR.ReadLine ()) != sectionEnd)
				{
				string[] car = line.Split (splitters, StringSplitOptions.RemoveEmptyEntries);
				if ((car.Length < 3) || (car.Length % 2 != 1))
					continue;

				carColors.Add ([]);
				int i = 1;
				int c = carColors.Count - 1;
				while (i < car.Length)
					{
					for (int k = 0; k < 2; k++)
						{
						byte v;
						try
							{
							v = byte.Parse (car[i++]);
							}
						catch
							{
							v = 0;
							}

						if (v >= colors.Count)
							v = 0;
						carColors[c].Add (v);
						}

					// Ограничение gtavc.exe
					if (i >= 2 * MaxColorsPerCar + 1)
						break;
					}

				carAliases.Add (car[0]);
				}

			// Завершение
			SR.Close ();
			FS.Close ();
			Error = 0;
			SaveFile ();
			}

		/// <summary>
		/// Метод сохраняет файл цветов авто
		/// </summary>
		/// <returns>Возвращает true в случае успеха</returns>
		public bool SaveFile ()
			{
			// Открытие файла
			if (!File.Exists (colorsFileBackup))
				try
					{
					File.Copy (colorsFile, colorsFileBackup);
					}
				catch
					{
					return false;
					}

			FileStream FS;
			try
				{
				FS = new FileStream (colorsFile, FileMode.Create);
				}
			catch
				{
				return false;
				}
			StreamWriter SW = new StreamWriter (FS, RDGenerics.GetEncoding (RDEncodings.CP1251));

			// Запись
			SW.WriteLine (commentPrefix + "CarCols.dat for GTA Vice City");
			SW.WriteLine (commentPrefix + "Updated by " + RDGenerics.DefaultAssemblyTitle + ", " +
				DateTime.Now.ToString ("dd.MM.yyyy; HH:mm"));
			SW.WriteLine ();

			// Цвета
			SW.WriteLine (colorsSection);
			for (int i = 0; i < colors.Count; i++)
				{
				SW.Write (colors[i].R.ToString () + "," + colors[i].G.ToString () + "," + colors[i].B.ToString ());
				SW.WriteLine ("\t\t" + commentPrefix + "Color #" + i.ToString ());
				}
			SW.WriteLine ();
			SW.WriteLine (sectionEnd);
			SW.WriteLine ();

			// Автомобили
			SW.WriteLine (carsSection);
			for (int i = 0; i < carAliases.Count; i++)
				{
				SW.Write (carAliases[i] + ", ");
				for (int j = 0; j < carColors[i].Count; j += 2)
					{
					SW.Write (carColors[i][j].ToString () + "," + carColors[i][j + 1].ToString ());
					if (j + 2 < carColors[i].Count)
						SW.Write (", ");
					else
						SW.WriteLine ();
					}
				}

			SW.WriteLine ();
			SW.WriteLine (sectionEnd);
			SW.WriteLine ();

			// Завершено
			SW.Close ();
			FS.Close ();
			return true;
			}

		/// <summary>
		/// Метод добавляет указанный цвет в таблицу
		/// </summary>
		/// <param name="Sample">Цветовой образец</param>
		/// <returns>Возвращает false, если таблица цветов уже заполнена</returns>
		public bool AddColor (Color Sample)
			{
			if (colors.Count >= MaxColors)
				return false;

			colors.Add (Sample);
			return true;
			}

		/// <summary>
		/// Метод обновляет указанный цвет в таблице
		/// </summary>
		/// <param name="Sample">Цветовой образец</param>
		/// <param name="ColorNumber">Номер цвета в таблице</param>
		/// <returns>Возвращает false, если указанный номер в таблице не существует</returns>
		public bool UpdateColor (byte ColorNumber, Color Sample)
			{
			if (ColorNumber >= colors.Count)
				return false;

			colors[ColorNumber] = Sample;
			return true;
			}

		/// <summary>
		/// Метод удаляет указанный цвет из таблицы
		/// </summary>
		/// <param name="ColorNumber">Номер цвета в таблице</param>
		/// <returns>Возвращает false, если в таблице осталось минимально допустимое
		/// количество цветов</returns>
		public bool DeleteColor (byte ColorNumber)
			{
			// Контроль
			if ((colors.Count <= MinColors) || (ColorNumber >= colors.Count))
				return false;

			// Удаление
			colors.RemoveAt (ColorNumber);

			// Обновление цветов в назначениях авто
			for (int i = 0; i < carColors.Count; i++)
				{
				for (int j = 0; j < carColors[i].Count; j++)
					{
					if (carColors[i][j] == ColorNumber)
						carColors[i][j] = 0;
					else if (carColors[i][j] > ColorNumber)
						carColors[i][j]--;
					}
				}

			return true;
			}

		/// <summary>
		/// Метод возвращает идентификатор транспорта по его номеру в таблице
		/// </summary>
		/// <param name="CarNumber">Номер транспорта в таблице</param>
		/// <returns>Возвращает пустую строку, если номер указан неправильно</returns>
		public string GetCarID (byte CarNumber)
			{
			if (CarNumber >= carAliases.Count)
				return "";

			return carAliases[CarNumber];
			}

		/// <summary>
		/// Возвращает количество видов транспорта в таблице
		/// </summary>
		public byte CarsCount
			{
			get
				{
				return (byte)carAliases.Count;
				}
			}

		/// <summary>
		/// Метод возвращает список цветов транспорта по его номеру в таблице
		/// </summary>
		/// <param name="CarNumber">Номер транспорта в таблице</param>
		/// <returns>Возвращает пустой массив, если номер указан неправильно</returns>
		public byte[] GetCarColors (byte CarNumber)
			{
			if (CarNumber >= carAliases.Count)
				return [];

			return carColors[CarNumber].ToArray ();
			}

		/// <summary>
		/// Метод добавляет новый вариант цвета для указанного транспорта
		/// </summary>
		/// <param name="CarNumber">Номер транспорта в таблице</param>
		/// <param name="Color1">Первый цвет варианта</param>
		/// <param name="Color2">Второй цвет варианта</param>
		/// <returns>Возвращает false, если номер транспорта или одного из цветов
		/// указан неправильно, или все варианты для указанного транспорта уже заняты</returns>
		public bool AddCarColorVariant (byte CarNumber, byte Color1, byte Color2)
			{
			// Контроль
			if ((CarNumber >= carAliases.Count) || (Color1 >= colors.Count) ||
				(Color2 >= colors.Count))
				return false;

			if (carColors[CarNumber].Count >= 2 * MaxColorsPerCar)
				return false;

			carColors[CarNumber].Add (Color1);
			carColors[CarNumber].Add (Color2);
			return true;
			}

		/// <summary>
		/// Метод обновляет указанный вариант цвета для указанного транспорта
		/// </summary>
		/// <param name="CarNumber">Номер транспорта в таблице</param>
		/// <param name="Color1">Первый цвет варианта</param>
		/// <param name="Color2">Второй цвет варианта</param>
		/// <param name="VariantNumber">Номер варианта цвета</param>
		/// <returns>Возвращает false, если номер транспорта, варианта или одного из цветов
		/// указан неправильно</returns>
		public bool UpdateCarColorVariant (byte CarNumber, byte VariantNumber, byte Color1, byte Color2)
			{
			// Контроль
			if ((CarNumber >= carAliases.Count) || (Color1 >= colors.Count) ||
				(Color2 >= colors.Count))
				return false;

			if (VariantNumber >= carColors[CarNumber].Count / 2)
				return false;

			carColors[CarNumber][2 * VariantNumber] = Color1;
			carColors[CarNumber][2 * VariantNumber + 1] = Color2;
			return true;
			}

		/// <summary>
		/// Метод обновляет указанный вариант цвета для указанного транспорта
		/// </summary>
		/// <param name="CarNumber">Номер транспорта в таблице</param>
		/// <param name="VariantNumber">Номер варианта цвета</param>
		/// <returns>Возвращает false, если номер транспорта или варианта указан неправильно,
		/// либо у данного транспорта нет вариантов для удаления</returns>
		public bool DeleteCarColorVariant (byte CarNumber, byte VariantNumber)
			{
			// Контроль
			if (CarNumber >= carAliases.Count)
				return false;

			if ((carColors[CarNumber].Count <= 2) || (VariantNumber >= carColors[CarNumber].Count / 2))
				return false;

			carColors[CarNumber].RemoveAt (2 * VariantNumber);
			carColors[CarNumber].RemoveAt (2 * VariantNumber);
			return true;
			}
		}
	}
