// Отключение предупреждений
#define _CRT_SECURE_NO_WARNINGS

// Подключение общего заголовка
#include "ViceCityToolset.h"

// Общая переменная для внутреннего хранения структуры
struct SaveData SD;

// Оболочка функции загрузки файла сохранения
B_API (sint) SaveData_LoadEx (schar *FilePath)
	{
	return SaveData_Load (FilePath, &SD);
	}

// Оболочка функции обработки команд
B_API (schar *) SaveData_CommandInterpreterEx (uint Mode, uint OpCode, uint ParCode, schar *Value)
	{
	return SaveData_CommandInterpreter (&SD, Mode, OpCode, ParCode, Value);
	}

// Оболочка для последнего сообщения функции-интерпретатора
schar sdLastMessage[SD_MaxStrSize];
B_API (schar *) SaveData_GetLastMessageEx ()
	{
	return sdLastMessage;
	}

void SaveData_SetLastMessage (schar *Message)
	{
	memcpy (sdLastMessage, Message, strlen (Message));
	sdLastMessage[strlen (Message)] = '\0';
	}

// Метод запроса краткого описания сохранения
B_API (schar *) SaveData_GetSaveInfoEx (void)
	{
	static schar info[SD_MaxStrSize];
	ulong i;

	for (i = 0; i < sizeof (SD.SD_DP.DP.DP_SaveName) / 2; i++)
		info[i] = (schar)SD.SD_DP.DP.DP_SaveName[i];

	info[i] = '\0';

	sprintf (info, "%s (%u.%02u.%04u; %u:%02u)", info, SD.SD_DP.DP.DP_Day, SD.SD_DP.DP.DP_Month,
		SD.SD_DP.DP.DP_Year, SD.SD_DP.DP.DP_Hour, SD.SD_DP.DP.DP_Minute);

	return info;
	}

// Метод запроса ToDo-статуса сохранения
B_API (schar *) SaveData_GetToDoStatusEx (void)
	{
	static schar status[SD_MaxStrSize];
	schar flags1[SD_MaxStrSize],
		outofs1[SD_MaxStrSize],
		flags2[SD_MaxStrSize];

	// Контроль параметров
	if (SD.SD_DP.DP.DP_BlockSize == 0)
		return NULL;

	// Флаги, готовые к отправке
	sprintf (flags1, "%lu%lu%lu%lu%lu%lu%lu%lu%lu%lu%lu%lu",
		SD.SD_SBA[270].SBA_Variable[0],		// Main missions flag
		SD.SD_SBA[373].SBA_Variable[0],		// Taxi mission flag
		SD.SD_SBA[389].SBA_Variable[0],		// Pizza boy mission flag
		SD.SD_SBA[363].SBA_Variable[0],		// Trial by dirt mission flag
		SD.SD_SBA[364].SBA_Variable[0],		// Test track mission flag
		SD.SD_SBA[108].SBA_Variable[0],		// Shooting range mission flag
		SD.SD_SBA[7854].SBA_Variable[0],	// Cherry popper asset flag
		SD.SD_SBA[1096].SBA_Variable[0],	// Pole position asset flag
		SD.SD_SBA[51].SBA_Variable[0],		// Sunshine export 1 flag
		SD.SD_SBA[52].SBA_Variable[0],		// Sunshine export 2 flag
		SD.SD_SBA[53].SBA_Variable[0],		// Sunshine export 3 flag
		SD.SD_SBA[54].SBA_Variable[0]);		// Sunshine export 4 flag

	// Диапазонные значения
	sprintf (outofs1, "%lu%lu%2lu%lu%2lu%lu%2lu%lu%2lu%lu%3lu%lu",
		(ulong)SD.SD_ST.ST.ST_ToDoAsassinationContractsDone, 5,
		SD.SD_ST.ST.ST_ToDoOwnedPropertyCount, 15,
		SD.SD_ST.ST.ST_ToDoRampagesPassed, SD.SD_ST.ST.ST_RampagesCount,
		(ulong)SD.SD_ST.ST.ST_ToDoStoresKnockedOff, 15,
		SD.SD_ST.ST.ST_ToDoUniqueJumpsPassed, SD.SD_ST.ST.ST_UniqueJumpsCount,
		SD.SD_PL.PL.PL_CollectedHiddenPackages, SD.SD_PL.PL.PL_HiddenPackagesCount);

	// Флаги, требующие подготовки
	flags2[0] = (SD.SD_ST.ST.ST_ToDoCheckpointCharlieRecord > 0) ? '1' : '0';
	flags2[1] = (SD.SD_ST.ST.ST_ToDoConeCrazyRecord > 0) ? '1' : '0';
	flags2[2] = (SD.SD_ST.ST.ST_ToDoDirtringRecord > 0) ? '1' : '0';
	flags2[3] = (SD.SD_ST.ST.ST_ToDoDowntownChopperRecord > 0) ? '1' : '0';
	flags2[4] = (SD.SD_ST.ST.ST_ToDoHighestAmbulanceLevel >= 12) ? '1' : '0';
	flags2[5] = (SD.SD_ST.ST.ST_ToDoHighestFirefighterLevel >= 12) ? '1' : '0';
	flags2[6] = (SD.SD_ST.ST.ST_ToDoHighestVigilanteLevel >= 12) ? '1' : '0';
	flags2[7] = (SD.SD_ST.ST.ST_ToDoHotringRecord > 0) ? '1' : '0';
	flags2[8] = (SD.SD_ST.ST.ST_ToDoLittleHaitiChopperRecord > 0) ? '1' : '0';
	flags2[9] = (SD.SD_ST.ST.ST_ToDoLongestTimeInBlooding > 0) ? '1' : '0';
	flags2[10] = (SD.SD_ST.ST.ST_ToDoOceanBeachChopperRecord > 0) ? '1' : '0';
	flags2[11] = (SD.SD_ST.ST.ST_ToDoPCJPlaygroundRecord > 0) ? '1' : '0';
	flags2[12] = (SD.SD_ST.ST.ST_ToDoRCCarRecord > 0) ? '1' : '0';
	flags2[13] = (SD.SD_ST.ST.ST_ToDoRCHelicopterRecord > 0) ? '1' : '0';
	flags2[14] = (SD.SD_ST.ST.ST_ToDoRCPlaneRecord > 0) ? '1' : '0';
	flags2[15] = (SD.SD_ST.ST.ST_ToDoSunshineTrack1Record > 0) ? '1' : '0';
	flags2[16] = (SD.SD_ST.ST.ST_ToDoSunshineTrack2Record > 0) ? '1' : '0';
	flags2[17] = (SD.SD_ST.ST.ST_ToDoSunshineTrack3Record > 0) ? '1' : '0';
	flags2[18] = (SD.SD_ST.ST.ST_ToDoSunshineTrack4Record > 0) ? '1' : '0';
	flags2[19] = (SD.SD_ST.ST.ST_ToDoSunshineTrack5Record > 0) ? '1' : '0';
	flags2[20] = (SD.SD_ST.ST.ST_ToDoSunshineTrack6Record > 0) ? '1' : '0';
	flags2[21] = (SD.SD_ST.ST.ST_ToDoVicePointChopperRecord > 0) ? '1' : '0';
	flags2[22] = '\0';

	// Сборка и возврат
	sprintf (status, "%s%s%s", flags1, flags2, outofs1);
	return status;
	}

