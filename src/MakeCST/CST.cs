using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает спецификации форматов CST1 и CST2
	/// </summary>
	public class CSTWriter
		{
		/// <summary>
		/// Расширение формата файла
		/// </summary>
		public const string MasterExtension = ".cst";

		/// <summary>
		/// Метод создаёт CST-скрипт по указанным точкам
		/// </summary>
		/// <param name="FileName">Имя создаваемого файла</param>
		/// <param name="Points">Вершины модели</param>
		/// <param name="Triangles">Треугольники модели</param>
		/// <returns>Возвращает true в случае успеха</returns>
		public static bool WriteCST (string FileName, List<Point3D> Points, List<Triangle3D> Triangles)
			{
			return WriteCST (FileName, false, Points, Triangles);
			}

		/// <summary>
		/// Метод создаёт CST-скрипт по указанным точкам
		/// </summary>
		/// <param name="FileName">Имя создаваемого файла</param>
		/// <param name="CST1">Флаг указывает на необходимость записи скрипта первой версии</param>
		/// <param name="Points">Вершины модели</param>
		/// <param name="Triangles">Треугольники модели</param>
		/// <returns>Возвращает true в случае успеха</returns>
		public static bool WriteCST (string FileName, bool CST1, List<Point3D> Points, List<Triangle3D> Triangles)
			{
			FileStream FS = null;
			try
				{
				FS = new FileStream (FileName, FileMode.Create);
				}
			catch
				{
				return false;
				}
			StreamWriter SW = new StreamWriter (FS);

			// Заголовок и пустые поля
			SW.WriteLine ("# Converted with " + ProgramDescription.AssemblyDescription + "\n");
			if (CST1)
				{
				SW.WriteLine ("=> Spheres: 0\n");
				SW.WriteLine ("=> Boxes: 0\n");
				}
			else
				{
				SW.WriteLine ("CST2\n");
				}

			// Запись точек
			if (CST1)
				SW.WriteLine ("=> Vertex count: " + Points.Count.ToString ());
			else
				SW.WriteLine (Points.Count.ToString () + ", Vertex");

			NumberFormatInfo nfi = Localization.GetCulture (SupportedLanguages.en_us).NumberFormat;
			for (int p = 0; p < Points.Count; p++)
				{
				if (CST1)
					{
					SW.WriteLine ("V " + p.ToString ("D03") + ": " + Points[p].X.ToString (nfi) + "; " +
						Points[p].Y.ToString (nfi) + "; " + Points[p].Z.ToString (nfi));
					}
				else
					{
					SW.WriteLine (Points[p].X.ToString (nfi) + ", " +
						Points[p].Y.ToString (nfi) + ", " + Points[p].Z.ToString (nfi));
					}
				}

			// Запись треугольников
			if (CST1)
				SW.WriteLine ("\n=> Face count: " + Triangles.Count.ToString ());
			else
				SW.WriteLine ("\n" + Triangles.Count.ToString () + ", Face");

			for (int t = 0; t < Triangles.Count; t++)
				{
				// Непрямой порядок треугольников требуется для того, чтобы избежать "выворачивания" модели
				if (CST1)
					{
					SW.WriteLine ("F " + t.ToString ("D03") + ": " + Triangles[t].Point2ArrayPosition.ToString () +
						"; " + Triangles[t].Point1ArrayPosition.ToString () + "; " +
						Triangles[t].Point3ArrayPosition.ToString () + "  |  [0]");
					}
				else
					{
					SW.WriteLine (Triangles[t].Point2ArrayPosition.ToString () + ", " +
						Triangles[t].Point1ArrayPosition.ToString () + ", " +
						Triangles[t].Point3ArrayPosition.ToString () + ", 0, 0, 0, 0");
					}
				}

			// Завершение
			SW.Close ();
			FS.Close ();
			return true;
			}
		}
	}
