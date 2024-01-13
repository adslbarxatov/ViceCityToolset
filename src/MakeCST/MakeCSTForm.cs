using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает главную форму программы
	/// </summary>
	public partial class MakeCSTForm: Form
		{
		/// <summary>
		/// Конструктор. Запускает главную форму программы
		/// </summary>
		public MakeCSTForm ()
			{
			InitializeComponent ();

			// Настройка контролов
			this.Text = ProgramDescription.AssemblyTitle + " – " + RDLocale.GetText (this.Name);
			RDGenerics.LoadWindowDimensions (this);

			OFLabel.Text = OFDialog.Title = RDLocale.GetText ("MakeCSTForm_OFLabel");
			SFLabel.Text = RDLocale.GetText ("MakeCSTForm_SFLabel");
			ConvertButton.Text = RDLocale.GetText ("MakeCSTForm_ConvertButton");
			ExitButton.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Exit);

			OFDialog.Filter = "DFF model|*" + DFFReader.MasterExtension +
				"|QHullOFF script|*" + QHullOFFReader.MasterExtension;

			// Запуск
			this.ShowDialog ();
			}

		// Выбор входного файла
		private void OFSelect_Click (object sender, EventArgs e)
			{
			OFDialog.ShowDialog ();
			}

		private void OFDialog_FileOk (object sender, CancelEventArgs e)
			{
			OFName.Text = OFDialog.FileName;
			SFName.Text = Path.GetDirectoryName (OFName.Text) + "\\" +
				Path.GetFileNameWithoutExtension (OFName.Text) + CSTWriter.MasterExtension;
			ConvertButton.Enabled = !string.IsNullOrWhiteSpace (OFName.Text);
			}

		// Выход
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void MakeCSTForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			RDGenerics.SaveWindowDimensions (this);
			}

		// Преобразование
		private void ConvertButton_Click (object sender, EventArgs e)
			{
			// Попытка открытия файла
			FileStream FS = null;
			try
				{
				FS = new FileStream (OFName.Text, FileMode.Open);
				}
			catch
				{
				RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
					RDLocale.GetFileProcessingMessage (OFName.Text,
					RDL_FP_Messages.Load_Failure));
				return;
				}

			// Определение версии файла
			bool dff = OFName.Text.ToLower ().EndsWith (DFFReader.MasterExtension);
			List<Triangle3D> triangles = new List<Triangle3D> ();

			// Загрузка DFF
			if (dff)
				{
				BinaryReader BR = new BinaryReader (FS);
				DFFReader dffr = new DFFReader (BR);
				BR.Close ();

				if (dffr.ExtractedPoints.Count == 0)
					{
					RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
						string.Format (RDLocale.GetText ("MakeCST_UnsupportedDFF"), OFName.Text));
					return;
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
					RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
						string.Format (RDLocale.GetText ("MakeCST_UnsupportedQHull"), OFName.Text));
					return;
					}

				for (int i = 0; i < qhoffr.ExtractedTriangles.Count; i++)
					triangles.Add (new Triangle3D (qhoffr.ExtractedTriangles[i]));
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
			if (!CSTWriter.WriteCST (SFName.Text, points, triangles))
				{
				RDGenerics.MessageBox (RDMessageTypes.Warning_Center,
					RDLocale.GetFileProcessingMessage (SFName.Text,
					RDL_FP_Messages.Save_Failure));
				return;
				}

			RDGenerics.LocalizedMessageBox (RDMessageTypes.Success_Center, "MakeCST_Success");
			}
		}
	}
