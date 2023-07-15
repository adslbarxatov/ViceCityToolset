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
			FileStream FS = null;
			try
				{
				FS = new FileStream (colorsFile, FileMode.Open);
				}
			catch
				{
				RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
					string.Format (Localization.GetText ("CarColorsFileUnavailable"), colorsFile));
				Error = -1;
				return;
				}
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
		}
	}
