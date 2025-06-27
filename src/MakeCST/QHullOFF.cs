using System;
using System.Collections.Generic;
using System.IO;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает спецификации формата QHullOFF
	/// </summary>
	public class QHullOFFReader
		{
		// Константы

		/// <summary>
		/// Расширение формата файла
		/// </summary>
		public const string MasterExtension = ".txt";

		/// <summary>
		/// Вовращает треугольники, извлечённые из файла
		/// </summary>
		public List<Triangle3D> ExtractedTriangles
			{
			get
				{
				return extractedTriangles;
				}
			}
		private List<Triangle3D> extractedTriangles = [];

		/// <summary>
		/// Конструктор. Извлекает из файла QHullOFF информацию о вершинах и треугольниках модели
		/// </summary>
		/// <param name="SR">Открытый файловый поток QHullOFF</param>
		public QHullOFFReader (StreamReader SR)
			{
			// Переменные
			List<Point3D> points = [];
			uint pointsCount, trianglesCount;
			char[] separators = [' '];

			// Получение размеров модели
			try
				{
				SR.ReadLine ();
				string s = SR.ReadLine ();
				string[] s2 = s.Split (separators, StringSplitOptions.RemoveEmptyEntries);

				pointsCount = uint.Parse (s2[0]);
				trianglesCount = uint.Parse (s2[1]);
				}
			catch
				{
				return;
				}

			// Чтение точек
			var nfi = RDLocale.GetCulture (RDLanguages.en_us).NumberFormat;
			for (uint p = 0; p < pointsCount; p++)
				{
				try
					{
					string s = SR.ReadLine ();
					string[] s2 = s.Split (separators, StringSplitOptions.RemoveEmptyEntries);
					points.Add (new Point3D (double.Parse (s2[0], nfi),
						double.Parse (s2[1], nfi), double.Parse (s2[2], nfi)));
					}
				catch
					{
					return;
					}
				}

			// Чтение треугольников
			for (uint t = 0; t < trianglesCount; t++)
				{
				try
					{
					string s = SR.ReadLine ();
					string[] s2 = s.Split (separators, StringSplitOptions.RemoveEmptyEntries);
					extractedTriangles.Add (new Triangle3D (points[int.Parse (s2[1])], points[int.Parse (s2[2])],
						points[int.Parse (s2[3])]));
					}
				catch
					{
					return;
					}
				}

			// Завершено
			}
		}
	}