// Метод получения списка файлов из архива IMG / DIR
B_API (ulong) Archive_GetFilesListEx (schar *DIRPath, ulong **Metrics, uchar **Names)
	{
	FILE *FI;
	ulong filesCount = 0;
	union IMGItem item;
	uchar itemLength = sizeof (union IMGItem);
	uchar fileNameLength = sizeof (item.Str.FileName);
	ulong i;
	ulong *metrics;
	uchar *names;

	// Получение размера
	if ((FI = fopen (DIRPath, "rb")) == NULL)
		return 0;

	fseek (FI, 0, SEEK_END);
	filesCount = ftell (FI) / itemLength;
	fseek (FI, 0, SEEK_SET);

	// Выделение памяти
	if ((metrics = (ulong *)malloc (filesCount * 2 * sizeof (ulong))) == NULL)
		{
		fclose (FI);
		return 0;
		}
	if ((names = (uchar *)malloc (filesCount * fileNameLength)) == NULL)
		{
		fclose (FI);
		return 0;
		}

	// Чтение
	for (i = 0; i < filesCount; i++)
		{
		if (fread (item.Ptr, 1, itemLength, FI) != itemLength)
			return 0;

		metrics[2 * i + 0] = item.Str.Offset;
		metrics[2 * i + 1] = item.Str.Size;
		memcpy (names + fileNameLength * i, item.Str.FileName, fileNameLength);
		}

	// Завершено
	fclose (FI);
	*Metrics = metrics;
	*Names = names;
	return filesCount;
	}

// Метод извлечения файла из архива
B_API (sint) Archive_ExtractFileEx (schar *IMGPath, schar *TargetFile, ulong Offset, ulong Size)
	{
	FILE *FI, *FO;
	ulong size = Size * IMG_SECTOR_SIZE;
	ulong i;
	int c;

	// Открытие
	if ((FI = fopen (IMGPath, "rb")) == NULL)
		return -1;

	if ((FO = fopen (TargetFile, "wb")) == NULL)
		{
		fclose (FI);
		return -2;
		}

	// Перенос
	fseek (FI, Offset * IMG_SECTOR_SIZE, SEEK_SET);
	for (i = 0; i < size; i++)
		{
		c = fgetc (FI);
		fputc (c, FO);
		}

	// Завершение
	fclose (FI);
	fclose (FO);
	return 0;
	}

