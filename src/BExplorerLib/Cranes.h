// COMPLETED //

// Количество описателей портовых кранов
#define SD_CR_S_Count	8

// Блок портовых кранов
union SD_Cranes
	{
	struct CR_Formatted
		{
		ulong CR_BlockSize;			// Размер блока (0x03EC)
		ulong CR_SubBlockSize;		// Размер субблока (0x03E8)
		ulong CR_CranesCount;		// Число кранов
		ulong CR_CarsCollected;		// Не используется

		// Описатели портовых кранов
		struct CR_Structure
			{
			ulong CR_S_CraneObjectPointer;		// Объект-кран
			ulong CR_S_MagnetObjectPointer;		// Объект-магнит
			float CR_S_PickupZoneX1;			// Границы зоны захвата авто
			float CR_S_PickupZoneX2;
			float CR_S_PickupZoneY1;
			float CR_S_PickupZoneY2;
			float CR_S_DropOffZoneX;			// Границы зоны сброса авто
			float CR_S_DropOffZoneY;
			float CR_S_DropOffZoneZ;
			float CR_S_DropOffAngle;
			float CR_S_CraneArmPickupAngle;		// Угол поворота стрелы крана при захвате
			float CR_S_CraneArmDropOffAngle;	// Угол поворота стрелы крана при сбросе
			float CR_S_CraneArmPickupDistance;	// Расстояние от стрелы крана при захвате
			float CR_S_CraneArmDropOffDistance;	// Расстояние от стрелы крана при сбросе
			float CR_S_CraneArmPickupHeight;	// Высота стрелы крана при захвате
			float CR_S_CraneArmDropOffHeight;	// Высота стрелы крана при сбросе
			float CR_S_CraneArmCurrentAngle;	// Текущий угол поворота стрелы крана
			float CR_S_CraneArmCurrentDistance;	// Текущее расстояние от стрелы крана
			float CR_S_CraneArmCurrentHeight;	// Текущая высота стрелы крана
			float CR_S_CraneHookInitialX;		// Исходные координаты крюка крана
			float CR_S_CraneHookInitialY;
			float CR_S_CraneHookInitialZ;
			float CR_S_CraneHookCurrentX;		// Текущие координаты крюка крана
			float CR_S_CraneHookCurrentY;
			float CR_S_CraneHookCurrentZ;
			uchar CR_S_Unused01[8];
			ulong CR_S_TargettedCarPointer;
			ulong CR_S_GameTimerOnLiftFinish;	// Время игры в момент завершения подъёма
			uchar CR_S_CraneActivity;			// Действие крана
			// 0 - idle, 1 - active, 2 - inactive
			uchar CR_S_CraneStatus;				// Состояние крана
			// 0 - idle, 1 - move to pickup, 3 - upward from pickup, 2 - move to destination,
			// 4 - move to drop off, 5 - move from drop off
			uchar CR_S_CollectedCarsCount;		// Количество собранный машин
			uchar CR_S_IsCrusherCrane;			// Флаг прессового крана
			uchar CR_S_IsMilitaryCrane;			// Флаг военного крана
			uchar CR_S_Unused02;
			uchar CR_S_IsNot_doc_crane_cab0;	// Флаг спецификации крана
			uchar CR_S_Unused03;
			} CR_S[SD_CR_S_Count];
		} CR;

	uchar CR_Raw [sizeof (struct CR_Formatted)];
	};
