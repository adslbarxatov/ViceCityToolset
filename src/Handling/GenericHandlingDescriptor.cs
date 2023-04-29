using System.Globalization;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает основной дескриптор транспортного средства
	/// </summary>
	public class GenericHandlingDescriptor: HandlingDescriptor
		{
		// Переменные
		private const uint expectedColumnsCount = 26 + 7;

		/// <summary>
		/// Строка заголовка таблицы данных дескриптора
		/// </summary>
		public static string TableHeader
			{
			get
				{
				return HandlingSupport.CreateHeader (expectedColumnsCount);
				}
			}

		/*
		public const string TableHeader = "	A			B		C		D		E		F		G		H		I	J		" +
			"K		L		M	N		O		P	Q	R		S		T	U		V		W		" +
			"X		Y			Z		AA		AB		AC		AD			AE		AF	AG";
		*/

		/// <summary>
		/// Конструктор. Загружает параметры транспортного средства из массива строк
		/// </summary>
		/// <param name="Values">Массив строк, содержащий значения параметров</param>
		public GenericHandlingDescriptor (string[] Values)
			{
			// Контроль массива
			if (Values.Length != expectedColumnsCount)
				return;

			// Загрузка параметров. Часть параметров записывается в поля напрямую.
			// Другая часть намеренно проводится через обработки, установленные в
			// свойствах класса

			// Далее следуют параметры, не зависящие от типа транспортного средства
			a_VehicleIdentifier = Values[0];
			/*NumberFormatInfo nfi = HandlingSupport.ValuesFormat;*/

			try
				{
				VehicleMass = float.Parse (Values[1], nfi);     // b_
				XDimension = float.Parse (Values[2], nfi);      // c_
				YDimension = float.Parse (Values[3], nfi);
				ZDimension = float.Parse (Values[4], nfi);
				XCentreOfMass = float.Parse (Values[5], nfi);   // f_
				YCentreOfMass = float.Parse (Values[6], nfi);
				ZCentreOfMass = float.Parse (Values[7], nfi);
				PercentSubmerged = int.Parse (Values[8]);       // i_
				NumberOfGears = uint.Parse (Values[12]);        // m_
				MaxVelocity = float.Parse (Values[13], nfi);
				EngineAcceleration = float.Parse (Values[14], nfi);

				switch (Values[15])                             // p_
					{
					case "R":
					case "r":
						DriveType = DriveTypes.RearWheelsDrive;
						break;

					case "F":
					case "f":
						DriveType = DriveTypes.ForwardWheelsDrive;
						break;

					case "4":
						DriveType = DriveTypes.FourWheelsDrive;
						break;

					default:
						return;
					}

				switch (Values[16])                             // q_
					{
					case "P":
					case "p":
						EngineType = EngineTypes.Petrol;
						break;

					case "D":
					case "d":
						EngineType = EngineTypes.Diesel;
						break;

					case "E":
					case "e":
						EngineType = EngineTypes.Electric;
						break;

					default:
						return;
					}

				t_ABS = uint.Parse (Values[19]);                        // t_
				SteeringLock = float.Parse (Values[20], nfi);
				SeatOffsetDistance = float.Parse (Values[23], nfi);     // x_
				CollisionDamageMultiplier = float.Parse (Values[24], nfi);
				MonetaryValue = uint.Parse (Values[25]);
				SuspensionUpperLimit = float.Parse (Values[26], nfi);
				SuspensionAntiDiveMultiplier = float.Parse (Values[29], nfi);   // ad_

				af_FrontLights = (LightsTypes)int.Parse (Values[31]);
				if (((int)af_FrontLights < 0) || ((int)af_FrontLights > 3))
					return;

				ag_RearLights = (LightsTypes)int.Parse (Values[32]);
				if (((int)ag_RearLights < 0) || ((int)ag_RearLights > 3))
					return;
				}
			catch
				{
				return;
				}

			// Следующие свойства требуют знания, относится ли транспортное средство к лодкам или нет
			uint flags = 0;
			try
				{
				flags = uint.Parse (Values[30], NumberStyles.AllowHexSpecifier);
				}
			catch
				{
				return;
				}

			// Получение флагов
			ae1_FirstGearBoost = ((flags & 0x1) != 0);
			ae1_SecondGearBoost = ((flags & 0x2) != 0);
			ae1_ReversedBonnet = ((flags & 0x4) != 0);

			// Биты 4, 10 и 11 (см. описание перечисления)
			ae13_TailgateType = (TailgateTypes)(((flags & 0x8) >> 3) | ((flags & 0x200) >> 8) | ((flags & 0x400) >> 8));
			if ((ae13_TailgateType != TailgateTypes.AttachedToBottom) &&
				(ae13_TailgateType != TailgateTypes.AttachedToTop) &&
				(ae13_TailgateType != TailgateTypes.DefaultBoot) &&
				(ae13_TailgateType != TailgateTypes.Locked))
				{
				return;
				}

			ae2_DoorsType = (DoorsTypes)((flags & 0xF0) >> 4);
			if ((ae2_DoorsType != DoorsTypes.Default) &&
				(ae2_DoorsType != DoorsTypes.IsBus) &&
				(ae2_DoorsType != DoorsTypes.IsLow) &&
				(ae2_DoorsType != DoorsTypes.IsVan) &&
				(ae2_DoorsType != DoorsTypes.NoDoors))
				{
				return;
				}

			ae3_DoubleExhaust = ((flags & 0x100) != 0);
			ae3_NonPlayerStabilizer = ((flags & 0x800) != 0);
			ae4_NeutralHandling = ((flags & 0x1000) != 0);
			ae4_HasNoRoof = ((flags & 0x2000) != 0);
			ae4_IsBig = ((flags & 0x4000) != 0);
			ae4_HalogenLights = ((flags & 0x8000) != 0);

			ae5_VehicleType = (VehicleTypes)((flags & 0xF0000) >> 16);
			if ((ae5_VehicleType != VehicleTypes.Bike) &&
				(ae5_VehicleType != VehicleTypes.Boat) &&
				(ae5_VehicleType != VehicleTypes.Car) &&
				(ae5_VehicleType != VehicleTypes.Helicopter) &&
				(ae5_VehicleType != VehicleTypes.Plane))
				{
				return;
				}

			ae6_NoExhaust = ((flags & 0x100000) != 0);
			ae6_RearWheelFirst = ((flags & 0x200000) != 0);
			ae6_HandbrakeTyre = ((flags & 0x400000) != 0);
			ae6_SitInBoat = ((flags & 0x800000) != 0);
			ae7_FatRearWheels = ((flags & 0x1000000) != 0);
			ae7_NarrowFrontWheels = ((flags & 0x2000000) != 0);
			ae7_GoodInSand = ((flags & 0x4000000) != 0);
			ae7_SpecialFlight = ((flags & 0x8000000) != 0);

			// Получение зависимых параметров
			try
				{
				if (VehicleType != VehicleTypes.Boat)
					{
					TractionMultiplier = float.Parse (Values[9], nfi);          // j_
					TractionLoss = float.Parse (Values[10], nfi);
					TractionBias = float.Parse (Values[11], nfi);
					BrakeDeceleration = float.Parse (Values[17], nfi);          // r_
					BrakeBias = float.Parse (Values[18], nfi);
					SuspensionForceLevel = float.Parse (Values[21], nfi);       // v_
					SuspensionDampingLevel = float.Parse (Values[22], nfi);
					SuspensionLowerLimit = float.Parse (Values[27], nfi);       // ab_
					SuspensionBias = float.Parse (Values[28], nfi);
					}
				else
					{
					BankForceMultiplier = float.Parse (Values[9], nfi);         // j_
					RudderTurnForce = float.Parse (Values[10], nfi);
					SpeedSteerFalloff = float.Parse (Values[11], nfi);
					VerticalWaveHitLimit = float.Parse (Values[17], nfi);       // r_
					ForwardWaveHitBrake = float.Parse (Values[18], nfi);
					WaterResistanceMultiplier = float.Parse (Values[21], nfi);  // v_
					WaterDampingMultiplier = float.Parse (Values[22], nfi);
					HandbrakeDragMultiplier = float.Parse (Values[27], nfi);    // ab_
					SideslipForce = float.Parse (Values[28], nfi);
					}
				}
			catch
				{
				return;
				}

			// Успешно завершено
			isInited = true;
			}

		/// <summary>
		/// Метод собирает все параметры дескриптора в строку для вывода в файл
		/// </summary>
		/// <returns>Строка параметров</returns>
		public string GetHDAsString ()
			{
			// Переменные
			string result = "";

			// Контроль инициализации
			if (!isInited)
				return result;

			// Сборка
			/*NumberFormatInfo nfi = HandlingSupport.ValuesFormat;*/
			result = VehicleIdentifier.PadRight (8, ' ') + "\t";
			result += (b_VehicleMass.ToString ("F1", nfi).PadLeft (8, ' ') + "\t");
			result += (c_XDimension.ToString ("F2", nfi) + "\t");
			result += (d_YDimension.ToString ("F2", nfi) + "\t");
			result += (e_ZDimension.ToString ("F2", nfi) + "\t");
			result += (f_XCentreOfMass.ToString ("F2", nfi) + "\t");
			result += (g_YCentreOfMass.ToString ("F2", nfi) + "\t");
			result += (h_ZCentreOfMass.ToString ("F2", nfi) + "\t");
			result += (i_PercentSubmerged.ToString () + "\t");
			result += (j_TractionMultiplier.ToString ("F2", nfi) + "\t");
			result += (k_TractionLoss.ToString ("F2", nfi) + "\t");
			result += (l_TractionBias.ToString ("F2", nfi) + "\t");
			result += (m_NumberOfGears.ToString () + "\t");
			result += (n_MaxVelocity.ToString ("F1", nfi) + "\t");
			result += (o_EngineAcceleration.ToString ("F1", nfi).PadLeft (4, ' ') + "\t");

			switch (p_DriveType)
				{
				case DriveTypes.ForwardWheelsDrive:
					result += "F\t";
					break;

				case DriveTypes.FourWheelsDrive:
					result += "4\t";
					break;

				case DriveTypes.RearWheelsDrive:
					result += "R\t";
					break;
				}

			switch (q_EngineType)
				{
				case EngineTypes.Diesel:
					result += "D\t";
					break;

				case EngineTypes.Electric:
					result += "E\t";
					break;

				case EngineTypes.Petrol:
					result += "P\t";
					break;
				}

			result += (r_BrakeDeceleration.ToString ("F2", nfi) + "\t");
			result += (s_BrakeBias.ToString ("F2", nfi) + "\t");
			result += (t_ABS.ToString () + "\t");
			result += (u_SteeringLock.ToString ("F1", nfi) + "\t");
			result += (v_SuspensionForceLevel.ToString ("F2", nfi) + "\t");
			result += (w_SuspensionDampingLevel.ToString ("F2", nfi) + "\t");
			result += (x_SeatOffsetDistance.ToString ("F2", nfi) + "\t");
			result += (y_CollisionDamageMultiplier.ToString ("F2", nfi) + "\t");
			result += (z_MonetaryValue.ToString ().PadLeft (8, ' ') + "\t");
			result += (aa_SuspensionUpperLimit.ToString ("F2", nfi) + "\t");
			result += (ab_SuspensionLowerLimit.ToString ("F2", nfi) + "\t");
			result += (ac_SuspensionBias.ToString ("F2", nfi) + "\t");
			result += (ad_SuspensionAntiDiveMultiplier.ToString ("F2", nfi) + "\t");

			uint flags = (ae1_FirstGearBoost ? 0x1u : 0u) |
				(ae1_SecondGearBoost ? 0x2u : 0u) |
				(ae1_ReversedBonnet ? 0x4u : 0u) |
				(((uint)ae13_TailgateType & 0x1u) << 3) |
				((uint)ae2_DoorsType << 4) |
				(ae3_DoubleExhaust ? 0x100u : 0u) |
				(((uint)ae13_TailgateType & 0x2u) << 8) |
				(((uint)ae13_TailgateType & 0x4u) << 8) |
				(ae3_NonPlayerStabilizer ? 0x800u : 0u) |
				(ae4_NeutralHandling ? 0x1000u : 0u) |
				(ae4_HasNoRoof ? 0x2000u : 0u) |
				(ae4_IsBig ? 0x4000u : 0u) |
				(ae4_HalogenLights ? 0x8000u : 0u) |
				((uint)ae5_VehicleType << 16) |
				(ae6_NoExhaust ? 0x100000u : 0u) |
				(ae6_RearWheelFirst ? 0x200000u : 0u) |
				(ae6_HandbrakeTyre ? 0x400000u : 0u) |
				(ae6_SitInBoat ? 0x800000u : 0u) |
				(ae7_FatRearWheels ? 0x1000000u : 0u) |
				(ae7_NarrowFrontWheels ? 0x2000000u : 0u) |
				(ae7_GoodInSand ? 0x4000000u : 0u) |
				(ae7_SpecialFlight ? 0x8000000u : 0u);
			result += (flags.ToString ("X").PadLeft (8, ' ') + "\t");

			result += (((int)af_FrontLights).ToString () + "\t");
			result += ((int)ag_RearLights).ToString ();

			// Завершено
			return result;
			}

		// Region A -> HandlingDescriptor

		#region B
		/// <summary>
		/// Минимальная масса транспорта
		/// </summary>
		public const float VehicleMass_Min = 100.0f;

		/// <summary>
		/// Максимальная масса транспорта
		/// </summary>
		public const float VehicleMass_Max = 20000.0f;

		/// <summary>
		/// Возвращает или задаёт массу транспортного средства в килограммах
		/// </summary>
		public float VehicleMass
			{
			get
				{
				return b_VehicleMass;
				}
			set
				{
				b_VehicleMass = HandlingSupport.CheckRange (value, VehicleMass_Min, VehicleMass_Max);
				}
			}
		private float b_VehicleMass = 1000.0f;
		#endregion

		#region C
		/// <summary>
		/// Минимальная ширина автомобиля
		/// </summary>
		public const float XDimension_Min = 0.1f;

		/// <summary>
		/// Максимальная ширина автомобиля
		/// </summary>
		public const float XDimension_Max = 20.0f;

		/// <summary>
		/// Возвращает или задаёт ширину автомобиля в метрах.
		/// Используется для формирования аэродинамических эффектов и эффектов движения
		/// </summary>
		public float XDimension
			{
			get
				{
				return c_XDimension;
				}
			set
				{
				c_XDimension = HandlingSupport.CheckRange (value, XDimension_Min, XDimension_Max);
				}
			}
		private float c_XDimension = 1.0f;
		#endregion

		#region D
		/// <summary>
		/// Минимальная длина автомобиля
		/// </summary>
		public const float YDimension_Min = 0.1f;

		/// <summary>
		/// Максимальная длина автомобиля
		/// </summary>
		public const float YDimension_Max = 20.0f;

		/// <summary>
		/// Возвращает или задаёт длину автомобиля в метрах.
		/// Используется для формирования аэродинамических эффектов и эффектов движения
		/// </summary>
		public float YDimension
			{
			get
				{
				return d_YDimension;
				}
			set
				{
				d_YDimension = HandlingSupport.CheckRange (value, YDimension_Min, YDimension_Max);
				}
			}
		private float d_YDimension = 2.0f;
		#endregion

		#region E
		/// <summary>
		/// Минимальная высота автомобиля
		/// </summary>
		public const float ZDimension_Min = 0.1f;

		/// <summary>
		/// Максимальная высота автомобиля
		/// </summary>
		public const float ZDimension_Max = 20.0f;

		/// <summary>
		/// Возвращает или задаёт высоту автомобиля в метрах.
		/// Используется для формирования аэродинамических эффектов и эффектов движения
		/// </summary>
		public float ZDimension
			{
			get
				{
				return e_ZDimension;
				}
			set
				{
				e_ZDimension = HandlingSupport.CheckRange (value, ZDimension_Min, ZDimension_Max);
				}
			}
		private float e_ZDimension = 1.0f;
		#endregion

		#region F
		/// <summary>
		/// Минимальное поперечное смещение центра тяжести
		/// </summary>
		public const float XCentreOfMass_Min = -10.0f;

		/// <summary>
		/// Максимальное поперечное смещение центра тяжести
		/// </summary>
		public const float XCentreOfMass_Max = 10.0f;

		/// <summary>
		/// Возвращает или задаёт поперечное смещение центра тяжести в метрах.
		/// Положительный показатель смещает центр тяжести вправо. Небольшие значения дают реалистичные заносы
		/// </summary>
		public float XCentreOfMass
			{
			get
				{
				return f_XCentreOfMass;
				}
			set
				{
				f_XCentreOfMass = HandlingSupport.CheckRange (value, XCentreOfMass_Min, XCentreOfMass_Max);
				}
			}
		private float f_XCentreOfMass = 0.0f;
		#endregion

		#region G
		/// <summary>
		/// Минимальное продольное смещение центра тяжести
		/// </summary>
		public const float YCentreOfMass_Min = -10.0f;

		/// <summary>
		/// Максимальное продольное смещение центра тяжести
		/// </summary>
		public const float YCentreOfMass_Max = 10.0f;

		/// <summary>
		/// Возвращает или задаёт продольное смещение центра тяжести в метрах.
		/// Положительный показатель смещает центр тяжести вперёд
		/// </summary>
		public float YCentreOfMass
			{
			get
				{
				return g_YCentreOfMass;
				}
			set
				{
				g_YCentreOfMass = HandlingSupport.CheckRange (value, YCentreOfMass_Min, YCentreOfMass_Max);
				}
			}
		private float g_YCentreOfMass = 0.0f;
		#endregion

		#region H
		/// <summary>
		/// Минимальное вертикальное смещение центра тяжести
		/// </summary>
		public const float ZCentreOfMass_Min = -10.0f;

		/// <summary>
		/// Максимальное вертикальное смещение центра тяжести
		/// </summary>
		public const float ZCentreOfMass_Max = 10.0f;

		/// <summary>
		/// Возвращает или задаёт вертикальное смещение центра тяжести в метрах.
		/// Положительный показатель смещает центр тяжести вверх. Большие отрицательные значения дают неконтролируемые прыжки
		/// </summary>
		public float ZCentreOfMass
			{
			get
				{
				return h_ZCentreOfMass;
				}
			set
				{
				h_ZCentreOfMass = HandlingSupport.CheckRange (value, ZCentreOfMass_Min, ZCentreOfMass_Max);
				}
			}
		private float h_ZCentreOfMass = 0.0f;
		#endregion

		#region I
		/// <summary>
		/// Минимальный процент погружения в воду
		/// </summary>
		public const int PercentSubmerged_Min = -1;

		/// <summary>
		/// Максимальный процент погружения в воду
		/// </summary>
		public const int PercentSubmerged_Max = 120;

		/// <summary>
		/// Возвращает или задаёт процент погружения в воду.
		/// -1 даёт ныряние без ущерба здоровью
		/// </summary>
		public int PercentSubmerged
			{
			get
				{
				return i_PercentSubmerged;
				}
			set
				{
				i_PercentSubmerged = HandlingSupport.CheckRange (value, PercentSubmerged_Min, PercentSubmerged_Max);
				}
			}
		private int i_PercentSubmerged = 80;
		#endregion

		#region J
		/// <summary>
		/// Минимальный коэффициент сцепления колёс с поверхностью земли
		/// </summary>
		public const float TractionMultiplier_Min = -2.0f;

		/// <summary>
		/// Максимальный коэффициент сцепления колёс с поверхностью земли
		/// </summary>
		public const float TractionMultiplier_Max = 2.0f;

		/// <summary>
		/// Минимальный наклон при повороте лодки
		/// </summary>
		public const float BankForceMultiplier_Min = -5.0f;

		/// <summary>
		/// Максимальный наклон при повороте лодки
		/// </summary>
		public const float BankForceMultiplier_Max = 5.0f;

		/// <summary>
		/// Возвращает или задаёт коэффициент сцепления колёс с поверхностью земли. 
		/// Нулевое и отрицательное значение даёт «обледенение трассы» для отдельного транспортного средства
		/// </summary>
		public float TractionMultiplier
			{
			get
				{
				return j_TractionMultiplier;
				}
			set
				{
				j_TractionMultiplier = HandlingSupport.CheckRange (value, TractionMultiplier_Min, TractionMultiplier_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт наклон при повороте
		/// </summary>
		public float BankForceMultiplier
			{
			get
				{
				return j_TractionMultiplier;
				}
			set
				{
				j_TractionMultiplier = HandlingSupport.CheckRange (value, BankForceMultiplier_Min, BankForceMultiplier_Max);
				}
			}
		private float j_TractionMultiplier = 1.0f;
		#endregion

		#region K
		/// <summary>
		/// Минимальный коэффициент сцепления колёс с землёй при ускорении и торможении
		/// </summary>
		public const float TractionLoss_Min = 0.0f;

		/// <summary>
		/// Максимальный коэффициент сцепления колёс с землёй при ускорении и торможении
		/// </summary>
		public const float TractionLoss_Max = 1.0f;

		/// <summary>
		/// Минимальная сила поворота руля лодки
		/// </summary>
		public const float RudderTurnForce_Min = 0.0f;

		/// <summary>
		/// Максимальная сила поворота руля лодки
		/// </summary>
		public const float RudderTurnForce_Max = 45.0f;

		/// <summary>
		/// Возвращает или задаёт коэффициент сцепления колёс с землёй при ускорении и торможении
		/// </summary>
		public float TractionLoss
			{
			get
				{
				return k_TractionLoss;
				}
			set
				{
				k_TractionLoss = HandlingSupport.CheckRange (value, TractionLoss_Min, TractionLoss_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт силу поворота руля
		/// </summary>
		public float RudderTurnForce
			{
			get
				{
				return k_TractionLoss;
				}
			set
				{
				k_TractionLoss = HandlingSupport.CheckRange (value, RudderTurnForce_Min, RudderTurnForce_Max);
				}
			}
		private float k_TractionLoss = 0.8f;
		#endregion

		#region L
		/// <summary>
		/// Минимальный показатель сцепления колёс с дорогой
		/// </summary>
		public const float TractionBias_Min = 0.0f;

		/// <summary>
		/// Максимальный показатель сцепления колёс с дорогой
		/// </summary>
		public const float TractionBias_Max = 1.0f;

		/// <summary>
		/// Минимальная потеря скорости лодки на поворотах
		/// </summary>
		public const float SpeedSteerFalloff_Min = 0.0f;

		/// <summary>
		/// Максимальная потеря скорости лодки на поворотах
		/// </summary>
		public const float SpeedSteerFalloff_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт показатель сцепления колёс с дорогой.
		/// Если значение равно 0, то передние колёса не имеют сцепления, 1 - задние колёса не имеют сцепления
		/// </summary>
		public float TractionBias
			{
			get
				{
				return l_TractionBias;
				}
			set
				{
				l_TractionBias = HandlingSupport.CheckRange (value, TractionBias_Min, TractionBias_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт потерю скорости на поворотах
		/// </summary>
		public float SpeedSteerFalloff
			{
			get
				{
				return l_TractionBias;
				}
			set
				{
				l_TractionBias = HandlingSupport.CheckRange (value, SpeedSteerFalloff_Min, SpeedSteerFalloff_Max);
				}
			}
		private float l_TractionBias = 0.5f;
		#endregion

		#region M
		/// <summary>
		/// Минимальное количество передач
		/// </summary>
		public const uint NumberOfGears_Min = 1;

		/// <summary>
		/// Максимальное количество передач
		/// </summary>
		public const uint NumberOfGears_Max = 5;

		/// <summary>
		/// Возвращает или задаёт количество передач. 
		/// Используется для формирования звуковых и анимационных эффектов
		/// </summary>
		public uint NumberOfGears
			{
			get
				{
				return m_NumberOfGears;
				}
			set
				{
				m_NumberOfGears = HandlingSupport.CheckRange (value, NumberOfGears_Min, NumberOfGears_Max);
				}
			}
		private uint m_NumberOfGears = 4;
		#endregion

		#region N
		/// <summary>
		/// Минимальная скорость транспортного средства
		/// </summary>
		public const float MaxVelocity_Min = 5.0f;

		/// <summary>
		/// Максимальная скорость транспортного средства
		/// </summary>
		public const float MaxVelocity_Max = 400.0f;

		/// <summary>
		/// Возвращает или задаёт максимальную скорость транспортного средства в км/ч
		/// </summary>
		public float MaxVelocity
			{
			get
				{
				return n_MaxVelocity;
				}
			set
				{
				n_MaxVelocity = HandlingSupport.CheckRange (value, MaxVelocity_Min, MaxVelocity_Max);
				}
			}
		private float n_MaxVelocity = 200.0f;
		#endregion

		#region O
		/// <summary>
		/// Минимальная скорость разгона двигателя
		/// </summary>
		public const float EngineAcceleration_Min = 0.1f;

		/// <summary>
		/// Максимальная скорость разгона двигателя
		/// </summary>
		public const float EngineAcceleration_Max = 100.0f;

		/// <summary>
		/// Возвращает или задаёт скорость разгона двигателя до максимума в мс^2
		/// </summary>
		public float EngineAcceleration
			{
			get
				{
				return o_EngineAcceleration;
				}
			set
				{
				o_EngineAcceleration = HandlingSupport.CheckRange (value, EngineAcceleration_Min, EngineAcceleration_Max);
				}
			}
		private float o_EngineAcceleration = 10.0f;
		#endregion

		#region P
		/// <summary>
		/// Варианты типа привода транспортного средства
		/// </summary>
		public enum DriveTypes
			{
			/// <summary>
			/// 4 ведущих колеса
			/// </summary>
			FourWheelsDrive = 0,

			/// <summary>
			/// Передние ведущие колёса
			/// </summary>
			ForwardWheelsDrive = 1,

			/// <summary>
			/// Задние ведущие колёса
			/// </summary>
			RearWheelsDrive = 2
			}

		/// <summary>
		/// Возвращает или задаёт тип привода транспортного средства
		/// </summary>
		public DriveTypes DriveType
			{
			get
				{
				return p_DriveType;
				}
			set
				{
				p_DriveType = value;
				}
			}
		private DriveTypes p_DriveType = DriveTypes.FourWheelsDrive;
		#endregion

		#region Q
		/// <summary>
		/// Варианты типа двигателя транспортного средства
		/// </summary>
		public enum EngineTypes
			{
			/// <summary>
			/// Бензиновый
			/// </summary>
			Petrol = 0,

			/// <summary>
			/// Дизельный
			/// </summary>
			Diesel = 1,

			/// <summary>
			/// Электрический
			/// </summary>
			Electric = 2
			}

		/// <summary>
		/// Возвращает или задаёт тип двигателя.
		/// Используется для определения его звучания
		/// </summary>
		public EngineTypes EngineType
			{
			get
				{
				return q_EngineType;
				}
			set
				{
				q_EngineType = value;
				}
			}
		private EngineTypes q_EngineType = EngineTypes.Petrol;
		#endregion

		#region R
		/// <summary>
		/// Минимальная сила торможения
		/// </summary>
		public const float BrakeDeceleration_Min = 0.0f;

		/// <summary>
		/// Максимальная сила торможения
		/// </summary>
		public const float BrakeDeceleration_Max = 10.0f;

		/// <summary>
		/// Минимальная сила удара волны
		/// </summary>
		public const float VerticalWaveHitLimit_Min = 0.0f;

		/// <summary>
		/// Максимальная сила удара волны
		/// </summary>
		public const float VerticalWaveHitLimit_Max = 0.5f;

		/// <summary>
		/// Возвращает или задаёт силу торможения.
		/// Значение 0 отключает основной тормоз, оставляя только ручной
		/// </summary>
		public float BrakeDeceleration
			{
			get
				{
				return r_BrakeDeceleration;
				}
			set
				{
				r_BrakeDeceleration = HandlingSupport.CheckRange (value, BrakeDeceleration_Min, BrakeDeceleration_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт силу удара волны
		/// </summary>
		public float VerticalWaveHitLimit
			{
			get
				{
				return r_BrakeDeceleration;
				}
			set
				{
				r_BrakeDeceleration = HandlingSupport.CheckRange (value, VerticalWaveHitLimit_Min, VerticalWaveHitLimit_Max);
				}
			}
		private float r_BrakeDeceleration = 7.5f;
		#endregion

		#region S
		/// <summary>
		/// Минимальный показатель смещения тормозного усилия
		/// </summary>
		public const float BrakeBias_Min = 0.0f;

		/// <summary>
		/// Максимальный показатель смещения тормозного усилия
		/// </summary>
		public const float BrakeBias_Max = 1.0f;

		/// <summary>
		/// Минимальная тормозящая сила волны
		/// </summary>
		public const float ForwardWaveHitBrake_Min = 0.0f;

		/// <summary>
		/// Максимальная тормозящая сила волны
		/// </summary>
		public const float ForwardWaveHitBrake_Max = 0.1f;

		/// <summary>
		/// Возвращает или задаёт показатель смещения тормозного усилия.
		/// Если значение равно 0, транспорт тормозит исключительно задними колёсами,
		/// 1 - передними, 0.5 - одновременно передними и задними
		/// </summary>
		public float BrakeBias
			{
			get
				{
				return s_BrakeBias;
				}
			set
				{
				s_BrakeBias = HandlingSupport.CheckRange (value, BrakeBias_Min, BrakeBias_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт тормозящую силу волны
		/// </summary>
		public float ForwardWaveHitBrake
			{
			get
				{
				return s_BrakeBias;
				}
			set
				{
				s_BrakeBias = HandlingSupport.CheckRange (value, ForwardWaveHitBrake_Min, ForwardWaveHitBrake_Max);
				}
			}
		private float s_BrakeBias = 0.5f;
		#endregion

		#region T
		/// <summary>
		/// Минимальный показатель состояния ABS
		/// </summary>
		public const uint ABS_Min = 0;

		/// <summary>
		/// Максимальный показатель состояния ABS
		/// </summary>
		public const uint ABS_Max = 1;

		/// <summary>
		/// Возвращает показатель состояния ABS
		/// </summary>
		public uint ABS
			{
			get
				{
				return t_ABS;
				}
			}
		private uint t_ABS = 0;
		#endregion

		#region U
		/// <summary>
		/// Минимальный поворот рулевого колеса
		/// </summary>
		public const float SteeringLock_Min = 0.0f;

		/// <summary>
		/// Максимальный поворот рулевого колеса
		/// </summary>
		public const float SteeringLock_Max = 90.0f;

		/// <summary>
		/// Возвращает или задаёт максимальный поворот рулевого колеса в градусах. 
		/// Если значение равно 0, авто вообще не сможет повернуть
		/// </summary>
		public float SteeringLock
			{
			get
				{
				return u_SteeringLock;
				}
			set
				{
				u_SteeringLock = HandlingSupport.CheckRange (value, SteeringLock_Min, SteeringLock_Max);
				}
			}
		private float u_SteeringLock = 35.0f;
		#endregion

		#region V
		/// <summary>
		/// Минимальная высота прыжков на кочках
		/// </summary>
		public const float SuspensionForceLevel_Min = 0.0f;

		/// <summary>
		/// Максимальная высота прыжков на кочках
		/// </summary>
		public const float SuspensionForceLevel_Max = 5.0f;

		/// <summary>
		/// Минимальное сопротивление воды
		/// </summary>
		public const float WaterResistanceMultiplier_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление воды
		/// </summary>
		public const float WaterResistanceMultiplier_Max = 5.0f;

		/// <summary>
		/// Возвращает или задаёт высоту прыжков на кочках. 
		/// Если значение равно 0, авто вообще не прыгает
		/// </summary>
		public float SuspensionForceLevel
			{
			get
				{
				return v_SuspensionForceLevel;
				}
			set
				{
				v_SuspensionForceLevel = HandlingSupport.CheckRange (value, SuspensionForceLevel_Min,
					SuspensionForceLevel_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт сопротивление воды
		/// </summary>
		public float WaterResistanceMultiplier
			{
			get
				{
				return v_SuspensionForceLevel;
				}
			set
				{
				v_SuspensionForceLevel = HandlingSupport.CheckRange (value, WaterResistanceMultiplier_Min,
					WaterResistanceMultiplier_Max);
				}
			}
		private float v_SuspensionForceLevel = 1.7f;
		#endregion

		#region W
		/// <summary>
		/// Минимальное отклонение авто при старте и ускорении
		/// </summary>
		public const float SuspensionDampingLevel_Min = 0.0f;

		/// <summary>
		/// Максимальное отклонение авто при старте и ускорении
		/// </summary>
		public const float SuspensionDampingLevel_Max = 1.0f;

		/// <summary>
		/// Минимальная амортизация воды
		/// </summary>
		public const float WaterDampingMultiplier_Min = 0.0f;

		/// <summary>
		/// Максимальная амортизация воды
		/// </summary>
		public const float WaterDampingMultiplier_Max = 10.0f;

		/// <summary>
		/// Возвращает или задаёт отклонение авто при старте и ускорении
		/// </summary>
		public float SuspensionDampingLevel
			{
			get
				{
				return w_SuspensionDampingLevel;
				}
			set
				{
				w_SuspensionDampingLevel = HandlingSupport.CheckRange (value, SuspensionDampingLevel_Min,
					SuspensionDampingLevel_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт амортизацию воды
		/// </summary>
		public float WaterDampingMultiplier
			{
			get
				{
				return w_SuspensionDampingLevel;
				}
			set
				{
				w_SuspensionDampingLevel = HandlingSupport.CheckRange (value, WaterDampingMultiplier_Min,
					WaterDampingMultiplier_Max);
				}
			}
		private float w_SuspensionDampingLevel = 0.1f;
		#endregion

		#region X
		/// <summary>
		/// Минимальная дистанция от центра до ручки двери
		/// </summary>
		public const float SeatOffsetDistance_Min = -10.0f;

		/// <summary>
		/// Максимальная дистанция от центра до ручки двери
		/// </summary>
		public const float SeatOffsetDistance_Max = 10.0f;

		/// <summary>
		/// Возвращает или задаёт дистанцию от центра транспортного средства до ручки двери
		/// </summary>
		public float SeatOffsetDistance
			{
			get
				{
				return x_SeatOffsetDistance;
				}
			set
				{
				x_SeatOffsetDistance = HandlingSupport.CheckRange (value, SeatOffsetDistance_Min, SeatOffsetDistance_Max);
				}
			}
		private float x_SeatOffsetDistance = 2.0f;
		#endregion

		#region Y
		/// <summary>
		/// Минимальный множитель повреждений от столкновений
		/// </summary>
		public const float CollisionDamageMultiplier_Min = 0.0f;

		/// <summary>
		/// Максимальный множитель повреждений от столкновений
		/// </summary>
		public const float CollisionDamageMultiplier_Max = 5.0f;

		/// <summary>
		/// Возвращает или задаёт множитель повреждений от столкновений.
		/// Значение 5 вызывает самопроизвольные взрывы, 0 даёт абсолютную защиту от ударов
		/// </summary>
		public float CollisionDamageMultiplier
			{
			get
				{
				return y_CollisionDamageMultiplier;
				}
			set
				{
				y_CollisionDamageMultiplier = HandlingSupport.CheckRange (value, CollisionDamageMultiplier_Min,
					CollisionDamageMultiplier_Max);
				}
			}
		private float y_CollisionDamageMultiplier = 0.4f;
		#endregion

		#region Z
		/// <summary>
		/// Минимальная стоимость транспортного средства
		/// </summary>
		public const uint MonetaryValue_Min = 0;

		/// <summary>
		/// Максимальная стоимость транспортного средства
		/// </summary>
		public const uint MonetaryValue_Max = 500000;

		/// <summary>
		/// Возвращает или задаёт стоимость транспортного средства в долларах
		/// </summary>
		public uint MonetaryValue
			{
			get
				{
				return z_MonetaryValue;
				}
			set
				{
				z_MonetaryValue = HandlingSupport.CheckRange (value, MonetaryValue_Min, MonetaryValue_Max);
				}
			}
		private uint z_MonetaryValue = 10000;
		#endregion

		#region AA
		/// <summary>
		/// Минимальное значение максимальной высоты подвески
		/// </summary>
		public const float SuspensionUpperLimit_Min = 0.0f;

		/// <summary>
		/// Максимальное значение максимальной высоты подвески
		/// </summary>
		public const float SuspensionUpperLimit_Max = 5.0f;

		/// <summary>
		/// Возвращает или задаёт значение максимальной высоты подвески
		/// </summary>
		public float SuspensionUpperLimit
			{
			get
				{
				return aa_SuspensionUpperLimit;
				}
			set
				{
				aa_SuspensionUpperLimit = HandlingSupport.CheckRange (value, SuspensionUpperLimit_Min,
					SuspensionUpperLimit_Max);
				}
			}
		private float aa_SuspensionUpperLimit = 0.4f;
		#endregion

		#region AB
		/// <summary>
		/// Минимальное значение минимальной высоты подвески
		/// </summary>
		public const float SuspensionLowerLimit_Min = -1.0f;

		/// <summary>
		/// Максимальное значение минимальной высоты подвески
		/// </summary>
		public const float SuspensionLowerLimit_Max = 1.0f;

		/// <summary>
		/// Минимальное значение силы торможения
		/// </summary>
		public const float HandbrakeDragMultiplier_Min = 0.0f;

		/// <summary>
		/// Максимальное значение силы торможения
		/// </summary>
		public const float HandbrakeDragMultiplier_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт значение минимальной высоты подвески
		/// </summary>
		public float SuspensionLowerLimit
			{
			get
				{
				return ab_SuspensionLowerLimit;
				}
			set
				{
				ab_SuspensionLowerLimit = HandlingSupport.CheckRange (value, SuspensionLowerLimit_Min,
					SuspensionLowerLimit_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт силу торможения
		/// </summary>
		public float HandbrakeDragMultiplier
			{
			get
				{
				return ab_SuspensionLowerLimit;
				}
			set
				{
				ab_SuspensionLowerLimit = HandlingSupport.CheckRange (value, HandbrakeDragMultiplier_Min,
					HandbrakeDragMultiplier_Max);
				}
			}
		private float ab_SuspensionLowerLimit = -0.2f;
		#endregion

		#region AC
		/// <summary>
		/// Минимальный показатель соотношения тормозного усилия между передней и задней подвеской
		/// </summary>
		public const float SuspensionBias_Min = 0.0f;

		/// <summary>
		/// Максимальный показатель соотношения тормозного усилия между передней и задней подвеской
		/// </summary>
		public const float SuspensionBias_Max = 5.0f;

		/// <summary>
		/// Минимальная сила бокового смещения лодки
		/// </summary>
		public const float SideslipForce_Min = 0.0f;

		/// <summary>
		/// Максимальная сила бокового смещения лодки
		/// </summary>
		public const float SideslipForce_Max = 5.0f;

		/// <summary>
		/// Возвращает или задаёт показатель соотношения тормозного усилия между передней и задней подвеской.
		/// Если значение равно 0, то транспорт тормозит исключительно задними колёсами, 1 - передними,
		/// 0.5 - одновременно передними и задними
		/// </summary>
		public float SuspensionBias
			{
			get
				{
				return ac_SuspensionBias;
				}
			set
				{
				ac_SuspensionBias = HandlingSupport.CheckRange (value, SuspensionBias_Min, SuspensionBias_Max);
				}
			}

		/// <summary>
		/// Для лодок возвращает или задаёт силу бокового смещения
		/// </summary>
		public float SideslipForce
			{
			get
				{
				return ac_SuspensionBias;
				}
			set
				{
				ac_SuspensionBias = HandlingSupport.CheckRange (value, SideslipForce_Min, SideslipForce_Max);
				}
			}
		private float ac_SuspensionBias = 0.5f;
		#endregion

		#region AD
		/// <summary>
		/// Минимальное сопротивление погружению
		/// </summary>
		public const float SuspensionAntiDiveMultiplier_Min = 0.0f;

		/// <summary>
		/// Максимальное сопротивление погружению
		/// </summary>
		public const float SuspensionAntiDiveMultiplier_Max = 1.0f;

		/// <summary>
		/// Возвращает или задаёт показатель сопротивления погружению
		/// </summary>
		public float SuspensionAntiDiveMultiplier
			{
			get
				{
				return ad_SuspensionAntiDiveMultiplier;
				}
			set
				{
				ad_SuspensionAntiDiveMultiplier = HandlingSupport.CheckRange (value, SuspensionAntiDiveMultiplier_Min,
					SuspensionAntiDiveMultiplier_Max);
				}
			}
		private float ad_SuspensionAntiDiveMultiplier = 0.0f;
		#endregion

		#region AE
		// Бит 1
		/// <summary>
		/// Возвращает или задаёт флаг ускорения вращения колёс на первой передаче
		/// </summary>
		public bool FirstGearBoost
			{
			get
				{
				return ae1_FirstGearBoost;
				}
			set
				{
				ae1_FirstGearBoost = value;
				}
			}
		private bool ae1_FirstGearBoost = false;

		// Бит 2
		/// <summary>
		/// Возвращает или задаёт флаг ускорения вращения колёс на второй передаче
		/// </summary>
		public bool SecondGearBoost
			{
			get
				{
				return ae1_SecondGearBoost;
				}
			set
				{
				ae1_SecondGearBoost = value;
				}
			}
		private bool ae1_SecondGearBoost = true;

		// Бит 3
		/// <summary>
		/// Возвращает или задаёт флаг открывания капота в противоположную сторону
		/// </summary>
		public bool ReversedBonnet
			{
			get
				{
				return ae1_ReversedBonnet;
				}
			set
				{
				ae1_ReversedBonnet = value;
				}
			}
		private bool ae1_ReversedBonnet = false;

		// Биты 4, 10, 11
		/// <summary>
		/// Варианты типа задних дверей транспортного средства
		/// </summary>
		public enum TailgateTypes
			{
			/// <summary>
			/// Стандартный багажник
			/// </summary>
			DefaultBoot = 0,

			/// <summary>
			/// Прикреплена к верху
			/// </summary>
			AttachedToTop = 1,

			/// <summary>
			/// Прикреплена к низу
			/// </summary>
			AttachedToBottom = 2,

			/// <summary>
			/// Не открывается
			/// </summary>
			Locked = 4,
			}

		/// <summary>
		/// Возвращает или задаёт тип задних дверей транспортного средства
		/// </summary>
		public TailgateTypes TailgateType
			{
			get
				{
				return ae13_TailgateType;
				}
			set
				{
				ae13_TailgateType = value;
				}
			}
		private TailgateTypes ae13_TailgateType = TailgateTypes.Locked;

		/// <summary>
		/// Варианты типа дверей транспортного средства
		/// </summary>
		public enum DoorsTypes
			{
			/// <summary>
			/// Стандартный
			/// </summary>
			Default = 0,

			/// <summary>
			/// Без дверей
			/// </summary>
			NoDoors = 1,

			/// <summary>
			/// Двойная дверь сзади
			/// </summary>
			IsVan = 2,

			/// <summary>
			/// Двери как у автобуса
			/// </summary>
			IsBus = 4,

			/// <summary>
			/// Двери соответствуют низкой посадке водителя
			/// </summary>
			IsLow = 8
			}

		// Биты 5 - 8
		/// <summary>
		/// Возвращает или задаёт тип дверей транспортного средства
		/// </summary>
		public DoorsTypes DoorsType
			{
			get
				{
				return ae2_DoorsType;
				}
			set
				{
				ae2_DoorsType = value;
				}
			}
		private DoorsTypes ae2_DoorsType = DoorsTypes.Default;

		// Бит 9
		/// <summary>
		/// Возвращает или задаёт флаг двойного выхлопа
		/// </summary>
		public bool DoubleExhaust
			{
			get
				{
				return ae3_DoubleExhaust;
				}
			set
				{
				ae3_DoubleExhaust = value;
				}
			}
		private bool ae3_DoubleExhaust = false;

		// Неизвестный параметр (бит 12)
		/// <summary>
		/// Возвращает флаг NonPlayerStabilizer
		/// </summary>
		public bool NonPlayerStabilizer
			{
			get
				{
				return ae3_NonPlayerStabilizer;
				}
			}
		private bool ae3_NonPlayerStabilizer = false;

		// Неизвестный параметр (бит 13)
		/// <summary>
		/// Возвращает флаг NeutralHandling
		/// </summary>
		public bool NeutralHandling
			{
			get
				{
				return ae4_NeutralHandling;
				}
			}
		private bool ae4_NeutralHandling = false;

		// Бит 14
		/// <summary>
		/// Возвращает или задаёт флаг отсутствия крыши
		/// </summary>
		public bool HasNoRoof
			{
			get
				{
				return ae4_HasNoRoof;
				}
			set
				{
				ae4_HasNoRoof = value;
				}
			}
		private bool ae4_HasNoRoof = false;

		// Бит 15
		/// <summary>
		/// Возвращает или задаёт флаг изменения поведения игрока на поворотах
		/// при управлении большим транспортом
		/// </summary>
		public bool TransportIsBig
			{
			get
				{
				return ae4_IsBig;
				}
			set
				{
				ae4_IsBig = value;
				}
			}
		private bool ae4_IsBig = false;

		// Бит 16
		/// <summary>
		/// Возвращает или задаёт флаг галогеновых фар
		/// </summary>
		public bool HalogenLights
			{
			get
				{
				return ae4_HalogenLights;
				}
			set
				{
				ae4_HalogenLights = value;
				}
			}
		private bool ae4_HalogenLights = false;

		/// <summary>
		/// Варианты типа транспортного средства
		/// </summary>
		public enum VehicleTypes
			{
			/// <summary>
			/// Автомобиль
			/// </summary>
			Car = 0,

			/// <summary>
			/// Мотоцикл
			/// </summary>
			Bike = 1,

			/// <summary>
			/// Вертолёт
			/// </summary>
			Helicopter = 2,

			/// <summary>
			/// Самолёт
			/// </summary>
			Plane = 4,

			/// <summary>
			/// Моторная лодка
			/// </summary>
			Boat = 8
			}

		// Биты 17 - 20
		/// <summary>
		/// Возвращает тип транспортного средства
		/// </summary>
		public VehicleTypes VehicleType
			{
			get
				{
				return ae5_VehicleType;
				}
			}
		private VehicleTypes ae5_VehicleType = VehicleTypes.Car;

		// Бит 21
		/// <summary>
		/// Возвращает или задаёт флаг отсутствия выхлопа
		/// </summary>
		public bool NoExhaust
			{
			get
				{
				return ae6_NoExhaust;
				}
			set
				{
				ae6_NoExhaust = value;
				}
			}
		private bool ae6_NoExhaust = false;

		// Бит 22
		/// <summary>
		/// Возвращает или задаёт флаг начального разгона задних колёс
		/// </summary>
		public bool RearWheelFirst
			{
			get
				{
				return ae6_RearWheelFirst;
				}
			set
				{
				ae6_RearWheelFirst = value;
				}
			}
		private bool ae6_RearWheelFirst = false;

		// Бит 23
		/// <summary>
		/// Возвращает или задаёт флаг следа от шин при ручном торможении
		/// </summary>
		public bool HandbrakeTyre
			{
			get
				{
				return ae6_HandbrakeTyre;
				}
			set
				{
				ae6_HandbrakeTyre = value;
				}
			}
		private bool ae6_HandbrakeTyre = false;

		// Бит 24
		/// <summary>
		/// Возвращает или задаёт флаг сидячего положения в лодке
		/// </summary>
		public bool SitInBoat
			{
			get
				{
				return ae6_SitInBoat;
				}
			set
				{
				ae6_SitInBoat = value;
				}
			}
		private bool ae6_SitInBoat = false;

		// Бит 25
		/// <summary>
		/// Возвращает или задаёт флаг утолщённых задних колёс
		/// </summary>
		public bool FatRearWheels
			{
			get
				{
				return ae7_FatRearWheels;
				}
			set
				{
				ae7_FatRearWheels = value;
				}
			}
		private bool ae7_FatRearWheels = false;

		// Бит 26
		/// <summary>
		/// Возвращает или задаёт флаг суженных передних колёс
		/// </summary>
		public bool NarrowFrontWheels
			{
			get
				{
				return ae7_NarrowFrontWheels;
				}
			set
				{
				ae7_NarrowFrontWheels = value;
				}
			}
		private bool ae7_NarrowFrontWheels = false;

		// Бит 27
		/// <summary>
		/// Возвращает или задаёт флаг подавления пробуксовки на песке
		/// </summary>
		public bool GoodInSand
			{
			get
				{
				return ae7_GoodInSand;
				}
			set
				{
				ae7_GoodInSand = value;
				}
			}
		private bool ae7_GoodInSand = false;

		// Бит 28
		/// <summary>
		/// Возвращает специальный флаг пилотируемых летательных аппаратов
		/// </summary>
		public bool SpecialFlight
			{
			get
				{
				return ae7_SpecialFlight;
				}
			}
		private bool ae7_SpecialFlight = false;
		#endregion

		#region AF
		/// <summary>
		/// Варианты типа фар транспортного средства
		/// </summary>
		public enum LightsTypes
			{
			/// <summary>
			/// Длинные
			/// </summary>
			Long = 0,

			/// <summary>
			/// Маленькие
			/// </summary>
			Small = 1,

			/// <summary>
			/// Большие
			/// </summary>
			Big = 2,

			/// <summary>
			/// Высокие
			/// </summary>
			High = 3
			}

		/// <summary>
		/// Возвращает или задаёт тип включённых передних фар
		/// </summary>
		public LightsTypes FrontLights
			{
			get
				{
				return af_FrontLights;
				}
			set
				{
				af_FrontLights = value;
				}
			}
		private LightsTypes af_FrontLights = LightsTypes.Long;
		#endregion

		#region AG
		/// <summary>
		/// Возвращает или задаёт тип включённых задних фар
		/// </summary>
		public LightsTypes RearLights
			{
			get
				{
				return ag_RearLights;
				}
			set
				{
				ag_RearLights = value;
				}
			}
		private LightsTypes ag_RearLights = LightsTypes.Small;
		#endregion
		}
	}
