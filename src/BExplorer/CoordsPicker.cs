using System;
using System.Drawing;
using System.Windows.Forms;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс позволяет указывать координаты объектов
	/// </summary>
	public partial class CoordsPicker: Form
		{
		// Исходные параметры
		private decimal oldX, oldY, oldZ, oldRot;
		private Graphics g;
		private bool active = false;

		/// <summary>
		/// Выбранная X-координата
		/// </summary>
		public decimal PickedX
			{
			get
				{
				return oldX;
				}
			}

		/// <summary>
		/// Выбранная Y-координата
		/// </summary>
		public decimal PickedY
			{
			get
				{
				return oldY;
				}
			}

		/// <summary>
		/// Выбранная Z-координата
		/// </summary>
		public decimal PickedZ
			{
			get
				{
				return oldZ;
				}
			}

		/// <summary>
		/// Выбранный поворот
		/// </summary>
		public decimal PickedRot
			{
			get
				{
				return oldRot;
				}
			}

		/// <summary>
		/// Конструктор. Инициализирует класс и его ограничения
		/// </summary>
		/// <param name="MaxRot">Максимальный поворот</param>
		/// <param name="MaxX">Максимальная абсцисса</param>
		/// <param name="MaxY">Максимальная ордината</param>
		/// <param name="MaxZ">Максимальная аппликата</param>
		/// <param name="MinRot">Минимальный поворот</param>
		/// <param name="MinX">Минимальная абсцисса</param>
		/// <param name="MinY">Минимальная ордината</param>
		/// <param name="MinZ">Минимальная аппликата</param>
		public CoordsPicker (decimal MinX, decimal MaxX, decimal MinY, decimal MaxY, decimal MinZ, decimal MaxZ,
			decimal MinRot, decimal MaxRot)
			{
			InitializeComponent ();

			// Установка ограничений
			PickX.Maximum = MaxX;
			PickX.Minimum = MinX;
			PickY.Maximum = MaxY;
			PickY.Minimum = MinY;
			PickZ.Maximum = MaxZ;
			PickZ.Minimum = MinZ;
			PickRot.Maximum = MaxRot;
			PickRot.Minimum = MinRot;

			VertPictScroll.Maximum = RD_AAOW.Properties.ViceCityToolset.DefaultMap.Height - PictMap.Height;
			HorPictScroll.Maximum = RD_AAOW.Properties.ViceCityToolset.DefaultMap.Width - PictMap.Width;
			}

		/// <summary>
		/// Метод запускает интерфейс выбора координат
		/// </summary>
		/// <param name="OldRotation">Исходный поворот</param>
		/// <param name="OldX">Исходная абсцисса</param>
		/// <param name="OldY">Исходная ордината</param>
		/// <param name="OldZ">Исходная аппликата</param>
		/// <param name="ViewOnly">Флаг режима, запрещающего выбор координат</param>
		public void PickCoords (decimal OldX, decimal OldY, decimal OldZ, decimal OldRotation, bool ViewOnly)
			{
			// Передача параметров
			oldX = PickX.Value = OldX;
			oldY = PickY.Value = OldY;
			oldZ = PickZ.Value = OldZ;
			oldRot = PickRot.Value = OldRotation;
			PickX.Enabled = PickY.Enabled = PickZ.Enabled = PickRot.Enabled = !ViewOnly;
			PickOK.Visible = !ViewOnly;

			RDLocale.SetControlsText (this);
			PickAbort.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Close);
			PickOK.Text = RDLocale.GetDefaultText (RDLDefaultTexts.Button_Select);
			this.Text = RDLocale.GetText ("CoordsPicker_Title");

			// Запуск
			g = Graphics.FromHwnd (PictMap.Handle);
			active = true;
			this.ShowDialog ();
			}

		// Выбор координат
		private void PickOK_Click (object sender, EventArgs e)
			{
			oldX = PickX.Value;
			oldY = PickY.Value;
			oldZ = PickZ.Value;
			oldRot = PickRot.Value;

			this.Close ();
			}

		// Отмена
		private void PickAbort_Click (object sender, EventArgs e)
			{
			this.Close ();
			}

		// Изменение смещения изображения
		private void VertPictScroll_ValueChanged (object sender, EventArgs e)
			{
			g.DrawImage (RD_AAOW.Properties.ViceCityToolset.DefaultMap, new Point (-HorPictScroll.Value,
				-VertPictScroll.Value));

			DrawPoint ();
			}

		private void HorPictScroll_ValueChanged (object sender, EventArgs e)
			{
			g.DrawImage (RD_AAOW.Properties.ViceCityToolset.DefaultMap, new Point (-HorPictScroll.Value,
				-VertPictScroll.Value));

			DrawPoint ();
			}

		// Указание новой позиции
		private void PictMap_MouseDown (object sender, MouseEventArgs e)
			{
			if (!PickOK.Visible)
				return;

			PickX.Value = PickX.Minimum + HorPictScroll.Value + e.X;
			PickY.Value = PickY.Maximum - VertPictScroll.Value - e.Y;

			DrawPoint ();
			}

		// Отрисовка позиции
		private void DrawPoint ()
			{
			Pen p1 = new Pen (Color.FromArgb (255, 0, 0), 5),
				p2 = new Pen (Color.FromArgb (255, 0, 0), 1);

			g.DrawEllipse (p1, (int)(PickX.Value - PickX.Minimum - HorPictScroll.Value - 2),
				(int)(-PickY.Value - PickY.Minimum - VertPictScroll.Value - 2), 4, 4);
			g.DrawLine (p2, (int)(PickX.Value - PickX.Minimum - HorPictScroll.Value),
				(int)(-PickY.Value - PickY.Minimum - VertPictScroll.Value),
				(int)(Math.Cos ((double)(PickRot.Value + 90) / 180.0 * Math.PI) * 20.0 +
				(double)(PickX.Value - PickX.Minimum - HorPictScroll.Value)),
				(int)(-Math.Sin ((double)(PickRot.Value + 90) / 180.0 * Math.PI) * 20.0 +
				(double)(-PickY.Value - PickY.Minimum - VertPictScroll.Value)));
			}

		// Запуск формы
		private void CoordsPicker_Load (object sender, EventArgs e)
			{
			// Установка горизонтальной позиции прокрутки
			if (PickX.Value < PickX.Minimum + PictMap.Width / 2)
				HorPictScroll.Value = 0;
			else if (PickX.Value > PickX.Maximum - PictMap.Width / 2)
				HorPictScroll.Value = HorPictScroll.Maximum;
			else
				HorPictScroll.Value = (int)(PickX.Value - PickX.Minimum - PictMap.Width / 2);

			// Установка вертикальной позиции прокрутки
			if (PickY.Value < PickY.Minimum + PictMap.Height / 2)
				VertPictScroll.Value = 0;
			else if (PickY.Value > PickY.Maximum - PictMap.Height / 2)
				VertPictScroll.Value = VertPictScroll.Maximum;
			else
				VertPictScroll.Value = (int)(-PickY.Value - PickY.Minimum - PictMap.Height / 2);
			}

		// Таймер перерисовки
		private void DrawTimer_Tick (object sender, EventArgs e)
			{
			if (active)
				HorPictScroll_ValueChanged (null, null);
			}

		// Скрытие формы
		private void CoordsPicker_FormClosing (object sender, FormClosingEventArgs e)
			{
			g.Dispose ();
			active = false;
			}
		}
	}
