// COMPLETED //

// Количество описателей триггер-зон
#define SD_TZ_S_Count	96

// Блок триггер-зон
union SD_TriggerZones
	{
	struct TZ_Formatted
		{
		ulong TZ_BlockSize;			// Размер блока (0x1508)
		ulong TZ_SubBlockSize;		// Размер субблока (0x1504)
		ulong TZ_ZonesCount;		// Число зон

		// Описатели триггер-зон
		struct TZ_Structure
			{
			ulong TZ_S_Type;				// Тип зоны
			float TZ_S_Timer;				// ???
			float TZ_S_X1;					// Границы зоны
			float TZ_S_Y2;
			float TZ_S_X2;
			float TZ_S_Y1;
			float TZ_S_PoliceSpawnA_X;		// Зона входа полиции
			float TZ_S_PoliceSpawnA_Y;
			float TZ_S_PoliceSpawnB_X;
			float TZ_S_PoliceSpawnB_Y;
			float TZ_S_PoliceHeadingA_X;	// Зона расположения полиции
			float TZ_S_PoliceHeadingA_Y;
			float TZ_S_PoliceHeadingB_X;
			float TZ_S_PoliceHeadingB_Y;
			} TZ_S [SD_TZ_S_Count];
		} TZ;

	uchar TZ_Raw [sizeof (struct TZ_Formatted)];
	};
