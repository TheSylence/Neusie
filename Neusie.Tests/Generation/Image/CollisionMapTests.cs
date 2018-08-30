using System.Drawing;
using System.Linq;
using Neusie.Generation.Image;
using Xunit;

namespace Neusie.Tests.Generation.Image
{
	public class CollisionMapTests
	{
		public class Check
		{
			[Fact]
			public void ShouldBePossibleWhenMapIsEmpty()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new RectangleF( 1, 1, 3, 3 ) );

				// Assert
				Assert.True( actual );
			}

			[Fact]
			public void ShouldBePossibleWithNonOverlappingRectangle()
			{
				// Arrange
				var sut = new CollisionMap( 10, 10 );
				sut.Insert( new RectangleF( 1, 1, 1, 1 ) );

				// Act
				var actual = sut.Check( new RectangleF( 4, 4, 1, 1 ) );

				// Assert
				Assert.True( actual );
			}

			[Fact]
			public void ShouldCheckForBottomBorder()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new Rectangle( 1, 4, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldCheckForLeftBorder()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new Rectangle( -1, 1, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldCheckForOutOfBottomBound()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new Rectangle( 1, 6, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldCheckForOutOfLeftBound()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new Rectangle( -5, 1, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldCheckForOutOfRightBound()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new Rectangle( 6, 1, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldCheckForOutOfTopBound()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new Rectangle( 1, -5, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldCheckForRightBorder()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new Rectangle( 4, 1, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldCheckForTopBorder()
			{
				// Arrange
				var sut = new CollisionMap( 5, 5 );

				// Act
				var actual = sut.Check( new Rectangle( 1, -1, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldNotBePossibleWhenCompletelyInsideExistingRect()
			{
				// Arrange
				var sut = new CollisionMap( 10, 10 );
				sut.Insert( new RectangleF( 1, 1, 7, 7 ) );

				// Act
				var actual = sut.Check( new RectangleF( 3, 3, 1, 1 ) );

				// Assert
				Assert.False( actual );
			}

			[Fact]
			public void ShouldNotBePossibleWhenOverlappingWithExisting()
			{
				// Arrange
				var sut = new CollisionMap( 10, 10 );
				sut.Insert( new RectangleF( 1, 1, 3, 3 ) );

				// Act
				var actual = sut.Check( new RectangleF( 2, 2, 3, 3 ) );

				// Assert
				Assert.False( actual );
			}
		}

		public class Rectangles
		{
			[Fact]
			public void ShouldBeEmptyWhenNothingWasAdded()
			{
				// Arrange
				var sut = new CollisionMap( 1, 1 );

				// Act
				var actual = sut.Rectangles.ToList();

				// Assert
				Assert.Empty( actual );
			}

			[Fact]
			public void ShouldContainAddedElements()
			{
				// Arrange
				var sut = new CollisionMap( 1, 1 );
				var expected = new RectangleF( 1, 1, 1, 1 );
				sut.Insert( expected );

				// Act
				var actual = sut.Rectangles.ToList();

				// Assert
				Assert.Contains( expected, actual );
			}
		}
	}
}