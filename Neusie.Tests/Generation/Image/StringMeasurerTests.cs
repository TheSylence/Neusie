using System;
using System.Drawing;
using System.Linq;
using JetBrains.Annotations;
using Neusie.Generation.Image;
using Xunit;

namespace Neusie.Tests.Generation.Image
{
	[UsedImplicitly]
	public class StringMeasurerTests
	{
		public class Measure : IDisposable
		{
			public Measure()
			{
				Font = new Font( FontFamily.GenericSerif, 12 );
			}

			[Fact]
			public void ShouldContainOneRectanglePerChar()
			{
				// Arrange
				var sut = new StringMeasurer();
				const string word = "testword";

				// Act
				var actual = sut.Measure( word, Font );

				// Assert
				Assert.Equal( word.Length, actual.Rectangles.Count );
			}

			[Fact]
			public void ShouldHaveOffsetThatAlignsRectToZero()
			{
				// Arrange
				var sut = new StringMeasurer();

				// Act
				var actual = sut.Measure( "test", Font );

				// Assert
				var offset = actual.Offset;
				var rect = actual.Rectangles.First();

				Assert.Equal( 0, offset.X + rect.X, 4 );
				Assert.Equal( 0, offset.Y + rect.Y, 4 );
			}

			[Fact]
			public void ShouldProduceRectangleBiggerThanRectSpacing()
			{
				// Arrange
				var sut = new StringMeasurer();

				// Act
				var actual = sut.Measure( ".", Font );

				// Assert
				Assert.True( actual.Rectangles.First().Width > 2 * StringMeasurer.RectSpacing );
				Assert.True( actual.Rectangles.First().Height > 2 * StringMeasurer.RectSpacing );
			}

			[Fact]
			public void ShouldProduceSmallerRectForSmallerChar()
			{
				// Arrange
				var sut = new StringMeasurer();

				// Act
				var result = sut.Measure( "xX", Font );

				// Assert
				var size1 = result.Rectangles.ElementAt( 0 ).Size;
				var size2 = result.Rectangles.ElementAt( 1 ).Size;

				Assert.True( size1.Width < size2.Width );
				Assert.True( size1.Height < size2.Height );
			}

			[Fact]
			public void ShouldReturnEmptyCollectionForEmptyString()
			{
				// Arrange
				var sut = new StringMeasurer();

				// Act
				var actual = sut.Measure( string.Empty, Font );

				// Assert
				Assert.Empty( actual.Rectangles );
			}

			[Fact]
			public void ShouldReturnMeasurementWithCorrectWord()
			{
				// Arrange
				var sut = new StringMeasurer();
				const string expected = "testword";

				// Act
				var actual = sut.Measure( expected, Font );

				// Assert
				Assert.Equal( expected, actual.Word );
			}

			public void Dispose()
			{
				Font.Dispose();
			}

			private readonly Font Font;
		}
	}
}