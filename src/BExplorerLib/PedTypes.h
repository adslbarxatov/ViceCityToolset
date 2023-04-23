// COMPLETED //

// Количество описателей типов персонажей
#define SD_PT_S_Count	23

// Блок типов персонажей
union SD_PedTypes
	{
	struct PT_Formatted
		{
		ulong PT_BlockSize;		// Размер блока (0x02EC)
		ulong PT_SubBlockSize;	// Размер субблока (0x02E8)
		uchar PT_BlockName[4];	// Название блока (PTP)
		ulong PT_SubBlock1Size;	// Размер оставшегося субблока (0x02E0)

		// Описатели типов персонажей (содержимое файла ped.dat)
		struct PT_Structure
			{
			ulong PT_S_Bitmask;
			float PT_S_2ndColumnDiv50;	// Столбцы файла
			float PT_S_3rdColumnDiv50;
			float PT_S_4thColumnDiv50;
			float PT_S_5thColumn;
			float PT_S_6thColumn;
			ulong PT_S_ThreatFlags;
			ulong PT_S_AvoidFlags;
			} PT_S[SD_PT_S_Count];

		ulong LastAlignBlockSize;
		} PT;

	uchar PT_Raw [sizeof (struct PT_Formatted)];
	};
