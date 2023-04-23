// INCOMPLETED //

// Количество описателей машин в гаражах
#define SD_GR_GC_Count	48

// Количество описателей гаражей
#define SD_GR_GS_Count	32

// Блок описателей гаражей
union SD_Garages
	{
	struct GR_Formatted
		{
		ulong GR_BlockSize;				// Размер блока (0x1EC8)
		ulong GR_SubBlockSize;			// Размер субблока (0x1EC4)
		ulong GR_NumberOfGarages;		// Число активных гаражей
		ulong GR_FreeBombs;				// Бесплатная установка бомб, 0 или 1
		ulong GR_FreeRespray;			// Бесплатная покраска, 0 или 1
		ulong GR_CarsCollected;			// Счётчики коллекций (не используются)
		ulong GR_SecuricarsCollected;
		ulong GR_PoliceCarsCollected;
		ulong GR_SunGarage1Status;		// Автомобили Sunshine; 000001b = авто #1 получено, 100100b = получены авто #3 и #6
		ulong GR_SunGarage2Status;		// ^ Гараж 2
		ulong GR_SunGarage3Status;		// ^ Гараж 3
		ulong GR_SunGarage4Status;		// ^ Гараж 4
		ulong GR_GarageFullMsgLastShow;	// Время последнего отображения сообщения о полном гараже

		// Структуры машин в гаражах
		struct GarageCars
				{
				ulong GR_GC_ModelID;		// Модель авто
				float GR_GC_X;				// Положение авто в гараже
				float GR_GC_Y;
				float GR_GC_Z;
				float GR_GC_vX;				// Поворот авто
				float GR_GC_vY;
				float GR_GC_vZ;
				ulong GR_GC_Immunity;		// Защита: b3 = damageproof, b2 = explosionproof, b1 = fireproof, b0 = bulletproof
				uchar GR_GC_PrimaryColor;	// Цвета авто
				uchar GR_GC_SecondaryColor;
				uchar GR_GC_RadioStation;	// Текущая радостанция (0 - 9, 10 - выключено)
				uchar GR_GC_Variation1;		// Конструктивные особенности
				uchar GR_GC_Variation2;
				uchar GR_GC_BombType;		// Тип мины (0 - нет, 1 - часовая, 2 - разгонная, 3 - радиоуправляемая, 4 - часовая включённая, 5 - разгонная включённая)
				uint GR_GC_Unused01;
				} GR_GC[SD_GR_GC_Count];

		// Структура гаражей
		struct GarageStructure
			{
			uchar GR_GS_Type;			// Тип гаража
			// 2 - Гараж для установки мин с таймером
			// 3 - Гараж для установки мин со взрывом при разгоне
			// 4 - Гараж для установки мин на радиоуправлении
			// 5 - Гараж для покраски
			// 7 - Пожиратель любых машин без вознаграждения
			// 8-10 - Sunshine auto collection 1-3
			// 16 - El Swanko Casa
			// 17, 18 - Human Condo 1, 2
			// 19 - Copland garage
			// 22 - Sunshine auto collection 4
			// 23 - 'Sir, yes, sir' garage
			// 24 - Human Condo 3
			// 25 - Ocean Heights
			// 26 - Links View
			// 27-30 - Sunshine auto 1-4
			// 31 - Versetti Estate
			// 32 - unused save garage
			uchar GR_GS_Status;				// Состояние: 0 - закрыт, 1 - открыт, 2 - закрывается, 3 - открывается,
											// 4 - открыт после установки бомбы или покраски, 5 - закрыт с удалённой машиной
			uchar GR_GS_MaxCarsInside;		// Максимум авто в гараже
			uchar GR_GS_Unused01;
			uchar GR_GS_MissionKeepCar;		// Машина сохраняется для миссии
			uchar GR_GS_IsDeactive;			// Гараж неактивен
			uchar GR_GS_ResprayHappened;	// Флаг состоявшейся покраски
			uchar GR_GS_Unused02;
			ulong GR_GS_TargetCarModel;		// Целевая модель авто (для миссий)
			ulong GR_GS_DoorObjectPointer;	// Указатель на объект-дверь
			ulong GR_GS_CrusherTopObjectPointer;	// Указатель на объект-пресс (не используется)
			uchar GR_GS_Unused03[5];
			uchar GR_GS_SwingDoor;			// Вид двери: поворотная (1) или подъёмная (0)
			uchar GR_GS_CameraFollow;		// Следование камеры (0) или вид снаружи (1)
			uchar GR_GS_Unused04;
			float GR_GS_EntranceBotLeft_X;	// Расположение внутреннего пространства
			float GR_GS_EntranceBotLeft_Y;
			float GR_GS_EntranceBotLeft_Z;
			float GR_GS_QuatRot_X;			// Поворот пространства
			float GR_GS_QuatRot_Y;
			float GR_GS_QuatRot_Z;
			float GR_GS_QuatRot_W;
			float GR_GS_CeilingZCoord;		// Высота подъёма двери
			uchar GR_GS_Unused05[8];
			float GR_GS_LowerX;				// Размерные координаты
			float GR_GS_UpperX;
			float GR_GS_LowerY;
			float GR_GS_UpperY;
			float GR_GS_DoorCurrentHeight;	// Отстояние двери в закрытом и открытом состоянии от низа гаража
			float GR_GS_DoorMaxHeight;
			float GR_GS_DoorX;				// Координаты двери (0, если двери нет)
			float GR_GS_DoorY;
			float GR_GS_CrusherTopX;		// Координаты пресса (не используются)
			float GR_GS_CrusherTopY;
			float GR_GS_DoorZ;				// Координаты двери
			float GR_GS_CrusherTopZ;		// Координаты пресса (не используются)
			ulong GR_GS_TimeToOpenDoor;		// Время до открытия двери
			ulong GR_GS_TargettedCar;		// Целевая машина (указатель)
			uchar GR_GS_Unknown01[44];
			} GR_GS[SD_GR_GS_Count];

		ulong GR_Unknown01[536 / 4];
		} GR;

	uchar GR_Raw [sizeof (struct GR_Formatted)];
	};
