﻿namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает специальный дескриптор мотоцикла
	/// </summary>
	public class BikeHandlingDescriptor:HandlingDescriptor
		{
		/// <summary>
		/// Символ-признак дескриптора
		/// </summary>
		public const string IdentifyingSymbol = "!";

		/// <summary>
		/// Строка заголовка таблицы данных дескриптора
		/// </summary>
		public const string TableHeader = "	A		B		C		D		E		F		G		H		I		J		" +
			"K		L		M		N		O		P";

		/// <summary>
		/// Конструктор. Загружает параметры транспортного средства из массива строк
		/// </summary>
		/// <param name="Values">Массив строк, содержащий значения параметров</param>
		public BikeHandlingDescriptor (string[] Values)
			{
			// Контроль массива
			if ((Values.Length != 17) || (Values[0] != IdentifyingSymbol))
				{
				return;
				}

			// Загрузка параметров
			a_VehicleIdentifier = Values[1];

			try
				{
				ForwardLeaningCoM = float.Parse (Values[2], cie.NumberFormat);
				ForwardLeaningForce = float.Parse (Values[3], cie.NumberFormat);
				BackwardLeaningCoM = float.Parse (Values[4], cie.NumberFormat);
				BackwardLeaningForce = float.Parse (Values[5], cie.NumberFormat);
				MaxLeaningAngle = float.Parse (Values[6], cie.NumberFormat);
				MaxDriverLeaningAngle = float.Parse (Values[7], cie.NumberFormat);
				MaxDecelerationLeaningAngle = float.Parse (Values[8], cie.NumberFormat);
				SteeringOnSpeed = float.Parse (Values[9], cie.NumberFormat);
				CoMWithoutDriver = float.Parse (Values[10], cie.NumberFormat);
				SteeringOnSlippery = float.Parse (Values[11], cie.NumberFormat);
				StoppieAngle = float.Parse (Values[12], cie.NumberFormat);
				WheelieAngle = float.Parse (Values[13], cie.NumberFormat);
				WheelieStabilization = float.Parse (Values[14], cie.NumberFormat);
				SteeringOnWheelie = float.Parse (Values[15], cie.NumberFormat);
				StoppieStabilization = float.Parse (Values[16], cie.NumberFormat);
				}
			catch
				{
				return;
				}

			// Завершено
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
				{
				return result;
				}

			// Сборка
			result = IdentifyingSymbol + " " + VehicleIdentifier.PadRight (8, ' ') + "\t";
			result += (b_ForwardLeaningCoM.ToString ("F2", cie.NumberFormat) + "\t");
			result += (c_ForwardLeaningForce.ToString ("F3", cie.NumberFormat) + "\t");
			result += (d_BackwardLeaningCoM.ToString ("F2", cie.NumberFormat) + "\t");
			result += (e_BackwardLeaningForce.ToString ("F3", cie.NumberFormat) + "\t");
			result += (f_MaxLeaningAngle.ToString ("F1", cie.NumberFormat) + "\t");
			result += (g_MaxDriverLeaningAngle.ToString ("F1", cie.NumberFormat) + "\t");
			result += (h_MaxDecelerationLeaningAngle.ToString ("F3", cie.NumberFormat) + "\t");
			result += (i_SteeringOnSpeed.ToString ("F2", cie.NumberFormat) + "\t");
			result += (j_CoMWithoutDriver.ToString ("F2", cie.NumberFormat) + "\t");
			result += (k_SteeringOnSlippery.ToString ("F2", cie.NumberFormat) + "\t");
			result += (l_StoppieAngle.ToString ("F1", cie.NumberFormat) + "\t");
			result += (m_WheelieAngle.ToString ("F1", cie.NumberFormat) + "\t");
			result += (n_WheelieStabilization.ToString ("F3", cie.NumberFormat) + "\t");
			result += (o_SteeringOnWheelie.ToString ("F2", cie.NumberFormat) + "\t");
			result += p_StoppieStabilization.ToString ("F2", cie.NumberFormat);

			// Завершено
			return result;
			}

		// Region A -> HandlingDescriptor

		#region B
		/// <summary>
		/// Минимальное смещение центра тяжести при наклоне вперёд
		/// </summary>
		public const float ForwardLeaningCoM_Min = -2.0f;

		/// <summary>
		/// Максимальное смещение центра тяжести при наклоне вперёд
		/// </summary>
		public const float ForwardLeaningCoM_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт смещение центра тяжести при наклоне вперёд
		/// </summary>
		public float ForwardLeaningCoM
			{
			get
				{
				return b_ForwardLeaningCoM;
				}
			set
				{
				b_ForwardLeaningCoM = CheckRange (value, ForwardLeaningCoM_Min, ForwardLeaningCoM_Max);
				}
			}
		private float b_ForwardLeaningCoM = 0.3f;
		#endregion

		#region C
		/// <summary>
		/// Минимальная сила наклона вперёд
		/// </summary>
		public const float ForwardLeaningForce_Min = 0.0f;

		/// <summary>
		/// Максимальная сила наклона вперёд
		/// </summary>
		public const float ForwardLeaningForce_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт силу наклона вперёд
		/// </summary>
		public float ForwardLeaningForce
			{
			get
				{
				return c_ForwardLeaningForce;
				}
			set
				{
				c_ForwardLeaningForce = CheckRange (value, ForwardLeaningForce_Min, ForwardLeaningForce_Max);
				}
			}
		private float c_ForwardLeaningForce = 0.3f;
		#endregion

		#region D
		/// <summary>
		/// Минимальное смещение центра тяжести при наклоне назад
		/// </summary>
		public const float BackwardLeaningCoM_Min = -2.0f;

		/// <summary>
		/// Максимальное смещение центра тяжести при наклоне назад
		/// </summary>
		public const float BackwardLeaningCoM_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт смещение центра тяжести при наклоне назад
		/// </summary>
		public float BackwardLeaningCoM
			{
			get
				{
				return d_BackwardLeaningCoM;
				}
			set
				{
				d_BackwardLeaningCoM = CheckRange (value, BackwardLeaningCoM_Min, BackwardLeaningCoM_Max);
				}
			}
		private float d_BackwardLeaningCoM = 0.3f;
		#endregion

		#region E
		/// <summary>
		/// Минимальная сила наклона назад
		/// </summary>
		public const float BackwardLeaningForce_Min = 0.0f;

		/// <summary>
		/// Максимальная сила наклона назад
		/// </summary>
		public const float BackwardLeaningForce_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт силу наклона назад
		/// </summary>
		public float BackwardLeaningForce
			{
			get
				{
				return e_BackwardLeaningForce;
				}
			set
				{
				e_BackwardLeaningForce = CheckRange (value, BackwardLeaningForce_Min, BackwardLeaningForce_Max);
				}
			}
		private float e_BackwardLeaningForce = 0.3f;
		#endregion

		#region F
		/// <summary>
		/// Минимальный угол наклона транспортного средства
		/// </summary>
		public const float MaxLeaningAngle_Min = 0.0f;

		/// <summary>
		/// Максимальный угол наклона транспортного средства
		/// </summary>
		public const float MaxLeaningAngle_Max = 90.0f;

		/// <summary>
		/// Возвращает или задаёт угол наклона транспортного средства
		/// </summary>
		public float MaxLeaningAngle
			{
			get
				{
				return f_MaxLeaningAngle;
				}
			set
				{
				f_MaxLeaningAngle = CheckRange (value, MaxLeaningAngle_Min, MaxLeaningAngle_Max);
				}
			}
		private float f_MaxLeaningAngle = 40.0f;
		#endregion

		#region G
		/// <summary>
		/// Минимальный угол наклона водителя
		/// </summary>
		public const float MaxDriverLeaningAngle_Min = 0.0f;

		/// <summary>
		/// Максимальный угол наклона водителя
		/// </summary>
		public const float MaxDriverLeaningAngle_Max = 90.0f;

		/// <summary>
		/// Возвращает или задаёт угол наклона водителя
		/// </summary>
		public float MaxDriverLeaningAngle
			{
			get
				{
				return g_MaxDriverLeaningAngle;
				}
			set
				{
				g_MaxDriverLeaningAngle = CheckRange (value, MaxDriverLeaningAngle_Min, MaxDriverLeaningAngle_Max);
				}
			}
		private float g_MaxDriverLeaningAngle = 35.0f;
		#endregion

		#region H
		/// <summary>
		/// Минимальный угол наклона транспортного средства при торможении
		/// </summary>
		public const float MaxDecelerationLeaningAngle_Min = 0.0f;

		/// <summary>
		/// Максимальный угол наклона транспортного средства при торможении
		/// </summary>
		public const float MaxDecelerationLeaningAngle_Max = 5.0f;

		/// <summary>
		/// Возвращает или задаёт угол наклона транспортного средства при торможении
		/// </summary>
		public float MaxDecelerationLeaningAngle
			{
			get
				{
				return h_MaxDecelerationLeaningAngle;
				}
			set
				{
				h_MaxDecelerationLeaningAngle = CheckRange (value, MaxDecelerationLeaningAngle_Min, MaxDecelerationLeaningAngle_Max);
				}
			}
		private float h_MaxDecelerationLeaningAngle = 0.9f;
		#endregion

		#region I
		/// <summary>
		/// Минимальный показатель регулировки направления на скорости
		/// </summary>
		public const float SteeringOnSpeed_Min = 0.0f;

		/// <summary>
		/// Максимальный показатель регулировки направления на скорости
		/// </summary>
		public const float SteeringOnSpeed_Max = 3.0f;

		/// <summary>
		/// Возвращает или задаёт показатель регулировки направления на скорости
		/// </summary>
		public float SteeringOnSpeed
			{
			get
				{
				return i_SteeringOnSpeed;
				}
			set
				{
				i_SteeringOnSpeed = CheckRange (value, SteeringOnSpeed_Min, SteeringOnSpeed_Max);
				}
			}
		private float i_SteeringOnSpeed = 0.7f;
		#endregion

		#region J
		/// <summary>
		/// Минимальное смещение центра тяжести при отсутствии водителя
		/// </summary>
		public const float CoMWithoutDriver_Min = 0.0f;

		/// <summary>
		/// Максимальное смещение центра тяжести при отсутствии водителя
		/// </summary>
		public const float CoMWithoutDriver_Max = 3.0f;

		/// <summary>
		/// Возвращает или задаёт смещение центра тяжести при отсутствии водителя
		/// </summary>
		public float CoMWithoutDriver
			{
			get
				{
				return j_CoMWithoutDriver;
				}
			set
				{
				j_CoMWithoutDriver = CheckRange (value, CoMWithoutDriver_Min, CoMWithoutDriver_Max);
				}
			}
		private float j_CoMWithoutDriver = 0.7f;
		#endregion

		#region K
		/// <summary>
		/// Минимальный показатель регулировки направления на скользких поверхностях
		/// </summary>
		public const float SteeringOnSlippery_Min = -2.0f;

		/// <summary>
		/// Максимальный показатель регулировки направления на скользких поверхностях
		/// </summary>
		public const float SteeringOnSlippery_Max = 2.0f;

		/// <summary>
		/// Возвращает или задаёт показатель регулировки направления на скользких поверхностях
		/// </summary>
		public float SteeringOnSlippery
			{
			get
				{
				return k_SteeringOnSlippery;
				}
			set
				{
				k_SteeringOnSlippery = CheckRange (value, SteeringOnSlippery_Min, SteeringOnSlippery_Max);
				}
			}
		private float k_SteeringOnSlippery = 0.1f;
		#endregion

		#region L
		/// <summary>
		/// Минимальный угол наклона, необходимый для выполнения трюка stoppie
		/// </summary>
		public const float StoppieAngle_Min = 0.0f;

		/// <summary>
		/// Максимальный угол наклона, необходимый для выполнения трюка stoppie
		/// </summary>
		public const float StoppieAngle_Max = 90.0f;

		/// <summary>
		/// Возвращает или задаёт угол наклона, необходимый для выполнения трюка stoppie
		/// </summary>
		public float StoppieAngle
			{
			get
				{
				return l_StoppieAngle;
				}
			set
				{
				l_StoppieAngle = CheckRange (value, StoppieAngle_Min, StoppieAngle_Max);
				}
			}
		private float l_StoppieAngle = 35.0f;
		#endregion

		#region M
		/// <summary>
		/// Минимальный угол наклона, необходимый для выполнения трюка wheelie
		/// </summary>
		public const float WheelieAngle_Min = -90.0f;

		/// <summary>
		/// Максимальный угол наклона, необходимый для выполнения трюка wheelie
		/// </summary>
		public const float WheelieAngle_Max = 0.0f;

		/// <summary>
		/// Возвращает или задаёт угол наклона, необходимый для выполнения трюка wheelie
		/// </summary>
		public float WheelieAngle
			{
			get
				{
				return m_WheelieAngle;
				}
			set
				{
				m_WheelieAngle = CheckRange (value, WheelieAngle_Min, WheelieAngle_Max);
				}
			}
		private float m_WheelieAngle = -40.0f;
		#endregion

		#region N
		/// <summary>
		/// Минимальный показатель стабилизации при трюке wheelie
		/// </summary>
		public const float WheelieStabilization_Min = -0.1f;

		/// <summary>
		/// Максимальный показатель стабилизации при трюке wheelie
		/// </summary>
		public const float WheelieStabilization_Max = 0.1f;

		/// <summary>
		/// Возвращает или задаёт показатель стабилизации при трюке wheelie
		/// </summary>
		public float WheelieStabilization
			{
			get
				{
				return n_WheelieStabilization;
				}
			set
				{
				n_WheelieStabilization = CheckRange (value, WheelieStabilization_Min, WheelieStabilization_Max);
				}
			}
		private float n_WheelieStabilization = -0.007f;
		#endregion

		#region O
		/// <summary>
		/// Минимальный показатель регулировки направления при трюке wheelie
		/// </summary>
		public const float SteeringOnWheelie_Min = 0.0f;

		/// <summary>
		/// Максимальный показатель регулировки направления при трюке wheelie
		/// </summary>
		public const float SteeringOnWheelie_Max = 3.0f;

		/// <summary>
		/// Возвращает или задаёт показатель регулировки направления при трюке wheelie
		/// </summary>
		public float SteeringOnWheelie
			{
			get
				{
				return o_SteeringOnWheelie;
				}
			set
				{
				o_SteeringOnWheelie = CheckRange (value, SteeringOnWheelie_Min, SteeringOnWheelie_Max);
				}
			}
		private float o_SteeringOnWheelie = 0.7f;
		#endregion

		#region P
		/// <summary>
		/// Минимальный показатель стабилизации при трюке stoppie
		/// </summary>
		public const float StoppieStabilization_Min = 0.0f;

		/// <summary>
		/// Максимальный показатель стабилизации при трюке stoppie
		/// </summary>
		public const float StoppieStabilization_Max = 3.0f;

		/// <summary>
		/// Возвращает или задаёт показатель стабилизации при трюке stoppie
		/// </summary>
		public float StoppieStabilization
			{
			get
				{
				return p_StoppieStabilization;
				}
			set
				{
				p_StoppieStabilization = CheckRange (value, StoppieStabilization_Min, StoppieStabilization_Max);
				}
			}
		private float p_StoppieStabilization = 0.5f;
		#endregion
		}
	}
