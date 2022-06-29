using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using Collections.Interfaces;
using NUnit.Framework;

namespace Collections.Tests
{
	public class DynamicArrayTests
	{
		private readonly Fixture _fixture = new Fixture();

		private static IEnumerable<TestCaseData> ArrayInitData
		{
			get
			{
				yield return new TestCaseData("a", 3);
				yield return new TestCaseData("a", 5);
				yield return new TestCaseData("a", 7);
				yield return new TestCaseData(1, 3);
				yield return new TestCaseData(1, 5);
				yield return new TestCaseData(1, 10);
			}
		}

		#region Ctor tests

		[Test]
		public void Ctor_WithoutParameters_MustCreateDynamicArrayWithCapacity8()
		{
			var dynamicArray = GetDynamicArray<string>();

			Assert.AreEqual(8, dynamicArray.Capacity);
		}

		[Test]
		public void Ctor_WithoutParameters_MustCreateDynamicArrayWithLength0()
		{
			var dynamicArray = GetDynamicArray<string>();

			Assert.AreEqual(0, dynamicArray.Length);
		}

		[TestCase(0, ExpectedResult = 0)]
		[TestCase(1, ExpectedResult = 1)]
		[TestCase(100, ExpectedResult = 100)]
		public int Ctor_WithNumberParameter_MustCreateDynamicArrayWithPassedCapacity(int capacity)
		{
			var dynamicArray = GetDynamicArray<string>(capacity);

			return dynamicArray.Capacity;
		}

		[TestCase(0)]
		[TestCase(1)]
		[TestCase(100)]
		public void Ctor_WithNumberParameter_MustCreateDynamicArrayWithLength0(int capacity)
		{
			var dynamicArray = GetDynamicArray<string>(capacity);

			Assert.AreEqual(0, dynamicArray.Length);
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Ctor_WithInitialValues_MustCreateDynamicArrayWithInitialItems<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);

			for (int i = 0; i < initialItems.Length; i++)
			{
				Assert.AreEqual(initialItems[i], dynamicArray[i]);
			}
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Ctor_WithInitialValues_MustCreateDynamicArrayWithLengthEqualsInitialLength<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);

