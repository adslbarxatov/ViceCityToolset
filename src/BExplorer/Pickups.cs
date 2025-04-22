using System;
using System.Collections.Generic;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс отвечает за обработку массива собираемых объектов
	/// (Пока доступна только работа с денежными накопителями)
	/// </summary>
	public class Pickups
		{
		/// <summary>
		/// Возвращает количество доступных накопителей
		/// </summary>
		public uint ActiveAssetsCount
			{
			get
				{
				return (uint)pickups.Count;
				}
			}

		/// <summary>
		/// Класс описывает отдельный собираемый объект
		/// </summary>
		public class Pickup
			{
			/// <summary>
			/// Возвращает номер объекта в исходном массиве
			/// </summary>
			public uint PickupNumber
				{
				get
					{
					return pickupNumber;
					}
				}
			private uint pickupNumber;

			/// <summary>
			/// Возвращает модель собираемого объекта
			/// </summary>
			public UInt16 ObjectModel
				{
				get
					{
					return objectModel;
					}
				}
			private UInt16 objectModel;

			/// <summary>
			/// Возвращает координату x собираемого объекта
			/// </summary>
			public float ObjectX
				{
				get
					{
					return objectX;
					}
				}
			private float objectX;

			/// <summary>
			/// Возвращает координату y собираемого объекта
			/// </summary>
			public float ObjectY
				{
				get
					{
					return objectY;
					}
				}
			private float objectY;

			/// <summary>
			/// Возвращает координату z собираемого объекта
			/// </summary>
			public float ObjectZ
				{
				get
					{
					return objectZ;
					}
				}
			private float objectZ;

			/// <summary>
			/// Возвращает тип собираемого объекта
			/// </summary>
			public UInt16 ObjectType
				{
				get
					{
					return objectType;
					}
				}
			private UInt16 objectType;

			/// <summary>
			/// Возвращает или задаёт максимальное значение выдаваемых/взымаемых денег/патронов
			/// </summary>
			public UInt32 ObjectAsset
				{
				get
					{
					return objectAsset;
					}
				set
					{
					objectAsset = value;
					}
				}
			private UInt32 objectAsset;

			/// <summary>
			/// Возвращает состояние флага "объект собран"
			/// </summary>
			public UInt16 HasBeenPickedUp
				{
				get
					{
					return hasBeenPickedUp;
					}
				}
			private UInt16 hasBeenPickedUp;

			/// <summary>
			/// Конструктор. Инициализирует собираемый объект
			/// </summary>
			public Pickup (uint Number, UInt16 Model, float X, float Y, float Z, UInt16 Type, UInt32 Asset,
				UInt16 PickedUp)
				{
				pickupNumber = Number;
				objectModel = Model;
				objectX = X;
				objectY = Y;
				objectZ = Z;
				objectType = Type;
				objectAsset = Asset;
				hasBeenPickedUp = PickedUp;
				}
			}

		// Массив экземпляров накопителей
		private List<Pickup> pickups = [];
		private const uint PickupsCount = 336;

		/// <summary>
		/// Конструктор. Загружает массив накопителей
		/// </summary>
		public Pickups ()
			{
			// Загрузка только денежных накопителей
			for (int i = 0; i < PickupsCount; i++)
				{
				if (decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base + i),
						(uint)PickupsParCodes.ObjectType)) == 16)
					{
					pickups.Add (new Pickup ((uint)i,
						(UInt16)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base + i),
						(uint)PickupsParCodes.ObjectModel)),
						float.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base + i),
						(uint)PickupsParCodes.ObjectX)),
						float.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base + i),
						(uint)PickupsParCodes.ObjectY)),
						float.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base + i),
						(uint)PickupsParCodes.ObjectZ)),
						(UInt16)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base + i),
						(uint)PickupsParCodes.ObjectType)),
						(UInt32)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base + i),
						(uint)PickupsParCodes.ObjectAsset)),
						(UInt16)decimal.Parse (BExplorerLib.SaveData_GetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base + i),
						(uint)PickupsParCodes.HasBeenPickedUp))));
					}
				}
			}

		/// <summary>
		/// Метод возвращает максимальное значение накопителя
		/// </summary>
		/// <param name="Index">Индекс накопителя</param>
		/// <returns>Максимальное значение или 0 в случае, если указан несуществующий накопитель</returns>
		public UInt32 GetAssetMaximum (int Index)
			{
			if (Index < pickups.Count)
				return pickups[Index].ObjectAsset;

			return 0;
			}

		/// <summary>
		/// Метод возвращает координату x накопителя
		/// </summary>
		/// <param name="Index">Индекс накопителя</param>
		/// <returns>Координата или NaN, если указан несуществующий накопитель</returns>
		public float GetAssetX (int Index)
			{
			if (Index < pickups.Count)
				return pickups[Index].ObjectX;

			return float.NaN;
			}

		/// <summary>
		/// Метод возвращает координату y накопителя
		/// </summary>
		/// <param name="Index">Индекс накопителя</param>
		/// <returns>Координата или NaN, если указан несуществующий накопитель</returns>
		public float GetAssetY (int Index)
			{
			if (Index < pickups.Count)
				return pickups[Index].ObjectY;

			return float.NaN;
			}

		/// <summary>
		/// Метод возвращает координату z накопителя
		/// </summary>
		/// <param name="Index">Индекс накопителя</param>
		/// <returns>Координата или NaN, если указан несуществующий накопитель</returns>
		public float GetAssetZ (int Index)
			{
			if (Index < pickups.Count)
				return pickups[Index].ObjectZ;

			return float.NaN;
			}

		/// <summary>
		/// Метод устанавливает новое максимальное значение накопителя
		/// </summary>
		/// <param name="Index">Индекс накопителя</param>
		/// <param name="NewValue">Новое значение</param>
		public void SetAssetMaximum (int Index, UInt32 NewValue)
			{
			if (Index < pickups.Count)
				{
				pickups[Index].ObjectAsset = NewValue;
				BExplorerLib.SaveData_SetParameterValue ((OpCodes)((int)OpCodes.Pickups_Base +
					pickups[Index].PickupNumber),
					(uint)PickupsParCodes.ObjectAsset, NewValue.ToString ());
				}
			}
		}
	}
