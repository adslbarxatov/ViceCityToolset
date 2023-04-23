// COMPLETED //

#define SP_PathsCount	3

// Блок параметров пути перемещения спецобъектов
union SD_ScriptPathHeader
	{
	struct SPH_Formatted
		{
		ulong SPH_BlockSize;		// Размер блока
		ulong SPH_SubBlockSize;	// Размер субблока
		} SPH;

	uchar SPH_Raw [sizeof (struct SPH_Formatted)];
	};

union SD_ScriptPath
	{
	struct SP_Formatted
		{
		ulong SP_PointsCount;
		ulong SP_PathNodesDataPointer;
		float SP_TotalDistance;
		float SP_PathSpeed;
		float SP_DistanceAlongPath;
		float SP_Width;
		ulong SP_State;
		ulong SP_ObjectHandles[6];
		} SP;

	uchar SP_Raw [sizeof (struct SP_Formatted)];
	};

union SD_ScriptPathNodes
	{
	struct SPN_Formatted
		{
		float SPN_X;
		float SPN_Y;
		float SPN_Z;
		float SPN_DistanceToPoint;
		ulong SPN_Unknown01;
		} SPN;

	uchar SPN_Raw [sizeof (struct SPN_Formatted)];
	};
