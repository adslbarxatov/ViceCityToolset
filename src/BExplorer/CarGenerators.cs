using System;
using System.Collections.Generic;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс отвечает за обработку массива генераторов авто (парковок)
	/// </summary>
	public class CarGenerators
		{
		/// <summary>
		/// Возвращает количество активных генераторов
		/// </summary>
		public uint ActiveGeneratorsCount
			{
			get
				{
				return notNull;
				}
			}
		private uint notNull;

		/// <summary>
		/// Класс описывает отдельный генератор
		/// </summary>
		public class CGData
			{
			/// <summary>
			/// Возвращает модель авто, создаваемую генератором
			/// </summary>
			public Int32 CarModel
				{
				get
					{
					return carModel;
					}
				}
			private Int32 carModel;

			/// <summary>
			/// Возвращает X-координату положения генератора
			/// </summary>
			public float X
				{
				get
					{
					return x;
					}
				}
			private float x;

			/// <summary>
			/// Возвращает Y-координату положения генератора
			/// </summary>
			public float Y
				{
				get
					{
					return y;
					}
				}
			private float y;

			/// <summary>
			/// Возвращает Z-координату положения генератора
			/// </summary>
			public float Z
				{
				get
					{
					return z;
					}
				}
			private float z;

			/// <summary>
			/// Возвращает поворот авто в градусах
			/// </summary>
			public float Rotation
				{
				get
					{
					return rotation;
					}
				}
			private float rotation;

			/// <summary>
			/// Указывает, включён ли генератор (-1 - включён, 0 - выключен)
			/// </summary>
			public Int16 AllowSpawn
				{
				get
					{
					return allowSpawn;
					}
				}
			private Int16 allowSpawn;

			/// <summary>
			/// Возвращает первый цвет авто
			/// </summary>
			public Int16 Color1
				{
				get
					{
					return color1;
					}
				}
			private Int16 color1;

			/// <summary>
			/// Возвращает второй цвет авто
			/// </summary>
			public Int16 Color2
				{
				get
					{
					return color2;
					}
				}
			private Int16 color2;

			/// <summary>
			/// Возвращает вероятность срабатывания сигнализации при краже авто
			/// </summary>
			public uint AlarmProbability
				{
				get
					{
					return alarmProbability;
					}
				}
			private uint alarmProbability;

			/// <summary>
			/// Возвращает вероятность блокировки авто
			/// </summary>
			public uint LockProbability
				{
				get
					{
					return lockProbability;
					}
				}
			private uint lockProbability;

			/// <summary>
			/// Возвращает флаг, указывающий на обязательность генерации авто (1 - обязательно, 0 - необязательно)
			/// </summary>
			public uint ForceSpawn
				{
				get
					{
					return forceSpawn;
					}
				}
			private uint forceSpawn;

			/// <summary>
			/// Конструктор. Инициализирует объект-генератор
			/// </summary>
			public CGData (Int32 CarModel, float X, float Y, float Z, float Rotation, Int16 AllowSpawn,
				Int16 Color1, Int16 Color2, uint AlarmProbability, uint LockProbability, uint ForceSpawn)
				{
				carModel = CarModel;
				x = X;
				y = Y;
				z = Z;
				rotation = Rotation;
				allowSpawn = AllowSpawn;
				color1 = Color1;
				color2 = Color2;
				alarmProbability = AlarmProbability;
				lockProbability = LockProbability;
				forceSpawn = ForceSpawn;
				}

			/// <summary>
			/// Конструктор. Инициализирует новый объект-генератор
			/// </summary>
			public CGData (CGData Original)
				{
				carModel = Original.CarModel;
				x = Original.X;
				y = Original.Y;
				z = Original.Z;
				rotation = Original.Rotation;
				allowSpawn = Original.AllowSpawn;
				color1 = Original.Color1;
				color2 = Original.Color2;
				alarmProbability = Original.AlarmProbability;
				lockProbability = Original.LockProbability;
				forceSpawn = Original.ForceSpawn;
				}
			}

		// Массив экземпляров генераторов
		private List<CGData> generators = [];

		/// <summary>
		/// Конструктор. Загружает массив генераторов
		/// </summary>
		public CarGenerators ()
			{
			notNull = 0;

			// Загрузка с помощью номеров команд
			for (int i = 0; i < BExplorerLib.GeneratorsCount; i++)
				{
				Int32 cm = (int)decimal.Parse (BExplorerLib.SaveData_GetParameterValue
					((OpCodes)((int)OpCodes.Generators_Base + i),
					(UInt16)GeneratorsParCodes.CarModel));
				cm = (cm < 0) ? -1 : cm;
				generators.Add (new CGData (cm,
					float.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.CarX)),
					float.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.CarY)),
					float.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.CarZ)),
					float.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.CarAngle)),
					(Int16)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.AllowSpawn)),
					(short)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.PrimaryColor)),
					(short)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.SecondaryColor)),
					(uint)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.AlarmProbability)),
					(uint)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.LockProbability)),
					(uint)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
						(uint)GeneratorsParCodes.ForceSpawn))));
				}

			// Вычисление количества
			for (int i = 0; i < generators.Count; i++)
				{
				if (generators[i].CarModel != 0)
					notNull++;
				}
			}

		/// <summary>
		/// Метод возвращает данные генератора по его индексу
		/// </summary>
		/// <param name="Index">Индекс генератора</param>
		/// <returns>Данные генератора</returns>
		public CGData GetGeneratorData (int Index)
			{
			if ((Index >= 1) && (Index <= generators.Count))
				return generators[Index - 1];

			return null;
			}

		/// <summary>
		/// Метод устанавливает новые данные для генератора по его индексу
		/// </summary>
		/// <param name="Index">Индекс генератора</param>
		/// <param name="Data">Данные генератора</param>
		public void SetGeneratorData (int Index, CGData Data)
			{
			if ((Index >= 1) && (Index <= generators.Count))
				generators[Index - 1] = new CGData (Data);
			}

		/// <summary>
		/// Метод сортирует и записывает данные генераторов в файл сохранения
		/// </summary>
		/// <param name="Sort">Указывает, требуется ли сортировка перед записью</param>
		public void SaveGenerators (bool Sort)
			{
			// Сортировка по убыванию индекса
			if (Sort)
				{
				bool sorted;

				do
					{
					sorted = true;

					for (int i = 0; i < generators.Count - 1; i++)
						{
						if (generators[i].CarModel < generators[i + 1].CarModel)
							{
							generators.Reverse (i, 2);
							sorted = false;
							}
						}
					} while (!sorted);
				}

			// Вычисление количества доступных генераторов и разворот последней части массива
			notNull = 0;
			int revStart = 0;

			for (int i = 0; i < generators.Count; i++)
				{
				if (generators[i].CarModel > 0)
					revStart = i;

				if (generators[i].CarModel != 0)
					notNull++;
				}

			if (Sort)
				generators.Reverse (++revStart, generators.Count - revStart);

			// Запись
			for (int i = 0; i < BExplorerLib.GeneratorsCount; i++)
				{
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.CarModel, generators[i].CarModel.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.CarX, generators[i].X.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.CarY, generators[i].Y.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.CarZ, generators[i].Z.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.CarAngle, generators[i].Rotation.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.AllowSpawn, generators[i].AllowSpawn.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.PrimaryColor, generators[i].Color1.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.SecondaryColor, generators[i].Color2.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.AlarmProbability, generators[i].AlarmProbability.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.LockProbability, generators[i].LockProbability.ToString ());
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Generators_Base + i),
					(uint)GeneratorsParCodes.ForceSpawn, generators[i].ForceSpawn.ToString ());
				}

			BExplorerLib.SaveData_SetParameterValue (OpCodes.ActiveGenerators, 0, notNull.ToString ());
			}
		}
	}
