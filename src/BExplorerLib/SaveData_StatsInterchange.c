// Отключение предупреждений
#define _CRT_SECURE_NO_WARNINGS

// Подключение общего заголовка
#include "ViceCityToolset.h"

// Макросы
#define SD_StatsUnitLoad(Unit)	SD->SD_ST.ST.Unit = stats.ST.Unit;
#define SD_StatsUnitSave(Unit,Format,Name)	stats.ST.Unit = SD->SD_ST.ST.Unit;	\
	sprintf (tmp, "%s\n", Format);	\
	fprintf (FTO, tmp, stats.ST.Unit);

// Загрузка статистики в структуру
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_LoadStats (struct SaveData *SD, schar *FilePath)
	{
	FILE *FI;
	union SD_Stats stats;
	long l;

	// Попытка открытия файла
	if ((FI = fopen (FilePath, "rb")) == NULL)
		return SD_INTRPR_ERR_StatsFileNotFound;

	// Контроль нового формата
	l = strlen (FilePath);
	if (!((FilePath[l - 3] == 'b') && (FilePath[l - 2] == 's') && (FilePath[l - 1] == 't')) &&
		!FILE_SIGNATURE_VALID (STATS_FILE_CODE))
		{
		fclose (FI);
		return SD_INTRPR_ERR_StatsFileIsIncorrect;
		}

	// Попытка считывания файла
	if (fread (stats.ST_Raw, 1, sizeof (union SD_Stats), FI) != sizeof (union SD_Stats))
		{
		fclose (FI);
		return SD_INTRPR_ERR_StatsFileIsIncorrect;
		}
	fclose (FI);

	// Перенос значений
	SD_StatsUnitLoad (ST_AutoRepairBudget)
		SD_StatsUnitLoad (ST_BestPercentageInShootist)
		SD_StatsUnitLoad (ST_BikersWasted)
		SD_StatsUnitLoad (ST_BloodringKills)
		SD_StatsUnitLoad (ST_BoatsExploded)
		SD_StatsUnitLoad (ST_BulletsFired)
		SD_StatsUnitLoad (ST_BulletsThatHit)
		SD_StatsUnitLoad (ST_CarsExploded)
		SD_StatsUnitLoad (ST_CheatRating)
		SD_StatsUnitLoad (ST_CopsWasted)
		SD_StatsUnitLoad (ST_CriminalsWasted)
		SD_StatsUnitLoad (ST_CriminalsWastedInVigilante)
		SD_StatsUnitLoad (ST_CubansWasted)
		SD_StatsUnitLoad (ST_DaysPassedInGame)
		SD_StatsUnitLoad (ST_DiazGangMembersWasted)
		SD_StatsUnitLoad (ST_DistanceOnBikeM)
		SD_StatsUnitLoad (ST_DistanceOnBoatM)
		SD_StatsUnitLoad (ST_DistanceOnCarM)
		SD_StatsUnitLoad (ST_DistanceOnFootM)
		SD_StatsUnitLoad (ST_DistanceOnGolfCartM)
		SD_StatsUnitLoad (ST_DistanceOnHelicopterM)
		SD_StatsUnitLoad (ST_DistanceOnPlaneM)
		SD_StatsUnitLoad (ST_ExplosivesKgsUsed)
		SD_StatsUnitLoad (ST_FashionBudget)
		SD_StatsUnitLoad (ST_FiresExtinguished)
		SD_StatsUnitLoad (ST_FlightMs)
		SD_StatsUnitLoad (ST_GolfersWasted)
		SD_StatsUnitLoad (ST_HaitiansWasted)
		SD_StatsUnitLoad (ST_HeadShotsCount)
		SD_StatsUnitLoad (ST_HelicoptersExploded)
		SD_StatsUnitLoad (ST_HighestMediaAttention)
		SD_StatsUnitLoad (ST_HighestScoreInShootist)
		SD_StatsUnitLoad (ST_HotringBestResult)
		SD_StatsUnitLoad (ST_Longest2WheelsDistance)
		SD_StatsUnitLoad (ST_Longest2WheelsTime)
		SD_StatsUnitLoad (ST_LongestStopieDistance)
		SD_StatsUnitLoad (ST_LongestStopieTime)
		SD_StatsUnitLoad (ST_LongestWheelieDistance)
		SD_StatsUnitLoad (ST_LongestWheelieTime)
		SD_StatsUnitLoad (ST_MaxInsaneJumpDistance)
		SD_StatsUnitLoad (ST_MaxInsaneJumpFlips)
		SD_StatsUnitLoad (ST_MaxInsaneJumpHeight)
		SD_StatsUnitLoad (ST_MaxInsaneJumpRating)
		SD_StatsUnitLoad (ST_MaxInsaneJumpRotation)
		SD_StatsUnitLoad (ST_PeopleSavedInAmbulance)
		SD_StatsUnitLoad (ST_PeopleWastedByOthers)
		SD_StatsUnitLoad (ST_Player1Wasted)
		SD_StatsUnitLoad (ST_Player2Wasted)
		SD_StatsUnitLoad (ST_Player3Wasted)
		SD_StatsUnitLoad (ST_Player4Wasted)
		SD_StatsUnitLoad (ST_MalesWasted)
		SD_StatsUnitLoad (ST_FemalesWasted)
		SD_StatsUnitLoad (ST_UnusedGangWasted)
		SD_StatsUnitLoad (ST_AmbulanceWasted)
		SD_StatsUnitLoad (ST_FirefightersWasted)
		SD_StatsUnitLoad (ST_AdditionalPedsWasted)
		SD_StatsUnitLoad (ST_ProstitutesWasted)
		SD_StatsUnitLoad (ST_ReservedPedsWasted)
		SD_StatsUnitLoad (ST_UnusedPedsWasted)
		SD_StatsUnitLoad (ST_PhotosTaken)
		SD_StatsUnitLoad (ST_PizzasDelivered)
		SD_StatsUnitLoad (ST_PropertyDestroyed)
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[0])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[1])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[2])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[3])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[4])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[5])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[6])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[7])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[8])
		SD_StatsUnitLoad (ST_RadioListetingTimeMs[9])
		SD_StatsUnitLoad (ST_RespraysCount)
		SD_StatsUnitLoad (ST_SafehousesVisits)
		SD_StatsUnitLoad (ST_SeagullsSniped)
		SD_StatsUnitLoad (ST_SecurityGuardsWasted)
		SD_StatsUnitLoad (ST_StreetWannabesWasted)
		SD_StatsUnitLoad (ST_TaxiCash)
		SD_StatsUnitLoad (ST_TaxiPassengers)
		SD_StatsUnitLoad (ST_TimesBusted)
		SD_StatsUnitLoad (ST_TimesDrowned)
		SD_StatsUnitLoad (ST_TimesWasted)
		SD_StatsUnitLoad (ST_TiresPopped)
		SD_StatsUnitLoad (ST_TopShootingRangeScore)
		SD_StatsUnitLoad (ST_TotalPedsKilled)
		SD_StatsUnitLoad (ST_TotalPeopleWasted)
		SD_StatsUnitLoad (ST_VersettiGangMembersWasted)
		SD_StatsUnitLoad (ST_WantedStarsAvoided)
		SD_StatsUnitLoad (ST_WantedStarsGot)
		SD_StatsUnitLoad (ST_WeaponBudget)
		SD_StatsUnitLoad (ST_HighestScoreInBeachBall)

		return SD_LOAD_SUCCESS;
	}

