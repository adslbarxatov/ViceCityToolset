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
		/// <summary>
		/// Возвращает список полученных цветов
		/// </summary>
		public List<Color> Colors
			{
			get
				{
				return colors;
				}
			}
		private List<Color> colors = new List<Color> ();

		/// <summary>
		/// Конструктор. Загружает список цветов из расположения приложения
		/// </summary>
		/// <param name="Error">Возвращает код ошибки или 0 в случае успеха</param>
		public CarColors (out int Error)
			{
			// Попытка открытия файла
			string colorsFile = ViceCityToolsetProgram.GTAVCDirectory + "\\data\\carcols.dat";
			FileStream FS = null;
			try
				{
				FS = new FileStream (colorsFile, FileMode.Open);
				}
			catch
				{
				/* Запрос имени файла
				OpenFileDialog ofd = new OpenFileDialog ();
				ofd.Filter = string.Format (Localization.GetText ("CarColorsFilter"), ColorsFile);
				ofd.Title = string.Format (Localization.GetText ("CarColorsTitle"), ColorsFile);
				if (ofd.ShowDialog () != DialogResult.OK)
					{
					Error = -1;
					return;
					}

				CarColorsPath = ofd.FileName;
				ofd.Dispose ();

				// Повторная попытка
				try
					{
					FS = new FileStream (CarColorsPath, FileMode.Open);
					}
				catch
					{*/
				RDGenerics.MessageBox (RDMessageTypes.Warning,
					string.Format (Localization.GetText ("CarColorsFileUnavailable"), colorsFile));
				Error = -1;
				return;
				}
			/*}*/
			StreamReader SR = new StreamReader (FS);

			// Загрузка цветовой схемы
			string line;
			char[] splitters = { ',', '\t', ' ' };

			do
				{
				line = SR.ReadLine ();
				} while (line != "col");

			while ((line = SR.ReadLine ()) != "end")
				{
				try
					{
					string[] rgb = line.Split (splitters, StringSplitOptions.RemoveEmptyEntries);
					int r = int.Parse (rgb[0]);
					int g = int.Parse (rgb[1]);
					int b = int.Parse (rgb[2]);
					colors.Add (Color.FromArgb (r, g, b));
					}
				catch { }
				}

			// Завершение
			SR.Close ();
			FS.Close ();
			Error = 0;
			}

		/*
		/// <summary>
		/// Возвращает или задаёт путь к файлу цветов авто
		/// </summary>
		private static string CarColorsPath
			{
			// Запрос
			get
				{
				if (string.IsNullOrWhiteSpace (carColorsPath))
					carColorsPath = RDGenerics.GetAppSettingsValue ("CarColorsPath");

				return carColorsPath;
				}

			// Установка
			set
				{
				carColorsPath = value;
				RDGenerics.SetAppSettingsValue ("CarColorsPath", value);
				}
			}
		private static string carColorsPath;*/
		}
	}
