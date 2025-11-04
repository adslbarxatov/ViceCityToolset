using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает главную форму программы
	/// </summary>
	public partial class ArchiveForm: Form
		{
		// Вызовы из библиотеки
		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern UInt32 Archive_GetFilesListEx (string DIRPath, out IntPtr Metrics, out IntPtr Names);

		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern Int16 Archive_ExtractFileEx (string IMGPath, string TargetFile, UInt32 Offset, UInt32 Size);

		[DllImport (ProgramDescription.AssemblyLibName)]
		private static extern Int16 Archive_ReplaceFileEx (string IMGPath, string TargetFile);

		// Переменные
		private List<IMGItem> items = [];
		private List<string> list = [];
		private int selectedIndex;

		/// <summary>
		/// Конструктор. Запускает главную форму программы
		/// </summary>
		public ArchiveForm ()
			{
			InitializeComponent ();

			// Настройка контролов
			this.Text = ProgramDescription.AssemblyTitle + " – " + RDLocale.GetText (this.Name);
			RDGenerics.LoadWindowDimensions (this);

			ExitButton.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Exit);
			EditButton.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Edit);
			ExtractButton.Text = RDLocale.GetText (this.Name + "_" + ExtractButton.Name);
			ReplaceButton.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Replace);
			ListLabel.Text = RDLocale.GetText (this.Name + "_" + ListLabel.Name);
			FilterField.MaxLength = IMGItem.MaxFileNameLength;
			FilterButton.Text = RDLocale.GetText (this.Name + "_" + FilterButton.Name);

			OFDialog.Title = ReplaceButton.Text;
			SFDialog.Title = ExtractButton.Text;

			// Запуск
			this.ShowDialog ();
			}

		private void ArchiveForm_Load (object sender, EventArgs e)
			{
			// Получение списка имён файлов
			RDInterface.RunWork (ReloadNames, null, " ", RDRunWorkFlags.CaptionInTheMiddle);
			if (RDInterface.WorkResultAsInteger < 0)
				{
				this.Close ();
				return;
				}

			// Готово, загрузка в интерфейс
			/*RDInterface.RunWork (ReloadList, null, " ", RDRunWorkFlags.CaptionInTheMiddle);*/
			FileNamesList.Items.AddRange (list.ToArray ());
			FileNamesList.SelectedIndex = 0;
			CountLabel.Text = items.Count.ToString ();
			}

		private void ReloadNames (object sender, DoWorkEventArgs e)
			{
			// Загрузка списка
			IntPtr pMetrics;
			IntPtr pNames;
			uint filesCount = Archive_GetFilesListEx (ViceCityToolsetProgram.GTAVCDirectory +
				IMGItem.IMGArchiveMapPath, out pMetrics, out pNames);

			if ((filesCount < 100) || (filesCount > 10000))
				{
				RDInterface.LocalizedMessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
					"IMGArchiveError");
				e.Result = -1;
				return;
				}

			// Распаковка списка
			BackgroundWorker bw = (BackgroundWorker)sender;
			string text = RDLocale.GetText ("IMGNamesLoading");
			var enc = RDGenerics.GetEncoding (RDEncodings.UTF8);

			items.Clear ();
			list.Clear ();
			for (int i = 0; i < filesCount; i++)
				{
				UInt32 offset = (UInt32)Marshal.ReadInt32 (pMetrics, (2 * i + 0) * sizeof (UInt32));
				UInt32 size = (UInt32)Marshal.ReadInt32 (pMetrics, (2 * i + 1) * sizeof (UInt32));

				byte[] name = new byte[IMGItem.MaxFileNameLength];
				int nulIdx = IMGItem.MaxFileNameLength;
				for (int j = 0; j < IMGItem.MaxFileNameLength; j++)
					{
					name[j] = Marshal.ReadByte (pNames, i * IMGItem.MaxFileNameLength + j);
					if ((name[j] == 0) && (nulIdx > j))
						{
						nulIdx = j;
						break;
						}
					}

				items.Add (new IMGItem (offset, size, enc.GetString (name, 0, nulIdx)));
				list.Add (items[i].FileName);
				bw.ReportProgress ((int)(RDWorkerForm.ProgressBarSize * i / filesCount), text);
				}

			// Завершено
			items.Sort ();
			list.Sort ();
			e.Result = 0;
			}

		// Выход
		private void BExit_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		private void ArchiveForm_FormClosing (object sender, FormClosingEventArgs e)
			{
			RDGenerics.SaveWindowDimensions (this);
			}

		// Отдельное извлечение файла
		private void ExtractButton_Click (object sender, EventArgs e)
			{
			// Контроль
			/*selectedIndex = FileNamesList.SelectedIndex;*/
			selectedIndex = list.IndexOf (FileNamesList.SelectedItem.ToString ());
			if (selectedIndex < 0)
				return;

			// Настройка диалога
			SFDialog.FileName = items[selectedIndex].FileName;
			string ext = Path.GetExtension (SFDialog.FileName);
			SFDialog.Filter = string.Format (RDLocale.GetText ("ArchiveSFFilter"), ext.Substring (1).ToUpper ()) +
				"|*" + ext;

			// Запуск
			SFDialog.ShowDialog ();
			}

		private void SFDialog_FileOk (object sender, CancelEventArgs e)
			{
			int res = Archive_ExtractFileEx (ViceCityToolsetProgram.GTAVCDirectory + IMGItem.IMGArchiveDataPath,
				SFDialog.FileName, items[selectedIndex].Offset, items[selectedIndex].Size);

			switch (res)
				{
				default:
				case 0:
					RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText | RDMessageFlags.NoSound,
						string.Format (RDLocale.GetDefaultText (RDLDefaultTexts.Message_SaveSuccess_Fmt),
						Path.GetFileName (SFDialog.FileName)), 750);
					break;

				case -1:
					RDInterface.LocalizedMessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
						"IMGArchiveError");
					break;

				case -2:
					RDInterface.MessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
						string.Format (RDLocale.GetDefaultText (RDLDefaultTexts.Message_SaveFailure_Fmt),
						Path.GetFileName (SFDialog.FileName)));
					break;
				}
			}

		// Отдельная замена файла
		private void ReplaceButton_Click (object sender, EventArgs e)
			{
			// Контроль
			/*selectedIndex = FileNamesList.SelectedIndex;*/
			selectedIndex = list.IndexOf (FileNamesList.SelectedItem.ToString ());
			if (selectedIndex < 0)
				return;

			// Настройка диалога
			OFDialog.FileName = items[selectedIndex].FileName;
			string ext = Path.GetExtension (OFDialog.FileName);
			OFDialog.Filter = string.Format (RDLocale.GetText ("ArchiveSFFilter"), ext.Substring (1).ToUpper ()) +
				"|*" + ext;

			// Запуск
			OFDialog.ShowDialog ();
			}

		private void OFDialog_FileOk (object sender, CancelEventArgs e)
			{
			// Запись нового файла в архив
			RDInterface.RunWork (UpdateArchive, OFDialog.FileName, RDLocale.GetText ("IMGArchiveUpdate"),
				RDRunWorkFlags.CaptionInTheMiddle);

			switch (RDInterface.WorkResultAsInteger)
				{
				default:
				case 0:
					RDInterface.MessageBox (RDMessageFlags.Success | RDMessageFlags.CenterText | RDMessageFlags.NoSound,
						string.Format (RDLocale.GetDefaultText (RDLDefaultTexts.Message_SaveSuccess_Fmt),
						Path.GetFileName (IMGItem.IMGArchiveDataPath)), 750);
					break;

				case -1:
				case -2:
					RDInterface.LocalizedMessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
						"IMGArchiveError");
					return;

				case -3:
				case -4:
					RDInterface.MessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
						string.Format (RDLocale.GetDefaultText (RDLDefaultTexts.Message_SaveFailure_Fmt),
						Path.GetFileName (IMGItem.IMGArchiveDataPath)));
					return;

				case -5:
					RDInterface.MessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
						string.Format (RDLocale.GetDefaultText (RDLDefaultTexts.Message_LoadFailure_Fmt),
						Path.GetFileName (OFDialog.FileName)));
					return;
				}

			// Повторное получение списка имён файлов
			RDInterface.RunWork (ReloadNames, null, " ", RDRunWorkFlags.CaptionInTheMiddle);
			if (RDInterface.WorkResultAsInteger < 0)
				{
				RDInterface.LocalizedMessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText,
					"IMGArchiveError");
				return;
				}

			// Готово, загрузка в интерфейс
			FileNamesList.Items.Clear ();
			FileNamesList.Items.AddRange (list.ToArray ());
			FileNamesList.SelectedIndex = 0;
			CountLabel.Text = items.Count.ToString ();
			}

		private void UpdateArchive (object sender, DoWorkEventArgs e)
			{
			string fileName = (string)e.Argument;
			int res = Archive_ReplaceFileEx (ViceCityToolsetProgram.GTAVCDirectory + IMGItem.IMGArchiveDataPath,
				fileName);
			e.Result = res;
			}

		// Извлечение, изменение и запись
		private void EditButton_Click (object sender, EventArgs e)
			{
			// Защита
			selectedIndex = list.IndexOf (FileNamesList.SelectedItem.ToString ());
			if (selectedIndex < 0)
				return;

			// Запуск на редактирование
			OFDialog.FileName = SFDialog.FileName = RDGenerics.AppStartupPath + list[selectedIndex];
			SFDialog_FileOk (null, null);

			RDGenerics.RunURL (OFDialog.FileName);
			if (RDInterface.LocalizedMessageBox (RDMessageFlags.Warning | RDMessageFlags.CenterText |
				RDMessageFlags.NoSound | RDMessageFlags.LockSmallSize, "AwaitingForEdition",
				RDLDefaultTexts.Button_Next, RDLDefaultTexts.Button_Cancel) != RDMessageButtons.ButtonOne)
				return;

			// Обновление архива
			OFDialog_FileOk (null, null);

			// Удаление копии
			try
				{
				File.Delete (OFDialog.FileName);
				}
			catch { }
			}

		// Фильтрация списка
		private void FilterButton_Click (object sender, EventArgs e)
			{
			// Защита
			FileNamesList.Items.Clear ();

			if (string.IsNullOrWhiteSpace (FilterField.Text))
				{
				FileNamesList.Items.AddRange (list.ToArray ());
				FileNamesList.SelectedIndex = 0;
				CountLabel.Text = list.Count.ToString ();

				return;
				}

			// Выполнение
			for (int i = 0; i < items.Count; i++)
				if (items[i].FileName.Contains (FilterField.Text, StringComparison.CurrentCultureIgnoreCase))
					FileNamesList.Items.Add (items[i].FileName);

			if (FileNamesList.Items.Count > 0)
				FileNamesList.SelectedIndex = 0;
			CountLabel.Text = FileNamesList.Items.Count.ToString ();
			}

		private void FilterField_KeyDown (object sender, KeyEventArgs e)
			{
			if (e.KeyCode == Keys.Return)
				FilterButton_Click (null, null);
			}
		}
	}
