// COMPLETED //

// Количество описателей парковок
#define SD_CG_S_Count	185

// Блок парковок
union SD_CarGenerators
	{
	struct CG_Formatted
		{
		ulong CG_BlockSize;			// Размер блока (0x1FEC)
		ulong CG_SubBlockSize;		// Размер субблока (0x1FE8)
		uchar CG_BlockName[4];		// Название блока (CGN)
		ulong CG_SubBlock2Size;		// Размер следующей части блока
		ulong CG_CountsBlockSize;	// Размер блока количеств авто
		ulong CG_CarsCount;			// Количество генераторов
		ulong CG_ActiveCarCount;	// Количество активных генераторов
		uchar CG_GeneratorCounter;	// Состояние счётчика генерации
		uchar CG_GenerateWhenCounterClosed;
		uchar CG_Unused01[2];
		ulong CG_SubBlock3Size;		// Размер следующей части блока

		// Описатели парковок
		struct CG_Structure
			{
			slong CG_S_ModelID;			// Модель авто (-1 = случайная)
			float CG_S_X;				// Положение
			float CG_S_Y;
			float CG_S_Z;
			float CG_S_Rotation;		// Поворот
			sint CG_S_PrimaryColor;		// Цвета
			sint CG_S_SecondaryColor;
			uchar CG_S_ForceSpawn;		// Обязательная генерация
			uchar CG_S_Alarm;			// Вероятность срабатывания сигнализации
			uchar CG_S_Lock;			// Вероятность блокировки
			uchar CG_S_Unused01;
			uint CG_S_MinDelay;			// Диапазон времени генерации
			uint CG_S_MaxDelay;
			ulong CG_S_GameTimerWhenGenerated;	// Время игры в момент генерации
			slong CG_S_CarIndex;
			sint CG_S_AllowSpawn;		// Доступность (-1 - доступна, 0 - недоступна)
			uchar CG_S_HasBeenStolen;	// Уже украдена или нет
			uchar CG_S_Unused02;
			} CG_S[SD_CG_S_Count];
		} CG;

	uchar CG_Raw [sizeof (struct CG_Formatted)];
	};
