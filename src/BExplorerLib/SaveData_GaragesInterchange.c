// Отключение предупреждений
#define _CRT_SECURE_NO_WARNINGS

// Подключение общего заголовка
#include "ViceCityToolset.h"

// Загрузка статистики в структуру
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_LoadGarages (struct SaveData *SD, schar *FilePath)
	{
	FILE *FI;
	struct GarageCars cg[SD_GR_GC_Count];
	schar *cgp, *sdp;

	// Попытка открытия файла
	if ((FI = fopen (FilePath, "rb")) == NULL)
		{
		return SD_INTRPR_ERR_GaragesFileNotFound;
		}

	// Попытка считывания файла
	cgp = (schar *)cg;
	if (fread (cgp, 1, sizeof (struct GarageCars) * SD_GR_GC_Count, FI) !=
		sizeof (struct GarageCars) * SD_GR_GC_Count)
		{
		fclose (FI);
		return SD_INTRPR_ERR_GaragesFileIsIncorrect;
		}
	fclose (FI);

	// Перенос данных
	sdp = (schar *)SD->SD_GR.GR.GR_GC;
	memcpy (sdp, cgp, sizeof (struct GarageCars) * SD_GR_GC_Count);

	return SD_LOAD_SUCCESS;
	}

// Выгрузка статистики из структуры
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_SaveGarages (struct SaveData *SD, schar *FilePath)
	{
	FILE *FO;
	struct GarageCars cg[SD_GR_GC_Count];
	schar *cgp, *sdp;

	// Попытка открытия файлов
	if ((FO = fopen (FilePath, "wb")) == NULL)
		{
		return SD_INTRPR_ERR_CannotCreateGaragesFile;
		}

	// Перенос значений
	cgp = (schar *)cg;
	sdp = (schar *)SD->SD_GR.GR.GR_GC;
	memcpy (cgp, sdp, sizeof (struct GarageCars) * SD_GR_GC_Count);

	// Запись и завершение
	fwrite (cgp, 1, sizeof (struct GarageCars) * SD_GR_GC_Count, FO);
	fclose (FO);

	return SD_SAVE_SUCCESS;
	}
