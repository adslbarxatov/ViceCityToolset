// Отключение предупреждений
#define _CRT_SECURE_NO_WARNINGS

// Подключение общего заголовка
#include "ViceCityToolset.h"

// Загрузка статистики в структуру
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_LoadCG (struct SaveData *SD, schar *FilePath)
	{
	FILE *FI;
	union SD_CarGenerators cg;
	ulong i;

	// Попытка открытия файла
	if ((FI = fopen (FilePath, "rb")) == NULL)
		{
		return SD_INTRPR_ERR_CGFileNotFound;
		}

	// Попытка считывания файла
	if (fread (cg.CG_Raw, 1, sizeof (union SD_CarGenerators), FI) != sizeof (union SD_CarGenerators))
		{
		fclose (FI);
		return SD_INTRPR_ERR_CGFileIsIncorrect;
		}
	fclose (FI);

	// Перенос данных
	for (i = 0; i < sizeof (union SD_CarGenerators); i++)
		{
		SD->SD_CG.CG_Raw[i] = cg.CG_Raw[i];
		}

	return SD_LOAD_SUCCESS;
	}

// Выгрузка статистики из структуры
// • FilePath - путь к файлу
// • SD - структура сохранения
sint SaveData_SaveCG (struct SaveData *SD, schar *FilePath)
	{
	FILE *FO;
	union SD_CarGenerators cg;
	ulong i;

	// Попытка открытия файлов
	if ((FO = fopen (FilePath, "wb")) == NULL)
		{
		return SD_INTRPR_ERR_CannotCreateCGFile;
		}

	// Перенос значений
	for (i = 0; i < sizeof (union SD_CarGenerators); i++)
		{
		cg.CG_Raw[i] = SD->SD_CG.CG_Raw[i];
		}

	// Запись и завершение
	fwrite (cg.CG_Raw, 1, sizeof (union SD_CarGenerators), FO);
	fclose (FO);

	return SD_SAVE_SUCCESS;
	}
