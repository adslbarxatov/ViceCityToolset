// COMPLETED //

// Блок статистики игры
union SD_Stats
	{
	struct ST_Formatted
		{
		ulong ST_BlockSize;				// Размер блока (0x258)
		ulong ST_SubBlockSize;			// Размер субблока (0x253)
		ulong ST_TotalPeopleWasted;		// Всего убито людей
		ulong ST_PeopleWastedByOthers;	// Убито другими
		ulong ST_CarsExploded;			// Взорвано машин
		ulong ST_BoatsExploded;			// Взорвано лодок
		ulong ST_TiresPopped;			// Пробито шин
		ulong ST_BulletsFired;			// Использовано пуль

		ulong ST_Player1Wasted;			// Убито людей (по группам)
		ulong ST_Player2Wasted;
		ulong ST_Player3Wasted;
		ulong ST_Player4Wasted;
		ulong ST_MalesWasted;
		ulong ST_FemalesWasted;
		ulong ST_CopsWasted;
		ulong ST_CubansWasted;
		ulong ST_HaitiansWasted;
		ulong ST_StreetWannabesWasted;
		ulong ST_DiazGangMembersWasted;
		ulong ST_SecurityGuardsWasted;
		ulong ST_BikersWasted;
		ulong ST_VersettiGangMembersWasted;
		ulong ST_GolfersWasted;
		ulong ST_UnusedGangWasted;
		ulong ST_AmbulanceWasted;
		ulong ST_FirefightersWasted;
		ulong ST_CriminalsWasted;
		ulong ST_AdditionalPedsWasted;
		ulong ST_ProstitutesWasted;
		ulong ST_ReservedPedsWasted;
		ulong ST_UnusedPedsWasted;

		ulong ST_HelicoptersExploded;	// Взорвано вертолётов
		float ST_ProgressMade;			// Прохождение игры
		float ST_TotalProgress;			// Точка завершения игры
		ulong ST_ExplosivesKgsUsed;		// Использовано килограммов взрывчатки
		ulong ST_BulletsThatHit;		// Пули, попавшие в цель
		ulong ST_HeadShotsCount;		// Количество попаданий в голову
		ulong ST_WantedStarsGot;		// Полученные звёзды розыска
		ulong ST_WantedStarsAvoided;	// Звёзды, которых удалось избежать
		ulong ST_TimesBusted;			// Арестов
		ulong ST_TimesWasted;			// Смертей
		ulong ST_DaysPassedInGame;		// Дней в игре
		ulong ST_SafehousesVisits;		// Сохранений
		ulong ST_RespraysCount;			// Количество покрасок
		float ST_MaxInsaneJumpDistance;	// Максимальная дистанция "сумасшедшего прыжка"
		float ST_MaxInsaneJumpHeight;	// Максимальная высота "сумасшедшего прыжка"
		ulong ST_MaxInsaneJumpFlips;	// Максимальное число кувырков в "сумасшедшем прыжке"
		ulong ST_MaxInsaneJumpRotation;	// Максимальный угол вращения "сумасшедшего прыжка"
		ulong ST_MaxInsaneJumpRating;	// Рейтинг "сумасшедшего прыжка"
		ulong ST_ToDoUniqueJumpsPassed;	// Выполнено уникальных прыжков
		ulong ST_UniqueJumpsCount;		// Количество уникальных прыжков
		ulong ST_MissionAttempts;		// Входов в миссии
		ulong ST_TaxiPassengers;		// Доставленных пассажиров в такси
		ulong ST_TaxiCash;				// Выручка в такси
		uchar ST_Unused01[12];
		ulong ST_PornLeafletsVisible;	// Видимость порнопостеров после миссии Dildo dodo
		ulong ST_AllowSecondaryWeatherTable;	// Разрешение второго файла timecyc
		float ST_DistanceOnFootM;		// Пройдено пешком, метров
		float ST_DistanceOnCarM;		// Преодолено на машинах, метров
		float ST_DistanceOnBikeM;		// Преодолено на байках, метров
		float ST_DistanceOnBoatM;		// Преодолено на лодках, метров
		float ST_DistanceOnGolfCartM;	// Преодолено на гольфкарте, метров
		float ST_DistanceOnHelicopterM;	// Преодолено на вертолётах, метров
		float ST_DistanceOnPlaneM;		// Преодолено на самолётах, метров
		ulong ST_PeopleSavedInAmbulance;	// Спасено людей в медицинской миссии
		ulong ST_CriminalsWastedInVigilante;	// Уничтожено людей в полицейской миссии
		ulong ST_FiresExtinguished;		// Потушено пожаров
		ulong ST_ToDoHighestVigilanteLevel;	// Наивысший уровень полицейской миссии
		ulong ST_ToDoHighestAmbulanceLevel;	// Наивысший уровень медицинской миссии
		ulong ST_ToDoHighestFirefighterLevel;	// Наивысший уровень пожарной миссии
		ulong ST_PhotosTaken;			// Сделано фотографий
		ulong ST_ToDoRampagesPassed;	// Пройдено буйств
		ulong ST_RampagesCount;			// Всего буйств
		ulong ST_MainMissionsCount;		// Количество главных миссий
		ulong ST_FlightMs;				// Суммарное время полётов, в миллисекундах
		ulong ST_TimesDrowned;			// Количество утоплений
		ulong ST_SeagullsSniped;		// Убитые чайки
		float ST_WeaponBudget;			// Бюджет оружия
		float ST_FashionBudget;			// Бюджет одежды
		float ST_LoanSharksVisits;		// Число визитов подпольных торговцев
		float ST_ToDoStoresKnockedOff;		// Ограбленные магазины
		float ST_MovieStunts;			// Киношные трюки
		float ST_ToDoAsassinationContractsDone;	// Выполнено контрактов на убийство
		float ST_PizzasDelivered;		// Доставлено пицц
		float ST_GarbagePickupsMade;	// Вывезено мусора
		float ST_IceCreamSold;			// Продано мороженного
		float ST_TopShootingRangeScore;	// Максимальные очки в тире
		float ST_ShootingAccuracy;		// Точностьстрельбы в тире
		ulong ST_LongestWheelieTime;	// Максимальное время на заднем колесе
		ulong ST_LongestStopieTime;		// Максимальное время на переднем колесе
		ulong ST_Longest2WheelsTime;	// Максимальное время на двух колёсах
		ulong ST_LongestWheelieDistance;	// Максимальная дистанция на заднем колесе
		ulong ST_LongestStopieDistance;		// Максимальная дистанция на переднем колесе
		float ST_Longest2WheelsDistance;	// Максимальная дистанция на двух колёсах
		float ST_PropertyBudget;		// Бюджет недвижимости
		float ST_AutoRepairBudget;		// Бюджет ремонта авто
		ulong ST_PropertyDestroyed;		// Ущерб окружающему имуществу
		ulong ST_ToDoOwnedPropertyCount;	// Количество приобретённых объектов недвижимости
		ulong ST_BloodringKills;		// Убийств в Blood ring
		ulong ST_ToDoLongestTimeInBlooding;	// Максимальное время в Blood ring
		uchar ST_OwnedProperties[15];	// Флаги, указывающие приобретённые объекты недвижимости
		float ST_HighestMediaAttention;	// Максимальное внимание СМИ
		ulong ST_BikersRaceRecord;		// Рекорд миссии с гонкой байкеров
		ulong ST_HilaryRaceRecord;		// Рекорд миссии с гонкой с Хилари
		ulong ST_ToDoDirtringRecord;	// Минимальное время в миссии Dirtring
		ulong ST_ToDoRCPlaneRecord;		// Минимальное время в миссии с RCPlane
		ulong ST_ToDoRCCarRecord;		// Минимальное время в миссии с RCCar
		ulong ST_ToDoRCHelicopterRecord;	// Минимальное время в миссии с RCHelicopter
		ulong ST_ToDoSunshineTrack1Record;	// Минимальное время в трассе №1 Sunshine
		ulong ST_ToDoSunshineTrack2Record;	// Минимальное время в трассе №2 Sunshine
		ulong ST_ToDoSunshineTrack3Record;	// Минимальное время в трассе №3 Sunshine
		ulong ST_ToDoSunshineTrack4Record;	// Минимальное время в трассе №4 Sunshine
		ulong ST_ToDoSunshineTrack5Record;	// Минимальное время в трассе №5 Sunshine
		ulong ST_ToDoSunshineTrack6Record;	// Минимальное время в трассе №6 Sunshine
		ulong ST_ToDoDowntownChopperRecord;	// Минимальное время в миссии Downtown chopper
		ulong ST_ToDoOceanBeachChopperRecord;	// Минимальное время в миссии Ocean beach chopper
		ulong ST_ToDoVicePointChopperRecord;	// Минимальное время в миссии Vice point chopper
		ulong ST_ToDoLittleHaitiChopperRecord;	// Минимальное время в миссии Little Haiti chopper
		ulong ST_ToDoPCJPlaygroundRecord;	// Минимальное время в миссии PCJ playground
		ulong ST_TrialByDirtRecord;		// Минимальное время в миссии Trial by dirt
		ulong ST_TestTrackRecord;		// Минимальное время в миссии Test track
		ulong ST_ToDoConeCrazyRecord;	// Минимальное время в миссии Cone crazy
		ulong ST_ToDoHotringRecord;		// Минимальное время в миссии Hotring
		ulong ST_HotringLapRecord;		// Минимальное время круга в миссии Hotring (в миллисекундах)
		ulong ST_ToDoCheckpointCharlieRecord;	// Минимальное время в миссии Checkpoint Charlie
		ulong ST_HighestScoreInShootist;	// Максимальные очки в Shootist
		ulong ST_BestPercentageInShootist;	// Максимальная точность в Shootist
		ulong ST_DrugDealsMade;			// Выполнено наркосделок
		ulong ST_HighestScoreInBeachBall;	// Максимальные очки в пляжном футболе
		uchar ST_Unused02[4];
		ulong ST_HotringBestResult;		// Лучший результат в Hotring
		ulong ST_PedsKilledInGame;		// Убито людей во время игры (при сохранении всегда 0)
		ulong ST_TotalPedsKilled;		// Всего убийств ???
		uchar ST_LastPassedMissionGXTEntry[8];	// Последняя точка входа в базу текстовых строк
		ulong ST_CheatRating;			// Счётчик читов
		float ST_RadioListetingTimeMs[10];	// Время прослушивания радиостанций (для каждой)
		uchar ST_Unused03;
		} ST;

	uchar ST_Raw [sizeof (struct ST_Formatted)];
	};
