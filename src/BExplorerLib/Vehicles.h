// INCOMPLETED //

// Размер структуры описания обычного авто
#define SD_VS_GVS_Size 0x5E6

// Размер структуры описания лодки
#define SD_VS_BoS_Size 0x4CA

// Размер структуры описания мотоцикла
#define SD_VS_BiS_Size 0x4F6

// Общие параметры массива транспортных средств
union SD_Vehicles
	{
	struct VH_Formatted
		{
		ulong VH_BlockSize;				// Размер блока
		ulong VH_SubBlockSize;			// Размер субблока

		ulong VH_GeneralVehiclesCount;	// Число основных машин
		ulong VH_BoatsCount;			// Число лодок
		ulong VH_BikesCount;			// Число мотоциклов
		} VH;

	uchar VH_Raw [sizeof (struct VH_Formatted)];
	};

// Массив транспортных средств
union SD_VehicleStructure
	{
	struct VS_Formatted
		{
		ulong VS_VehicleType;		// Тип транспорта: 0 - general, 1 - boat, 5 - bike
		uint VS_VehicleID;			// Объект-транспорт
		ulong VS_VehicleIndex;		// Индекс объекта
		uchar VS_Unknown01[42];
		float VS_X;					// Координаты объекта
		float VS_Y;
		float VS_Z;
		uchar VS_Unknown02[28];
		uint VS_ModelNumber;
		uchar VS_Unknown03[SD_VS_GVS_Size /* max */ - 94];
		} VS;

	uchar VS_Raw [1];
	};