// Выгрузка статистики из структуры
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_SaveStats (struct SaveData *SD, schar *FilePath)
	{
	FILE *FO, *FTO;
	union SD_Stats stats;
	schar TextFileName[SD_MaxStrSize],
		tmp[SD_MaxStrSize];

	// Попытка открытия файлов
	if ((FO = fopen (FilePath, "wb")) == NULL)
		return SD_INTRPR_ERR_CannotCreateStatsFile;
	PUT_FILE_SIGNATURE (STATS_FILE_CODE);	// Только новый формат

	sprintf (TextFileName, "%stmp", FilePath);
	if ((FTO = fopen (TextFileName, "wb")) == NULL)
		return SD_INTRPR_ERR_CannotCreateStatsFile;

	// Перенос значений
	fprintf (FTO, "\n\n\n");

	SD_StatsUnitSave (ST_AutoRepairBudget, "%0.0f", "Бюджет ремонта авто, $");
	SD_StatsUnitSave (ST_BoatsExploded, "%u", "Взорвано лодок");
	SD_StatsUnitSave (ST_BulletsFired, "%u", "Использовано патронов");
	SD_StatsUnitSave (ST_BulletsThatHit, "%u", "Попавшие пули");
	SD_StatsUnitSave (ST_CarsExploded, "%u", "Взорвано автомобилей");
	SD_StatsUnitSave (ST_CheatRating, "%u", "Рейтинг читов");
	SD_StatsUnitSave (ST_DaysPassedInGame, "%u", "Проведено дней в игре");
	SD_StatsUnitSave (ST_ExplosivesKgsUsed, "%u", "Использовано взрывчатки, кг");
	SD_StatsUnitSave (ST_FashionBudget, "%0.0f", "Бюджет одежды, $");
	SD_StatsUnitSave (ST_FiresExtinguished, "%u", "Потушено пожаров");
	SD_StatsUnitSave (ST_HeadShotsCount, "%u", "Попаданий в голову");
	SD_StatsUnitSave (ST_HelicoptersExploded, "%u", "Взорвано вертолётов и самолётов");
	SD_StatsUnitSave (ST_HighestMediaAttention, "%0.4f", "Максимальное внимание СМИ");
	SD_StatsUnitSave (ST_PhotosTaken, "%u", "Сделано фотографий");
	SD_StatsUnitSave (ST_PropertyDestroyed, "%u", "Уничтоженное имущество, $");
	SD_StatsUnitSave (ST_RespraysCount, "%u", "Количество покрасок авто");
	SD_StatsUnitSave (ST_SafehousesVisits, "%u", "Число сохранений");
	SD_StatsUnitSave (ST_SeagullsSniped, "%u", "Убито чаек");
	SD_StatsUnitSave (ST_TimesBusted, "%u", "Смертей");
	SD_StatsUnitSave (ST_TimesDrowned, "%u", "Утоплений");
	SD_StatsUnitSave (ST_TimesWasted, "%u", "Арестов");
	SD_StatsUnitSave (ST_TiresPopped, "%u", "Простреляно шин");
	SD_StatsUnitSave (ST_TopShootingRangeScore, "%0.2f", "Максимальный результат в тире");
	SD_StatsUnitSave (ST_WantedStarsAvoided, "%u", "Звёзды розыска, которых удалось избежать");
	SD_StatsUnitSave (ST_WantedStarsGot, "%u", "Полученные звёзды розыска");
	SD_StatsUnitSave (ST_WeaponBudget, "%0.0f", "Бюджет оружия, $");

	fprintf (FTO, "\n\n");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[0], "%0.0f", "Время прослушивания радиостанции Wild, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[1], "%0.0f", "Время прослушивания радиостанции Flash FM, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[2], "%0.0f", "Время прослушивания радиостанции KChat, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[3], "%0.0f", "Время прослушивания радиостанции Fever 105 FM, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[4], "%0.0f", "Время прослушивания радиостанции VCPR, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[5], "%0.0f", "Время прослушивания радиостанции VRock, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[6], "%0.0f", "Время прослушивания радиостанции Espantoso, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[7], "%0.0f", "Время прослушивания радиостанции Emotion 98.3 FM, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[8], "%0.0f", "Время прослушивания радиостанции Wave 103 FM, мс");
	SD_StatsUnitSave (ST_RadioListetingTimeMs[9], "%0.0f", "Время прослушивания MP3-плеера, мс");

	fprintf (FTO, "\n\n");
	SD_StatsUnitSave (ST_DistanceOnBikeM, "%0.2f", "Преодолено на мотоцикле, метров");
	SD_StatsUnitSave (ST_DistanceOnBoatM, "%0.2f", "Преодолено на лодке, метров");
	SD_StatsUnitSave (ST_DistanceOnCarM, "%0.2f", "Преодолено на автомобиле, метров");
	SD_StatsUnitSave (ST_DistanceOnFootM, "%0.2f", "Пройдено пешком, метров");
	SD_StatsUnitSave (ST_DistanceOnGolfCartM, "%0.2f", "Преодолено на гольфкарте, метров");
	SD_StatsUnitSave (ST_DistanceOnHelicopterM, "%0.2f", "Преодолено на вертолёте, метров");
	SD_StatsUnitSave (ST_DistanceOnPlaneM, "%0.2f", "Преодолено на самолёте, метров");
	SD_StatsUnitSave (ST_FlightMs, "%u", "Время полёта, мс");

	fprintf (FTO, "\n\n");
	SD_StatsUnitSave (ST_Longest2WheelsDistance, "%0.0f", "Максимальная дистанция на двух колёсах, метров");
	SD_StatsUnitSave (ST_Longest2WheelsTime, "%u", "Максимальное время на двух колёсах, секунд");
	SD_StatsUnitSave (ST_LongestStopieDistance, "%u", "Максимальная дистанция на переднем колесе, метров");
	SD_StatsUnitSave (ST_LongestStopieTime, "%u", "Максимальное время на переднем колесе, секунд");
	SD_StatsUnitSave (ST_LongestWheelieDistance, "%u", "Максимальная дистанция на заднем колесе, метров");
	SD_StatsUnitSave (ST_LongestWheelieTime, "%u", "Максимальное время на заднем колесе, секунд");
	SD_StatsUnitSave (ST_MaxInsaneJumpDistance, "%0.0f", "Максимальная дистанция сумасшедшего прыжка, метров");
	SD_StatsUnitSave (ST_MaxInsaneJumpFlips, "%u", "Максимальное число переворотов в сумасшедшем прыжке");
	SD_StatsUnitSave (ST_MaxInsaneJumpHeight, "%0.0f", "Максимальная высота сумасшедшего прыжка, метров");
	SD_StatsUnitSave (ST_MaxInsaneJumpRating, "%u", "Рейтинг сумасшедшего прыжка");
	SD_StatsUnitSave (ST_MaxInsaneJumpRotation, "%u", "Максимальный разворот в сумасшедшем прыжке, градусов");

	fprintf (FTO, "\n\n");
	SD_StatsUnitSave (ST_PeopleWastedByOthers, "%u", "Убито другими людьми");
	SD_StatsUnitSave (ST_MalesWasted, "%u", "Убито мужчин");
	SD_StatsUnitSave (ST_FemalesWasted, "%u", "Убито женщин");
	SD_StatsUnitSave (ST_AmbulanceWasted, "%u", "Убито медиков");
	SD_StatsUnitSave (ST_FirefightersWasted, "%u", "Убито пожарных");
	SD_StatsUnitSave (ST_AdditionalPedsWasted, "%u", "Убито специальных персонажей");
	SD_StatsUnitSave (ST_ProstitutesWasted, "%u", "Убито проституток");
	SD_StatsUnitSave (ST_ReservedPedsWasted, "%u", "Убито специальных персонажей");
	SD_StatsUnitSave (ST_BikersWasted, "%u", "Убито байкеров");
	SD_StatsUnitSave (ST_CopsWasted, "%u", "Убито копов");
	SD_StatsUnitSave (ST_CriminalsWasted, "%u", "Убито бандитов");
	SD_StatsUnitSave (ST_CubansWasted, "%u", "Убито кубинцев");
	SD_StatsUnitSave (ST_DiazGangMembersWasted, "%u", "Убито головорезов Диаза");
	SD_StatsUnitSave (ST_GolfersWasted, "%u", "Убито гольфистов");
	SD_StatsUnitSave (ST_HaitiansWasted, "%u", "Убито гаитян");
	SD_StatsUnitSave (ST_SecurityGuardsWasted, "%u", "Убито патрульных");
	SD_StatsUnitSave (ST_StreetWannabesWasted, "%u", "Убито бандитов с острова Креветок");
	SD_StatsUnitSave (ST_VersettiGangMembersWasted, "%u", "Убито гангстеров Версетти");
	SD_StatsUnitSave (ST_TotalPedsKilled, "%u", "Всего убито людей");
	SD_StatsUnitSave (ST_TotalPeopleWasted, "%u", "Всего убито людей");

	fprintf (FTO, "\n\n");
	SD_StatsUnitSave (ST_BestPercentageInShootist, "%u", "Максимальная точность в миссии Shootist");
	SD_StatsUnitSave (ST_BloodringKills, "%u", "Убито противников в Blood ring");
	SD_StatsUnitSave (ST_CriminalsWastedInVigilante, "%u", "Убито преступников в миссии Vigilante");
	SD_StatsUnitSave (ST_HighestScoreInShootist, "%u", "Максимальный результат в миссии Shootist");
	SD_StatsUnitSave (ST_HotringBestResult, "%u", "Лучший результат в миссии Hot ring");
	SD_StatsUnitSave (ST_PeopleSavedInAmbulance, "%u", "Спасено людей в медицинской миссии");
	SD_StatsUnitSave (ST_PizzasDelivered, "%0.0f", "Доставлено пицц");
	SD_StatsUnitSave (ST_TaxiCash, "%u", "Заработок таксистом, $");
	SD_StatsUnitSave (ST_TaxiPassengers, "%u", "Доставлено пассажиров");
	SD_StatsUnitSave (ST_HighestScoreInBeachBall, "%u", "Максимальное число набиваний головой в пляжном футболе");

	// Запись и завершение
	fwrite (stats.ST_Raw, 1, sizeof (union SD_Stats), FO);
	fclose (FO);
	fclose (FTO);

	return SD_SAVE_SUCCESS;
	}
