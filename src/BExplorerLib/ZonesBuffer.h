// COMPLETED //

// Количество зон типа "navig"
#define SD_ZB_Navig_Count		20

// Количество зон типа "info"
#define SD_ZB_Info_Count		169

// Количество зон типа "peds, cars, gangs"
#define SD_ZB_PCG_Info_Count	338

// Количество зон типа "map"
#define SD_ZB_Map_Count			39

// Блок зон карты
union SD_ZonesBuffer
	{
	struct ZB_Formatted
		{
		ulong ZB_BlockSize;			// Размер блока (0x8BE4)
		ulong ZB_SubBlockSize;		// Размер субблока (0x8BE0)
		uchar ZB_BlockName[4];		// Название блока (ZNS)
		ulong ZB_SubBlock1Size;		// Размер оставшегося субблока (0x8BD8)
		ulong ZB_CurrentLevel;		// Суперзона: 1 - beach, 2 - mainland
		uchar ZB_Unused01[4];

		// Описатели зон типа "navig"
		struct ZB_Zone
			{
			uchar ZB_Z_Name[8];				// Название зоны
			float ZB_Z_X1;					// Крайние точки бокса зоны
			float ZB_Z_Y1;
			float ZB_Z_Z1;
			float ZB_Z_X2;
			float ZB_Z_Y2;
			float ZB_Z_Z2;
			ulong ZB_Z_Type;				// Тип зоны (0, 1 - navig, 2 - info, 3 - map)
			ulong ZB_Z_Level;				// Суперзона
			uint ZB_Z_ZoneInfoIDNight;		// ???
			uint ZB_Z_ZoneInfoIDDay;		// ???
			slong ZB_Z_ChildZoneIndex;		// Связанные зоны
			slong ZB_Z_ParentZoneIndex;		// Принадлежность зоны
			uchar ZB_Z_Unused01[4];
			} ZB_NavigZones[SD_ZB_Navig_Count];	// Содержимое navig.zon

		// Описатели зон типа "info"
		struct ZB_Zone ZB_InfoZones[SD_ZB_Info_Count];	// Содержимое info.zon

		// Описатели зон типа "peds, cars, gangs"
		struct ZB_ZoneInfo
			{
			uint ZB_ZI_InCarPedDensity;
			uint ZB_ZI_CarClass0Density;	// Normal
			uint ZB_ZI_CarClass0to1Density;	// Poorfamily
			uint ZB_ZI_CarClass0to2Density;	// Richfamily
			uint ZB_ZI_CarClass0to3Density;	// Executive
			uint ZB_ZI_CarClass0to4Density;	// Worker
			uint ZB_ZI_CarClass0to5Density;	// Big
			uint ZB_ZI_CarClass0to6Density;	// Taxi
			uint ZB_ZI_CarClass0to7Density;	// Moped
			uint ZB_ZI_CarClass0to8Density;	// Motorbike
			uint ZB_ZI_CarClass9Density;	// Leisureboat
			uint ZB_ZI_CarClass9to10Density;// Workerboat
			uint ZB_ZI_InCarCopAndGang0Density;
			uint ZB_ZI_InCarCopAndGang0to1Density;
			uint ZB_ZI_InCarCopAndGang0to2Density;
			uint ZB_ZI_InCarCopAndGang0to3Density;
			uint ZB_ZI_InCarCopAndGang0to4Density;
			uint ZB_ZI_InCarCopAndGang0to5Density;
			uint ZB_ZI_InCarCopAndGang0to6Density;
			uint ZB_ZI_InCarCopAndGang0to7Density;
			uint ZB_ZI_InCarCopAndGang0to8Density;
			uint ZB_ZI_InCarCopDensity;
			uint ZB_ZI_OnFootPedDestiny;
			uint ZB_ZI_OnFootCopAndGang0Density;
			uint ZB_ZI_OnFootCopAndGang0to1Density;
			uint ZB_ZI_OnFootCopAndGang0to2Density;
			uint ZB_ZI_OnFootCopAndGang0to3Density;
			uint ZB_ZI_OnFootCopAndGang0to4Density;
			uint ZB_ZI_OnFootCopAndGang0to5Density;
			uint ZB_ZI_OnFootCopAndGang0to6Density;
			uint ZB_ZI_OnFootCopAndGang0to7Density;
			uint ZB_ZI_OnFootCopAndGang0to8Density;
			uint ZB_ZI_OnFootCopDensity;
			uint ZB_ZI_PedGroup;
			} ZB_PedCarGangInfoZones[SD_ZB_PCG_Info_Count];

		uint ZB_NavigZonesCount;			// Количество зон navig
		uint ZB_InfoZonesCount;				// Количество зон info
		uint ZB_InfoZonesCountMul;			// Количество зон info * 2 + 1
		uchar ZB_Unused2[2];
		
		// Описатели зон типа "map"
		struct ZB_Zone ZB_MapZones[SD_ZB_Map_Count];	// map.zon

		sint ZB_CrimeReportAudioZones[14];	// Зоны, звучащие по радио при интересе полиции
		uint ZB_MapZonesCount;				// Количество зон map
		uint ZB_CrimeReportAudioZonesCount;	// Количество зон CrimeReport
		} ZB;

	uchar ZB_Raw [sizeof (struct ZB_Formatted)];
	};
