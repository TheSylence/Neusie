using System.Drawing;
using System.Linq;
using Neusie.Generation.Image;
using NUnit.Framework;

namespace Neusie.Tests.Generation.Image
{
	[TestFixture]
	internal class StringMeasurerTests
	{
		[TestFixture]
		internal class Measure
		{
			[SetUp]
			public void Setup()
			{
				Font = new Font( FontFamily.GenericSerif, 12 );
			}

			[Test]
			public void ShouldContainOneRectanglePerChar()
			{
				// Arrange
				var sut = new StringMeasurer();
				const string word = "testword";

				// Act
				var actual = sut.Measure( word, Font );

				// Assert
				Assert.AreEqual( word.Length, actual.Rectangles.Count );
			}

			[Test]
			public void ShouldHaveOffsetThatAlignsRectToZero()
			{
				// Arrange
				var sut = new StringMeasurer();

				// Act
				var actual = sut.Measure( "test", Font );

				// Assert
				var offset = actual.Offset;
				var rect = actual.Rectangles.First();

				Assert.AreEqual( 0, offset.X + rect.X, 0.00001f );
				Assert.AreEqual( 0, offset.Y + rect.Y, 0.00001f );
			}

			[Test]
			public void ShouldProduceRectangleBiggerThanRectSpacing()
			{
				// Arrange
				var sut = new StringMeasurer();

				// Act
				var actual = sut.Measure( ".", Font );

				// Assert
				Assert.Greater( actual.Rectangles.First().Width, 2 * StringMeasurer.RectSpacing );
				Assert.Greater( actual.Rectangles.First().Height, 2 * StringMeasurer.RectSpacing );
			}

			[Test]
			public void ShouldProduceSmallerRectForSmallerChar()
			{
				// Arrange
				var sut = new StringMeasurer();

				// Act
				var result = sut.Measure( "xX", Font );

				// Assert
				var size1 = result.Rectangles.ElementAt( 0 ).Size;
				var size2 = result.Rectangles.ElementAt( 1 ).Size;

				Assert.LessOrEqual( size1.Width, size2.Width );
				Assert.Less( size1.Height, size2.Height );
			}

			[Test]
			public void ShouldReturnEmptyCollectionForEmptyString()
			{
				// Arrange
				var sut = new StringMeasurer();

				// Act
				var actual = sut.Measure( string.Empty, Font );

				// Assert
				CollectionAssert.IsEmpty( actual.Rectangles );
			}

			[Test]
			public void ShouldReturnMeasurementWithCorrectWord()
			{
				// Arrange
				var sut = new StringMeasurer();
				const string expected = "testword";

				// Act
				var actual = sut.Measure( expected, Font );

				// Assert
				Assert.AreEqual( expected, actual.Word );
			}

			[TearDown]
			public void TearDown()
			{
				Font.Dispose();
			}

			private Font Font;
		}
	}
}