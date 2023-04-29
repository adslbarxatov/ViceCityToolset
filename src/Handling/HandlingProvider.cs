using System;
using System.Collections.Generic;
using System.IO;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс доступ и управление данными Handling
	/// </summary>
	public class HandlingProvider
		{
		// Переменные
		private List<GenericHandlingDescriptor> genericHDs = new List<GenericHandlingDescriptor> ();
		private List<BoatHandlingDescriptor> boatHDs = new List<BoatHandlingDescriptor> ();
		private List<BikeHandlingDescriptor> bikeHDs = new List<BikeHandlingDescriptor> ();
		private List<FlyingHandlingDescriptor> flyingHDs = new List<FlyingHandlingDescriptor> ();
		private char[] splitters = new char[] { ' ', '\t' };
		private List<int> boatIndexes = new List<int> ();
		private List<int> bikeIndexes = new List<int> ();
		private List<int> flyingIndexes = new List<int> ();

		private string handlingFile = ViceCityToolsetProgram.GTAVCDirectory + "\\data\\handling.cfg";
		private string handlingBackup = ViceCityToolsetProgram.GTAVCDirectory + "\\data\\handling.vctbak";

		/// <summary>
		/// Символ-признак комментария
		/// </summary>
		public const string CommentSymbol = ";";

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
		/// Конструктор. Загружает данные из файла handling.cfg и формирует соответствующие дескрипторы
		/// </summary>
		public HandlingProvider (/*string HandlingFileName*/)
			{
			// Резервное копирование
			if (!File.Exists (handlingBackup))
				{
				try
					{
					File.Copy (handlingFile, handlingBackup, false);
					}
				catch
					{
					return;
					}
				}

			// Попытка открытия файла
			FileStream FS = null;
			try
				{
				FS = new FileStream (handlingFile, FileMode.Open);
				}
			catch
				{
				initStatus = InitStatuses.FileNotAvailable;
				return;
				}
			StreamReader SR = new StreamReader (FS/*, Encoding.GetEncoding (1251)*/);

			// Чтение файла
			while (!SR.EndOfStream)
				{
				string s = SR.ReadLine ();

				switch (s.Substring (0, 1))
					{
					// Комментарии
					case CommentSymbol:
						continue;

					// Строки boat descriptor
					case BoatHandlingDescriptor.IdentifyingSymbol:
						BoatHandlingDescriptor boatHD = new BoatHandlingDescriptor (s.Split (splitters,
							StringSplitOptions.RemoveEmptyEntries));

						if (!boatHD.IsInited)
							initStatus = InitStatuses.BrokenDescriptor;
						else
							boatHDs.Add (boatHD);
						break;

					// Строки bike descriptor
					case BikeHandlingDescriptor.IdentifyingSymbol:
						BikeHandlingDescriptor bikeHD = new BikeHandlingDescriptor (s.Split (splitters,
							StringSplitOptions.RemoveEmptyEntries));

						if (!bikeHD.IsInited)
							initStatus = InitStatuses.BrokenDescriptor;
						else
							bikeHDs.Add (bikeHD);
						break;

					// Строки flying descriptor
					case FlyingHandlingDescriptor.IdentifyingSymbol:
						FlyingHandlingDescriptor flyingHD = new FlyingHandlingDescriptor (s.Split (splitters,
							StringSplitOptions.RemoveEmptyEntries));
						if (!flyingHD.IsInited)
							initStatus = InitStatuses.BrokenDescriptor;
						else
							flyingHDs.Add (flyingHD);
						break;

					// Предположительно, строки общих дескрипторов
					default:
						GenericHandlingDescriptor genericHD = new GenericHandlingDescriptor (s.Split (splitters,
							StringSplitOptions.RemoveEmptyEntries));
						if (!genericHD.IsInited)
							initStatus = InitStatuses.BrokenDescriptor;
						else
							genericHDs.Add (genericHD);
						break;
					}
				}

			// Завершено
			SR.Close ();
			FS.Close ();

			if (genericHDs.Count == 0)
				{
				initStatus = InitStatuses.FileIsEmpty;
				return;
				}

			// Индексирование дескрипторов
			for (int g = 0; g < genericHDs.Count; g++)
				{
				// Индексация специальных дескрипторов лодок
				int v = -1;
				for (int b = 0; b < boatHDs.Count; b++)
					{
					if (genericHDs[g].VehicleIdentifier == boatHDs[b].VehicleIdentifier)
						{
						v = b;
						break;
						}
					}
				boatIndexes.Add (v);

				// Индексация специальных дескрипторов мотоциклов
				v = -1;
				for (int b = 0; b < bikeHDs.Count; b++)
					{
					if (genericHDs[g].VehicleIdentifier == bikeHDs[b].VehicleIdentifier)
						{
						v = b;
						break;
						}
					}
				bikeIndexes.Add (v);

				// Индексация специальных дескрипторов мотоциклов
				v = -1;
				for (int f = 0; f < flyingHDs.Count; f++)
					{
					if (genericHDs[g].VehicleIdentifier == flyingHDs[f].VehicleIdentifier)
						{
						v = f;
						break;
						}
					}
				flyingIndexes.Add (v);
				}

			// Успешно
			initStatus = InitStatuses.Ok;
			}

		/// <summary>
		/// Метод возвращает основной дескриптор по его номеру в списке
		/// </summary>
		/// <param name="GenericHDNumber">Номер основного дескриптора в списке</param>
		/// <returns>Возвращает дескриптор или null, если номер превышает размер списка</returns>
		public GenericHandlingDescriptor GetGenericHD (uint GenericHDNumber)
			{
			if (GenericHDNumber < genericHDs.Count)
				return genericHDs[(int)GenericHDNumber];

			return null;
			}

		/// <summary>
		/// Возвращает количество основных дескрипторов
		/// </summary>
		public uint GenericHDsCount
			{
			get
				{
				return (uint)genericHDs.Count;
				}
			}

		/// <summary>
		/// Метод возвращает специальный дескриптор лодки по его номеру в списке
		/// </summary>
		/// <param name="GenericHDNumber">Номер основного дескриптора в списке</param>
		/// <returns>Возвращает дескриптор или null, если номер превышает размер списка</returns>
		public BoatHandlingDescriptor GetBoatHD (uint GenericHDNumber)
			{
			if ((int)GenericHDNumber < genericHDs.Count)
				{
				if (boatIndexes[(int)GenericHDNumber] != -1)
					return boatHDs[boatIndexes[(int)GenericHDNumber]];
				}

			return null;
			}

		/// <summary>
		/// Метод возвращает специальный дескриптор мотоцикла по его номеру в списке
		/// </summary>
		/// <param name="GenericHDNumber">Номер основного дескриптора в списке</param>
		/// <returns>Возвращает дескриптор или null, если номер превышает размер списка</returns>
		public BikeHandlingDescriptor GetBikeHD (uint GenericHDNumber)
			{
			if ((int)GenericHDNumber < genericHDs.Count)
				{
				if (bikeIndexes[(int)GenericHDNumber] != -1)
					return bikeHDs[bikeIndexes[(int)GenericHDNumber]];
				}

			return null;
			}

		/// <summary>
		/// Метод возвращает специальный дескриптор летательного аппарата по его номеру в списке
		/// </summary>
		/// <param name="GenericHDNumber">Номер основного дескриптора в списке</param>
		/// <returns>Возвращает дескриптор или null, если номер превышает размер списка</returns>
		public FlyingHandlingDescriptor GetFlyingHD (uint GenericHDNumber)
			{
			if ((int)GenericHDNumber < genericHDs.Count)
				{
				if (flyingIndexes[(int)GenericHDNumber] != -1)
					return flyingHDs[flyingIndexes[(int)GenericHDNumber]];
				}

			return null;
			}

		/// <summary>
		/// Метод сохраняет данные в файл handling.cfg
		/// </summary>
		/// <returns>Возвращает true в случае успеха</returns>
		public bool SaveHandlingData (/*string HandlingFileName, bool AddComments*/)
			{
			// Контроль инициализации
			if (initStatus != InitStatuses.Ok)
				return false;

			// Попытка открытия файла
			FileStream FS = null;
			try
				{
				FS = new FileStream (handlingFile, FileMode.Create);
				}
			catch
				{
				return false;
				}
			StreamWriter SW = new StreamWriter (FS/*, Encoding.GetEncoding (1251)*/);

			// Запись
			// Заголовок файла и основные дескрипторы
			SW.WriteLine (CommentPrefix + "HANDLING.CFG for GTA Vice City");
			SW.WriteLine (CommentPrefix + "Updated by " + ProgramDescription.AssemblyTitle + ", " +
				DateTime.Now.ToString ("dd.MM.yyyy; HH.mm"));
			SW.WriteLine (CommentSymbol);

			/*if (AddComments)
				{
				SW.WriteLine (CommentPrefix +
					RD_AAOW.Properties.ViceCityToolset.GenericComment.Replace ("\n", "\n" + CommentPrefix));
				SW.WriteLine (CommentSymbol);
				}*/

			SW.WriteLine (CommentPrefix + GenericHandlingDescriptor.TableHeader);

			for (int g = 0; g < genericHDs.Count; g++)
				SW.WriteLine (genericHDs[g].GetHDAsString ());

			// Дескрипторы моторных лодок
			SW.WriteLine (CommentSymbol);

			/*if (AddComments)
				{
				SW.WriteLine (CommentPrefix +
					RD_AAOW.Properties.ViceCityToolset.BoatComment.Replace ("\n", "\n" + CommentPrefix));
				SW.WriteLine (CommentSymbol);
				}*/

			SW.WriteLine (CommentPrefix + BoatHandlingDescriptor.TableHeader);

			for (int b = 0; b < boatHDs.Count; b++)
				SW.WriteLine (boatHDs[b].GetHDAsString ());

			// Дескрипторы мотоциклов
			SW.WriteLine (CommentSymbol);

			/*if (AddComments)
				{
				SW.WriteLine (CommentPrefix +
					RD_AAOW.Properties.ViceCityToolset.BikeComment.Replace ("\n", "\n" + CommentPrefix));
				SW.WriteLine (CommentSymbol);
				}*/

			SW.WriteLine (CommentPrefix + BikeHandlingDescriptor.TableHeader);

			for (int b = 0; b < bikeHDs.Count; b++)
				SW.WriteLine (bikeHDs[b].GetHDAsString ());

			// Дескрипторы летательных аппаратов
			SW.WriteLine (CommentSymbol);

			/*if (AddComments)
				{
				SW.WriteLine (CommentPrefix +
					RD_AAOW.Properties.ViceCityToolset.FlyingComment.Replace ("\n", "\n" + CommentPrefix));
				SW.WriteLine (CommentSymbol);
				}*/

			SW.WriteLine (CommentPrefix + FlyingHandlingDescriptor.TableHeader);

			for (int f = 0; f < flyingHDs.Count; f++)
				SW.WriteLine (flyingHDs[f].GetHDAsString ());

			// Как оказалось, следующая вставка обязательна. Причём, без исправлений
			SW.WriteLine (CommentSymbol + "\n" + CommentSymbol + "\n" + CommentSymbol + "the end");

			// Завершено
			SW.Close ();
			FS.Close ();
			return true;
			}
		}
	}
