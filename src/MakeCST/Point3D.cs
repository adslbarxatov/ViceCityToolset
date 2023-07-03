using System;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает отдельную 3D-точку
	/// </summary>
	public class Point3D: IEquatable<Point3D>
		{
		/// <summary>
		/// Возвращает координату X точки
		/// </summary>
		public double X
			{
			get
				{
				return x;
				}
			}
		private double x;

		/// <summary>
		/// Возвращает координату Y точки
		/// </summary>
		public double Y
			{
			get
				{
				return y;
				}
			}
		private double y;

		/// <summary>
		/// Возвращает координату Z точки
		/// </summary>
		public double Z
			{
			get
				{
				return z;
				}
			}
		private double z;

		/// <summary>
		/// Конструктор. Создаёт объект-точку
		/// </summary>
		/// <param name="vX">Координата X точки</param>
		/// <param name="vY">Координата Y точки</param>
		/// <param name="vZ">Координата Z точки</param>
		public Point3D (double vX, double vY, double vZ)
			{
			x = vX;
			y = vY;
			z = vZ;
			}

		/// <summary>
		/// Конструктор. Копирует имеющуюся точку
		/// </summary>
		/// <param name="OldPoint">Точка для копирования</param>
		public Point3D (Point3D OldPoint)
			{
			x = OldPoint.x;
			y = OldPoint.y;
			z = OldPoint.z;
			}

		/// <summary>
		/// Метод выполняет сравнение данного экземпляра к указанным
		/// </summary>
		/// <param name="Other">Экземпляр для сравнения</param>
		/// <returns>Возвращает true, если экземпляры совпадают</returns>
		public bool Equals (Point3D Other)
			{
			return ((x == Other.X) && (y == Other.Y) && (z == Other.Z));
			}
		}
	}
