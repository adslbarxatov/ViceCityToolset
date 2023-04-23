namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает 3D-треугольник
	/// </summary>
	public class Triangle3D
		{
		/// <summary>
		/// Возвращает первую точку треугольника
		/// </summary>
		public Point3D Point1
			{
			get
				{
				return point1;
				}
			}
		private Point3D point1;

		/// <summary>
		/// Возвращает вторую точку треугольника
		/// </summary>
		public Point3D Point2
			{
			get
				{
				return point2;
				}
			}
		private Point3D point2;

		/// <summary>
		/// Возвращает третью точку треугольника
		/// </summary>
		public Point3D Point3
			{
			get
				{
				return point3;
				}
			}
		private Point3D point3;

		/// <summary>
		/// Возвращает или задаёт порядковый номер первой точки треугольника
		/// </summary>
		public uint Point1ArrayPosition
			{
			get
				{
				return point1ArrayPosition;
				}
			set
				{
				point1ArrayPosition = value;
				}
			}
		private uint point1ArrayPosition;

		/// <summary>
		/// Возвращает или задаёт порядковый номер второй точки треугольника
		/// </summary>
		public uint Point2ArrayPosition
			{
			get
				{
				return point2ArrayPosition;
				}
			set
				{
				point2ArrayPosition = value;
				}
			}
		private uint point2ArrayPosition;

		/// <summary>
		/// Возвращает или задаёт порядковый номер третьей точки треугольника
		/// </summary>
		public uint Point3ArrayPosition
			{
			get
				{
				return point3ArrayPosition;
				}
			set
				{
				point3ArrayPosition = value;
				}
			}
		private uint point3ArrayPosition;

		/// <summary>
		/// Конструктор. Создаёт объект-треугольник
		/// </summary>
		/// <param name="FirstPoint">Первая точка треугольника</param>
		/// <param name="SecondPoint">Вторая точка треугольника</param>
		/// <param name="ThirdPoint">Третья точка треугольника</param>
		public Triangle3D (Point3D FirstPoint, Point3D SecondPoint, Point3D ThirdPoint)
			{
			point1 = new Point3D (FirstPoint);
			point2 = new Point3D (SecondPoint);
			point3 = new Point3D (ThirdPoint);
			}

		/// <summary>
		/// Конструктор. Дублирует объект-треугольник
		/// </summary>
		/// <param name="OldTriangle">Дублируемый треугольник</param>
		public Triangle3D (Triangle3D OldTriangle)
			{
			point1 = OldTriangle.point1;
			point2 = OldTriangle.point2;
			point3 = OldTriangle.point3;
			}
		}
	}
