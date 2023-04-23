// COMPLETED //

// Количество точек создания такси
#define SD_TS_SC_Count	16

// Блок описателей точек создания такси
union SD_TaxiShortcuts
	{
	struct TS_Formatted
		{
		ulong TS_BlockSize;				// Размер блока (0x0108)
		ulong TS_SubBlockSize;			// Размер субблока (0x0104)
		ulong TS_TaxiShortcutsCount;	// Число точек прибытия такси

		// Описатели точек создания такси
		struct TS_Shortcut
			{
			float TS_SC_X;			// Положение
			float TS_SC_Y;
			float TS_SC_Z;
			float TS_SC_Rotation;	// Поворот
			} TS_SC[SD_TS_SC_Count];
		} TS;

	uchar TS_Raw [sizeof (struct TS_Formatted)];
	};
