using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс-описатель программы
	/// </summary>
	public class MakeCollisionScriptProgram
		{
		/// <summary>
		/// Точка входа приложения
		/// </summary>
		/// <param name="args">Аргументы командной строки</param>
		[STAThread]
		static public int Main (string[] args)
			{
			// Заголовок
			Console.Title = ProgramDescription.AssemblyTitle;
			Console.Write ("\n " + ProgramDescription.AssemblyDescription +
				"\n by RD AAOW Free development lab\n\n");

			// Проверка имени файла
			bool visual = false;
			if (!Localization.IsXPUNClassAcceptable)
				{
				ShowMessage ("This product is prohibited for XPUNE area (special act of FDF No. 4 of April 17, 2022)",
					visual, true);
				return -21;
				}

			if (args.Length != 1)
				{
				ShowMessage ("Usage: " +
					ProgramDescription.AssemblyMainName + " <QHullOFF_FileName" + QHullOFFReader.MasterExtension +
					">\n          or\n          " +
					ProgramDescription.AssemblyMainName + " <DFF_FileName" + DFFReader.MasterExtension + ">",
					visual, false);
				return 1;
				}
			else if (args[0].ToLower () == "-v")
				{
				visual = true;
				}

			// Запрос имён входного и выходного файлов, если требуется
			string inFileName = args[0],
				outFileName = Path.GetFileNameWithoutExtension (args[0]) + CSTWriter.MasterExtension2;
			if (visual)
				{
				// Входной файл
				OpenFileDialog ofd = new OpenFileDialog ();
				ofd.CheckFileExists = ofd.CheckPathExists = true;
				ofd.Filter = "DFF model|*" + DFFReader.MasterExtension +
					"|QHullOFF script|*" + QHullOFFReader.MasterExtension;
				ofd.Multiselect = false;
				ofd.RestoreDirectory = true;
				ofd.ShowHelp = ofd.ShowReadOnly = false;
				ofd.Title = "Select model file";

				if (ofd.ShowDialog () != DialogResult.OK)
					return -1;
				else
					inFileName = ofd.FileName;

				// Выходной файл
				SaveFileDialog sfd = new SaveFileDialog ();
				sfd.CreatePrompt = false;
				sfd.FileName = Path.GetFileNameWithoutExtension (ofd.FileName);
				sfd.Filter = "CST script|*" + CSTWriter.MasterExtension2;
				sfd.OverwritePrompt = sfd.RestoreDirectory = true;
				sfd.ShowHelp = false;
				sfd.Title = "Select CST script file for creation";

				if (sfd.ShowDialog () != DialogResult.OK)
					return -2;
				else
					outFileName = sfd.FileName;
				}

			// Попытка открытия файла
			FileStream FS = null;
			try
				{
				FS = new FileStream (inFileName, FileMode.Open);
				}
			catch
				{
				ShowMessage ("File \"" + inFileName + "\" is unavailable", visual, true);
				return -1;
				}

			// Определение версии файла
			bool dff = inFileName.ToLower ().EndsWith (DFFReader.MasterExtension);
			List<Triangle3D> triangles = new List<Triangle3D> ();

			// Загрузка DFF
			if (dff)
				{
				BinaryReader BR = new BinaryReader (FS);
				DFFReader dffr = new DFFReader (BR);
				BR.Close ();

				if (dffr.ExtractedPoints.Count == 0)
					{
					ShowMessage ("File \"" + inFileName + "\": this version is unsupported or file is empty",
						visual, true);
					return -11;
					}

				for (int i = 0; i < dffr.ExtractedTriangles.Count; i++)
					{
					triangles.Add (new Triangle3D (dffr.ExtractedPoints[(int)dffr.ExtractedTriangles[i].X],
						dffr.ExtractedPoints[(int)dffr.ExtractedTriangles[i].Y],
						dffr.ExtractedPoints[(int)dffr.ExtractedTriangles[i].Z]));
					}
				}

			// Загрузка Qhull OFF
			else
				{
				StreamReader SR = new StreamReader (FS);
				QHullOFFReader qhoffr = new QHullOFFReader (SR);
				SR.Close ();

				if (qhoffr.ExtractedTriangles.Count == 0)
					{
					ShowMessage ("File \"" + inFileName + "\" is unsupported or corrupted", visual, true);
					return -21;
					}

				for (int i = 0; i < qhoffr.ExtractedTriangles.Count; i++)
					{
					triangles.Add (new Triangle3D (qhoffr.ExtractedTriangles[i]));
					}
				}

			// Чтение завершено. Сброс массива точек, формирование массива уникальных точек и ссылок на них
			FS.Close ();

			List<Point3D> points = new List<Point3D> ();
			for (int t = 0; t < triangles.Count; t++)
				{
				// Точка 1
				if (points.Contains (triangles[t].Point1))
					{
					triangles[t].Point1ArrayPosition = (uint)points.IndexOf (triangles[t].Point1);
					}
				else
					{
					triangles[t].Point1ArrayPosition = (uint)points.Count;
					points.Add (triangles[t].Point1);
					}

				// Точка 2
				if (points.Contains (triangles[t].Point2))
					{
					triangles[t].Point2ArrayPosition = (uint)points.IndexOf (triangles[t].Point2);
					}
				else
					{
					triangles[t].Point2ArrayPosition = (uint)points.Count;
					points.Add (triangles[t].Point2);
					}

				// Точка 3
				if (points.Contains (triangles[t].Point3))
					{
					triangles[t].Point3ArrayPosition = (uint)points.IndexOf (triangles[t].Point3);
					}
				else
					{
					triangles[t].Point3ArrayPosition = (uint)points.Count;
					points.Add (triangles[t].Point3);
					}
				}

			// Запись файла
			if (!CSTWriter.WriteCST (outFileName, points, triangles))
				{
				ShowMessage ("Cannot create file \"" + outFileName + "\"", visual, true);
				return -2;
				}

			ShowMessage ("Conversion completed successfully", visual, false);
			return 0;
			}

		// Метод отображает сообщение об ошибке или предупреждение
		static private void ShowMessage (string Text, bool Visual, bool Error)
			{
			if (Visual)
				MessageBox.Show (Text, ProgramDescription.AssemblyTitle, MessageBoxButtons.OK,
					Error ? MessageBoxIcon.Exclamation : MessageBoxIcon.Information);
			else
				Console.Write ((Error ? " \x13 " : " \x10 ") + Text + "\n\n");
			}
		}
	}
