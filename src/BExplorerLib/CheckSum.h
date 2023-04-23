// Блок контрольной суммы
union SD_CheckSum
	{
	struct CS_Formatted
		{
		ulong CS_Sum;	// Контрольная сумма
		} CS;

	uchar CS_Raw [sizeof (struct CS_Formatted)];
	};
