using System;
using System.Collections.Generic;
using System.IO;

namespace RD_AAOW
	{
	/// <summary>
	/// Класс описывает спецификации формата DFF
	/// Информация: http://gtamodding.ru/wiki/DFF
	///             https://gtamods.com/wiki/List_of_RW_section_IDs
	/// </summary>
	public class DFFReader
		{
		// Константы

		/// <summary>
		/// Расширение формата файла
		/// </summary>
		public const string MasterExtension = ".dff";

		// Переменные

		/// <summary>
		/// Возвращает вершины, извлечённые из файла DFF
		/// </summary>
		public List<Point3D> ExtractedPoints
			{
			get
				{
				return extractedPoints;
				}
			}
		private List<Point3D> extractedPoints = new List<Point3D> ();

		/// <summary>
		/// Вовращает треугольники, извлечённые из файла DFF
		/// </summary>
		public List<Point3D> ExtractedTriangles
			{
			get
				{
				return extractedTriangles;
				}
			}
		private List<Point3D> extractedTriangles = new List<Point3D> ();

		/// <summary>
		/// Допустимые секции DFF
		/// </summary>
		private enum Sections
			{
			/// <summary>
			/// Пустая секция
			/// </summary>
			Null = 0x00,

			/// <summary>
			/// Structure
			/// </summary>
			Struct = 0x01,

			/// <summary>
			/// String
			/// </summary>
			String = 0x02,

			/// <summary>
			/// Extension
			/// </summary>
			Extension = 0x03,

			/// <summary>
			/// Камера (тип RwCamera)
			/// </summary>
			rwID_CAMERA = 0x05,

			/// <summary>
			/// Текстура (тип RwTexture)
			/// </summary>
			rwID_TEXTURE = 0x06,

			/// <summary>
			/// Материал модели (тип RpMaterial)
			/// </summary>
			rwID_MATERIAL = 0x07,

			/// <summary>
			/// Materials list
			/// </summary>
			MaterialsList = 0x08,

			/// <summary>
			/// Atomic section
			/// </summary>
			AtomicSection = 0x09,

			/// <summary>
			/// Plane section
			/// </summary>
			PlaneSection = 0x0A,

			/// <summary>
			/// Мир / уровень (тип RpWorld)
			/// </summary>
			rwID_WORLD = 0x0B,

			/// <summary>
			/// Spline
			/// </summary>
			Spline = 0x0C,

			/// <summary>
			/// Матрица (тип RwMatrix)
			/// </summary>
			rwID_MATRIX = 0x0D,

			/// <summary>
			/// Frames list
			/// </summary>
			FramesList = 0x0E,

			/// <summary>
			/// Геометрия модели (тип RpGeometry)
			/// </summary>
			rwID_GEOMETRY = 0x0F,

			/// <summary>
			/// Clump (тип RpClump)
			/// </summary>
			rwID_CLUMP = 0x10,

			/// <summary>
			/// Источник освещения (тип RpLight)
			/// </summary>
			rwID_LIGHT = 0x12,

			/// <summary>
			/// Unicode string
			/// </summary>
			UnicodeString = 0x13,

			/// <summary>
			/// Составная часть модели (тип RpAtomic)
			/// </summary>
			rwID_ATOMIC = 0x14,

			/// <summary>
			/// Texture native
			/// </summary>
			TextureNative = 0x15,

			/// <summary>
			/// Список текстур (тип RwTexDictionary)
			/// </summary>
			rwID_TEXDICTIONARY = 0x16,

			/// <summary>
			/// Animation database
			/// </summary>
			AnimationDatabase = 0x17,

			/// <summary>
			/// Картинка (тип RwImage)
			/// </summary>
			rwID_IMAGE = 0x18,

			/// <summary>
			/// Skin animation
			/// </summary>
			SkinAnimation = 0x19,

			/// <summary>
			/// Geometry list
			/// </summary>
			GeometryList = 0x1A,

			/// <summary>
			/// Anim animation
			/// </summary>
			AnimAnimation = 0x1B,

			/// <summary>
			/// Team
			/// </summary>
			Team = 0x1C,

			/// <summary>
			/// Crowd
			/// </summary>
			Crowd = 0x1D,

			/// <summary>
			/// Delta morph animation
			/// </summary>
			DeltaMorphAnimation = 0x1E,

			/// <summary>
			/// Right to render
			/// </summary>
			RightToRender = 0x1F,

			/// <summary>
			/// MultiTexture effect native
			/// </summary>
			MultiTextureEffectNative = 0x20,

			/// <summary>
			/// MultiTexture effect dictionary
			/// </summary>
			MultiTextureEffectDictionary = 0x21,

			/// <summary>
			/// Team dictionary
			/// </summary>
			TeamDictionary = 0x22,

			/// <summary>
			/// Platform-independent textures dictionary
			/// </summary>
			PlatformIndependentTextureDictionary = 0x23,

			/// <summary>
			/// Table of contents
			/// </summary>
			TableOfContents = 0x24,

			/// <summary>
			/// Particle standard global data
			/// </summary>
			ParticleStandardGlobalData = 0x25,

			/// <summary>
			/// AltPipe
			/// </summary>
			AltPipe = 0x26,

			/// <summary>
			/// Platform-independent peds
			/// </summary>
			PlatformIndependentPeds = 0x27,

			/// <summary>
			/// Patch mesh
			/// </summary>
			PatchMesh = 0x28,

			/// <summary>
			/// Старт группы Chunk (тип RwChunkGroup)
			/// </summary>
			rwID_CHUNKGROUPSTART = 0x29,

			/// <summary>
			/// Конец группы Chunk (тип RwChunkGroup)
			/// </summary>
			rwID_CHUNKGROUPEND = 0x2A,

			/// <summary>
			/// UV animation dictionary
			/// </summary>
			UVAnimationDictionary = 0x2B,

			/// <summary>
			/// Collision tree
			/// </summary>
			CollisionTree = 0x2C
			}

		/// <summary>
		/// Конструктор. Извлекает из файла DFF информацию о вершинах и треугольниках модели
		/// </summary>
		/// <param name="BR">Открытый файловый поток DFF</param>
		public DFFReader (BinaryReader BR)
			{
			ExtractPointsAndTriangles (BR);
			}

		// Рекурсивный экстрактор структур DFF
		private void ExtractPointsAndTriangles (BinaryReader BR)
			{
			// Контроль
			if (BR == null)
				return;

			// Чтение
			while (BR.BaseStream.Position < BR.BaseStream.Length)
				{
				// Поиск нужной секции
				Sections section = (Sections)BR.ReadUInt32 ();
				UInt32 length = BR.ReadUInt32 ();
				UInt32 version = BR.ReadUInt32 ();

				// Отсечка для версий и недопустимых секций
				if (version != 0x1003FFFF)
					return;

				if (section == Sections.Null)
					return;

				switch (section)
					{
					case Sections.rwID_CLUMP:
						ExtractPointsAndTriangles (BR);
						return;

					case Sections.FramesList:
						section = (Sections)BR.ReadUInt32 ();
						length = BR.ReadUInt32 ();
						version = BR.ReadUInt32 ();

						// Пропуск настроек фреймов
						UInt32 frames = BR.ReadUInt32 ();
						BR.BaseStream.Position += (frames * (36 + 12 + 4 + 4));
						break;

					case Sections.GeometryList:
						section = (Sections)BR.ReadUInt32 ();
						length = BR.ReadUInt32 ();
						version = BR.ReadUInt32 ();

						// Получение количества геометрий объекта
						UInt32 geometries = BR.ReadUInt32 ();
						break;

					case Sections.rwID_GEOMETRY:
						section = (Sections)BR.ReadUInt32 ();
						length = BR.ReadUInt32 ();
						version = BR.ReadUInt32 ();

						// Получение параметров геометрии объекта
						UInt32 format = BR.ReadUInt32 ();
						UInt32 triangles = BR.ReadUInt32 ();
						UInt32 vertices = BR.ReadUInt32 ();
						UInt32 morphTargets = BR.ReadUInt32 ();
						UInt32 texCoords = (format & 0xFF0000) >> 16;

						// Запрет недопустимых параметров
						if (((format & 0x0C) != 0x0C) || (morphTargets != 1))
							return;

						// Пропуск цветов предосвещения
						BR.BaseStream.Position += (4 * sizeof (byte) * vertices);

						// Пропуск UV-координат текстур
						BR.BaseStream.Position += (2 * sizeof (float) * vertices * texCoords);

						// Чтение треугольников
						for (int i = 0; i < triangles; i++)
							{
							UInt16 v2 = BR.ReadUInt16 ();
							UInt16 v1 = BR.ReadUInt16 ();
							UInt16 material = BR.ReadUInt16 ();
							UInt16 v3 = BR.ReadUInt16 ();
							extractedTriangles.Add (new Point3D (v1, v2, v3));
							}

						// Пропуск ограничивающей сферы
						BR.BaseStream.Position += (4 * sizeof (float));

						// Запрет недопустимых параметров
						UInt32 hasVertices = BR.ReadUInt32 ();
						if (hasVertices == 0)
							return;
						UInt32 hasNormals = BR.ReadUInt32 ();

						// Чтение вершин
						for (int i = 0; i < vertices; i++)
							{
							float x = BR.ReadSingle ();
							float y = BR.ReadSingle ();
							float z = BR.ReadSingle ();
							extractedPoints.Add (new Point3D (x, y, z));
							}

						// Завершено
						break;

					default:
						// Пропуск всего остального
						BR.BaseStream.Position += length;
						continue;
					}
				}
			}
		}
	}