// Метод извлечения файла из архива
B_API (sint) Archive_ReplaceFileEx (schar *IMGPath, schar *TargetFile)
	{
	FILE *FIImg, *FIDir, *FOImg, *FODir, *FIP;
	schar iImgPath[0x100];
	schar iDirPath[0x100];
	schar oImgPath[0x100];
	schar oDirPath[0x100];

	schar fileName[0x100];
	uint pathLength, fileNameLength;
	uint left;

	union IMGItem item;
	ulong currentOffset = 0;
	ulong size, offset, i;
	int c;

	// Подготовка нового файла
	fileNameLength = strlen (TargetFile);
	for (left = fileNameLength - 1; left > 0; left--)
		if (TargetFile[left] == '\\')
			break;

	left++;
	// 0123L5678
	// abc\d.efg  9
	//     01234
	fileNameLength -= left;
	memcpy (fileName, TargetFile + left, fileNameLength);
	fileName[fileNameLength] = '\0';

	if ((FIP = fopen (TargetFile, "rb")) == NULL)
		return -5;

	// Попытка открытия файлов архива
	iImgPath[0] = '\0';
	strcpy (iImgPath, IMGPath);
	if ((FIImg = fopen (iImgPath, "rb")) == NULL)
		{
		fclose (FIP);
		return -1;
		}

	pathLength = strlen (IMGPath);
	iDirPath[0] = '\0';
	strcpy (iDirPath, IMGPath);
	iDirPath[pathLength - 3] = 'd';
	iDirPath[pathLength - 2] = 'i';
	iDirPath[pathLength - 1] = 'r';
	if ((FIDir = fopen (iDirPath, "rb")) == NULL)
		{
		fclose (FIP);
		fclose (FIImg);
		return -2;
		}

	oImgPath[0] = '\0';
	sprintf (oImgPath, "%s.tmp", iImgPath);
	if ((FOImg = fopen (oImgPath, "wb")) == NULL)
		{
		fclose (FIP);
		fclose (FIImg);
		fclose (FIDir);
		return -3;
		}

	oDirPath[0] = '\0';
	sprintf (oDirPath, "%s.tmp", iDirPath);
	if ((FODir = fopen (oDirPath, "wb")) == NULL)
		{
		fclose (FIP);
		fclose (FIImg);
		fclose (FIDir);
		fclose (FOImg);
		return -4;
		}

	// Перенос незатронутых файлов
	while (fread (item.Ptr, 1, sizeof (union IMGItem), FIDir) == sizeof (union IMGItem))
		{
		// Пропуск совпадения
		if (memcmp (item.Str.FileName, fileName, fileNameLength) == 0)
			continue;

		// Расчёт разметки
		size = item.Str.Size * IMG_SECTOR_SIZE;
		offset = item.Str.Offset * IMG_SECTOR_SIZE;

		item.Str.Offset = currentOffset;
		currentOffset += item.Str.Size;

		fseek (FIImg, offset, SEEK_SET);
		for (i = 0; i < size; i++)
			{
			c = fgetc (FIImg);
			fputc (c, FOImg);
			}

		fwrite (item.Ptr, 1, sizeof (union IMGItem), FODir);
		}

	// Добавление нового файла
	for (i = 0; i < sizeof (item.Str.FileName); i++)
		{
		if (i < fileNameLength)
			item.Str.FileName[i] = fileName[i];
		else
			item.Str.FileName[i] = '\0';
		}
	item.Str.Offset = currentOffset;

	fseek (FIP, 0, SEEK_END);
	size = ftell (FIP);
	fseek (FIP, 0, SEEK_SET);

	if (size % IMG_SECTOR_SIZE != 0)
		{
		size = size / IMG_SECTOR_SIZE + 1;
		item.Str.Size = size;
		size = size * IMG_SECTOR_SIZE;
		}
	else
		{
		item.Str.Size = size / IMG_SECTOR_SIZE;
		}

	for (i = 0; i < size; i++)
		{
		c = fgetc (FIP);
		fputc (c, FOImg);
		}

	fwrite (item.Ptr, 1, sizeof (union IMGItem), FODir);

	// Завершено
	fclose (FIP);
	fclose (FIImg);
	fclose (FIDir);
	fclose (FOImg);
	fclose (FODir);

	// Замена файлов
	if ((remove (iImgPath) != 0) || (remove (iDirPath) != 0))
		return -6;

	if ((rename (oImgPath, iImgPath) != 0) || (rename (oDirPath, iDirPath) != 0))
		return -7;

	// Выход
	return 0;
	}
