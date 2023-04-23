// COMPLETED //

// Количество описателей банд
#define SD_GD_S_Count	9

// Блок банд
union SD_GangData
	{
	struct GD_Formatted
		{
		ulong GD_BlockSize;			// Размер блока (0x00E4)
		ulong GD_SubBlockSize;		// Размер субблока (0x00E0)
		uchar GD_BlockName[4];		// Название блока (GNG)
		ulong GD_SubBlock1Size;		// Размер оставшегося блока (0x00D8)

		// Описатели банд
		struct GD_Structure
			{
			slong GD_S_CarModel;		// Авто банды (-1 = нет авто)
			ulong GD_S_PedModel1;		// Первый тип представителя банды
			ulong GD_S_PedModel2;		// Второй тип представителя банды
			schar GD_S_PedModelPreference;	// Приоритетный тип
			uchar GD_S_Unused1[3];
			ulong GD_S_WeaponNumber1;	// Оружие 1
			ulong GD_S_WeaponNumber2;	// Оружие 2
			} GD_S [SD_GD_S_Count];
		} GD;

	uchar GD_Raw [sizeof (struct GD_Formatted)];
	};