			Assert.AreEqual(initialItems.Length, dynamicArray.Length);
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Ctor_WithInitialValues_MustCreateDynamicArrayWithCapacityEqualsInitialLength<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);

			Assert.AreEqual(initialItems.Length, dynamicArray.Capacity);
		}

		#endregion

		#region Indexer tests

		[TestCaseSource(nameof(ArrayInitData))]
		public void Indexer_IndexIsGreaterThanLength_ThrowsIndexOutOfRangeException<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count);

			var dynamicArray = GetDynamicArray(initialItems);

			var newItem = _fixture.Create<T>();
			dynamicArray.Add(newItem);

			Assert.Throws<IndexOutOfRangeException>(() =>
			{
				var __ = dynamicArray[dynamicArray.Length];
			});
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Indexer_IndexIsLessThanZero_ThrowsIndexOutOfRangeException<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count);

			var dynamicArray = GetDynamicArray(initialItems);
			Assert.Throws<IndexOutOfRangeException>(() =>
			{
				var __ = dynamicArray[-1];
			});
		}

		#endregion

		#region Length tests

		[Test]
		public void Length_AddingSomeElementsOneByOne_LengthEqualsAddedItemsCount()
		{
			var initialItems = new[] {"a", "f", "x", "h", "m"};
			var dynamicArray = GetDynamicArray<string>(0);
			for (int i = 0; i < initialItems.Length; i++)
			{
				dynamicArray.Add(initialItems[i]);
				Assert.AreEqual(i + 1, dynamicArray.Length);
			}
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Length_AddingRangeOfElementsBulk_LengthEqualsInitialLengthPlusAddedItemsCount<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count);
			var dynamicArray = GetDynamicArray(initialItems);
			var initialLength = dynamicArray.Length;

			var newItems = _fixture.CreateMany<T>(5).ToArray();
			dynamicArray.AddRange(newItems);

			Assert.AreEqual(initialLength + newItems.Length, dynamicArray.Length);
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Length_RemovingElements_LengthIsLessThanBefore<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);
			var initialLength = dynamicArray.Length;

			for (int i = 0; i < initialItems.Length; i++)
			{
				dynamicArray.Remove(initialItems[i]);
				Assert.AreEqual(initialLength - i - 1, dynamicArray.Length);
			}
		}

		#endregion

		#region Capacity tests

		[TestCaseSource(nameof(ArrayInitData))]
		public void Capacity_AddNewElementInFullArray_CapacityHasDoubled<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count);

			var dynamicArray = GetDynamicArray(initialItems);
			var originCapacity = dynamicArray.Capacity;

			var newItem = _fixture.Create<T>();
			dynamicArray.Add(newItem);

			Assert.AreEqual(originCapacity * 2, dynamicArray.Capacity);
		}

		[Test]
		public void Capacity_AddRangeFrom4ElementsInArrayWithLength2AndCapacity5_CapacityHasDoubled()
		{
			var dynamicArray = GetDynamicArray<string>(5);
			dynamicArray.AddRange(new []{"t1", "t2"});

			var originCapacity = dynamicArray.Capacity;

			dynamicArray.AddRange(new []{"q1", "q2" , "q3" , "q4" });

			Assert.AreEqual(originCapacity * 2, dynamicArray.Capacity);
		}

		[Test]
		public void Capacity_AddRangeFrom5ElementsInArrayWithLength2AndCapacity3_CapacityIncreasedBy4Times()
		{
			var dynamicArray = GetDynamicArray<string>(3);
			dynamicArray.AddRange(new []{"t1", "t2"});

			var originCapacity = dynamicArray.Capacity;

			dynamicArray.AddRange(new []{"q1", "q2" , "q3" , "q4" , "q5" });

			Assert.AreEqual(originCapacity * 4, dynamicArray.Capacity);
		}

		[Test]
		public void Capacity_AddRangeFrom10ElementsInArrayWithLength2AndCapacity3_CapacityIncreasedBy4Times()
		{
			var dynamicArray = GetDynamicArray<string>(3);
			dynamicArray.AddRange(new []{"t1", "t2"});

			var originCapacity = dynamicArray.Capacity;

			dynamicArray.AddRange(new []{"q1", "q2" , "q3" , "q4" , "q5", "w1", "w2" , "w3" , "w4" , "w5" });

			Assert.AreEqual(originCapacity * 4, dynamicArray.Capacity);
		}

		[Test]
		public void Capacity_AddRangeFrom11ElementsInArrayWithLength2AndCapacity3_CapacityIncreasedBy8Times()
		{
			var dynamicArray = GetDynamicArray<string>(3);
			dynamicArray.AddRange(new []{"t1", "t2"});

			var originCapacity = dynamicArray.Capacity;

			dynamicArray.AddRange(new []{"q1", "q2" , "q3" , "q4" , "q5", "w1", "w2" , "w3" , "w4" , "w5", "e1" });

			Assert.AreEqual(originCapacity * 8, dynamicArray.Capacity);
		}

		[Test]
		public void Capacity_AddRangeFrom20ElementsInArrayWithLength2AndCapacity3_CapacityIncreasedBy8Times()
		{
			var dynamicArray = GetDynamicArray<string>(3);
			dynamicArray.AddRange(new []{"t1", "t2"});

			var originCapacity = dynamicArray.Capacity;

			dynamicArray.AddRange(new []{"q1", "q2" , "q3" , "q4" , "q5", "w1", "w2" , "w3" , "w4" , "w5", "e1", "e2", "e3", "e4", "e5", "r1", "r2", "r3", "r4", "r5" });

			Assert.AreEqual(originCapacity * 8, dynamicArray.Capacity);
		}

		#endregion

		#region Add tests

		[Test]
		public void Add_AddOneItem_ArrayContainsJustAddedItem()
		{
			var dynamicArray = GetDynamicArray<int>(1);

			dynamicArray.Add(Int32.MaxValue);

			CollectionAssert.Contains(dynamicArray, Int32.MaxValue);
		}

		#endregion

		#region AddRange tests

		[TestCaseSource(nameof(ArrayInitData))]
		public void AddRange_RangeOfItems_DynamicArrayContainsAllOfTheAddedItems<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count);
			var dynamicArray = GetDynamicArray(initialItems);
			var newItems = _fixture.CreateMany<T>(3).ToArray();

			dynamicArray.AddRange(newItems);

			foreach (var item in newItems)
			{
				CollectionAssert.Contains(dynamicArray, item);
			}
		}

		#endregion

		#region Insert tests

		[TestCase(-1, 1)]
		[TestCase(-1, 100)]
		[TestCase(10, 5)]
		[TestCase(7, 6)]
		public void Insert_InvalidIndex_ThrowsIndexOutOfRangeException(int index, int initialCapacity)
		{
			var dynamicArray = GetDynamicArray<int>(initialCapacity);

			Assert.Throws<IndexOutOfRangeException>(() => dynamicArray.Insert(Int32.MaxValue, index));
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Insert_NewItemAtTheBeginningOfArray_NewItemIsAvailableByIndex0<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count);
			var dynamicArray = GetDynamicArray(initialItems);

			var newItem = _fixture.Create<T>();
			dynamicArray.Insert(newItem, 0);

			Assert.AreEqual(newItem, dynamicArray[0]);
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Insert_NewItemAtTheEndOfArray_NewItemIsAvailableBySpecifiedIndex<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);
			var lastIndex = initialItems.Length - 1;

			var newItem = _fixture.Create<T>();
			dynamicArray.Insert(newItem, lastIndex);

			Assert.AreEqual(newItem, dynamicArray[lastIndex]);
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Insert_NewItemInTheMiddleOfArray_NewItemIsAvailableBySpecifiedIndex<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);
			var index = new Random().Next(1, initialItems.Length - 2);

			var newItem = _fixture.Create<T>();
			dynamicArray.Insert(newItem, index);

			Assert.AreEqual(newItem, dynamicArray[index]);
		}

		#endregion

		#region Remove tests

		[TestCaseSource(nameof(ArrayInitData))]
		public void Remove_NonExistingElement_ReturnsFalse<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);

			var newItem = _fixture.Create<T>();
			while (initialItems.Contains(newItem))
			{
				newItem = _fixture.Create<T>();
			}

			var removeResult = dynamicArray.Remove(newItem);

			Assert.IsFalse(removeResult);
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Remove_ExistingElement_ReturnsTrue<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);
			var randomIndex = new Random().Next(0, initialItems.Length - 1);
			var itemToRemove = initialItems[randomIndex];

			var removeResult = dynamicArray.Remove(itemToRemove);

			Assert.IsTrue(removeResult);
		}

		[TestCaseSource(nameof(ArrayInitData))]
		public void Remove_ExistingElement_ItemHasBeenRemoved<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);

			var randomIndex = new Random().Next(0, initialItems.Length - 1);
			var itemToRemove = initialItems[randomIndex];
			var beforeRemoveCount = dynamicArray.Count(item => itemToRemove.Equals(item));

			dynamicArray.Remove(itemToRemove);
			var afterRemoveCount = dynamicArray.Count(item => itemToRemove.Equals(item));
			var removedElementsCount = beforeRemoveCount - afterRemoveCount;

			Assert.AreEqual(1, removedElementsCount);
		}

		#endregion

		#region IEnumerable tests

		[TestCaseSource(nameof(ArrayInitData))]
		public void GetEnumerator_Iterate_ReturnsExistingItems<T>(T _, int count)
		{
			var initialItems = _fixture.CreateMany<T>(count).ToArray();
			var dynamicArray = GetDynamicArray(initialItems);

			foreach (var item in dynamicArray)
			{
				CollectionAssert.Contains(dynamicArray, item);
			}
		}

		#endregion

		#region IDynamicArray factory methods

		private IDynamicArray<T> GetDynamicArray<T>()
		{
			return new DynamicArray<T>();
		}
		private IDynamicArray<T> GetDynamicArray<T>(int capacity)
		{
			return new DynamicArray<T>(capacity);
		}

		private IDynamicArray<T> GetDynamicArray<T>(IEnumerable<T> initialItems)
		{
			return new DynamicArray<T>(initialItems);
		}

		#endregion

	}
}
